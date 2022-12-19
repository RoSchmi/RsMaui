using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using static System.Net.Mime.MediaTypeNames;

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
        private string counterCopy = "0";


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
        private void Remove(string s)
        {
            if (Items.Contains(s))
                Items.Remove(s);
        }
        /*
        [RelayCommand]
        private async void Tap(string s)
        {
            await Shell.Current.GoToAsync($"{nameof(Page 1)}?Text={s}");
        }
        */
    }
}

