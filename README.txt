Student: [Asakhe Tebele] - [ST10455918]
Project: RaceDayApi + RaceDayMvc

How to Run:
1. Open RaceDayApi.sln in Visual Studio 2022
2. Tools > NuGet Package Manager > Package Manager Console
   Run: Update-Database
   Or terminal: dotnet ef database update
3. Right-click Solution > Properties > Startup Projects > Multiple > RaceDayApi and RaceDayMvc = Start
4. Press F5
5. API Swagger: https://localhost:5001/swagger
6. Mvc Site: https://localhost:5002

Features Implemented:
- Events CRUD
- Enrolments
- Results by user
- Banner image upload via BlobService
- Fixed RaceDayContext (removed duplicate AppDbContext)

Connection String: Uses LocalDB - see appsettings.json