# Implementation Plan: Settings Page Feature

## Overview
Implement a Settings page with three main settings: Keep me logged in (toggle switch), First day of the week (picker), and Manage categories (navigation button). The design should match the provided screenshot with clean, card-based UI.

## Prerequisites
- Existing SettingsPage and SettingsViewModel
- Icons.cs for chevron icon
- Material Symbols font already configured

## Implementation Tasks

### TODO 1: Add Chevron Right Icon (if not already exists)
**Goal**: Ensure chevron_right icon is available for the Categories navigation arrow

**Steps**:
1. Check if `ChevronRight` icon exists in `Resources/Icons.cs`
2. If not, add it: `\ue5cc`

**Files to modify**:
- `Resources/Icons.cs` (if needed)

**Validation**:
- Icon compiles without errors
- Icon is accessible from XAML

---

### TODO 2: Implement SettingsViewModel Properties
**Goal**: Add observable properties for the settings values

**Steps**:
1. Add `[ObservableProperty]` for:
   - `bool keepMeLoggedIn` (default: false)
   - `string selectedFirstDayOfWeek` (default: "Monday")
   - `List<string> daysOfWeek` - list with all 7 days
2. Initialize `daysOfWeek` in constructor with: Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday
3. Add command for managing categories (optional for now, can be navigation later)

**Files to modify**:
- `Pages/Settings/SettingsViewModel.cs`

**Expected properties**:
```csharp
[ObservableProperty]
private bool keepMeLoggedIn;

[ObservableProperty]
private string selectedFirstDayOfWeek = "Monday";

public List<string> DaysOfWeek { get; set; }
```

**Validation**:
- ViewModel compiles successfully
- Properties are observable

---

### TODO 3: Implement SettingsPage XAML - Basic Structure
**Goal**: Create the page layout with ScrollView and VerticalStackLayout

**Steps**:
1. Set page background to #F5F5F5
2. Wrap content in ScrollView
3. Use VerticalStackLayout with spacing for settings items
4. Set padding to 20

**Files to modify**:
- `Pages/Settings/SettingsPage.xaml`

**Validation**:
- XAML compiles without errors
- Basic structure is in place

---

### TODO 4: Implement Keep Me Logged In Setting
**Goal**: Create the first setting row with label and switch

**Steps**:
1. Create Border with white background, rounded corners (12), padding (16)
2. Inside Border, create Grid with 2 columns (*, Auto)
3. Column 0: VerticalStackLayout with:
   - Label "Keep me logged in" (FontSize 16, Bold, TextColor #333333)
   - Label "Stay signed in for faster access" (FontSize 14, TextColor #666666)
4. Column 1: Switch control bound to `KeepMeLoggedIn` property
   - Set OnColor to blue (#5B9BED)
   - VerticalOptions="Center"

**Files to modify**:
- `Pages/Settings/SettingsPage.xaml`

**Validation**:
- Switch control appears on the right
- Labels are properly styled
- Switch toggles state and updates ViewModel property

---

### TODO 5: Implement First Day of Week Setting
**Goal**: Create the picker setting for selecting the first day of the week

**Steps**:
1. Create Border with white background, rounded corners (12), padding (16), margin top (12)
2. Inside Border, create Grid with 2 columns (*, Auto)
3. Column 0: VerticalStackLayout with:
   - Label "First day of the week" (FontSize 16, Bold, TextColor #333333)
   - Label "Choose your week start day" (FontSize 14, TextColor #666666)
4. Column 1: Picker control
   - ItemsSource bound to `DaysOfWeek`
   - SelectedItem bound to `SelectedFirstDayOfWeek`
   - BackgroundColor: light blue (#E0F2FE)
   - TextColor: #333333
   - FontSize: 14
   - Padding: 12,8
   - VerticalOptions="Center"
   - Add rounded corners using Border wrapper if needed

**Files to modify**:
- `Pages/Settings/SettingsPage.xaml`

**Validation**:
- Picker displays all 7 days
- Monday is selected by default
- Selecting a day updates the ViewModel
- Picker has rounded corners and proper styling

---

### TODO 6: Implement Manage Categories Navigation Button
**Goal**: Create a clickable card that navigates to category management

**Steps**:
1. Create Border with white background, rounded corners (12), padding (16), margin top (12)
2. Add TapGestureRecognizer (command can be added later or left empty for now)
3. Inside Border, create Grid with 2 columns (*, Auto)
4. Column 0: VerticalStackLayout with:
   - Label "Categories" (FontSize 16, Bold, TextColor #333333)
   - Label "Manage expense categories" (FontSize 14, TextColor #666666)
5. Column 1: Label with chevron right icon
   - Text: `{x:Static resources:Icons.ChevronRight}`
   - FontFamily: MaterialSymbolsOutlined
   - FontSize: 24
   - TextColor: #666666
   - VerticalOptions: Center

**Files to modify**:
- `Pages/Settings/SettingsPage.xaml`

**Validation**:
- Card displays with proper styling
- Chevron icon appears on the right
- Card is tappable (visual feedback)
- Layout matches the design

---

### TODO 7: Add App Information Footer
**Goal**: Add app name and version at the bottom (matching the screenshot)

**Steps**:
1. After the settings cards, add a VerticalStackLayout with margin top (40)
2. Add two centered labels:
   - "MoneyBuddy" (or "FinanceBuddy") - FontSize 16, TextColor #666666, centered
   - "Version 1.2.0" - FontSize 14, TextColor #999999, centered

**Files to modify**:
- `Pages/Settings/SettingsPage.xaml`

**Validation**:
- App info appears at bottom
- Text is centered and properly styled

---

### TODO 8: Final Styling and Polish
**Goal**: Ensure all visual details match the design

**Steps**:
1. Verify all Border elements have:
   - BackgroundColor: White
   - StrokeThickness: 0
   - CornerRadius: 12
   - Proper padding and margins
2. Verify text colors:
   - Primary text: #333333
   - Secondary text: #666666
   - Tertiary text: #999999
3. Verify spacing between cards (12-16px)
4. Test on Android emulator to ensure it matches the screenshot

**Files to modify**:
- `Pages/Settings/SettingsPage.xaml`

**Validation**:
- Visual appearance matches the screenshot
- All spacing and colors are correct
- Layout is clean and professional

---

## Implementation Order
1. TODO 1: Add chevron icon (if needed)
2. TODO 2: Implement ViewModel properties
3. TODO 3: Create basic page structure
4. TODO 4: Add Keep me logged in setting
5. TODO 5: Add First day of week setting
6. TODO 6: Add Manage categories button
7. TODO 7: Add app information footer
8. TODO 8: Final styling and testing

## Testing Checklist
After all TODOs:
- [ ] Page title is "Settings"
- [ ] Keep me logged in toggle appears with subtitle
- [ ] Switch control works and updates ViewModel
- [ ] First day of week picker shows all 7 days
- [ ] Monday is selected by default
- [ ] Picker updates ViewModel when changed
- [ ] Categories card appears with subtitle and chevron
- [ ] All cards have white background and rounded corners
- [ ] Page background is light gray (#F5F5F5)
- [ ] Spacing and padding match the design
- [ ] App name and version appear at bottom
- [ ] Page is scrollable if needed
- [ ] Visual appearance matches the screenshot

## Notes
- Keep code simple and clean
- Use VerticalStackLayout inside each card for label groups
- Follow MVVM patterns with data binding
- Ask for approval after each TODO before proceeding
- No complex logic needed - this is primarily a UI implementation
- The Manage Categories button can be a placeholder for now (no navigation required initially)
