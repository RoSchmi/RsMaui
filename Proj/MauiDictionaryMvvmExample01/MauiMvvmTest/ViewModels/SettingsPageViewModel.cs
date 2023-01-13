using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Microsoft.UI.Xaml.Controls;
using MauiDictionaryMvvmExample01.Models;
using System.Collections.ObjectModel;

namespace MauiDictionaryMvvmExample01.ViewModels
{
    // Navigation data can be received by decorating the receiving class with a
    // QueryPropertyAttribute for each string-based query parameter and object-based navigation parameter.
    // Object-based navigation data can be passed with a GoToAsync overload that specifies an IDictionary<string, object> argument:

    [QueryProperty("SettingsName", "Parameter")]

    //  'IQueryAttributable' is needed to unwrap the dictionary using the method 'ApplyQueryAttributes(..)'
    public partial class SettingsPageViewModel : ObservableObject, IQueryAttributable  
    {

        // Don't forget to register the viewmodel in 'MauiProgram.cs'
        // Don't forget to set the reference in 'SettingsPage.xaml.cs

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {

            ReceivedSettingsProperties = query[query.Keys.First()] as SettingsProperties;

            ItemCollection = new();

            foreach (KeyValuePair<string, string> member in ReceivedSettingsProperties.Properties)
            {
                ItemCollection.Add(new SettingItems() { Name = member.Key, Description = member.Value });
            }

            // At first I wanted to try Dictionary for holding the Settings Data but then I switched over to 'Observable Collection'
            // Didn't delete it for now for some further test.

            MyDict = new();

            foreach (var item in ItemCollection)
            {
                MyDict.Add(new SettingItems() { Name = item.Name  , Description = item.Description }, item.Description);
                int dummy56 = 1;
            }

            // Then I wanted to try this example from
            // -https://stackoverflow.com/questions/36359631/wpf-mvvm-bind-dictionarystring-liststring-to-datagrid

            // but didn't make further efforts since in Maui there is no DataGrid

            var partners = new Dictionary<string, List<string>>
        {
            {"Company", new List<string> {"company1", "company2"}},
            {"Operator", new List<string> {"false", "true"}},
            {"Interest", new List<string> {"40%", "60%"}},
            {"Type", new List<string> {"type1", "type2"}}
        };
            Asset = new Result("TestType", "TestName", partners);

            int dummy24 = 1;

        }
        [ObservableProperty]
        private SettingItems settingsItem;


        [ObservableProperty]
        private Result asset; 

        [ObservableProperty]
        private SettingsProperties receivedSettingsProperties;

        [ObservableProperty]
        private ObservableCollection<SettingItems> itemCollection;

        [ObservableProperty]
        private Dictionary<SettingItems, string> myDict;

        [ObservableProperty]
        string settingsName;

        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        } 
    }
}
