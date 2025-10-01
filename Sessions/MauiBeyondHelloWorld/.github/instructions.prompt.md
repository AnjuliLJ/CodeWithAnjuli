# Instructions for GitHub Copilot
Implement a feature based on the Task.md file. Make sure you use the following guidelines when implementing the feature.

## Execution Guidelines
1. Read the Task.md file to understand the feature requirements.
- Understand the context, constraints, and all requirements
- Follow all instructions in the requirements document exactly
- Ensure you have all needed context to implement the requirements fully
- Perform additional web and codebase search as necessary to fill any gaps
- Look at the Microsoft documentation for any information on .NET MAUI and C#

2. Overwrite the implementation.md file with a detailed plan for implementing the feature.
- Think before you execute the plan. Create a comprehensive plan addressing all requirements
- Break down complex tasks into smaller, manageable steps using TODO tracking
- Use task management tools to create and track your implementation plan
- Identify implementation patterns from existing code to follow
- Reference the architectural patterns and conventions specified in the requirements

3. Ask if the plan is approved before proceeding.

4. Once approved, implement the feature according to the plan.
- Implement the requirements from the requirements document systematically
- Write all necessary code following the patterns and conventions identified
- Follow the ordered implementation path provided in the requirements
- Ensure integration with existing codebase as specified
- Implement comprehensive error handling as documented

5. Validate Implementation
- Validate if the implementation meets all requirements set in Task.md
- Fix any failures that occur

## Implementation Guidelines
1. Code Quality
- Follow existing architectural patterns and conventions
- Mirror the coding style and patterns from referenced files
- Implement comprehensive error handling as specified
- Ensure proper integration with existing systems
- Always use the latest .NET version and C# features

## Architectural Patterns
- Use MVVM pattern with data binding
- Use Dependency Injection for service management
- Use async/await for asynchronous operations
- Use ObservableCollection for dynamic data lists, only in the ViewModel, otherwise use List<T>
- Don't use INotifyPropertyChanged directly, use ObservableObject from CommunityToolkit.Mvvm
- Use RelayCommand for command binding in MVVM
- Each Page should have a corresponding ViewModel
- Use XAML for UI definitions and C# for code-behind and logic
- Services that are necessary are registered in the MauiProgram.cs file
- Use community toolkits like CommunityToolkit.Mvvm for MVVM helpers
- Use HttpClient for network operations
- Use ObservableProperty attribute for properties that need to notify changes
- All viewmodels should inherit from ObservableObject
- All commands should use the RelayCommand attribute
- All pages are stored in the Pages folder, in subfolders by feature. The ViewModels are also stored in the same subfolder as the page they correspond to.


## Coding Style
- Use clear and descriptive names for variables, methods, and classes
- Write modular and reusable code
- Include comments and documentation for complex logic
- Follow SOLID principles for object-oriented design
- Ensure proper indentation and formatting for readability
- Prefer easy to understand code over clever or complex solutions
- Avoid deep nesting of code blocks
- Use consistent naming conventions (e.g., PascalCase for classes and methods, _camelCase for variables)
- Keep methods focused on a single task or responsibility
- When namespaces are used more than once in a file, use GlobalUsings.cs usings to reduce clutter

## MAUI Specific Guidelines
- Use XAML for defining UI components
- Use data binding to connect UI elements to ViewModel properties
- Use DependencyService or constructor injection for accessing services
- Avoid using custom code, try to find alternative solutions using existing libraries or frameworks, like the CommunityToolkit or Syncfusion
- Use platform-specific code only when absolutely necessary, prefer cross-platform solutions
- Ensure responsiveness and adaptability of UI across different device sizes and orientations
- Prefer using Grid with star sizing for layout over StackLayout for better performance and flexibility.
- Only use VerticalStackLayout and HorizontalStackLayout for simple layouts with a few elements.
- Never use StackLayout. 
- Try to use less layouts as possible. Prefer using Margins and Padding to create spacing between elements instead of adding more layouts.
- Don't use deprecated or obsolete APIs or controls. Always check the latest documentation for recommended practices.
- Don't use Frame or ListView. Prefer using Border and CollectionView instead.
- Don't use code-behind for business logic. Keep code-behind for UI-specific logic only.
- All business logic should be in the ViewModel or in Services.
- When binding methods to controls, prefer using Commands in the ViewModel instead of event handlers in the code-behind.


