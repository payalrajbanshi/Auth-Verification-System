# Auth Verification System

A full-stack authentication and verification platform built using ASP.NET Core, React, and Syncfusion.

The system provides secure user and organization management with email verification, mobile verification, password recovery,
and scalable business-layer architecture using Clean Architecture principles.

# Tech Stack

## Backend

- ASP.NET Core
- C#
- Entity Framework Core
- SQL Server
- BCrypt.Net
- SMTP Email Service
- Clean Architecture
- Repository Pattern

## Frontend

- React
- Syncfusion UI Components
- React Router

# Features

## Authentication & Security

- User Registration
- Email Verification using PIN
- Mobile Verification
- Password Reset using PIN
- Secure Password Hashing with BCrypt
- Change Password
- Admin Password Reset
- Account Activation / Deactivation

## Organization Management

- Create Organization
- Update Organization
- Organization Email Verification
- Organization Activation / Deactivation
- Duplicate Organization Validation
## Frontend Features

- Modern React UI
- Syncfusion DataGrid Integration
- Form Validation
- Responsive Dashboard
- API Integration with Axios
- Reusable Components
- Clean User Experience

# Architecture

The project follows a layered Clean Architecture approach to separate business logic from infrastructure and presentation layers.

# Backend Business Logic

## User Service

Handles:

- User creation
- Password hashing
- Email verification
- Mobile verification
- Password reset


## Organization Service

Handles:

- Organization creation
- Verification workflows
- Duplicate code validation
- Organization status management

## Email Service

SMTP-based email service used for:

- Verification emails
- Password reset emails
- Notification handling

