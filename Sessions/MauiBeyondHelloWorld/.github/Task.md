# Feature: Add Expense page
On the ExpensesPage, there's a floating button with a plus-icon. This button must lead to a AddExpensePage. On the AddExpensePage, the user should be able to fill in fields for adding a Expense. I also want this page to be an example for using triggers and behaviors in .NET MAUI. Can you create the page with the necessary validation.
- ExpensesPage: Button with plus-icon should lead to AddExpensePage.
- When hovering over the button, it should animate being pressed (fade to being smaller and bigger for example), using a Trigger of Behavior, whatever is the best solution for this.
- On the AddExpensePage, the user should be able to enter the necessary field: Date, Name, Store, Amount and Category. 

## Acceptance criteria
- ExpensesPage: the button with the plus sign animated when hovering over it.
- ExpensesPage: the button with the plus sign leads to the AddExpensePage.
- AddExpensePage: has a corresponding ViewModel, which is also set as the x:DataType in the AddExpensePage.xaml.
- AddExpensePage: Has a Data Picker for the Date field
- AddExpensePage: Has Entry fields for Name, Store and Amount. Amount should be only numeric. 
- AddExpensePage: Has a Picker for the Categories. The list of categories should be fetched from the service. The first category in the list should be set as the chosen category by default.
- AddExpensePage: There should be a button for saving the expense, saving it with the ExpenseService and then navigating back (await Shell.Current.GoToAsync("..");)
- There should be validation on the fields Name, Store and Amount field. 
- AddExpensePage: The button for saving the expense should be enabled only when the Name, Store and Amount field are set.
- Use Triggers or Behaviors or both when this is the best option (mainly for XAML only changes), but use data binding with the viewmodel if that makes more sense. Use best practices regarding this.

## Other considerations
You must follow the rules set in the instructions.prompt.md file.
Ask rather use less and simple code than complicated code
Ask for after each implemented TODO if I agree with the implementation and if you should continue.
You should add namespaces to the GlobalXmlns.cs when they're used more than once throughout the project.
For the chevron icons and other icons, always use the font MateMaterialSymbolsOutlined and define the icons in the Resources/Icons.cs.