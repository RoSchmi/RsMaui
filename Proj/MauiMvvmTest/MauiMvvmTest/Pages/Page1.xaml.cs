using Microsoft.Maui.Hosting;
using MauiMvvmTest.ViewModels;
using Microsoft.Maui.Controls;

namespace MauiMvvmTest.Pages;

public partial class Page1 : ContentPage
{
	public Page1(Page1ViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}