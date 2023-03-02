using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiMixedShellNavigation.Pages;



namespace MauiMixedShellNavigation
{

    // String message from the sending page
    [QueryProperty("InjectedSender", "Parameter")]
    public partial class MainPageViewModel : ObservableObject, IQueryAttributable
    {

        public IDictionary<string, object> queryHandle; 

        // Here come the messages from the sending page 
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            queryHandle = query;
            var injectedDictionary = query;
            int breakpoint = 1;
        }

        [RelayCommand]
        public async void NavigatedTo(NavigatedToEventArgs e)
        {
            NavigationState = Shell.Current.CurrentState.Location.ToString();

            Sender = InjectedSender;
            InjectedSender = null;
            if (queryHandle != null)
            {
                queryHandle.Clear();
            }
        }

        [RelayCommand]
        private void ShowNavigationState()
        {
            NavigationState = Shell.Current.CurrentState.Location.ToString();   
        }

        [RelayCommand]
        private async void GoToSettings(string s)
        {
            string targetPage = nameof(SettingsPage);
            string injectedParameter = s;

            var navigationParameter = new Dictionary<string, object>
        {
            { nameof(MainPage), new object() }
        };

            await Shell.Current.GoToAsync($"///{nameof(SettingsPage)}?Parameter={s}", navigationParameter);
            
        }

        [ObservableProperty]
        private string injectedSender;

        [ObservableProperty]
        private string sender;

        [ObservableProperty]
        private string navigationState;
    }
}
