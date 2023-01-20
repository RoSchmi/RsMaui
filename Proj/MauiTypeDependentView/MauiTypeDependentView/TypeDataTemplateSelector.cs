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

        public DataTemplate StringTypeReadOnlyTemplate { get; set; }

        public DataTemplate BoolTypeTemplate { get; set; }

        public DataTemplate DateTypeTemplate { get; set; }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var returnTemplate = StringTypeTemplate;

            switch (((WorkItem)item).TypeIdentifier) 
            {
                case WorkItem.TypeID.RsBoolean: 
                {
                        returnTemplate = BoolTypeTemplate;
                    }
                    break;

                case WorkItem.TypeID.RsDateTime:
                    {
                        returnTemplate = DateTypeTemplate;
                    }
                    break;

                case WorkItem.TypeID.RsStringRo:
                    {
                        returnTemplate = StringTypeReadOnlyTemplate;
                    }
                    break;

                case WorkItem.TypeID.RsString:
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
        }    
    }
    
}
