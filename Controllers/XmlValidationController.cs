using Microsoft.AspNetCore.Mvc;
using System.Xml;
using System.Xml.Schema;
using XMLValidator.Models;

namespace XMLValidator.Controllers
{
    public class XmlValidationController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View(new XMLandSchemaFileUpload());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Validate(XMLandSchemaFileUpload files)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", files);
            }

            if (files.XmlFile == null || files.SchemaFile == null)
            {
                ModelState.AddModelError("", "Both XML file and Schema file are required.");
                return View("Index", files);
            }

            var xmlFileName = files.XmlFile.FileName;
            var schemaFileName = files.SchemaFile.FileName;

            if (!xmlFileName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("XmlFile", "Please select a valid XML file (.xml extension required).");
                return View("Index", files);
            }

            if (!schemaFileName.EndsWith(".xsd", StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("SchemaFile", "Please select a valid schema file (.xsd extension required).");
                return View("Index", files);
            }

            if (files.XmlFile.Length == 0)
            {
                ModelState.AddModelError("XmlFile", "The XML file is empty.");
                return View("Index", files);
            }

            if (files.SchemaFile.Length == 0)
            {
                ModelState.AddModelError("SchemaFile", "The schema file is empty.");
                return View("Index", files);
            }

            var validationErrors = new List<XmlValidationError>();

            try
            {
                var schemaSet = new XmlSchemaSet();
                using (var schemaStream = files.SchemaFile.OpenReadStream())
                {
                    var schema = XmlSchema.Read(schemaStream, null);
                    if (schema != null)
                    {
                        schemaSet.Add(schema);
                    }
                    else
                    {
                        ModelState.AddModelError("SchemaFile", "Invalid schema file. Could not read the XSD schema.");
                        return View("Index", files);
                    }
                }

                var settings = new XmlReaderSettings
                {
                    Schemas = schemaSet,
                    ValidationType = ValidationType.Schema
                };

                string currentElementName = "Unknown";

                settings.ValidationEventHandler += (sender, eventArgs) =>
                {
                    if (eventArgs.Exception != null)
                    {
                        var lineInfo = eventArgs.Exception as IXmlLineInfo;
                        var lineNumber = lineInfo?.LineNumber ?? eventArgs.Exception.LineNumber;
                        var columnNumber = lineInfo?.LinePosition ?? eventArgs.Exception.LinePosition;

                        validationErrors.Add(new XmlValidationError
                        {
                            Element = currentElementName,
                            ErrorType = eventArgs.Severity.ToString(),
                            Line = lineNumber,
                            Column = columnNumber,
                            Message = eventArgs.Message
                        });
                    }
                };

                using (var xmlStream = files.XmlFile.OpenReadStream())
                {
                    using var reader = XmlReader.Create(xmlStream, settings);
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            currentElementName = reader.LocalName;
                        }
                    }
                }
            }
            catch (XmlException ex)
            {
                ModelState.AddModelError("XmlFile", $"Invalid XML file format: {ex.Message}");
                return View("Index", files);
            }
            catch (XmlSchemaException ex)
            {
                ModelState.AddModelError("SchemaFile", $"Invalid schema file: {ex.Message}");
                return View("Index", files);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred during validation: {ex.Message}");
                return View("Index", files);
            }

            ViewBag.XmlFileName = xmlFileName;
            ViewBag.SchemaFileName = schemaFileName;
            return View("ValidationResult", validationErrors);
        }
    }
}
