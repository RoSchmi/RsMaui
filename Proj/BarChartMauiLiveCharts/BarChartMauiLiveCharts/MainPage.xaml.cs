using BarChartMauiLiveCharts;
using System.Collections.ObjectModel;




namespace BarChartMauiLiveCharts;

public partial class MainPage : ContentPage
{
    MainPageViewModel vm;
    //DeviceDisplay deviceDisplay;
public MainPage(MainPageViewModel theViewModel)
    {
        InitializeComponent();
        BindingContext = theViewModel;
        vm = theViewModel;
    }

    private void ResetButton_Clicked(object sender, EventArgs e)
    {
    }
}

