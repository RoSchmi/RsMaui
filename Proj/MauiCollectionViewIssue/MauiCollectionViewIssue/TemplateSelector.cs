
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiCollectionViewIssue.Models;


namespace DataTemplates;
public class TemplateSelector : Microsoft.Maui.Controls.DataTemplateSelector
{
    public DataTemplate GridTemplate { get; set; }

    public DataTemplate SwipeTemplate { get; set; }

    

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        var returnTemplate = GridTemplate;

        if (((Person)item).Location == "Germany")
        {
            return  GridTemplate;
        }
        else
        {
            return SwipeTemplate;
        }
    }
}

