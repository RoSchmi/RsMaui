using MauiMixedShellNavigation.ViewModels;
using MauiMixedShellNavigation.Models;

namespace MauiMixedShellNavigation.Pages;

public partial class SettingsPage : ContentPage
{
    SettingsViewModel vm = new();
    public SettingsPage()
	{
		InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        vm.NavigatedTo(args);

        int breakpoint = 1;
    }
}