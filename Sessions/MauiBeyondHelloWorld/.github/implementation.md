# Implementation Plan: Add Expense Feature

## Overview
This plan implements an "Add Expense" feature with a focus on showcasing .NET MAUI triggers and behaviors for animations and validation. The implementation follows MVVM patterns and existing architectural conventions in the project.

## Context Analysis
- **Existing Patterns**: MVVM with CommunityToolkit.Mvvm, dependency injection, Shell navigation
- **Service Layer**: ExpenseService provides category data and will need an `AddExpense` method
- **Navigation**: Shell-based navigation using `GoToAsync`
- **UI Style**: Material Design 3 with rounded borders, shadows, and card-based layouts
- **Icons**: MaterialSymbolsOutlined font, defined in Resources/Icons.cs

## Implementation Steps

### TODO 1: Update ExpenseService to support adding expenses
**File**: `FinanceBuddy/Services/ExpenseService.cs`

**Changes needed**:
- Add a private `List<Expense>` field to store user-added expenses
- Create `AddExpense(Expense expense)` method to add new expenses to the list
- Modify `GetExpensesByMonth()` to include user-added expenses along with generated ones
- Ensure user expenses are persisted in the list (for this demo, in-memory is sufficient)

**Rationale**: The service needs to support adding expenses, not just generating random ones.

---

### TODO 2: Add Plus icon to Icons.cs
**File**: `FinanceBuddy/Resources/Icons.cs`

**Changes needed**:
- Add `public const string Add = "\ue145";` for the plus icon

**Rationale**: Centralizes icon definitions as per project conventions.

---

### TODO 3: Create AddExpenseViewModel
**File**: `FinanceBuddy/Pages/Expenses/AddExpenseViewModel.cs` (new file)

**Implementation details**:
- Inherit from `ObservableObject`
- Observable properties:
  - `DateTime SelectedDate` (default: DateTime.Now)
  - `string Name`
  - `string Store`
  - `string Amount` (string to allow validation before parsing)
  - `ObservableCollection<Category> Categories`
  - `Category SelectedCategory`
  - `bool IsValid` (computed property for button state)
- Constructor:
  - Inject `ExpenseService`
  - Load categories from service
  - Set first category as default
- Command:
  - `SaveExpenseCommand` (async Task):
    - Validate all fields
    - Parse amount to decimal
    - Create new Expense object
    - Call `ExpenseService.AddExpense()`
    - Navigate back using `await Shell.Current.GoToAsync("..");`
- Validation logic:
  - `IsValid` should return true only when Name, Store, and Amount are not empty/null
  - Amount must be numeric (use partial property to validate on change)

**Rationale**: Follows MVVM pattern with proper separation of concerns. Validation logic in ViewModel enables data binding for button state.

---

### TODO 4: Create AddExpensePage.xaml
**File**: `FinanceBuddy/Pages/Expenses/AddExpensePage.xaml` (new file)

**Implementation details**:
- `ContentPage` with `x:DataType="viewmodel:AddExpenseViewModel"`
- Use `ScrollView` for content (allows keyboard scrolling on mobile)
- Layout structure (using Grid for better performance):
  - Header with title "Add Expense"
  - Form card (white background, rounded corners, shadow)
    - **DatePicker** for Date field (bound to `SelectedDate`)
    - **Entry** for Name (bound to `Name`, placeholder "Coffee, Lunch, etc.")
    - **Entry** for Store (bound to `Store`, placeholder "Store name")
    - **Entry** for Amount (bound to `Amount`, `Keyboard="Numeric"`, placeholder "0.00")
    - **Picker** for Category (bound to `Categories` and `SelectedCategory`, `ItemDisplayBinding="{Binding Name}"`)
  - Save button at bottom (bound to `SaveExpenseCommand`, `IsEnabled` bound to `IsValid`)

**Validation UI**:
- Use **DataTrigger** on each Entry to change border color to red when empty (but only after user has interacted)
- Use **DataTrigger** on Save button to change opacity/appearance when disabled

**Styling**: 
- Match existing app style (Material Design 3)
- Use Border with RoundRectangle for form fields
- Apply shadows and proper spacing

