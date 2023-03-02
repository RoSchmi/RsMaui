using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiMixedShellNavigation.Models;
using MauiMixedShellNavigation.Pages;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace MauiMixedShellNavigation.ViewModels
{
    // String message from the sending page
    [QueryProperty("InjectedSender", "Parameter")]

    //InjectedSender

    public partial class SettingsViewModel : ObservableObject, IQueryAttributable
    {
        public SettingsViewModel()
        {}

        private IDictionary<string, object> queryHandle;

        // Here come the messages from the sending page
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            queryHandle = query;  // make a handle to clear the dictionary
            int breakpoint_1 = 1;
        }

        [RelayCommand]
        public void NavigatedTo(NavigatedToEventArgs e)
        {
            NavigationState = Shell.Current.CurrentState.Location.ToString();
            Sender = InjectedSender;
            InjectedSender = null;
            if (queryHandle != null)
            {
                queryHandle.Clear();
            }

        }

        #region RelayCommand GotoHome
        [RelayCommand]
        private async void GotoHome()
        {
            Dictionary<string, object> navigationParameter = new Dictionary<string, object>()
            {
                {nameof(SettingsPage), new object()}
               
            };
                await Shell.Current.GoToAsync($"///{nameof(MainPage)}?Parameter={nameof(SettingsPage)}", false, navigationParameter);
        }
        #endregion

        #region RelayCommand ShowNavigationState
        [RelayCommand]
        private void ShowNavigationState()
        {
            /*
            var thePage = Shell.Current.CurrentPage as SettingsPage;
            await Shell.Current.Navigation.PopToRootAsync();
            await Shell.Current.Navigation.PushAsync(thePage);
            theState = Shell.Current.CurrentState;
            await Shell.Current.GoToAsync($"//MainPage");
            */

            NavigationState = Shell.Current.CurrentState.Location.ToString();
            int breakpoint = 1;
        }
#endregion

        #region RelayCommand Tap
        [RelayCommand]
        private async void Tap(object s)
        {
            Dictionary<string, object> navigationParameter = new Dictionary<string, object>()
            {
                {((SettingItems)s).Sender, s},
                {"Parameter", ((SettingItems)s).Sender}
            };

            
           //await Shell.Current.GoToAsync($"{nameof(SettingsDetailPage)}?Parameter={((SettingItems)s).Sender}", false, navigationParameter);

           await Shell.Current.Navigation.PushModalAsync(new SettingsDetailPage(((SettingItems)s).Sender, navigationParameter));

            int breakpoint = 1;
        }
        #endregion

        [ObservableProperty]
        private string sender;

        [ObservableProperty]
        private string injectedSender;

        [ObservableProperty]
        private string navigationState;

        [ObservableProperty]
        private ObservableCollection<SettingItems> settingsItems = new ObservableCollection<SettingItems>() {
            new SettingItems() {Sender = "SettingsPage", Name = "Item_1", Instruction = "Click me" },
            new SettingItems() {Sender = "SettingsPage", Name = "Item_2", Instruction = "Click me" },
            new SettingItems() {Sender = "SettingsPage", Name = "Item_3", Instruction = "Click me" },
            new SettingItems() {Sender = "SettingsPage", Name = "Item_4", Instruction = "Click me" },
            new SettingItems() {Sender = "SettingsPage", Name = "Item_5", Instruction = "Click me" },
            new SettingItems() {Sender = "SettingsPage", Name = "Item_6", Instruction = "Click me" },
            new SettingItems() {Sender = "SettingsPage", Name = "Item_7", Instruction = "Click me" },
            new SettingItems() {Sender = "SettingsPage", Name = "Item_8", Instruction = "Click me" },
            };
    }
}
