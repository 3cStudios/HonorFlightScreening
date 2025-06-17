# Copilot Instructions


## Project Overview
A mobile friendly C#, Blazor solution that captures special needs when transporting veterans for medical care. 

## Coding Standards
- Follow C# coding conventions and .NET best practices
- Use consistent naming conventions: PascalCase for public members, camelCase for private fields
- Use XML documentation comments for public APIs and methods
- Use `var` for local variable declarations when the type is obvious
- Use expression-bodied members for simple properties and methods
- Use `string.IsNullOrEmpty()` for string checks
- Use `string interpolation` for string formatting
- Remove unnecessary `using` directives
- Use `nameof` operator for method and property names in exceptions and logging
- Use `using` directives for namespaces at the top of files
- Use `async` and `await` for asynchronous programming
- Use `IServiceCollection` for dependency injection registration
- Use `IOptions<T>` for configuration settings
- Use `IHttpClientFactory` for HTTP client creation
- Use `CancellationToken` for long-running operations

## Default Workflow
1. Always create a new branch before starting work on any issue
2. Use descriptive branch names following the pattern: `{type}/issue-{number}-{description}`
3. Examples: `feature/issue-123-add-login`, `fix/issue-456-resolve-crash`

## Branch Creation
- Create branches from the default branch (main/master)
- Ensure branch name is kebab-case and descriptive

# GitHub Copilot Database Guidelines

## Database Changes Policy
- **NEVER** directly modify `ProjectIDAContext.cs` or any DbContext files
- **ALWAYS** create database migration scripts for schema changes
- Use SQL scripts depending on the project setup

## Database Change Workflow
1. Use descriptive migration names: `YYYY-MM-DD_AddUserTable.sql`
2. Include both UP and DOWN migration scripts
3. Create appropriate migration files in the `Migrations/` folder
4. Use the following naming convention for migration files:
   - `YYYYMMDD_AddTableName.sql` for adding tables
   - `YYYYMMDD_AlterColumnName.sql` for modifying columns
   - `YYYYMMDD_AddIndexName.sql` for adding indexes
   - `YYYYMMDD_DropTableName.sql` for removing tables
   - `YYYYMMDD_RenameTableName.sql` for renaming tables
5. Ensure migration scripts are idempotent and can be run multiple times without failure

## CSS Organization Rule
All custom CSS classes must be properly organized according to these guidelines:
1. Global Styles → custom.css. CSS classes that will be reused across multiple components or pages
   - Site-wide styling rules and utilities
   - Theme-related styles and CSS variables
   - Layout classes used throughout the application
   - Location: wwwroot/css/custom.css
2. Component-Specific Styles → {PageName}.razor.css
   - CSS classes used only within a specific Blazor component or page
   - Styles that are tightly coupled to a particular component's functionality
   - Component-scoped styling that shouldn't leak to other parts of the application
   - Location: {ComponentName}.razor.css (isolated CSS files)
3. Implementation Requirements
   - No inline styles in generated code - All styling must be externalized to appropriate CSS files
   - Document new classes - Add comments describing the purpose of custom classes
   - Follow naming conventions - Use consistent, descriptive class names
   - Verify scope - Ensure component-scoped CSS uses Blazor's CSS isolation features

4. Copilot Agent Instructions
   - When GitHub Copilot Agent generates code with CSS:
      - Identify all custom CSS classes in the generated HTML/Razor markup
      - Determine appropriate location (global vs component-scoped)
      - Create or update the corresponding CSS file
      - Ensure all classes are properly defined before the solution is considered complete
      
### Language-Specific Guidelines
- Use C# 12.0 features where appropriate
- Follow .NET and Blazor best practices for component and service design
- Use async/await for all asynchronous operations
- Prefer dependency injection for services and helpers

### General Conventions
- Use meaningful variable and function names
- Write self-documenting code with minimal comments
- Prefer composition over inheritance
- Keep functions small and focused on single responsibility
- Component Structure: Use Blazor's partial class pattern (`.razor` + `.razor.cs`), separating markup and logic
- Dependency Injection: Inject services and helpers using `[Inject]` in code-behind
- Cascading Parameters: Use `[CascadingParameter]` for shared state (e.g., `ApplicationSessionViewModel`)
- Async/Await: Use `async Task` for data loading and event handlers
- Access Modifiers: Prefer private fields/methods for encapsulation; public/protected for Blazor lifecycle and parameters
- Event Handling: Use Blazor's `EventCallback` and lambda expressions for event handlers
- State Management: Call `StateHasChanged()` after state updates to trigger UI refresh
- Naming: Use `_camelCase` for private fields, `PascalCase` for public properties and methods
- ViewModel Usage: Use ViewModels for UI state and data transfer
- Service Abstraction: Use interfaces for service abstraction and dependency injection

## Architecture Patterns
- Use MVVM pattern for UI and data binding
- Implement dependency injection throughout the solution
- Follow SOLID principles
- Use repository pattern for data access
- Service Layer Pattern: Each entity has a dedicated `{Entity}Service.cs` class
- Helper Pattern: Supporting logic in `{Entity}Helper.cs` classes

## File Structure

```
WebApp.ProjectIda/
+-- Components/
+-- Services/
+-- Helper/
+-- Models/
+-- ViewModels/
+-- wwwroot/
+-- Tests/
```

## Testing Requirements
- Write unit tests for all business logic (service and helper classes)
- Use MSTest for .NET unit testing
- Aim for 80%+ code coverage
- Include integration tests for API endpoints and major workflows

## Security Guidelines
- Validate all user inputs
- Never commit secrets or API keys; use Azure App Configuration or environment variables

## Performance Considerations
- Optimize database queries and use no-tracking where possible
- Use memory caching for frequently accessed data
- Minimize bundle size for frontend assets
- Profile code for bottlenecks and optimize as needed

## Error Handling
- Use try-catch blocks appropriately in helper classes when making calls to service and data access layers. Avoid try-catch in UI components unless absolutely necessary.Avoid try-catch in the service unless absolutely necessary
- Return meaningful error messages to the UI
- Log errors with sufficient context (use Application Insights)
- Implement graceful degradation for failed operations

## Dependencies
- Prefer well-maintained, popular libraries (e.g., Azure SDKs, CsvHelper, FuzzySharp)
- Keep dependencies up to date
- Document why specific dependencies were chosen in the README
- Avoid unnecessary dependencies

## Documentation
- Update README.md for significant changes
- Document public APIs and major service/helper methods
- Include inline documentation for complex logic
- Maintain changelog for releases

## Git Workflow
- Use conventional commit messages
- Create feature branches for new development
- Require code reviews before merging
- Keep commits atomic and focused
