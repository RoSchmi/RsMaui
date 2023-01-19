using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MauiTypeDependentView.Models.SettingItem;

namespace Common.Models
{
    public class TransportItem
    {
        public string Name { get; set; }
        public TypeID TypeIdentifier { get; set; }
        public object Content { get; set; }
    }
}
