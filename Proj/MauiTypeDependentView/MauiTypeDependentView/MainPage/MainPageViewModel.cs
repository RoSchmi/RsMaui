﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiTypeDependentView.Pages;
using MauiTypeDependentView.Models;
using Common.Models;
using System.Collections;
using Microsoft.Maui.Controls;
using Microsoft.UI.Xaml.Controls;
using MauiTypeDependentView.ViewModels;
using MauiTypeDependentView.Common;

namespace MauiTypeDependentView
{
    public partial class MainPageViewModel : ObservableObject, IQueryAttributable
    {
        public MainPageViewModel()
        {
            ItemNames = new ObservableCollection<string>();
        }

        //public Dictionary<string, object> ItemDescriptions = new Dictionary<string, object>();
        public Dictionary<string, SuitCaseProperties> ItemDescriptions = new Dictionary<string, SuitCaseProperties>();

        [ObservableProperty] // source generator
        private ObservableCollection<string> itemNames;

        [ObservableProperty]
        private ObservableCollection<WorkItem> workItemCollection;


        [ObservableProperty] // source generator
        private string itemText;

        [ObservableProperty] // source generator
        private string counterCopy = "0";

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            // This comes back, evtl. changed from the Detail Page, use it to do what you want

            SuitCaseProperties receivedTransportProperties = query[query.Keys.First()] as SuitCaseProperties;

            WorkItemCollection = Wrapper.TransportItemsToWorkItems(receivedTransportProperties.PropertiesDictionary);
         
            // Get new name out of the collection of WorkItems (it can be that the name was changed)
            string newName = WorkItemCollection.FirstOrDefault(x => x.Name == "SettingsName").StringValue as string;
            // the old name, it's the key
            string oldName = query.Keys.First();


           
            if (!ItemNames.Contains(newName))
            {         
                ItemDescriptions.Add(newName, receivedTransportProperties);

                ItemDescriptions.Remove(oldName);

                if (ItemNames.Contains(oldName))
                {
                    // Don't know why I had to create a new Items observable collection
                    // The outcommented code below did not work, threw an exception

                    List<string> itemList = ItemNames.ToList<string>();
                    itemList[itemList.IndexOf(oldName)] = newName;
                    
                    ItemNames = new ObservableCollection<string>();
                    foreach(var item in itemList)
                    {
                        ItemNames.Add(item);
                    }
          
                    //  ItemNames[ItemNames.IndexOf(oldName)] = newName;
                }            
            }
            else
            {
                if (oldName == newName)   // Don't allow changing of a name to a name which is already there
                {
                    ItemDescriptions[newName] = receivedTransportProperties;
                }
            }
        }



        [RelayCommand]
        private void Add()
        {
            if (string.IsNullOrEmpty(itemText))
                return;

            // Adding twice the same name is not allowed
            if (!ItemNames.Contains(itemText))
            {
                ItemNames.Add(itemText);
            }
            ItemText = string.Empty;
        }

        [RelayCommand] // source generator
        private void IncrementContent()
        {
            int counterCopyInt = int.Parse(counterCopy);
            counterCopyInt++;
            CounterCopy = counterCopyInt.ToString();
        }

        [RelayCommand]
        private void SayHello(string s)
        {

            int breakPoint3 = 1;  // Do nothing
        }

        [RelayCommand]
        private void Remove(string s)
        {
            if (ItemNames.Contains(s))
            {
                ItemNames.Remove(s);
            }

            if (ItemDescriptions.ContainsKey(s))
            {
                ItemDescriptions.Remove(s);
            }
        }

        [RelayCommand]
        private async void Tap(string s)
        {
            //string targetPage = nameof(SettingsPage);
            //string injectedParameter = s;

            // As the ID we take a GUID, is not used for now
            // As the SettingsName we take 's' which comes from the bar which was tapped

            var ID = Guid.NewGuid().ToString();
           
            var suitCaseProperties = new SuitCaseProperties()
            {
                // properties get wrapped in this inner Dictionary
               
                PropertiesDictionary = new Dictionary<string, TransportItem>() {
                    { "SettingsID", new TransportItem() { Name = "SettingsID", TypeIdentifier = WorkItem.TypeID.RsStringRo, Content = new StringTypeContent() { Value = ID } } },
                    { "SettingsName", new TransportItem() { Name = "SettingsName", TypeIdentifier = WorkItem.TypeID.RsString, Content = new StringTypeContent() { Value = s } } },
                    { "SettingsState", new TransportItem() { Name = "SettingsState", TypeIdentifier = WorkItem.TypeID.RsBoolean, Content = new BoolTypeContent() { Value = null } } },
                    { "SettingsDate", new TransportItem() { Name = "SettingsDate", TypeIdentifier = WorkItem.TypeID.RsDateTime, Content = new DateTimeTypeContent() { Value = null } } },
                }
            };

            var navigationParameter = new Dictionary<string, object>();

            if (!ItemDescriptions.ContainsKey(s))
            {
                ItemDescriptions.Add(s, suitCaseProperties);

                navigationParameter = new Dictionary<string, object>() {
                    { s, suitCaseProperties }
                };
            }
            else
            {
                if (ItemDescriptions.ContainsKey(s))
                {
                    var properties = ItemDescriptions[s] as SuitCaseProperties;
                    
                    navigationParameter = new Dictionary<string, object>()
                    {
                        { s, properties }
                    };
                }
                // Passing Parameter from a Page to a DetailPage can be handled through strings and dictionaries
                // Here the class instance suitCaseProperties (containing the inner dictionary) is wrapped in another dictionary (here: 'navigationParameter')

                // here we send a string ('s') and a dictionary
                await Shell.Current.GoToAsync($"{nameof(SettingsPage)}?Parameter={s}", navigationParameter);

                // alternatively you can send merely a string
                // await Shell.Current.GoToAsync($"{nameof(ProfileDetailPage)}?Parameter={s}");

                // alternatively you can send merely a dictionary
                // await Shell.Current.GoToAsync($"{nameof(SettingsPage)}", navigationParameter);

            }
        }
    }
}


