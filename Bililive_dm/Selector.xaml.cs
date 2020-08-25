﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bililive_dm
{
    /// <summary>
    /// Selector.xaml 的交互逻辑
    /// </summary>
    public partial class Selector: Window
    {
        public List<KeyValuePair<string, ResourceDictionary>> Themes { get; } = new List<KeyValuePair<string, ResourceDictionary>>();

        void AddTheme(string theme, string variant = "NormalColor")
        {
            variant = theme + "." + variant;

            Themes.Add(new KeyValuePair<string, ResourceDictionary>(variant, new ResourceDictionary
            {
                Source =
                new Uri($"/PresentationFramework.{theme},Version=0.0.0.0,PublicKeyToken=31bf3856ad364e35;component/Themes/{variant}.xaml",
                UriKind.Relative)
            }));
        }

        ResourceDictionary appRes { get; } = Application.Current.Resources;
        Collection<ResourceDictionary> merged { get; }
        public Selector()
        {
            merged = appRes.MergedDictionaries;

            AddTheme("Aero");
            AddTheme("Royale");
            AddTheme("Luna");
            AddTheme("Luna", "Homestead");
            AddTheme("Luna", "Metallic");

            Themes.Add(new KeyValuePair<string, ResourceDictionary>("Classic", (ResourceDictionary)appRes["Classic"]));

            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        public void Select()
        {
            var last = merged[0];

            if (!ShowDialog().GetValueOrDefault())
            {
                merged[0] = last;
            }
        }

        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            merged[0] = (ResourceDictionary)list.SelectedValue ?? new ResourceDictionary();
        }

        private void list_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Button_Click(sender, e);
        }
    }
}
