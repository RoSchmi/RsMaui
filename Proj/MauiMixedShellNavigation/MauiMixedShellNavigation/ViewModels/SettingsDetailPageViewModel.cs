using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiMixedShellNavigation.Models;
using MauiMixedShellNavigation.Pages;

namespace MauiMixedShellNavigation.ViewModels
{

    [QueryProperty("InjectedSender", "Parameter")]

    public partial class SettingsDetailPageViewModel : ObservableObject, IQueryAttributable 
    {

        // Here come the messages from the sending page
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            // This method is not used when navigation is (like here) performed by 'PushModalAsync'
            // The arguments are injected through the constructor of 'SettingsDetailPage.xaml.cs'

            /*
            if (query != null)
            {
                var settingItems = query[(query["Parameter"]).ToString()] as SettingItems;
                InjectedName = settingItems.Name;
            }
            */

            int breakpoint_1 = 1;
        }

        public object transmissionObject { get; set; }

        [ObservableProperty]
        private string navigationState;

        [ObservableProperty]
        private string injectedSender;

        [ObservableProperty]
        private string injectedName;

        [RelayCommand]
        public void GoBack()
        {
            Dictionary<string, object> navigationParameter = new Dictionary<string, object>()
            {
                {nameof(SettingsDetailPage), transmissionObject},
                {"Parameter", nameof(SettingsDetailPage)}
            };

            
            //await Shell.Current.GoToAsync($"{nameof(SettingsDetailPage)}?Parameter={((SettingItems)s).Sender}", false, navigationParameter);
            Shell.Current.GoToAsync($"{new string("..")}?Parameter ={nameof(SettingsDetailPage)}", false, navigationParameter);
        }

        [RelayCommand]
        public void ShowNavigationState() 
        {
            NavigationState = Shell.Current.CurrentState.Location.ToString();
        }

        [RelayCommand]
        public void NavigatedTo(NavigatedToEventArgs e)
        {
            NavigationState = Shell.Current.CurrentState.Location.ToString();
            if (transmissionObject != null)
            {
                var settingsDictionary = transmissionObject as Dictionary<string, object>;
                var settingItems = settingsDictionary[(settingsDictionary["Parameter"]).ToString()] as SettingItems;
                InjectedName = settingItems.Name;
            }
        }
    }
}
