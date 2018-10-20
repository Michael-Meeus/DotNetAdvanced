using System;
using System.Windows;

namespace Exercise3
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Rectangle1.Width <= Canvas1.Width -10)
            {
                Rectangle1.Width += 10;
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Rectangle1.Width >= 10)
            {
                Rectangle1.Width -= 10;
            }
        }
    }
}
