using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using static System.Net.Mime.MediaTypeNames;
using MauiMvvmTest.Pages;
//using Windows.UI.Popups;

namespace MauiMvvmTest
{
    public partial class MainPageViewModel :  ObservableObject
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
        [NotifyPropertyChangedFor(nameof(CounterWithMessage))]
        private string counterCopy = "0";

        public string CounterWithMessage
        {
            get => $"This is the State of the Counter: {counterCopy}";
            set => SetProperty(ref counterCopy, value);
        }


        [RelayCommand]
        private void Add()
        {
            if (string.IsNullOrEmpty(itemText))
                return;

            //if (connectivity.NetworkAccess != NetworkAccess.Internet)
            //{
            //	await Shell.Current.DisplayAlert("Network", "No Internet", "Ok");
            //}

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

            int dummy3 = 1;  // Do nothing
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
            string targetPage = nameof(Page1);
            string injectedParameter = s;
           
           await Shell.Current.GoToAsync($"{nameof(Page1)}?Parameter={s}");
        }
        
    }
}

