using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTypeDependentView.ViewModels
{
    public partial class BoolTypeContent : ObservableObject
    {
        [ObservableProperty]
        private bool value; //{ get; set; }
        
    }
}