**Rationale**: XAML-only triggers for visual validation feedback. Behavior-based validation would be overcomplicated for this simple scenario.

---

### TODO 5: Create AddExpensePage.xaml.cs code-behind
**File**: `FinanceBuddy/Pages/Expenses/AddExpensePage.xaml.cs` (new file)

**Implementation details**:
- Constructor with `AddExpenseViewModel` parameter (dependency injection)
- Set `BindingContext = viewModel`
- Keep minimal code in code-behind (only initialization)

**Rationale**: Follows project pattern of minimal code-behind with ViewModel injection.

---

### TODO 6: Add hover animation to floating action button
**File**: `FinanceBuddy/Pages/Expenses/ExpensesPage.xaml`

**Implementation details**:
Update the floating action button Border to include:
- **PointerGestureRecognizer** with `PointerEntered` and `PointerExited` events
- OR use **EventTrigger** with **ScaleToAnimation** for hover effect
- Animation: Scale from 1.0 to 1.1 on hover (smooth, 200ms duration)

**Decision**: Use MAUI built-in animations via triggers if possible, otherwise use behavior or simple code-behind for PointerOver state.

**Rationale**: Triggers are XAML-only and demonstrate best practices. However, pointer events may require minimal code-behind or custom behavior depending on platform support.

---

### TODO 7: Add navigation command to floating action button
**File**: `FinanceBuddy/Pages/Expenses/ExpensesPage.xaml`

**Implementation details**:
- Add `TapGestureRecognizer` to the floating action button Border
- Bind to new command in ExpensesViewModel: `NavigateToAddExpenseCommand`

**File**: `FinanceBuddy/Pages/Expenses/ExpensesViewModel.cs`

**Implementation details**:
- Add `[RelayCommand]` method `NavigateToAddExpense`:
  ```csharp
  [RelayCommand]
  private async Task NavigateToAddExpense()
  {
      await Shell.Current.GoToAsync("AddExpensePage");
  }
  ```

**Rationale**: Command binding keeps business logic in ViewModel, not code-behind.

---

### TODO 8: Register AddExpensePage in AppShell
**File**: `FinanceBuddy/AppShell.xaml.cs` (or use code registration)

**Implementation details**:
- Register route in AppShell constructor:
  ```csharp
  Routing.RegisterRoute("AddExpensePage", typeof(AddExpensePage));
  ```

**Rationale**: Required for Shell navigation to work.

---

### TODO 9: Register AddExpenseViewModel and AddExpensePage in DI container
**File**: `FinanceBuddy/MauiProgram.cs`

**Implementation details**:
- Add to services:
  ```csharp
  builder.Services.AddTransient<AddExpenseViewModel>();
  builder.Services.AddTransient<AddExpensePage>();
  ```

**Rationale**: Enables constructor injection for ViewModel and Page.

---

### TODO 10: Refresh expenses list after adding
**File**: `FinanceBuddy/Pages/Expenses/ExpensesViewModel.cs`

**Implementation details**:
- Make `LoadExpenses` method public or create a `RefreshExpenses` method
- Override `OnAppearing` in ExpensesPage.xaml.cs to call ViewModel refresh
- OR use Shell navigation events to detect return from AddExpensePage

**Alternative approach**: Use MessagingCenter or CommunityToolkit.Mvvm Messenger to send message when expense is added

**Rationale**: Ensures expense list updates when returning from AddExpensePage.

---

## Validation Strategy

### Field-level validation (using Triggers):
- **Visual feedback**: Border color changes to red when field is invalid
- **Entry validation**: Use DataTrigger on `Text` property length
- **Amount validation**: Additional trigger for numeric validation

### Form-level validation (using ViewModel):
- **IsValid property**: Computed based on all required fields
- **Save button**: Enabled only when IsValid is true
- Use `[ObservableProperty]` with `NotifyCanExecuteChangedFor` attribute to update button state

### Example validation pattern:
```csharp
[ObservableProperty]
[NotifyPropertyChangedFor(nameof(IsValid))]
[NotifyCanExecuteChangedFor(nameof(SaveExpenseCommand))]
private string _name = string.Empty;

public bool IsValid => 
    !string.IsNullOrWhiteSpace(Name) && 
    !string.IsNullOrWhiteSpace(Store) && 
    !string.IsNullOrWhiteSpace(Amount) &&
    decimal.TryParse(Amount, out _);
```

