using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiCollectionViewIssue.Pages;

namespace MauiCollectionViewIssue
{
    public partial class MainPageViewModel : ObservableObject
    {
        [RelayCommand]
        private async void OpenSettingsPage()
        {  
            await Shell.Current.GoToAsync($"///{nameof(SettingsPage)}"); 
        }
    }
}
