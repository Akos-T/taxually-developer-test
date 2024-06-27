# Taxually technical test

## Task

This solution contains an [API endpoint](https://github.com/Taxually/developer-test/blob/main/Taxually.TechnicalTest/Taxually.TechnicalTest/Controllers/VatRegistrationController.cs) to register a company for a VAT number. Different approaches are required based on the country where the company is based:

- UK companies can register via an API
- French companies must upload a CSV file
- German companies must upload an XML document

We'd like you to refactor the existing solution with the following in mind:

- Readability
- Testability
- Adherance to SOLID principles

We'd also like you to add some tests to show us how you'd test your solution, although we aren't expecting exhaustive test coverage.

We don't expect you to implement the classes for making HTTP calls and putting messages on queues.

We'd like you to spend not more than a few hours on the exercise.

To develop and submit your solution please follow these steps:

1. Create a public repo in your own GitHub account and push the technical test there
2. Develop your solution and push your changes to your own public GitHub repo
3. Once you're happy with your solution send us a link to your repo

## Solution

- I used the Strategy pattern to move out the main logic from the controller
- I used the Factory pattern to move out the switch case logic from the controller
- I organized the code following the Clean Architecture (Domain, Application, Infrastructure, Presentation layers)
  - To keep it simple, not all the layers implemented completely (if at all)
- Added Dependency Injection to support testability better
- I changed TaxuallyHttpClient's PostAsync and TaxuallyQueueClient's EnqueueAsync methods to virtual so they can be mocked
	- Probably better solution would be to have interfaces for them, but I decided to do this for the simplicity
- I added some unit tests to show how I would test the solution
- I added a simple integration test to show how I would test the API endpoint

### Some possible future improvements

- Input validation could be improved further (encapsulate the common part and reuse that part)
- Country could be an enum instead of a string to be less error-prone
- To better enforce the clean architecture all the different layers should move to seperate projects (assemblies)
- E2E tests could be added as well. In real life the app is probably dockerized, so I would use testcontainers to do them.
- The queue names, urls could be moved to appsettings.json and use the Options pattern to inject them dynamically