using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTypeDependentView.Models
{
    public class SettingItem
    {
        public SettingItem() { }

        public  enum TypeID
        {
            RsString,
            RsStringRo,
            RsBoolean,
            RsDateTime,
            RsTimeSpan,
            RsGuid,
            RsDouble,
            RsFloat,
            RsInt,
            RsLong,
            RsShort,
        };

        public string Name { get; set; }
        public TypeID TypeIdentifier { get; set; }
        public string StringValue { get; set; }
        public DateTime? DateValue { get; set; }
        public bool? BoolValue { get; set; }

        // Add other types if needed
        // e.g. public TimeSpan TimeSpanValue { get; set; }
        //public object Content { get; set; }
    }
}
