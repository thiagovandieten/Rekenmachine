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

namespace Rekenmachine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            bindNumberButtons();
        }

        private void bindNumberButtons()
        {
            Num0.Click += NumButton_Click;
            Num1.Click += NumButton_Click;
            Num2.Click += NumButton_Click;
            Num3.Click += NumButton_Click;
            Num4.Click += NumButton_Click;
            Num5.Click += NumButton_Click;
            Num6.Click += NumButton_Click;
            Num7.Click += NumButton_Click;
            Num8.Click += NumButton_Click;
            Num9.Click += NumButton_Click;
        }

        private void NumButton_Click(object sender, RoutedEventArgs e)
        {
            Button buttonPressed = (Button)sender;
            Display.Text += buttonPressed.Content;
        }
    }
}
