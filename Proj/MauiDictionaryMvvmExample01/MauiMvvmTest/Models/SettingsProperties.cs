using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDictionaryMvvmExample01.Models;
public class SettingsProperties

{
    // must expose a parameter-less constructor
    public SettingsProperties() { }

    // The settings properties get wrapped in this Dictionary
    public Dictionary<string, string> Properties { get; set; }
}
