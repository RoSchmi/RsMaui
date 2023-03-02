using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiMixedShellNavigation.Models
{
    public class SettingItems
    {
        public SettingItems() { }
        public string Sender { get; set; }
        public string Name { get; set; }
        public bool State { get; set; }
        public DateTime CreationDate { get; set; }
        public string Instruction { get; set; }
    }
}
