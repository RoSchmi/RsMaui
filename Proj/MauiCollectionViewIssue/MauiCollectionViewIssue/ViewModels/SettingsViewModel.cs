using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MauiCollectionViewIssue.ViewModels
{
    partial class SettingsViewModel : ObservableObject 
    {
        [RelayCommand]
        private async void GoBack()
        {
            await Shell.Current.GoToAsync($"///{nameof(MainPage)}");
        }  
    }
}

