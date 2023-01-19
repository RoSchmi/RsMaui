using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiTypeDependentView.Models;
using MauiTypeDependentView.ViewModels;
using System.Runtime.InteropServices;

namespace DataTemplates
{
    
    public class TypeDataTemplateSelector : Microsoft.Maui.Controls.DataTemplateSelector
    {
        public DataTemplate StringTypeTemplate { get; set; }

        public DataTemplate BoolTypeTemplate { get; set; }

        public DataTemplate DateTypeTemplate { get; set; }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var returnTemplate = StringTypeTemplate;

            switch (((SettingItem)item).TypeIdentifier)
            
            {
                case SettingItem.TypeID.RsBoolean: 
                {
                        returnTemplate = BoolTypeTemplate;
                    }
                    break;

                case SettingItem.TypeID.RsDateTime:
                    {
                        returnTemplate = DateTypeTemplate;
                    }
                    break;
                case SettingItem.TypeID.RsString:
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
