﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UtilityWpf.DemoApp
{
    /// <summary>
    /// Interaction logic for DictionaryUserControl.xaml
    /// </summary>
    public partial class DictionaryUserControl : UserControl
    {
        public DictionaryUserControl()
        {
            InitializeComponent();
        }
    }

    public class MyDictionary : Dictionary<string, int> { }
}
