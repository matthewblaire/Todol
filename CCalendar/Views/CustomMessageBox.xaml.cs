using System;
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
using System.Windows.Shapes;

namespace Todol
{
    /// <summary>
    /// Interaction logic for CustomMessageBox.xaml
    /// 
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        /// <summary>
        /// True when user selects affirmative button, false when user selects negative button
        /// </summary>
        public bool Result { get; set; }

        public CustomMessageBox()
        {
            InitializeComponent();
        }

        public CustomMessageBox(string messageText)
            :this()
        {
            message.Text = messageText;
            message.VerticalAlignment = VerticalAlignment.Center;
            message.HorizontalAlignment = HorizontalAlignment.Center;
            message.Margin = new Thickness(0,0,0,0);
            affirmativeButton.Visibility = Visibility.Hidden;
            negativeButton.Visibility = Visibility.Hidden;
        }
        public CustomMessageBox(string messageText, string affirmativeText, string negativeText)
            : this()
        {
           
            message.Text = messageText;
            affirmativeButton.Content = affirmativeText;
            negativeButton.Content = negativeText;
        }

        public CustomMessageBox(string messageText, string affirmativeText)
            : this()
        {
            InitializeComponent();
            negativeButton.Visibility = Visibility.Hidden;

            message.Text = messageText;
            affirmativeButton.Content = affirmativeText;
            affirmativeButton.HorizontalAlignment = HorizontalAlignment.Center;
           
        }

        private void AffirmativeButton_Click(object sender, RoutedEventArgs e)
        {
            Result = true;
            Close();
        }

        private void NegativeButton_Click(object sender, RoutedEventArgs e)
        {
            Result = false;
            Close();
        }

        

    }
}
