using Microsoft.Maui.Hosting;
using MauiTypeDependentView.ViewModels;
using Microsoft.Maui.Controls;
using DataTemplates;

namespace MauiTypeDependentView.Pages;

[QueryProperty("TextToShow", "Parameter")]

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsPageViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}