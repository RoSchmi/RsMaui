using MauiMixedShellNavigation.Pages;


namespace MauiMixedShellNavigation;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(Pages.SettingsPage), typeof(Pages.SettingsPage));
        Routing.RegisterRoute(nameof(Pages.AboutPage), typeof(Pages.AboutPage));
        Routing.RegisterRoute(nameof(Pages.SettingsDetailPage), typeof(Pages.SettingsDetailPage));

    }

    protected override void OnNavigating(ShellNavigatingEventArgs args)
    {
        base.OnNavigating(args);

        int breakpoint = 1;

        // Cancel any back navigation
        /*
        if (e.Source == ShellNavigationSource.Pop)
        {
            e.Cancel();
        }
        */
    }

    ///protected override void OnNavigating(ShellNavigatingEventArgs args)
    //    //}
    //}

    protected override void OnNavigated(ShellNavigatedEventArgs args)
    {
        base.OnNavigated(args);

    //    // Perform required logic
    }
}