---

## Animation Strategy

### Floating Action Button Hover Animation:
**Option 1: Use VisualStateManager** (Recommended)
- Define VisualStates for "Normal" and "PointerOver"
- Use VisualStateManager.VisualStateGroups in XAML
- Animate Scale property

**Option 2: Use PointerGestureRecognizer with Triggers**
- PointerEntered/Exited events
- Trigger animations via ViewExtensions (ScaleTo)

**Option 3: Custom Behavior**
- Create HoverScaleBehavior
- Attach to Button
- More reusable but adds complexity

**Decision**: Use **VisualStateManager** for hover animation as it's XAML-based and follows MAUI best practices.

---

## Technical Considerations

### Keyboard Handling:
- Ensure `ScrollView` properly adjusts when keyboard appears on mobile
- Use `Entry.ReturnType="Next"` for better UX
- Last field should have `ReturnType="Done"`

### Category Selection:
- Default to first category to avoid null selection
- Use `SelectedIndex="0"` on Picker for default selection

### Amount Input:
- Use `Keyboard="Numeric"` for mobile numeric keyboard
- Validate decimal format in ViewModel
- Consider locale-specific decimal separators (future enhancement)

### Navigation:
- Use modal presentation for AddExpensePage: `Shell.PresentationMode="ModalAnimated"`
- Ensures clear visual hierarchy

---

## Testing Validation

After implementation, verify:
1. ✅ Floating action button animates on hover (desktop) or press (mobile)
2. ✅ Clicking FAB navigates to AddExpensePage
3. ✅ AddExpensePage has correct ViewModel binding
4. ✅ DatePicker defaults to current date
5. ✅ Entry fields for Name, Store, Amount are present
6. ✅ Amount field shows numeric keyboard on mobile
7. ✅ Picker shows categories loaded from service
8. ✅ First category is selected by default
9. ✅ Save button is disabled when fields are empty
10. ✅ Visual validation feedback (red borders) appears for invalid fields
11. ✅ Save button becomes enabled when all required fields are filled
12. ✅ Clicking Save adds expense and navigates back
13. ✅ Expense list refreshes and shows newly added expense

---

## Files to Create
1. `FinanceBuddy/Pages/Expenses/AddExpenseViewModel.cs`
2. `FinanceBuddy/Pages/Expenses/AddExpensePage.xaml`
3. `FinanceBuddy/Pages/Expenses/AddExpensePage.xaml.cs`

## Files to Modify
1. `FinanceBuddy/Services/ExpenseService.cs` - Add AddExpense method
2. `FinanceBuddy/Resources/Icons.cs` - Add Plus icon
3. `FinanceBuddy/Pages/Expenses/ExpensesPage.xaml` - Add hover animation and navigation to FAB
4. `FinanceBuddy/Pages/Expenses/ExpensesViewModel.cs` - Add navigation command and refresh logic
5. `FinanceBuddy/AppShell.xaml.cs` - Register route (or create if doesn't exist)
6. `FinanceBuddy/MauiProgram.cs` - Register DI services
7. `FinanceBuddy/GlobalXmlns.cs` - Add namespace if behaviors are used

---

## Implementation Order
1. TODO 1: Update ExpenseService (foundation)
2. TODO 2: Add icon (small, quick)
3. TODO 3: Create ViewModel (core logic)
4. TODO 4 & 5: Create Page XAML and code-behind (UI)
5. TODO 8 & 9: Register in Shell and DI (infrastructure)
6. TODO 7: Add navigation command (connect pages)
7. TODO 6: Add hover animation (polish)
8. TODO 10: Add refresh logic (complete flow)

---

## Notes
- This implementation prioritizes **simplicity and best practices** over complex custom solutions
- Uses **Triggers for visual changes** (XAML-only) and **ViewModel properties for logic validation**
- Behaviors could be added for reusable validation patterns but may be overkill for this scope
- VisualStateManager provides the cleanest hover animation approach
- All code follows existing project conventions and architectural patterns
