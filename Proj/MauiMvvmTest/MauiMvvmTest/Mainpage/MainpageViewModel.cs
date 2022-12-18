using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MauiMvvmTest
{
    public partial class MainPageViewModel :  ObservableObject
    {
        public MainPageViewModel() 
        {}

        [ObservableProperty]
        private string counterCopy = "0";

        [RelayCommand]
        public void IncrementContent()
        {
            int counterCopyInt = int.Parse(counterCopy);
            counterCopyInt++;
            CounterCopy= counterCopyInt.ToString();
        }  
    }
}

