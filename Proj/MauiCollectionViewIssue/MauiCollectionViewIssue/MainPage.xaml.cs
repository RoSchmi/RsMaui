namespace MauiCollectionViewIssue;

public partial class MainPage : ContentPage
{
	MainPageViewModel vm = new();
	public MainPage()
	{
		InitializeComponent();
		BindingContext = vm;
	}
}

