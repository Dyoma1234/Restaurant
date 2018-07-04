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
    public partial class AddUserWnd : Window , IPasswordChanget
    {
       private Brush borderbrush;
        private Brush selectionbrush;
        public AddUserWnd()
        {
            InitializeComponent();
            DataContext = new AddUserViewModel();
            borderbrush = Fpwb.Foreground;
            selectionbrush = Fpwb.SelectionBrush;
        }
        public AddUserWnd(ICreateUserList cul)
        {
            InitializeComponent();
            DataContext = new AddUserViewModel(cul);
            borderbrush = Fpwb.Foreground;
            selectionbrush = Fpwb.BorderBrush;
        }

        public string GetPassword_F()
        {
            return Fpwb.Password;
        }

        public string GetPassword_S()
        {
            return Spwb.Password;
        }

        public void ShowInfo(string msg,string cap)
        {
            System.Windows.MessageBox.Show(this, msg, cap, System.Windows.MessageBoxButton.OK,System.Windows.MessageBoxImage.Information);
        }

        public void ShowWarning(string msg,string cap)
        {
            System.Windows.MessageBox.Show(this, msg, cap, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

       

        private void Fpwb_PasswordChanged(object sender, RoutedEventArgs e)
        {
            InitFirst();

        }

        private void Spwb_PasswordChanged(object sender, RoutedEventArgs e)
        {
            InitSec();
        }
        private void InitFirst()
        {
            if (string.IsNullOrWhiteSpace(Fpwb.Password))
            {
                Fpwb.BorderBrush = System.Windows.Media.Brushes.Red;
                Fpwb.CaretBrush = System.Windows.Media.Brushes.Red;

                FpwbMsg.Text = "Это поле должно быть заполнено.";
                FpwbMsg.Visibility = Visibility.Visible;
            }
            else
            {
                Fpwb.BorderBrush = borderbrush;
                Fpwb.SelectionBrush = selectionbrush;
                FpwbMsg.Visibility = Visibility.Hidden;

            }
        }
        private void InitSec()
        {
            if (string.IsNullOrWhiteSpace(Spwb.Password))
            {
                Spwb.BorderBrush = System.Windows.Media.Brushes.Red;
                Spwb.CaretBrush = System.Windows.Media.Brushes.Red;

                SpwbMsg.Text = "Это поле должно быть заполнено.";
                SpwbMsg.Visibility = Visibility.Visible;
            }
            else
            {
                Spwb.BorderBrush = borderbrush;
                Spwb.SelectionBrush = selectionbrush;
                SpwbMsg.Visibility = Visibility.Hidden;

            }
        }

        public void ClearPassword()
        {
            Fpwb.Password = string.Empty;
            Spwb.Password = string.Empty;
        }
    }
}
