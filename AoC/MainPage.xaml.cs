using AoC.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace AdventOfCode2016
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Day1Part1_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Day1Part1.Text = new Day1().Part1();
        }

        private void Day1Part2_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Day1Part2.Text = new Day1().Part2();
        }

        private void Day2Part1_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Day2Part1.Text = new Day2().Part1();
        }

        private void Day2Part2_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Day2Part2.Text = new Day2().Part2();
        }

        private void Day3Part1_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Day3Part1.Text = new Day3().Part1();
        }
        private void Day3Part2_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Day3Part2.Text = new Day3().Part2();
        }

        private void Day4Part1_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Day4Part1.Text = new Day4().Part1();
        }

        private void Day4Part2_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Day4Part2.Text = new Day4().Part2();
        }

        private void Day5Part1_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Day5Part1.Text = new Day5().Part1();
        }

        private void Day5Part2_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Day5Part2.Text = new Day5().Part2();
        }

        private void Day6Part1_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Day6Part1.Text = new Day6().Part1();
        }

        private void Day6Part2_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Day6Part2.Text = new Day6().Part2();
        }

        private void Day7Part1_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Day7Part1.Text = new Day7().Part1();
        }

        private void Day7Part2_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Day7Part2.Text = new Day7().Part2();
        }
    }
}
