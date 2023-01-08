using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiTableViewExample.Pages;


namespace MauiTableViewExample
{
    public partial class MainPageViewModel : ObservableObject
    {

        [RelayCommand]
        public async void GoToSettings(string s)
        {
            //string targetPage = nameof(SettingsPage);
            //string injectedParameter = s;
            await Shell.Current.GoToAsync($"{nameof(SettingsPage)}?Parameter={s}");
        }

    }
}
