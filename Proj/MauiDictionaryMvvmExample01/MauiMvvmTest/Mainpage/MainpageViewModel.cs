using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiDictionaryMvvmExample01.Pages;
using MauiDictionaryMvvmExample01.Models;
using System.Collections;
using Microsoft.Maui.Controls;
using Microsoft.UI.Xaml.Controls;

namespace MauiDictionaryMvvmExample01
{
    public partial class MainPageViewModel :  ObservableObject, IQueryAttributable
    {
        public MainPageViewModel() 
        {
            Items = new ObservableCollection<string>();
        }
        
        [ObservableProperty] // source generator
        private ObservableCollection<string> items;
        

        [ObservableProperty] // source generator
        private string itemText;

        [ObservableProperty] // source generator
        private string counterCopy = "0";

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            // This comes back, evtl. changed from the Detail Page, use it to do what you want
            
            SuitCaseProperties changedSettingProperties = query[query.Keys.First()] as SuitCaseProperties;

            int breakPoint67 = 1;
        }



        [RelayCommand]
        private void Add()
        {
            if (string.IsNullOrEmpty(itemText))
                return;          

            Items.Add(itemText);
            ItemText = string.Empty;
        }

        [RelayCommand] // source generator
        private void IncrementContent()
        {
            int counterCopyInt = int.Parse(counterCopy);
            counterCopyInt++;
            CounterCopy= counterCopyInt.ToString();
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

            // Passing Parameter from a Page to a DetailPage can be handled through strings and dictionaries
            // Here the class instance suitCaseProperties (containing the inner dictionary) is wrapped in another dictionary (here: 'navigationParameter')
            var navigationParameter = new Dictionary<string, object>
            {            
                // Only one member in the dictionary
                { "FirstAndOnlyRow", suitCaseProperties}
            };
            
            // here we send a string ('s') and a dictionary
            await Shell.Current.GoToAsync($"{nameof(SettingsPage)}?Parameter={s}", navigationParameter);

            // alternatively you can send merely a string
            // await Shell.Current.GoToAsync($"{nameof(ProfileDetailPage)}?Parameter={s}");

            // alternatively you can send merely a dictionary
            // await Shell.Current.GoToAsync($"{nameof(SettingsPage)}", navigationParameter);
        
        }
        
    }
}

