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

        [ObservableProperty]
        private SettingItem settingsItem;

        [ObservableProperty]
        private ObservableCollection<SettingItem> itemCollection;

       // [ObservableProperty]
       // private StringTypeContent stringTypeContent;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {

            receivedTransportProperties = query[query.Keys.First()] as SuitCaseProperties;

            ItemCollection = new();
     
                foreach (KeyValuePair<string, TransportItem> property in receivedTransportProperties.PropertiesDictionary)
                {
                switch (property.Value.TypeIdentifier)
                {
                    case SettingItem.TypeID.RsString:
                        {
                            ItemCollection.Add(new SettingItem() { Name = property.Value.Name, TypeIdentifier = property.Value.TypeIdentifier, StringValue = ((StringTypeContent)property.Value.Content).Value });
                            break;
                        }
                    case SettingItem.TypeID.RsBoolean:
                        {
                            ItemCollection.Add(new SettingItem() { Name = property.Value.Name, TypeIdentifier = property.Value.TypeIdentifier, BoolValue = ((BoolTypeContent)property.Value.Content).Value });
                            break;
                        }
                    case SettingItem.TypeID.RsDateTime:
                        {
                            ItemCollection.Add(new SettingItem() { Name = property.Value.Name, TypeIdentifier = property.Value.TypeIdentifier, DateValue = ((DateTimeTypeContent)property.Value.Content).Value });
                            break;
                        }
                }
            }


                // https://learn.microsoft.com/en-us/odata/odatalib/edm/build-basic-model
                // only some efforts to understand EDM things

             /*
             EdmModel _model = new EdmModel();
             EdmEntityType _customerType;
            _customerType = new EdmEntityType("Sample.NS", "Customer");
            _customerType.AddKeys(_customerType.AddStructuralProperty("Id", EdmPrimitiveTypeKind.Int32, isNullable: false));
            _customerType.AddStructuralProperty("Name", EdmPrimitiveTypeKind.String, isNullable: false);

            
            _customerType.AddStructuralProperty("Credits",
                new EdmCollectionTypeReference(new EdmCollectionType(EdmCoreModel.Instance.GetInt64(isNullable: true))));
                _model.AddElement(_customerType);
            

            EdmEntityContainer _defaultContainer;

            _defaultContainer = new EdmEntityContainer("Sample.NS", "DefaultContainer");
            _model.AddElement(_defaultContainer);

            EdmEntitySet _customerSet;
            _customerSet.

        _customerSet = _defaultContainer.AddEntitySet("Customers", _customerType);

                var theName = _customerSet.Name;
               
           */

            /*

            ItemCollection.Add(new SettingItem() { Name = "SettingsID", TypeIdentifier = SettingItem.TypeID.RsString, StringValue= "String_ID_123", Content = new StringTypeContent() { Value = "String_ID_123" } });
            ItemCollection.Add(new SettingItem() { Name = "SettingsState", TypeIdentifier = SettingItem.TypeID.RsBoolean, BoolValue = true, Content = new BoolTypeContent() { Value = true } });
            ItemCollection.Add(new SettingItem() { Name = "SettingsDate", TypeIdentifier = SettingItem.TypeID.RsDateTime,  DateValue = DateTime.Now, Content = new DateTimeTypeContent() { Value = DateTime.Now } });

            */

            /*
            foreach (KeyValuePair<string, string> property in receivedSettingsProperties.PropertiesDictionary)
            {
                ItemCollection.Add(new SettingItem() { Name = property.Key, Content = property.Value });
            }
            */

            
            // ToDo: First I wanted to try this example from
            // -https://stackoverflow.com/questions/36359631/wpf-mvvm-bind-dictionarystring-liststring-to-datagrid
            // but didn't make further efforts since in Maui there is no DataGrid

        }

        [RelayCommand]
        async Task GoBack()
        {
            

            // Create a dictionary with TransportItems to hold all the Settings belonging to the selected SettingsName / SettingsID
            Dictionary<string, TransportItem> PropertiesDictionary = new Dictionary<string, TransportItem>();

            // Add the TransportItems from the SettingItems
            foreach (SettingItem settingItem in ItemCollection)
            {
                switch (settingItem.TypeIdentifier)
                {
                    case SettingItem.TypeID.RsString:
                        {
                            PropertiesDictionary.Add(settingItem.Name, new TransportItem() { Name = settingItem.Name, TypeIdentifier = settingItem.TypeIdentifier, Content = new StringTypeContent() { Value = settingItem.StringValue } } );
                            break;
                        }
                    case SettingItem.TypeID.RsBoolean:
                        {
                            PropertiesDictionary.Add(settingItem.Name, new TransportItem() { Name = settingItem.Name, TypeIdentifier = settingItem.TypeIdentifier, Content = new BoolTypeContent() { Value = settingItem.BoolValue } });
                            break;
                        }
                    case SettingItem.TypeID.RsDateTime:
                        {
                            PropertiesDictionary.Add(settingItem.Name, new TransportItem() { Name = settingItem.Name, TypeIdentifier = settingItem.TypeIdentifier, Content = new DateTimeTypeContent() { Value = settingItem.DateValue } });
                            break;
                        }
                        default:
                        {
                            PropertiesDictionary.Add(settingItem.Name, new TransportItem() { Name = settingItem.Name, TypeIdentifier = settingItem.TypeIdentifier, Content = new StringTypeContent() { Value = settingItem.StringValue } });
                            break;
                        }
                }
            }
        
            SuitCaseProperties suitCaseProperties = new SuitCaseProperties();
            suitCaseProperties.PropertiesDictionary = PropertiesDictionary;

            // Get name out of the collection of SettingItems (it can be that the name was changed)
            var newName = ItemCollection.FirstOrDefault(x => x.Name == "SettingsName").StringValue;


            var navigationParameter = new Dictionary<string, object>();
            // We have the old name as the key, may be that there is a changed name in the suitCaseProperties
            navigationParameter.Add(SettingsName, suitCaseProperties);



            //await Shell.Current.GoToAsync("..");

            //await Shell.Current.GoToAsync($"{nameof(MainPage)}", navigationParameter);

            await Shell.Current.GoToAsync("..", navigationParameter);

            //await Shell.Current.GoToAsync($"{nameof(SettingsPage)}", navigationParameter);
        } 
    }
}
