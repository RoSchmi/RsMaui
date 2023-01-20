using Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace Common.Models;
public class SuitCaseProperties
{
    // must expose a parameter-less constructor
    public SuitCaseProperties() { }

    // The properties get wrapped in this 'SuitCase' Dictionary
    
    public Dictionary<string, TransportItem> PropertiesDictionary { get; set; }
}
