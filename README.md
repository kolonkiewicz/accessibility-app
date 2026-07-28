# Digital Accessibility Assessment Application 

A web application that supports the analysis of website compliance with the WCAG guidelines and the Polish Digital Accessibility Act.

## Technologies

- **Framework:** .NET 6.0 (ASP.NET Core MVC)
- **Language:** C#
- **Data Access:** Entity Framework Core
- **Database:** SQLite
- **Automation and Accessibility Audit:** PuppeteerSharp (Chromium browser automation), axe-core (HTML accessibility testing engine)
- **Frontend:** Razor (.cshtml), Bootstrap, CSS3

## Features

- **User Account System:** User registration, login, email verification, and password recovery.
- **Automated Accessibility Analysis:** Analyzes a specified website URL using the axe-core library to detect violations of the WCAG 2.1 accessibility guidelines.
- **Report Generation:** Generates reports containing detected accessibility issues, their severity, and recommendations for improvement.
- **Accessibility Statement Generator:** Automatically determines the website's accessibility compliance level based on the analysis results.
- **User Dashboard:** User account management.
- **Analysis History:** Stores and displays the results of previous accessibility audits.

## Author

Mateusz Kolonko - https://github.com/kolonkiewicz
