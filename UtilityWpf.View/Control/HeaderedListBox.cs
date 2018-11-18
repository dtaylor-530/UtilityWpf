﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace UtilityWpf.View
{
    [TemplatePart(Name = PartListBox, Type = typeof(ListBox))]
    public class HeaderedListBox:HeaderedContentControl
    {

        private const string PartListBox = "PART_ListBox";
        private ListBox ListBoxPart;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.ListBoxPart = GetTemplateChild(PartListBox) as ListBox;

            this.ListBoxPart.SelectionChanged += ListBoxPart_SelectionChanged;
           // FindName(PartListBox);


        }

        private void ListBoxPart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Header = e.AddedItems.Cast<object>().First();
        }
    }
}