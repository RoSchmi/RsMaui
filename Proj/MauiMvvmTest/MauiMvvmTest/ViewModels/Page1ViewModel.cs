using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
namespace MauiMvvmTest.ViewModels
{
    // Navigation data can be received by decorating the receiving class with a
    // QueryPropertyAttribute for each string-based query parameter and object-based navigation parameter.
    // Object-based navigation data can be passed with a GoToAsync overload that specifies an IDictionary<string, object> argument:

    [QueryProperty("TextToShow", "Parameter")]
    public partial class Page1ViewModel : ObservableObject
    {

        // Don't forget to register the viewmodel in 'MauiProgram.cs'
        // Don't forget to set the reference in 'Page1.xaml.cs

        [ObservableProperty]
        string textToShow;

        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }
        
    }
}
