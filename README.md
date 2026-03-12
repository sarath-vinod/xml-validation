# xml-validation

ASP.NET Core MVC project for XML validation and restaurant review data, built for the CST8259 Web Programming course.

## How to Run

### Method 1: Using the batch file
Double-click `run.bat` to start the application.

### Method 2: Using Command Prompt (CMD)
1. Open Command Prompt (CMD)
2. Change to the project directory:
   ```
   cd c:\sem4\web programming-2\lab-3
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

This is an ASP.NET Core MVC application for validating XML files against an XSD schema and displaying restaurant, menu, and review data from XML.

### Features
- Upload XML and XSD schema files
- Validate XML against the schema definitions
- Display detailed validation error information (including line number, column number, and error message)
- View restaurant details, reviews, and menu items loaded from XML

### Usage
1. After starting the project, the browser will open automatically (typically at `http://localhost:5001`)
2. Upload the XSD and XML files on the validation page
3. Click the "Validate XML" button
4. View the validation results or restaurant overview

## Project Structure
```
XMLValidator/
├── Controllers/
│   ├── HomeController.cs           # Restaurant overview and pages
│   └── XmlValidationController.cs  # Handles XML validation logic
├── Models/
│   ├── XMLandSchemaFileUpload.cs   # File upload model
│   ├── XmlValidationError.cs       # Validation error model
│   ├── RestaurantOverviewViewModel.cs
│   └── RestaurantEditViewModel.cs
├── Views/
│   ├── Home/
│   │   ├── Index.cshtml            # Restaurant overview page
│   │   └── Edit.cshtml             # Restaurant edit page
│   ├── XmlValidation/
│   │   ├── Index.cshtml            # Upload form page
│   │   ├── ValidationResult.cshtml # Validation result page
│   │   └── Success.cshtml          # Success page
│   └── Shared/
│       └── _Layout.cshtml          # Shared layout
├── Data/
│   ├── restaurant_review.cs        # XSD-generated C# classes
│   └── restaurant_review.xml       # Sample restaurant XML
├── wwwroot/                        # Static files
├── Program.cs                      # Application entry point
└── XMLValidator.csproj             # Project file
```
