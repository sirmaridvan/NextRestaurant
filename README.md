# Social Reades

#This app is the term project of BLG440E 2016 Spring Term Course.


Project Description: 

1. Brief description of the project 
  a. "NextRestaurant" is an application that aims to democratize and automate the selection of restaurants of closed groups working in the same office and going out to lunch. 
  b. The application will make a selection within the limits determined by each member of the group for the restaurants he/she wants to go and will inform the members before the lunch. 
  
2. User requirements
  a. The application must provide a web interface for restaurant management, restaurant additions, restaurant removal and update of restaurant information. 
    i. Restaurant Name 
    ii. Transportation Type (Pedestrian | Car) 
    iii. Weather Sensitive?
    iv. Other areas deemed necessary
  b. The application must provide a web interface for adding members to the management of the group members, removing members, and updating the member information.
    i. Member Name
    ii. Other areas deemed necessary
  c. The application must provide a web interface where users can upload a periodic restaurant scoring file to fill.
    i. The user must be able to download a ready-made Excel file in alphabetical order to all restaurants in the restaurant pool via the        screen.
    ii. The user should be able to upload the Excel file by simply filling in the scoring field (s) in the downloaded Excel file.
  d. The application should run every day at a set time and contain a component that chooses the restaurant for that day and throws the notification email. to. Application; provide a web interface reporting statistical information on the selection of restaurants in the pool.
  f. Web interfaces to be developed should have responsive design and should be available on mobile devices.
  g. The application should run periodically over the number of days to be determined. When the period ends, all members must wait for new rates to be determined.
  h. The developer team will determine the restaurant selection algorithm and inputs with the following limitations.
    i. The system should not recommend the same restaurant for 2 consecutive days.
    ii. The system should not select a restaurant that requires 2 cars in 3 consecutive days.
    iii. The system should provide weather integration and should not select weather-sensitive and transport-type restaurants on days when weather is not favorable.
    iv. The above constraints may be violated if there are no other restaurants to choose from.
    
3. There is no expectation of user authentication and authorization to limit the scope. For similar reasons, it can be assumed that the application will only be used by one closed group.
