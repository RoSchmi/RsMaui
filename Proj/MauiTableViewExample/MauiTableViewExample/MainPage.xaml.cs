namespace MauiTableViewExample;

public partial class MainPage : ContentPage
{
    // Note (RoSchmi): Important:

    // For MVVM in .xaml has to be included:            
    // xmlns:viewmodel="clr-namespace:MauiTableViewExample"
    // x:DataType="viewmodel:MainPageViewModel">
    // In 'MauiProgram.cs' References to MainPage and MainPageViewModel have to be added
    // In AppShell.xaml.cs the navigation routes have to be registered

    public MainPage()
	{
		InitializeComponent();
	}
}

