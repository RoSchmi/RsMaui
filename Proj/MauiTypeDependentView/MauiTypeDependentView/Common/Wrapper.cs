using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;
using MauiTypeDependentView.Models;
using MauiTypeDependentView.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace MauiTypeDependentView.Common
{
    public static class Wrapper
    {

        public static int returnTheInput(int a)
        { return a; }

        public static Dictionary<string, TransportItem> SettingItemsToTransportItems(ObservableCollection<SettingItem> pSettingItems)
        {
            Dictionary<string, TransportItem> PropertiesDictionary = new Dictionary<string, TransportItem>();

            // Add the TransportItems from the SettingItems
            foreach (SettingItem settingItem in pSettingItems)
            {
                switch (settingItem.TypeIdentifier)
                {
                    case SettingItem.TypeID.RsString:
                        {
                            PropertiesDictionary.Add(settingItem.Name, new TransportItem() { Name = settingItem.Name, TypeIdentifier = settingItem.TypeIdentifier, Content = new StringTypeContent() { Value = settingItem.StringValue } });
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

            return PropertiesDictionary;
        }

        
        public static ObservableCollection<SettingItem> TransportItemsToSettingItems(Dictionary<string, TransportItem> pTransportItems)
        {
            ObservableCollection<SettingItem> SettingPropertyCollection = new ObservableCollection<SettingItem>();
          
            foreach (KeyValuePair<string, TransportItem> property in pTransportItems)          
            {
                switch (property.Value.TypeIdentifier)
                {
                    case SettingItem.TypeID.RsString:
                    case SettingItem.TypeID.RsStringRo:
                        {
                            SettingPropertyCollection.Add(new SettingItem() { Name = property.Value.Name, TypeIdentifier = property.Value.TypeIdentifier, StringValue = ((StringTypeContent)property.Value.Content).Value });
                            break;
                        }

                    case SettingItem.TypeID.RsBoolean:
                        {
                            SettingPropertyCollection.Add(new SettingItem() { Name = property.Value.Name, TypeIdentifier = property.Value.TypeIdentifier, BoolValue = ((BoolTypeContent)property.Value.Content).Value });
                            break;
                        }

                    case SettingItem.TypeID.RsDateTime:
                        {
                            SettingPropertyCollection.Add(new SettingItem() { Name = property.Value.Name, TypeIdentifier = property.Value.TypeIdentifier, DateValue = ((DateTimeTypeContent)property.Value.Content).Value });
                            break;
                        }
                }  
            }
            return SettingPropertyCollection;
        }
    }
}
