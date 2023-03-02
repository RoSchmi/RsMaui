namespace MauiMixedShellNavigation;

public partial class MainPage : ContentPage
{
	

    MainPageViewModel vm = new();

    public MainPage()
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

