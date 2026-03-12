using System.ComponentModel.DataAnnotations;

namespace XMLValidator.Models
{
    public class XmlValidationError
    {
        [Display(Name = "XML Element")]
        public string Element { get; set; } = string.Empty;

        [Display(Name = "Error Type")]
        public string ErrorType { get; set; } = string.Empty;

        [Display(Name = "Line Number")]
        public int Line { get; set; }

        [Display(Name = "Column Number")]
        public int Column { get; set; }

        [Display(Name = "Error Message")]
        public string Message { get; set; } = string.Empty;
    }
}
