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

        private SuitCaseProperties receivedSettingsProperties;

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

            receivedSettingsProperties = query[query.Keys.First()] as SuitCaseProperties;

            ItemCollection = new();


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
           
    _customerSet = _defaultContainer.AddEntitySet("Customers", _customerType);

            var theName = _customerSet.Name;
            */

            ItemCollection.Add(new SettingItem() { Name = "SettingsID", ValueType = "RsString", StringValue= "String_ID_123", Content = new StringTypeContent() { Value = "String_ID_123" } });
            ItemCollection.Add(new SettingItem() { Name = "SettingsState", ValueType = "RsBoolean", BoolValue = true, Content = new BoolTypeContent() { Value = true } });
            ItemCollection.Add(new SettingItem() { Name = "SettingsDate", ValueType = "RsDateTime",  DateValue = DateTime.Now, Content = new DateTimeTypeContent() { Value = DateTime.Now } });


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
            /*
            Dictionary<string, string> properties = new Dictionary<string, string>();

            foreach(SettingItem settingsItem in ItemCollection)
            {
                properties.Add(settingsItem.Name, settingsItem.Content);
            }

            SuitCaseProperties suitCaseProperties = new SuitCaseProperties();
            suitCaseProperties.PropertiesDictionary = properties;
           

            var navigationParameter = new Dictionary<string, object>();
            navigationParameter.Add("FirstAndOnly", suitCaseProperties);

             */

            await Shell.Current.GoToAsync("..");

            //await Shell.Current.GoToAsync($"{nameof(MainPage)}", navigationParameter);

            //await Shell.Current.GoToAsync("..", navigationParameter);

            //await Shell.Current.GoToAsync($"{nameof(SettingsPage)}", navigationParameter);
        } 
    }
}
