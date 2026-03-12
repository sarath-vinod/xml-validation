using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace XMLValidator.Models
{
    public class XMLandSchemaFileUpload
    {
        [Required(ErrorMessage = "Please select an XML file")]
        [Display(Name = "XML File")]
        public IFormFile? XmlFile { get; set; }

        [Required(ErrorMessage = "Please select a schema file")]
        [Display(Name = "Schema File")]
        public IFormFile? SchemaFile { get; set; }
    }
}
