using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTypeDependentView.ViewModels
{
    public class SettingItem
    {
        public SettingItem() { }
        public string Name { get; set; }
        public string ValueType { get; set; }
        public string StringValue { get; set; }
        public DateTime DateValue { get; set; }
        public bool BoolValue { get; set; }
        public object Content { get; set; }
    }
}
