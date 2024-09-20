# User API Dev Guide

The API can be viewed using Swagger. Swagger is configured in the launchSettings.json file in the UserApi folder. The Swagger UI can be accessed by running the API and navigating to https://localhost:7043/swagger/index.html

## Building

Open in Visual Studio
Right-click the solution and select Build All. This will download all necessary packages and build the solution
Open Terminal or CMD-Prom and navigate to the Zip.WebAPI folder
Run "dotnet ef database update"

## Testing

Use the built-in testing framework.
Right Click the solution file in Visual Studio and select run tests.

## Deploying

Not implemented here. This would be done using Azure DevOps or similar.

## Additional Information

Due to time constraints I could not implement:
Upgrading to .Net 8 (or latest long term support version of .Net)
Implement DTOs for input and output data and AutoMapper to map between them instead of passing the models directly
Adding Paging, Sorting, Ordering to GetAll queries.
Implement concrete exceptions instead of strings.
Log to Elasticsearch (Kibana) or similar for long term storage of errors.
