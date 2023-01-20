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

        public static Dictionary<string, TransportItem> WorkItemsToTransportItems(ObservableCollection<WorkItem> pWorkItems)
        {
            Dictionary<string, TransportItem> PropertiesDictionary = new Dictionary<string, TransportItem>();

            // Add the TransportItems from the WorkItems
            foreach (WorkItem workItem in pWorkItems)
            {
                switch (workItem.TypeIdentifier)
                {
                    case WorkItem.TypeID.RsString:
                        {
                            PropertiesDictionary.Add(workItem.Name, new TransportItem() { Name = workItem.Name, TypeIdentifier = workItem.TypeIdentifier, Content = new StringTypeContent() { Value = workItem.StringValue } });
                            break;
                        }
                    case WorkItem.TypeID.RsBoolean:
                        {
                            PropertiesDictionary.Add(workItem.Name, new TransportItem() { Name = workItem.Name, TypeIdentifier = workItem.TypeIdentifier, Content = new BoolTypeContent() { Value = workItem.BoolValue } });
                            break;
                        }
                    case WorkItem.TypeID.RsDateTime:
                        {
                            PropertiesDictionary.Add(workItem.Name, new TransportItem() { Name = workItem.Name, TypeIdentifier = workItem.TypeIdentifier, Content = new DateTimeTypeContent() { Value = workItem.DateValue } });
                            break;
                        }
                    default:
                        {
                            PropertiesDictionary.Add(workItem.Name, new TransportItem() { Name = workItem.Name, TypeIdentifier = workItem.TypeIdentifier, Content = new StringTypeContent() { Value = workItem.StringValue } });
                            break;
                        }
                }
            }

            return PropertiesDictionary;
        }

        
        public static ObservableCollection<WorkItem> TransportItemsToWorkItems(Dictionary<string, TransportItem> pTransportItems)
        {
            ObservableCollection<WorkItem> SettingPropertyCollection = new ObservableCollection<WorkItem>();
          
            foreach (KeyValuePair<string, TransportItem> property in pTransportItems)          
            {
                switch (property.Value.TypeIdentifier)
                {
                    case WorkItem.TypeID.RsString:
                    case WorkItem.TypeID.RsStringRo:
                        {
                            SettingPropertyCollection.Add(new WorkItem() { Name = property.Value.Name, TypeIdentifier = property.Value.TypeIdentifier, StringValue = ((StringTypeContent)property.Value.Content).Value });
                            break;
                        }

                    case WorkItem.TypeID.RsBoolean:
                        {
                            SettingPropertyCollection.Add(new WorkItem() { Name = property.Value.Name, TypeIdentifier = property.Value.TypeIdentifier, BoolValue = ((BoolTypeContent)property.Value.Content).Value });
                            break;
                        }

                    case WorkItem.TypeID.RsDateTime:
                        {
                            SettingPropertyCollection.Add(new WorkItem() { Name = property.Value.Name, TypeIdentifier = property.Value.TypeIdentifier, DateValue = ((DateTimeTypeContent)property.Value.Content).Value });
                            break;
                        }
                }  
            }
            return SettingPropertyCollection;
        }
    }
}
