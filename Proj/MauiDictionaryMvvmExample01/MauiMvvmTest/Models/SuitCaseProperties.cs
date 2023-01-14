using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDictionaryMvvmExample01.Models;
public class SuitCaseProperties

{
    // must expose a parameter-less constructor
    public SuitCaseProperties() { }

    // The properties get wrapped in this 'SuitCase' Dictionary
    public Dictionary<string, string> PropertiesDictionary { get; set; }

}
