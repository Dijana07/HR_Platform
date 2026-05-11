# HR Platform

Full-stack HR platform for managing job candidates and their skills.

## Project Structure

- backend/ → ASP.NET Core Web API
- frontend/ → React frontend

## Technologies

- .NET 8
- ASP.NET Core Web API
- React
- MySQL
- Entity Framework Core

## Note

The most challenging part of this project was handling candidate skills because a candidate can have multiple skills or none at all. While adding candidate skills, there is also an option to create a completely new skill, which introduced many different cases and edge scenarios to consider. Additionally, skills should not be duplicated, which required extra validation and checks.

I implemented this by separating candidate and skill management responsibilities. `SkillService` is responsible for handling skill-related logic and validation, while `CandidateService` handles candidate creation and assigning already existing skills to candidates.
