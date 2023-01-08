using MauiTableViewExample.ViewModels;

namespace MauiTableViewExample.Pages;

public partial class SettingsPage : ContentPage

{
    SettingsViewModel vm = new SettingsViewModel();
    
    public SettingsPage()
    {
		InitializeComponent();      
        BindingContext = vm;
        TableViewAccounts.Root = vm.AccountsTableRoot;
    }
}

