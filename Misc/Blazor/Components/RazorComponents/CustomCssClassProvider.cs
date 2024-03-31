﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.VisualBasic;
using System;

namespace Components
{
    public class CustomCssClassProvider<ProviderType> : ComponentBase where ProviderType : FieldCssClassProvider, new()
    {
        [CascadingParameter]
        EditContext? CurrentEditContext { get; set; }
        public ProviderType Provider { get; set; } = new ProviderType();
        protected override void OnInitialized()
        {
            if (CurrentEditContext == null)
            {
                throw new InvalidOperationException($"{nameof(CustomCssClassProvider<ProviderType>)} requires a cascading parameter of type {nameof(EditContext)}. For example, you can use {nameof(CustomCssClassProvider<ProviderType>)} inside an EditForm.");
            }
            CurrentEditContext.SetFieldCssClassProvider
            (Provider);
        }
    }
}
