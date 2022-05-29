# Hazir
Hazir is a proof of concept of a web application used for attendance tracking and management. It uses face recognition to serve the purpose. The application is designed to help teachers take attendance easily and with no need to maintain huge registers with numerous entries for the same.

## General Features:
- Attendance can be taken for any date.

- Attendance can be viewed for any date.

- Camera is used to capture the image of a student to help in real time tracking.

- Face detection and recognition is used to mark student as present.

## Getting Started:
**Step 1:**
> [Install .Net 6.0 Framework](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

**Step 2:**
> [Download Visual Studio](https://visualstudio.microsoft.com/downloads/)

**Step 3:**
>Download or clone this repository by using the link given below.
>
```
https://github.com/PriyaDebo/Hazir
```
**Step 4:**
> Open the solution from the downloaded repository in the Visual Studio.
> 
> ```Hazir/Source/Hazir.sln```

**Step 5:**
> Go to the file:
> 
> ```Hazir/Source/Frontend/wwwroot/appsettings.json```

**Step 6:**
> In line number 2, in place of Your Secret Key add Face API secret key:
**The Face API secret key is provided in the supporting document.**

**Step 7:**
> Go to the Frontend Project in the solution explorer in the Visual Studio and right click and go to Manage NuGet Packages.

**Step 8:**
> Search for the package and install it:
> 
> ```Microsoft.Azure.CognitiveServices.Vision.Face -Version 2.2.0-preview```

**Step 9:**
> Once the installing is completed, the app is ready to run. 

**Step 10:**
> In the toolbar in Visual Studio:
>> Select **Debug** as Solution Configuration.
>
>> Select **Any CPU** as Solution Platforms.
>
>> Select **Frontend** as Startup Project.

**Step 11:**
> Start the project.
> 
> ```The app will start running on your local host.```

## Tech Stack:
>Framework: .Net 6.0

>Frontend: Blazor WebAssembly

>API: ASP.NET Core Web API Project

>Business Logic Layer: Class Library 

>Database Access Layer: Class Library 

>Database: Azure Cosmos Db (SQL)

>Face recognition and detection: Face API (Azure Cognitive Services)
