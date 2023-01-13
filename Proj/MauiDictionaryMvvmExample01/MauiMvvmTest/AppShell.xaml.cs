using MauiDictionaryMvvmExample01.Pages;

namespace MauiDictionaryMvvmExample01;


public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        
    }
}
