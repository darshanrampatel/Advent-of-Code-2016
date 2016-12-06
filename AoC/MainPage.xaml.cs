﻿using AoC.Days;
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
            Day1Part1.Text = new Day1().Part1();
            Day1Part2.Text = new Day1().Part2();

            Day2Part1.Text = new Day2().Part1();
            Day2Part2.Text = new Day2().Part2();

            Day3Part1.Text = new Day3().Part1();
            Day3Part2.Text = new Day3().Part2();

            Day4Part1.Text = new Day4().Part1();
            Day4Part2.Text = new Day4().Part2();

            Day5Part1.Text = new Day5().Part1();
            Day5Part2.Text = new Day5().Part2();
        }
    }
}
