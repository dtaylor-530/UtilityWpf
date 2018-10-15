﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UtilityWpf.View
{
    public class PropertyDataTemplateSelector : System.Windows.Controls.DataTemplateSelector
    {
        public DataTemplate DefaultDataTemplate { get; set; }
        public DataTemplate EnumerableDataTemplate { get; set; }
        public DataTemplate ContentPresenterTemplate { get; set; }
        public DataTemplate DictionaryDataTemplate { get; set; }
        public DataTemplate IConvertibleTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if(item==null)
                return DefaultDataTemplate;

            var type = item.GetType();

            DataTemplate myDataTemplate = ((System.Windows.Controls.ContentPresenter)container).ContentTemplate;

            try
            {
                var dataTemplateKey = new DataTemplateKey(type);
                var dataTemplate = (container as FrameworkElement).FindResource(dataTemplateKey);
                if (dataTemplate != null)
                    return ContentPresenterTemplate;
            }
            catch
            {

            }


            var interfaces = type.GetInterfaces();
            if ((interfaces.Contains(typeof(IConvertible))))
                return IConvertibleTemplate;
            else if (interfaces.SingleOrDefault(_ => _.Name == "IDictionary`2") != null || interfaces.Contains(typeof(IDictionary)))
                return DictionaryDataTemplate;
            else if (interfaces.Contains(typeof(IEnumerable)))
                return EnumerableDataTemplate;
            else
                return DefaultDataTemplate;

        }
    }

}