using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiTypeDependentView.Models;
using MauiTypeDependentView.ViewModels;

namespace DataTemplates
{
    
    public class TypeDataTemplateSelector : Microsoft.Maui.Controls.DataTemplateSelector
    {
        public DataTemplate StringTypeTemplate { get; set; }

        public DataTemplate BoolTypeTemplate { get; set; }

        public DataTemplate DateTypeTemplate { get; set; }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
           // return ValidTemplate;

          // var returnValue = ((SettingItem)item).ValueType == "RsString" ? StringTypeTemplate : BoolTypeTemplate;

          //  string dataType = ((SettingItem)item).ValueType;

            var returnTemplate = StringTypeTemplate;

            switch (((SettingItem)item).ValueType)
            {
                case "RsBoolean":
                    {
                        returnTemplate = BoolTypeTemplate;
                    }
                    break;

                case "RsDateTime":
                    {
                        returnTemplate = DateTypeTemplate;
                    }
                    break;
                case "RsString":
                    {
                        returnTemplate = StringTypeTemplate;
                    }
                    break;
                default:
                    {
                        returnTemplate = StringTypeTemplate;
                    }
                    break;
            }

            return returnTemplate;
            /*
            ItemCollection.
            return ((Person)item).DateOfBirth.Year >= 1980 ? ValidTemplate : InvalidTemplate;
            */
        }
        
    }
    
}
