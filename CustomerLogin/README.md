# GymOffice.CustomerLogin 
This is for temporary app setting to test and setup login in the client app

#DONE:
- Standard Identity UI
- DB contains 4 roles: Customer, Admin, Receptionist, Coach. New users are registered as Customers
- One page ("Privacy policy") is closed for anonymous users (redirected to Denied page with the links to Login and Register)
- "Privacy Policy" is also opened only for Customers: if a user is logged in but is not a Customer then "Access denied" page is shown
- EmailSender added with SendGrid key in the Windows secret area. This allows user to confirm email and reset password

#TODO:
- Add external login services (Facebook, Google). But there is basic external app functional (the user can install an app at a mobile and enter the code)