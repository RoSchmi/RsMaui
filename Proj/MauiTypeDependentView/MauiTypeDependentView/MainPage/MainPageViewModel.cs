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
using Microsoft.Extensions.Primitives;
using MauiTypeDependentView.ViewModels;

namespace MauiTypeDependentView
{
    public partial class MainPageViewModel : ObservableObject, IQueryAttributable
    {
        public MainPageViewModel()
        {
            Items = new ObservableCollection<string>();
        }

        //public Dictionary<string, object> ItemDescriptions = new Dictionary<string, object>();
        public Dictionary<string, SuitCaseProperties> ItemDescriptions = new Dictionary<string, SuitCaseProperties>();

        [ObservableProperty] // source generator
        private ObservableCollection<string> items;

        [ObservableProperty]
        private ObservableCollection<SettingItem> itemCollection;


        [ObservableProperty] // source generator
        private string itemText;

        [ObservableProperty] // source generator
        private string counterCopy = "0";

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            // This comes back, evtl. changed from the Detail Page, use it to do what you want

            SuitCaseProperties receivedTransportProperties = query[query.Keys.First()] as SuitCaseProperties;

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


            // Get name out of the collection of SettingItems (it can be that the name was changed)
            string newName = ItemCollection.FirstOrDefault(x => x.Name == "SettingsName").StringValue as string;

            if (!ItemDescriptions.ContainsKey(newName))
            {
                var oldName = query.Keys.First();
                ItemDescriptions.Add(newName, receivedTransportProperties);
                ItemDescriptions.Remove(oldName);

                if (Items.Contains(oldName))
                {
                    // Don't know why I had to create a new Items observable collection
                    // The outcommented code below did not work, threw an exception

                    List<string> itemList = Items.ToList<string>();
                    itemList[itemList.IndexOf(oldName)] = newName;
                    //itemList.Remove(oldName);
                    Items = new ObservableCollection<string>();
                    foreach(var item in itemList)
                    {
                        Items.Add(item);
                    }
                    

                    //  Items[Items.IndexOf(oldName)] = newName;
                }            
            }
            else
            {
                ItemDescriptions[newName] = receivedTransportProperties;
            }
        }



        [RelayCommand]
        private void Add()
        {
            if (string.IsNullOrEmpty(itemText))
                return;

            // Adding twice the same name is not allowed
            if (!Items.Contains(itemText))
            {
                Items.Add(itemText);
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
            if (Items.Contains(s))
                Items.Remove(s);
        }

        [RelayCommand]
        private async void Tap(string s)
        {
            //string targetPage = nameof(SettingsPage);
            //string injectedParameter = s;

            // As the ID we take a GUID
            // As the SettingsName we take 's' which comes from the bar which was tapped

            var ID = Guid.NewGuid().ToString();


            // Create an instance of Class SuitCaseProperties
            // SuitCaseProperties contains a Dictionary (PropertiesDictionary) with its content (Names and Values)
            /*
            var suitCaseProperties = new SuitCaseProperties()
            {
                // properties get wrapped in this inner Dictionary

                PropertiesDictionary = new Dictionary<string, string>() {
                    { "SettingsID", ID },
                    { "SettingsName", s },
                    { "SettingsProperty_1", "" },
                    { "SettingsProperty_2", "" }
                }
            };
            */

            // Create an instance of Class SuitCaseProperties
            // SuitCaseProperties contains a Dictionary (PropertiesDictionary) with its content (Names and Values)


            /*
            var suitCaseProperties = new SuitCaseProperties()
            {
                // properties get wrapped in this inner Dictionary

                //PropertiesDictionary = new Dictionary<string, object>() {
                    PropertiesDictionary = new Dictionary<string, SettingItem>() {
                    { "SettingsID", new SettingItem() { Name = "SettingsID", TypeIdentifier = SettingItem.TypeID.RsString, Content = new StringTypeContent() { Value = ID } } },
                    { "SettingsName", new SettingItem() { Name = "SettingsName", TypeIdentifier = SettingItem.TypeID.RsString, Content = new StringTypeContent() { Value = s } } },
                    { "SettingsState", new SettingItem() { Name = "SettingsState", TypeIdentifier = SettingItem.TypeID.RsBoolean, Content = new BoolTypeContent() { Value = null } } },
                    { "SettingsDate", new SettingItem() { Name = "SettingsDate", TypeIdentifier = SettingItem.TypeID.RsDateTime, Content = new DateTimeTypeContent() { Value = null } } },         
                }
            };
            */
            var suitCaseProperties = new SuitCaseProperties()
            {
                // properties get wrapped in this inner Dictionary

                //PropertiesDictionary = new Dictionary<string, object>() {
                PropertiesDictionary = new Dictionary<string, TransportItem>() {
                    { "SettingsID", new TransportItem() { Name = "SettingsID", TypeIdentifier = SettingItem.TypeID.RsString, Content = new StringTypeContent() { Value = ID } } },
                    { "SettingsName", new TransportItem() { Name = "SettingsName", TypeIdentifier = SettingItem.TypeID.RsString, Content = new StringTypeContent() { Value = s } } },
                    { "SettingsState", new TransportItem() { Name = "SettingsState", TypeIdentifier = SettingItem.TypeID.RsBoolean, Content = new BoolTypeContent() { Value = null } } },
                    { "SettingsDate", new TransportItem() { Name = "SettingsDate", TypeIdentifier = SettingItem.TypeID.RsDateTime, Content = new DateTimeTypeContent() { Value = null } } },
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
                    var theValue = ItemDescriptions[s] as SuitCaseProperties;
                    // SuitCaseProperties theValue = new SuitCaseProperties() { PropertiesDictionary = ItemDescriptions[s]. as }

                    navigationParameter = new Dictionary<string, object>()
                    {
                        { s, theValue }
                    };
                }
                // Passing Parameter from a Page to a DetailPage can be handled through strings and dictionaries
                // Here the class instance suitCaseProperties (containing the inner dictionary) is wrapped in another dictionary (here: 'navigationParameter')

                /*
                var navigationParameter = new Dictionary<string, object>
                {            
                    // Only one member in the dictionary
                    { "FirstAndOnlyRow", suitCaseProperties}
                };
                */

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

