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

namespace restaurant_manager
{
    /// <summary>
    /// Логика взаимодействия для AdminWnd.xaml
    /// </summary>
    public partial class AdminWnd : Window , ICreateUserList
    {
      
        public AdminWnd()
        {
           
        InitializeComponent();
        }

        private void BtnOpenMenu_Click(object sender, RoutedEventArgs e)
        {

            BtnOpenMenu.Visibility = Visibility.Collapsed;
            BtnCloseMenu.Visibility = Visibility.Visible;
            FullName.Visibility = Visibility.Visible;
            PhoneNumber.Visibility = Visibility.Visible;
        }

        private void BtnCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            BtnOpenMenu.Visibility = Visibility.Visible;
            BtnCloseMenu.Visibility = Visibility.Collapsed;
            FullName.Visibility = Visibility.Collapsed;
            PhoneNumber.Visibility = Visibility.Collapsed;
        }

       


       public void AddUser(int id,string name,string lastname, string login,string password)
        {
            Ctn_main.Children.Add(new User_card(id, name, lastname, login, password));
         
        }
        public void Clear()
        {
            Ctn_main.Children.Clear();
          
        }

       public void InintNav()
        {
            Nav.Children.Clear();
            Clear();
            TextBlock tb = new TextBlock();
            tb.Text = "Персонал";
            tb.HorizontalAlignment = HorizontalAlignment.Center;
            tb.VerticalAlignment = VerticalAlignment.Center;
            tb.Foreground= new SolidColorBrush(Colors.White);
            Nav.Children.Add(tb);
            Nav.Children.Add(new PopUpBtn(this));
            
        }

       
    }
}
