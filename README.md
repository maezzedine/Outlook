# Outlook
Outlook is a weekly publication of the American University of Beirut (AUB) and represents the voice of the student body. It is an independent, non-affiliated publication that favors no ethic, religious or political group. All commons, articles and reports are the property of Outlook. And not neccesary represents the views of outlook or the AUB community. Outlook welcomes all contributions. No part of this publication may be reproduces in any way, shape or form without the written consent of Outlook and/or higher authorities. Outlook reserves the right to edit all material.

# Website
This platform is published in both Arabic and English Languages. It supports dark and light modes. It also provide the users with some social features like up and down voting an article, adding comments, storing favorites, and much more.

<img src="https://github.com/mezdn/Outlook/blob/master/images/web.gif" />

# Server:
Is divided into 5 projects:
1. <a href="https://github.com/mezdn/Outlook/tree/master/backend/src/Outlook.Models">Outlook.Models</a>: Class library that stores Outlook Models, DbContext, and constants
2. <a href="https://github.com/mezdn/Outlook/tree/master/backend/src/Outlook.Services">Outlook.Services</a>: Class library that stores helper functions
3. <a href="https://github.com/mezdn/Outlook/tree/master/backend/src/Outlook.Logger">Outlook.Logger</a>: Class library that implements Outlook's custom logger to text file and slack channel
4. <a href="https://github.com/mezdn/Outlook/tree/master/backend/src/Outlook.Server">Outlook.Server</a>: Web project that serves as a Content Management System for Outlook website
5. <a href="https://github.com/mezdn/Outlook/tree/master/backend/src/Outlook.Api">Outlook.Api</a>: Web project that serves as a RESTfull API for Outlook website

## Documentation:
API function descriptions, input, expected results and sample calls are documented in using Swagger <a href="http://server.auboutlook/swagger/index.html">here</a>.

## Technology Stack:
<ul>
  <li>ASP.NET Core</li>
  <li>SignalR</li>
  <li>IdentityServer4</li>
  <li>C#</li>
</ul>

# Web:
Web UI

## Technology Stack:
<ul>
  <li>Vue.js</li>
  <li>Typescript</li>
  <li>HTML</li>
  <li>SCSS</li> 
</ul>

# License:
<a href="https://github.com/mezdn/Outlook/blob/master/LICENSE">MIT Open-Source</a>
