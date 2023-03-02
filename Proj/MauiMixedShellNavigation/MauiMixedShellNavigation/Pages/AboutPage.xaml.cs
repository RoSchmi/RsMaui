using MauiMixedShellNavigation.ViewModels;

namespace MauiMixedShellNavigation.Pages;

public partial class AboutPage : ContentPage
{
	AboutPageViewModel vm = new();
	public AboutPage()
	{
		InitializeComponent();
        BindingContext = vm;
    }
}


