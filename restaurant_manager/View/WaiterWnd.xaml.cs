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
using restaurant_manager.ViewModel;
namespace restaurant_manager
{
    /// <summary>
    /// Логика взаимодействия для WaiterWnd.xaml
    /// </summary>
    public partial class WaiterWnd : Window
    {
        public WaiterWnd()
        {
            InitializeComponent();
            CheckViewModel vm = new CheckViewModel();
            NewChekControl newChek = new NewChekControl();
            vm.Update += newChek.Update;
            newChek.DataContext = vm;
            content.Children.Add(newChek);
        }
        private void bg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BtnOpenMenu.Visibility = Visibility.Visible;
            BtnCloseMenu.Visibility = Visibility.Collapsed;
            FullName.Visibility = Visibility.Collapsed;
            PhoneNumber.Visibility = Visibility.Collapsed;
        }
        private void BtnOpenMenu_Click(object sender, RoutedEventArgs e)
        {
           // bg.Visibility = Visibility.Visible;
            BtnOpenMenu.Visibility = Visibility.Collapsed;
            BtnCloseMenu.Visibility = Visibility.Visible;
            FullName.Visibility = Visibility.Visible;
            PhoneNumber.Visibility = Visibility.Visible;
        }

        private void BtnCloseMenu_Click(object sender, RoutedEventArgs e)
        {
          //  bg.Visibility = Visibility.Collapsed;
            BtnOpenMenu.Visibility = Visibility.Visible;
            BtnCloseMenu.Visibility = Visibility.Collapsed;
            FullName.Visibility = Visibility.Collapsed;
            PhoneNumber.Visibility = Visibility.Collapsed;
        }
       

        

    }
}
