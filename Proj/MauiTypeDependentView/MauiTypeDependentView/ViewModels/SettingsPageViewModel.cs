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
using MauiTypeDependentView.Models;
using Common.Models;
using MauiTypeDependentView.Common;
using System.Collections.ObjectModel;
using MauiTypeDependentView.Pages;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;




namespace MauiTypeDependentView.ViewModels
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

        private SuitCaseProperties receivedTransportProperties;

        [ObservableProperty]
        string settingsName;

        //[ObservableProperty]
        //private WorkItem settingsItem;

        [ObservableProperty]
        private ObservableCollection<WorkItem> workItemCollection;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            receivedTransportProperties = query[query.Keys.First()] as SuitCaseProperties;

            WorkItemCollection = Wrapper.TransportItemsToWorkItems(receivedTransportProperties.PropertiesDictionary);

            
            // ToDo: First I wanted to try this example from
            // -https://stackoverflow.com/questions/36359631/wpf-mvvm-bind-dictionarystring-liststring-to-datagrid
            // but didn't make further efforts since in Maui there is no DataGrid

        }

        [RelayCommand]
        async Task GoBack()
        {
            // Create a dictionary with TransportItems to hold all the Settings belonging to the selected SettingsName / SettingsID
            
            Dictionary<string, TransportItem> TransportItemDictionary = Wrapper.WorkItemsToTransportItems(WorkItemCollection);
      
            SuitCaseProperties suitCaseProperties = new SuitCaseProperties() { PropertiesDictionary = TransportItemDictionary };
            
            // Get name out of the collection of WorkItems (it can be that the name was changed)
            var newName = WorkItemCollection.FirstOrDefault(x => x.Name == "SettingsName").StringValue;

            var navigationParameter = new Dictionary<string, object>();
            // We have the old name as the key, may be that there is a changed name in the suitCaseProperties
            navigationParameter.Add(SettingsName, suitCaseProperties);

            await Shell.Current.GoToAsync("..", navigationParameter);

            //await Shell.Current.GoToAsync("..");

            //await Shell.Current.GoToAsync($"{nameof(MainPage)}", navigationParameter);
          
        }
    }
}
