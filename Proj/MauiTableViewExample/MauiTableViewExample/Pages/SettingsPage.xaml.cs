using MauiTableViewExample.ViewModels;

namespace MauiTableViewExample.Pages;

public partial class SettingsPage : ContentPage

{
    //SettingsViewModel vm;
    SettingsViewModel vm = new SettingsViewModel();
    //public SettingsPage(SettingsViewModel theViewModel)
    public SettingsPage()
    {
		InitializeComponent();
        //vm = new SettingsViewModel();
        BindingContext = vm;
        TableViewAccounts.Root = vm.AccountsTableRoot;
    }
}

