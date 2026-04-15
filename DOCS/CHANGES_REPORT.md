# Technical Changes Report - MiniClinicManagementSystem

This document summarizes the major architectural and code changes made to the project to improve performance, maintainability, and adhere to Clean Architecture principles.

## 1. Domain & Entities (Core)
- **New Entity: `Person`**: Introduced as a bridge between `ApplicationUser` and profiles (`Doctor`/`Patient`). It stores common personal data like `FirstName` and `LastName`.
- **Refactored `ApplicationUser`**: Removed names; added a navigation property to `Person`.
- **Updated `Doctor` & `Patient`**: Now linked to `Person` instead of `ApplicationUser`.
- **Enums**: `AppointmentStatus` and `Role` are now consistently used across the system.

## 2. Infrastructure & Data Access
- **Configurations (`Data/Config`)**:
    - **`PersonConfig`**: Defined table structure for the new entity.
    - **Relationship Management**: Set `OnDelete(DeleteBehavior.Cascade)` for User -> Person, and `Restrict` for Person -> Profiles to protect medical data.
    - **Data Types**: Optimized column types (e.g., `decimal(18,2)` for fees, `time` for availability slots).
- **Repositories**:
    - **`PatientRepository`**: 
        - `GetPatientDetailsByIdAsync`: Uses SQL Projection (`Select`) for high-performance read-only data.
        - `GetPatientWithDetailsByIdAsync`: Uses `Include` for tracked entities (required for Update/Delete).
    - **New Repositories**: Added `AppointmentRepository` and `PrescriptionRepository`.

## 3. Application Services (Logic)
- **`IdentityService`**: New service to abstract `UserManager` operations from business services.
- **`PatientService`**: 
    - **Transactions**: Implemented for registration and deletion to ensure data consistency.
    - **Change Tracking**: Refactored `Update` to use EF Core tracking.
- **Mappers**: Manual extension methods in `PatientMapper` for type-safe and performant DTO conversion.

## 4. API & Architecture
- **Dependency Inversion**:
    - Removed `Infrastructure` reference from `Services` project.
    - Added `Infrastructure` reference to `API` project for DI registration.
- **Controllers**:
    - **`PatientController`**: Implemented CRUD operations with input validation (`Range`, `Required`).
- **Global Error Handling**: Integrated `GlobalExceptionHandler` to catch unhandled exceptions.

## 5. Summary of Deleted/Renamed Files
- Deleted `PatientSerivce.cs` (typo in name).
- Reorganized `DTOs` and `Validations` into module-specific folders.
