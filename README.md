## Courier hub APP - SOME_VERY_MAGNIFICENT_NAME

## Project – the description
The team working on this project will create a Courier Hub that will give the option of requesting couriers from various sour ces/companies. The client of
that app can use WebSite to interact with the system. The Landing page should have some information about the system, like how many people use it, its
name, some short descriptions, and a button for creating it anonymously for delivery request inquiries. The inquiry should co ntain the dimensions of the
package, wieght, the delivery date, the destination and source address and two required options: priority (can be low, or high), and delivery at a
weekend. After requests are sent, the application should wait for the best deals from each integrated courier supplier. The a pplication should wait for
max 30sek for offers and shows that are available. It should show in the list form. The list item should contain the currier company name, the price for
delivery, option to get this offer. After clicking it if a person is not logged in we need to ask the user to create an accou nt or log in. If the user wants to go
in anonymous mode he should be able to. The next step for anonymous users is to give first name/last name/company name/email/ address information.
This page should also show a summary that will contain source and destination information, price split on taxes, service fee, distance fee, and additional
fees. If all data is provided, you can submit a request. After that, you should have to get requestId. Also, the client should get a notification that it was
requested with all order information. When this request is accepted (or auto-accepted) on the courier side client should get an email with the contract
agreement and receipt. After delivery client should be able to rate service. If the user was logged in or logged in on one o f the steps of getting an inquiry
his address information should be set up automatically. Also, he should have option to check all of his orders and check the history of his inquiries. The
user can add a request to his account if he has requestId. On details of each request, there should be information about steps in delivery. There will be
the option of cancelling the request if the supplier allows that by the deadline provided by the supplier. For purposes of th is project, we will reuse this
page for the courier company so if the login user is a currier from your company he should have a list of this company's deli very and he can change the
state of delivery to “received” with the courier name and date, “Delivered” with the courier name and date, “Cannot deliver” reason why it could be
delivered, date and courier name. He can filter out delivery requests by dates and status. You can also log in as an office worker and you should have the
option to look at all pending offerts and accept them or reject them. If accepted we should provide two files, agreement and receipt. Also, we need to
send a proper notification email (it should be separate from hub email). If it is rejected also, some notification should be sent.
Be aware that you need to create an API that will be used by different teams and your solution to get and get inquiries, and manage requests. Your API
should have some authorisation. Also, need to provide swagger documentation. You will also need to integrated provided by lecture API (it will be give
later in the process)


## Requirements overall
### Web UI (App) for Courier Hub app
 - [ ] Can register using AzureAD or other OpenId (Google, Facebook, custom)
 - [ ] When registered, need to provide some additional information:
    - [ ] First Name
    - [ ] Last Name
    - [ ] Address
    - [ ] Email
    - [ ] Default Source address
 - [ ] Need to use provided by lecturer Courier API
 - [ ] Need to use at least one other group API
 - [ ] Need to create own Courier API
 - [ ] Need to meet the business requirements
 - [ ] Need to meet the technical requirements

## Requirements – business part 1: Client

 -  [ ] Client can have register/login abilities
 -  [ ] Client (logged in) as default should see a list of the last 30 days’ inquires
 -  [ ] Package dimension and weight,
 -  [ ] Source and destination
 -  [ ] Date of inquiring
 -  [ ] Status
 -  [ ] If inquiry has offer, submit just one that we offer with the status of the offer
    -  [ ] Client (logged in) will have a button to create a new inquire
    -  [ ] Client (not logged in) will have a button to create a new inquire
 -  [ ] Client creating new inquire should provide
    -  [ ] Package dimension and weight,
    -  [ ] Source and destination
    -  [ ] Pickup date and delivery date
    -  [ ] If it is a company or not
    -  [ ] Pirority and delivery at weekend
 -  [ ] Client should get an email when the inquiry is submitted
 -  [ ] Client should get a list of possible offers
 -  [ ] Client can click on one of the offers to accept it (offer are valid for some time)
 -  [ ] Client should need to provide some that before he can submit acceptance of the offer
 -  [ ] Personal data or company data (if possible, take it form user data for logged in users)
 -  [ ] Address (if possible, take it form user data for logged-in users)
 -  [ ] Email address
 -  [ ] Client should get a link that he can use to check the offer status and the offer Id
 -  [ ] Client should get an email with a link to the offer status
 -  [ ] Client should get an email when the decision about it will be made

## Requirements – business part 2: Courier company office worker
 - [ ] Show all inquiries to your bank
    - [ ] Could be filtered out
    - [ ] Should be sortable
 - [ ] Show all offer request for your bank
    - [ ] Could be filtered out
    - [ ] Should be sortable
 - [ ] Can check details of the offers
    - [ ] Show all sent data
 - [ ] Can accept or reject pending offers
    - [ ] If accepted need to provide
        - [ ] Agreement
        - [ ] Receipt
    - [ ] If rejected need to provide reason why

## Requirements – business part 3: Courier
 - [ ] Show all delivery
    - [ ] Could be filtered out
    - [ ] Should be sortable
 - [ ] Can check details of the delivery
 - [ ] Can set delivery as picked up
    - [ ] Name of courier
    - [ ] Date of pickup
 - [ ] Can set delivered package
    - [ ] Name of courier
    - [ ] Date of delivery
 - [ ] Can set cannot deliver
    - [ ] Date of attempt
    - [ ] Name of courier
    - [ ] Reason why

## Requirements – business part 4: API functions

 - [ ] Create inquire
    - [ ] Some auth
    - [ ] Request must have all business needed fields as required
    - [ ] Can have some option
    - [ ] Need return id of this inquire
 - [ ] Create offer
    - [ ] Some auth
    - [ ] Required field check
 - [ ] Check the status of the offer
    - [ ] Some auth
    - [ ] Should require id
    - [ ] Return status with description
 - [ ] List of inquiring
    - [ ] Just for Courier company office worker
    - [ ] Return a list of inquiring
 - [ ] List of offers
    - [ ] Just for Courier comapny office worker
    - [ ] Return a list of offers
 - [ ] What the offer need to contain:
    - [ ] The inquire id
    - [ ] The date of creation of offer
    - [ ] The date of update of offer
    - [ ] The status offer (with description)
    - [ ] The requested value
    - [ ] The offer time period
    - [ ] Information about package
    - [ ] Information about Source and Delivery
    - [ ] Price (taxes, fees and other things)


 ## Technical requirements
 - [ ] Front-end
    - [ ] Javascript based (could react/angular if accepted by group lecturer)
    - [ ] Responsive
    - [ ] Using AJAX
    - [ ] Consistent
    - [ ] Host on Azure
 - [ ] Back-end
    - [ ] .NET (6 or Core 3.+) (Maybe the .NET CORE will be more suitable)
    - [ ] Host on Azure
    - [ ] Using SQL database with EntityFramework (or similar) as the provider
    - [ ] Use AzureBlob (or similar) to store files
    - [ ] Use SendGrid (or similar) for sending mail
    - [ ] Use some form of OpenId to authentication (or other based on OATH)
    - [ ] Create custom authorization or use OpenId to achieve that

## Technology stack
 - SCRUM, git, git-flow, Azure DevOps
 - Visual Studio\Rider
 - App Service, Azure SQL DB, Azure AD B2C, SendGrid
 - ASP.NET Core MVC, ASP.NET Core API, SQL, Entity Framework
 - REST, OAuth 2.0, OWIN
 - PowerShell
 - Unit Tests, Integration tests, UI tests, API tests
 - CD/CI