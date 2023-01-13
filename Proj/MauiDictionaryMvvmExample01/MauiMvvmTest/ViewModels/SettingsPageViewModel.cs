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

        private SettingsProperties receivedSettingsProperties;

        [ObservableProperty]
        string settingsName;

        [ObservableProperty]
        private SettingItems settingsItem;

        [ObservableProperty]
        private ObservableCollection<SettingItems> itemCollection;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {

            receivedSettingsProperties = query[query.Keys.First()] as SettingsProperties;

            ItemCollection = new();

            foreach (KeyValuePair<string, string> member in receivedSettingsProperties.Properties)
            {
                ItemCollection.Add(new SettingItems() { Name = member.Key, Description = member.Value });
            }

            
            // ToDo: First I wanted to try this example from
            // -https://stackoverflow.com/questions/36359631/wpf-mvvm-bind-dictionarystring-liststring-to-datagrid
            // but didn't make further efforts since in Maui there is no DataGrid

        }

        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        } 
    }
}
