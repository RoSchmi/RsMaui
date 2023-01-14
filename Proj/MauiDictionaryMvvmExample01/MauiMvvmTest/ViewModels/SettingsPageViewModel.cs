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
using MauiDictionaryMvvmExample01.Pages;


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

        private SuitCaseProperties receivedSettingsProperties;

        [ObservableProperty]
        string settingsName;

        [ObservableProperty]
        private SettingItems settingsItem;

        [ObservableProperty]
        private ObservableCollection<SettingItems> itemCollection;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {

            receivedSettingsProperties = query[query.Keys.First()] as SuitCaseProperties;

            ItemCollection = new();

            foreach (KeyValuePair<string, string> property in receivedSettingsProperties.PropertiesDictionary)
            {
                ItemCollection.Add(new SettingItems() { Name = property.Key, Content = property.Value });
            }

            
            // ToDo: First I wanted to try this example from
            // -https://stackoverflow.com/questions/36359631/wpf-mvvm-bind-dictionarystring-liststring-to-datagrid
            // but didn't make further efforts since in Maui there is no DataGrid

        }

        [RelayCommand]
        async Task GoBack()
        {

            Dictionary<string, string> properties = new Dictionary<string, string>();

            foreach(SettingItems settingsItem in ItemCollection)
            {
                properties.Add(settingsItem.Name, settingsItem.Content);
            }

            SuitCaseProperties suitCaseProperties = new SuitCaseProperties();
            suitCaseProperties.PropertiesDictionary = properties;


            var navigationParameter = new Dictionary<string, object>();
            navigationParameter.Add("FirstAndOnly", suitCaseProperties);

            // await Shell.Current.GoToAsync("..");

            //await Shell.Current.GoToAsync($"{nameof(MainPage)}", navigationParameter);

            await Shell.Current.GoToAsync("..", navigationParameter);

            //await Shell.Current.GoToAsync($"{nameof(SettingsPage)}", navigationParameter);
        } 
    }
}
