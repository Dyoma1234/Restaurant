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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace restaurant_manager.View
{

    /// <summary>
    /// Логика взаимодействия для Booking.xaml
    /// </summary>
    public partial class Booking : UserControl
    {
        private Button SelectedButton;
        public Booking()
        {
            InitializeComponent();
            SelectedButton = new Button();
            Picker.CultureInfo = System.Globalization.CultureInfo.InvariantCulture;
            Picker.Value = DateTime.Now;
        }
        public void Update(object obj,EventArgs e)
        {
            Table.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            SelectedButton.BorderBrush = System.Windows.Media.Brushes.Black;
            SelectedButton.Foreground = System.Windows.Media.Brushes.Black;
            SelectedButton.Background = System.Windows.Media.Brushes.White;
            SelectedButton.BorderThickness = new Thickness(1);

            (sender as Button).BorderBrush= System.Windows.Media.Brushes.Green;
            (sender as Button).Background = System.Windows.Media.Brushes.Green;
            (sender as Button).Foreground = System.Windows.Media.Brushes.White;
            (sender as Button).BorderThickness = new Thickness(3);

            SelectedButton = (sender as Button);
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SelectedButton.BorderBrush = System.Windows.Media.Brushes.Black;
            SelectedButton.Background = System.Windows.Media.Brushes.White;
            SelectedButton.BorderThickness = new Thickness(1);
        }
    }
}
