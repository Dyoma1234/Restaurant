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
using restaurant_manager.Interfaces;


namespace restaurant_manager
{
    /// <summary>
    /// Логика взаимодействия для AddUserWnd.xaml
    /// </summary>
    public partial class AddUserWnd : Window ,Imsg
    {
     
        public AddUserWnd()
        {
            InitializeComponent();
            DataContext = new AddUserViewModel();
        }
        public AddUserWnd(ICreateUserList cul)
        {
            InitializeComponent();
            DataContext = new AddUserViewModel(cul);
          
        }
        public void ShowInfo(string msg,string cap)
        {
            System.Windows.MessageBox.Show(this, msg, cap, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ShowWarning(string msg,string cap)
        {
            System.Windows.MessageBox.Show(this, msg, cap, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
