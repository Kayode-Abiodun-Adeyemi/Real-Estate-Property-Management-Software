# Real-Estate-Property-Management-Software
This is an N-Tier Real Estate Property Management Web Application for a Case Study that was developed by Kayode Abiodun Adeyemi as an assignment towards the MSc Computing Programme in Sheffield Hallam University.  

The objective of the Application is to offer online-real-time services in a simplistic and streamlined fashion to the customers of the Client/Case Study. 

In this Application, there are three (3) users who can remotely and securely use the common functionalities of the application such as login, logout, register, change password, view category of building, view property listed by the landlord, chat with other users within the application online real-time. At the same time, the Admin can create category, other Admin user (in case of emergency), assign roles to users, suspend and delete erring users. The Landlord can upload his or her available property into the appropriate categories created by Admin, delete property after successful rentage or sale, while the Tenant can also search for properties for rent or sale, book for inspection visit, make payment as well as send mail to the Landlord.

I setup the project and ran the first migration (being asp.net core framework) that created the database and the required tables. I thereafter implemented Microsoft Identity and Access Management (IAM) to manage the users, logins, user registration, password change, create roles and control access to the roles, although it was a bit challenging at first. 

I later implemented the live chat feature using SignalR to build a compelling and dynamic real-time dashboard which manages real-time communication or responses of all the users. Although, WebSockets API can also be deployed to handle the same function, but I chose SignalR because of its ease of implementation and for the fact that it already included a server implementation that works out of the box as against building a WebSockets server by myself. In addition, For the email feature to be integrated into the application, MailKit, a cross-platform mail client library built on top of MimeKit in asp.Net Core was used and I had to open a dedicated Gmail account for the purpose of this project. This allowed me to get the developer credentials for the setup which I used for the implementation. The alternative to this would have been to use FluentEmail or Postal, but MailKit standouts because of its ease of integration and lack of complexity. Since the application is an enterprise solution with payment requirement, I integrated a speedy and secure Stripe Payment Platform to handle the payment processing. I also opened a developer account with Stripe, and this provided me with the required credentials (i.e. secretkey and publishablekey) for integration into the application. However, I considered using the PayPal Payment platform but found its checkout implementation in asp.net Core a bit complex and cumbersome.

The Tools/Technology used are as follows:
  •	IDE:                              	Visual Studio 2019 
  •	Programming Language:   		        C#
  •	Framework:                          .Net Core 3.5
  •	Architectural Pattern Approach:     MVC 
  •	Database:                           SQL Server 
