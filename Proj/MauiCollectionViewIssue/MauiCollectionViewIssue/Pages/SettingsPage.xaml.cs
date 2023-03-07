using MauiCollectionViewIssue.ViewModels;


namespace MauiCollectionViewIssue.Pages;

public partial class SettingsPage : ContentPage
{
	SettingsViewModel vm = new();
	public SettingsPage()
	{
		InitializeComponent();
		BindingContext = vm;
	}
}