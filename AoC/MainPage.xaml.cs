using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Reflection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Toolkit.Uwp.UI;
using Microsoft.Toolkit.Uwp;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.ViewManagement;

namespace AdventOfCode2016
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView"))
            {
                var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                if (titleBar != null)
                {
                    titleBar.BackgroundColor = Microsoft.Toolkit.Uwp.ColorHelper.ToColor("#0f0f23");
                    titleBar.ButtonBackgroundColor = Microsoft.Toolkit.Uwp.ColorHelper.ToColor("#0f0f23");
                }
            }

            var ics = new Style(typeof(ListViewItem));
            var s = new Setter(HorizontalContentAlignmentProperty, HorizontalAlignment.Stretch);
            ics.Setters.Add(s);
            for (int i = 1; i <= 25; i++)
            {
                ListView lv = new ListView()
                {
                    ItemContainerStyle = ics
                };
                lv.SelectionChanged += ListView_SelectionChanged;
                var lvhi = new ListViewHeaderItem()
                {
                    Name = "Day" + i,
                    Content = "Day " + i
                };
                lvhi.Tapped += ListViewHeaderItem_Tapped;
                lv.Items.Add(lvhi);
                for (int part = 1; part <= 2; part++)
                {
                    var lvi = new ListViewItem()
                    {
                        Name = "Day" + i + "Part" + part
                    };
                    lvi.Tapped += ListViewItem_Tapped;
                    lvi.Content = new HeaderedTextBlock()
                    {
                        Name = "AoC.Days.Day" + i + "|Part" + part,
                        Header = "Part " + part,
                        Orientation = Orientation.Vertical,
                        Text = "Tap to run"
                    };
                    lv.Items.Add(lvi);
                }
                AoC_AdaptiveGridViewGridView.Items.Add(lv);
            }
        }

        private void ListViewHeaderItem_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var lvhi = (sender as ListViewHeaderItem);
            var day = lvhi?.Name;
            var parent = lvhi.Parent as ListView;
            if (parent != null)
            {
                for (int part = 1; part <= 2; part++)
                {
                    var item = parent.FindDescendantByName(day + "Part" + part);
                    if (item != null)
                    {
                        ListViewItem_Tapped(item, null);
                    }
                }
            }
        }

        private void ListViewItem_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var htb = (sender as ListViewItem)?.Content as HeaderedTextBlock;
            if (htb != null)
            {
                var className = htb.Name;
                if (className != null)
                {
                    var day = className.Split('|')?[0];
                    var part = className.Split('|')?[1];
                    if (day != null && part != null)
                    {
                        Type type = Type.GetType(day);
                        if (type != null)
                        {
                            MethodInfo methodInfo = type.GetMethod(part);
                            if (methodInfo != null)
                            {
                                var instance = Activator.CreateInstance(type);
                                if (instance != null)
                                {
                                    htb.Text = methodInfo.Invoke(instance, null)?.ToString();
                                    return;
                                }
                            }

                        }
                    }
                }
                htb.Text = "Failed to run";
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (sender as ListView).SelectedIndex = -1;
        }
    }
}
