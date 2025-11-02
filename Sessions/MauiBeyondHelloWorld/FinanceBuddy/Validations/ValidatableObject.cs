using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace FinanceBuddy.Validations;

public partial class ValidatableObject<T>: ObservableObject
{
    [ObservableProperty]
    private IEnumerable<string> _error = Array.Empty<string>();

    
}
