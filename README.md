# XML Validator - Lab 2 (CST8259)

## How to Run

### Method 1: Using the batch file
Double-click `run.bat` to start the application.

### Method 2: Using Command Prompt (CMD)
1. Open Command Prompt (CMD)
2. Change to the project directory:
   ```
   cd C:\Users\OO7Ag\Downloads\lab-2
   ```
3. Run:
   ```
   dotnet run
   ```

### Method 3: Using Visual Studio
1. Open Visual Studio
2. Open the project file `XMLValidator.csproj`
3. Press F5 or click the Run button

## Project Description

This is an ASP.NET Core MVC application for validating XML files against an XSD schema.

### Features
- Upload XML and XSD schema files
- Validate XML against the schema definitions
- Display detailed validation error information (including line number, column number, and error message)

### Usage
1. After starting the project, the browser will open automatically (typically at `https://localhost:5001`)
2. Upload the XSD and XML files on the home page
3. Click the "Validate XML" button
4. View the validation results

## Project Structure
```
XMLValidator/
├── Controllers/
│   └── HomeController.cs      # Handles XML validation logic
├── Models/
│   ├── XMLandSchemaFileUpload.cs  # File upload model
│   └── XmlValidationError.cs      # Validation error model
├── Views/
│   ├── Home/
│   │   ├── Index.cshtml           # Upload form page
│   │   └── ValidationResult.cshtml # Validation result page
│   └── Shared/
│       └── _Layout.cshtml         # Shared layout
├── wwwroot/                        # Static files
├── Program.cs                      # Application entry point
└── XMLValidator.csproj             # Project file
