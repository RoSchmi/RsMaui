using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTypeDependentView.ViewModels
{
    public partial class DateTimeTypeContent : ObservableObject
    {
        [ObservableProperty]
        private DateTime value; // { get; set; }
    }
}
