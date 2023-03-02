using MauiMixedShellNavigation.ViewModels;

namespace MauiMixedShellNavigation.Pages;

public partial class SettingsDetailPage : ContentPage
{

    SettingsDetailPageViewModel vm = new();
    public SettingsDetailPage(string sender, object transmissionObject)
    {
        InitializeComponent();
        BindingContext = vm;
        vm.InjectedSender = sender;
        vm.transmissionObject = transmissionObject;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        vm.NavigatedTo(args);
    }
}