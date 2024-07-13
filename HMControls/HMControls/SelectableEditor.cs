using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace HMControls;

public class SelectableEditor : KeyboardlessEditor
{
    #region Bindable

    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), 
        typeof(ICommand), 
        typeof(SelectableEditor), 
        default);

    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
        nameof(CommandParameter), 
        typeof(object), 
        typeof(SelectableEditor), 
        null);

    #endregion

    #region Properties

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    #endregion

    #region Methods

    public override void ActionOnFocused()
    {
        if (Command.CanExecute(CommandParameter))
        {
            Command?.Execute(CommandParameter);
        }
    }

    #endregion
}
