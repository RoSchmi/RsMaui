using Microsoft.Maui.Hosting;
using MauiDictionaryMvvmExample01.ViewModels;
using Microsoft.Maui.Controls;

namespace MauiDictionaryMvvmExample01.Pages;

[QueryProperty("TextToShow", "Parameter")]

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsPageViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}