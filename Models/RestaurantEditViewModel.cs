using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace XMLValidator.Models
{
    /// <summary>
    /// ViewModel for editing restaurant information
    /// </summary>
    public class RestaurantEditViewModel
    {
        /// <summary>
        /// Restaurant ID (hidden field)
        /// </summary>
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        /// <summary>
        /// Restaurant name
        /// </summary>
        [Display(Name="Restaurant Name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Street address
        /// </summary>
        [Required(ErrorMessage = "Street address is required")]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; } = string.Empty;

        /// <summary>
        /// City
        /// </summary>
        [Required(ErrorMessage = "City is required")]
        [Display(Name = "City")]
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// Province or state (enum from XSD.EXE generated classes)
        /// </summary>
        [Display(Name = "Province")]
        public ProvinceType ProvinceState { get; set; }

        /// <summary>
        /// Postal or zip code (with regex validation)
        /// </summary>
        [Required]
        [RegularExpression(@"^[a-zA-Z]\d[a-zA-Z](\s)*\d[a-zA-Z]\d$", 
            ErrorMessage = "Must be in the form of A1A 1A1")]
        [Display(Name = "Postal Code")]
        public string PostalZipCode { get; set; } = string.Empty;

        /// <summary>
        /// Summary/description (textarea)
        /// </summary>
        [Required]
        [Display(Name="Summary")]
        public string Summary { get; set; } = string.Empty;

        /// <summary>
        /// Rating (1-5)
        /// </summary>
        [Required]
        [Range(1, 5)]
        [Display(Name = "Rating (1 to 5)")]
        public decimal Rating { get; set; }
    }
}
