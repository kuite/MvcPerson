Before run MvcPerson.Web application set up following things:
1. Point connection string 'PersonDataBase' to valid SQL server.
2. Point NLog storage file to valid path ('fileName' property).
3. Point 'xmlPath' (xml stogare file path) in Web.config/<appSettings>

To run tests repeat steps above but without NLog storage (not tested) and for the App.config file.