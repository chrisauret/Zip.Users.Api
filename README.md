# Zip Candidate User API Challenge: C#
We have a scenario where we'd like to build an API to manage Zip Pay users. We require the ability to create, get, list and delete users. Once we create users, we need the ability for the user to create an account.

This should be implemented with an API and database.

## Business Requirements

* Should not allow more than one user with the same email address
* Zip Pay allows credit for up to $1000, so if `monthly salary - monthly expenses` is less than $1000 we should not create an account for the user and return an error

## Tech Requirements
 
* API: Please use .net core 6 or above for your solution
* Database: See *Supporting Services* below
* Documentation: Update `DEVELOPERS.md` with information you'd give to other members of your team about how to work with the solution

## Supporting Services

To assist with your submission and to save you time in downloading and installing software, we've included a `docker-compose.yml` with boilerplate configuration for a few popular database options (MSSQL, PostgresQL, and MySQL). Choose one and remove the other two (or substitute) based on your preference. Note that you only need to have *one* database, you don't need to use all of them.

If your submission calls for other supporting services, feel free to add as necessary to your `docker-compose` config.

## API Requirements

1. The following Endpoints are required:
    * Create user
      * Fields to be provided and persist
        * Name
        * Email Address (Must be unique)
        * Monthly Salary (Must be a positive number - for simplicity, pretend there is no such thing as income tax)
        * Monthly Expenses (Must be a positive number)
    * Create an account
      * Given a user, create an account
      * Should take into account the business requirements above
      * Up to you to decide the appropriate fields to persist here
    * Get accounts given a user
2. Should be robust and include relevant response codes for different scenarios
3. No need to implement any kind of auth for this test


## Database Requirements

Users and accounts should persist to a database of your choice.
In memory database is also acceptable.

## Definition of Done

Pretend this is a code that will be deployed in production.
We want to see best practices you would have used for production code.
You will not have time to get everything perfect, so think carefully about what you want to include.

If there are additional features you'd like to implement but don't have enough time, we encourage you to implement a very simple example and place some comments or explain what you would have done more to DEVELOPERS.md (as specific as you can).

We value your time, so we don't want you to spend a massive amount of time on this so we'd recommend spending around 3-4 hours on this.