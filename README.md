1. Login with Admin User ( In Postman)
    + http://localhost:8080/realms/my-realm/protocol/openid-connect/token
    + Body (x-www-form-urlencoded):
     - client_id = my-app
     - username = khoa
     - password = 12312
     - grant_type = password
     - client_secret = A2KSQGgSGBYzcr0FpLSqM0AoAbwqXNH
2. Use Access Token to create new users (Role-Based: Admin)
3. Login with New Users
4. Get Access Token to create Posts
5. Try to DELETE, PUT Post with user that not the owner (Resource-Based)
6. Try to DELETE, PUT Post with user that is the owner
7. Do step 5,6 with Comment
8. Assign Permission to User and test create a Post or Comment
9. Add attribute "DOB" in Keycloak and test the Age Attribute Policy
10. Add attribute "sex" in KeyCloak and test the Sex Attribute Policy