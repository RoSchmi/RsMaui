

// Uses examples from
// https://github.com/janguar/FirstMauiApp


using MauiMvvmTest;
using System.Collections.ObjectModel;

namespace MauiMvvmTest;

public partial class MainPage : ContentPage
{
	int count = 0;

    // RoSchmi: Important:

    // For MVVM in .xaml has to be included:            
    // xmlns:viewmodel="clr-namespace:MauiMvvmTest"
    // x:DataType="viewmodel:MainPageViewModel">
    // In 'MauiProgram.cs' References to MainPage and MainPageViewModel have to be added

    MainPageViewModel theViewModel= null;

    public MainPage(MainPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        theViewModel= vm;
        
    }

    private void ResetButton_Clicked(object sender, EventArgs e)
	{
		theViewModel.CounterCopy = "0";
	}

    private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
	}

    
}

