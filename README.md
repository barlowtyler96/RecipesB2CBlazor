# Recipes Blazor App

#### This is a project I created to practice authentication/authorization, front end design, as well as consuming APIs. The app is granted an access token from an Azure AD authentication endpoint using a shared secret and then calls my api for recipe data. Along with this client credentials flow, I used Azure AD B2C to allow users to create an account and sign in. It also allows users to edit their profile and reset their password.  If a user creates an account, they can submit a recipe for review before being added to the site. 

#### I used Microsoft's documentation & code examples, Stack Overflow, and ChatGPT to complete the project. The biggest challenge I faced was learning how to implement Azures auth systems.

## Technologies used: 
* Bootstrap 5
* Azure AD B2C
* OAuth 2.0
* OpenID Connect
* Razor Components
* Minimal vanilla Javascript

## Features: 
* User account creation
* User account editing
* User password resetting
* Page-specific Authorization
* Ability for users with an account to submit recipes
* Server-Side paging for pagination component
* Web API consumption

<iframe width="560" height="315" src="https://www.youtube.com/watch?v=dlsZ7XV1Vsk" frameborder="0" allowfullscreen></iframe>










