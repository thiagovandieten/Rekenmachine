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
        Parser parser = new Parser();

        public MainWindow()
        {
            InitializeComponent();
            bindNumberButtons();
            bindOperatorButtons();
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
            NumDecimal.Click += NumButton_Click;
        }

        private void bindOperatorButtons() {
            ButtonDivision.Click += OperatorButton_Click;
            ButtonMultiply.Click += OperatorButton_Click;
            ButtonSubtraction.Click += OperatorButton_Click;
            ButtonAddition.Click += OperatorButton_Click;
        }

        private void NumButton_Click(object sender, RoutedEventArgs e)
        {
            Button buttonPressed = (Button)sender;
            Display.Text += buttonPressed.Content;
        }
        private void OperatorButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //TODO: Momenteel throwt tie als er al een operator in de display zit. 
                if (!char.IsDigit(Display.Text.Last()) || String.IsNullOrWhiteSpace(Display.Text))
                {
                    throw new InvalidOperationException("Kan geen uitvoering doen zonder eerst een getal te geven");
                }
            }
            catch (InvalidOperationException error)
            {
                MessageBox.Show(error.ToString());
                return;
            }
            Button buttonPressed = (Button)sender;
            Display.Text += buttonPressed.Content;
        }

        private void ButtonEquals_Click(object sender, RoutedEventArgs e)
        {
            Answer.Text = parser.calculate(Display.Text).ToString();
            Display.Text = "";
        }

        private void ButtonBackspace_Click(object sender, RoutedEventArgs e)
        {
            if(Display.Text.Count() > 0)
            {
                Display.Text = Display.Text.Remove(Display.Text.Count() - 1);
            }
        }

        private void ButtonPrecentage_Click(object sender, RoutedEventArgs e)
        {
            Display.Text = parser.convertPrecentageToDecimal(Display.Text);
        }

        private void ButtonEUtoUS_Click(object sender, RoutedEventArgs e)
        {
            Answer.Text = parser.convertEUtoUS(Answer.Text);
        }

        private void ButtonUStoEU_Click(object sender, RoutedEventArgs e)
        {
            Answer.Text = parser.convertUStoEU(Answer.Text);
        }
    }
}
