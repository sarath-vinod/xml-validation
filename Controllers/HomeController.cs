using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;
using XMLValidator.Models;

namespace XMLValidator.Controllers
{
    /// <summary>
    /// Home controller for Lab 3 - Restaurant Review Management
    /// </summary>
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public HomeController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        /// <summary>
        /// Gets the path to the restaurant_review.xml file
        /// </summary>
        private string GetXmlFilePath()
        {
            // Use ContentRootPath for reliable path resolution
            string xmlPath = Path.Combine(_environment.ContentRootPath, "Data", "restaurant_review.xml");
            return xmlPath;
        }

        /// <summary>
        /// Deserializes the XML file into a restaurants object
        /// </summary>
        private restaurants? LoadRestaurants()
        {
            string xmlPath = GetXmlFilePath();
            
            if (!System.IO.File.Exists(xmlPath))
            {
                return null;
            }

            XmlSerializer serializer = new XmlSerializer(typeof(restaurants));
            using (FileStream fileStream = new FileStream(xmlPath, FileMode.Open))
            {
                return (restaurants?)serializer.Deserialize(fileStream);
            }
        }

        /// <summary>
        /// Serializes the restaurants object back to XML file
        /// </summary>
        private void SaveRestaurants(restaurants restaurantsData)
        {
            string xmlPath = GetXmlFilePath();
            
            XmlSerializer serializer = new XmlSerializer(typeof(restaurants));
            using (FileStream fileStream = new FileStream(xmlPath, FileMode.Create))
            {
                serializer.Serialize(fileStream, restaurantsData);
            }
        }

        /// <summary>
        /// Converts price range string to numeric cost (1-5)
        /// $ = 1, $$ = 2, $$$ = 3, $$$$ = 4, $$$$$ = 5
        /// </summary>
        private decimal ConvertPriceRangeToCost(string priceRange)
        {
            if (string.IsNullOrEmpty(priceRange))
                return 0;
            
            return priceRange.Length;
        }

        /// <summary>
        /// GET: Home/Index
        /// Displays overview of all restaurants in a table
        /// </summary>
        public IActionResult Index()
        {
            // Load XML file
            restaurants? restaurantsData = LoadRestaurants();
            
            if (restaurantsData == null || restaurantsData.restaurant == null)
            {
                // Return empty list if file doesn't exist or has no restaurants
                return View(new List<RestaurantOverviewViewModel>());
            }

            // Create list of view models
            List<RestaurantOverviewViewModel> viewModels = new List<RestaurantOverviewViewModel>();
            
            for (int i = 0; i < restaurantsData.restaurant.Length; i++)
            {
                restaurant rest = restaurantsData.restaurant[i];
                
                // Get average rating from statistics if available, otherwise from first review
                decimal rating = 0;
                if (rest.statistics != null)
                {
                    rating = rest.statistics.averageRating;
                }
                else if (rest.reviews?.review != null && rest.reviews.review.Length > 0)
                {
                    rating = rest.reviews.review[0].rating;
                }

                RestaurantOverviewViewModel vm = new RestaurantOverviewViewModel
                {
                    // Use index as Id (0-based, but we can use i+1 for display)
                    Id = i,
                    Name = rest.name ?? string.Empty,
                    FoodType = rest.category?.foodType ?? string.Empty,
                    Rating = rating,
                    Cost = ConvertPriceRangeToCost(rest.category?.priceRange ?? string.Empty),
                    City = rest.address?.city ?? string.Empty,
                    ProvinceState = rest.address?.province.ToString() ?? string.Empty
                };
                
                viewModels.Add(vm);
            }

            return View(viewModels);
        }

        /// <summary>
        /// GET: Home/Edit/{id}
        /// Displays edit form for a specific restaurant
        /// </summary>
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            // Validate id parameter
            if (id == null)
            {
                return NotFound();
            }

            // Load XML file
            restaurants? restaurantsData = LoadRestaurants();
            
            if (restaurantsData == null || restaurantsData.restaurant == null)
            {
                return NotFound();
            }

            // Validate id is within range
            if (id < 0 || id >= restaurantsData.restaurant.Length)
            {
                return NotFound();
            }

            restaurant rest = restaurantsData.restaurant[id.Value];
            
            // Get the first review's summary and rating (or use defaults)
            string summary = string.Empty;
            decimal rating = 1;
            if (rest.reviews?.review != null && rest.reviews.review.Length > 0)
            {
                summary = rest.reviews.review[0].summary ?? string.Empty;
                rating = rest.reviews.review[0].rating;
            }

            // Create view model
            RestaurantEditViewModel vm = new RestaurantEditViewModel
            {
                Id = id.Value,
                Name = rest.name ?? string.Empty,
                StreetAddress = rest.address?.street ?? string.Empty,
                City = rest.address?.city ?? string.Empty,
                ProvinceState = rest.address?.province ?? ProvinceType.ON, // Default to ON if null
                PostalZipCode = rest.address?.postalCode ?? string.Empty,
                Summary = summary,
                Rating = rating
            };

            return View(vm);
        }

        /// <summary>
        /// POST: Home/Edit
        /// Saves changes to the restaurant back to XML
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RestaurantEditViewModel rsVM)
        {
            // Validate model
            if (!ModelState.IsValid)
            {
                return View(rsVM);
            }

            // Load XML file
            restaurants? restaurantsData = LoadRestaurants();
            
            if (restaurantsData == null || restaurantsData.restaurant == null)
            {
                return NotFound();
            }

            // Validate id is within range
            if (rsVM.Id < 0 || rsVM.Id >= restaurantsData.restaurant.Length)
            {
                return NotFound();
            }

            restaurant rest = restaurantsData.restaurant[rsVM.Id];

            // Update restaurant properties
            rest.name = rsVM.Name;
            
            // Update address
            if (rest.address == null)
            {
                rest.address = new address();
            }
            rest.address.street = rsVM.StreetAddress;
            rest.address.city = rsVM.City;
            rest.address.province = rsVM.ProvinceState;
            rest.address.postalCode = rsVM.PostalZipCode;

            // Update first review's summary and rating (or create if doesn't exist)
            if (rest.reviews == null)
            {
                rest.reviews = new restaurantReviews();
            }
            if (rest.reviews.review == null || rest.reviews.review.Length == 0)
            {
                rest.reviews.review = new restaurantReviewsReview[1];
                rest.reviews.review[0] = new restaurantReviewsReview
                {
                    id = "REV001",
                    date = DateTime.Now,
                    reviewer = "User",
                    rating = (int)rsVM.Rating, // Convert decimal to int for XML
                    summary = rsVM.Summary
                };
            }
            else
            {
                rest.reviews.review[0].summary = rsVM.Summary;
                rest.reviews.review[0].rating = (int)rsVM.Rating; // Convert decimal to int for XML
            }

            // Save back to XML
            SaveRestaurants(restaurantsData);

            // Redirect to index
            return RedirectToAction(nameof(Index));
        }
    }
}
