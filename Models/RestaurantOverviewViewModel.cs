using System.ComponentModel.DataAnnotations;

namespace XMLValidator.Models
{
    /// <summary>
    /// ViewModel for displaying restaurant overview information in a table
    /// </summary>
    public class RestaurantOverviewViewModel
    {
        /// <summary>
        /// Restaurant ID (from XML attribute or index)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Restaurant name
        /// </summary>
        [Display(Name = "Rataurant")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Type of food served
        /// </summary>
        [Display(Name = "Food Type")]
        public string FoodType { get; set; } = string.Empty;

        /// <summary>
        /// Average rating (1-5, where 5 is best)
        /// </summary>
        [Display(Name = "Rating (best=5)")]
        public decimal Rating { get; set; }

        /// <summary>
        /// Cost/price range (1-5, where 5 is most expensive)
        /// </summary>
        [Display(Name = "Cost (most expensive=5)")]
        public decimal Cost { get; set; }

        /// <summary>
        /// City where restaurant is located
        /// </summary>
        [Display(Name = "City")]
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// Province or state code
        /// </summary>
        [Display(Name = "Province")]
        public string ProvinceState { get; set; } = string.Empty;
    }
}
