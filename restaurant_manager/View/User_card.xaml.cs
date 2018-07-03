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
using restaurant_manager.ViewModel;
namespace restaurant_manager
{
    /// <summary>
    /// Логика взаимодействия для User_card.xaml
    /// </summary>
    public partial class User_card : UserControl, IPasswordChanget
    {
        public event EventHandler TryDel;
        public int Id { set; get; }
        public User_card()
        {
            InitializeComponent();

        }

        public User_card(int id)
        {
            InitializeComponent();
            EditPersonalViewModel vm = new EditPersonalViewModel(id);
            vm.DelVis += UseTryDal;
            DataContext = vm;
            vm.DelUserEvent += Hide;

        }
        public void UseTryDal(object obj, EventArgs e)
        {
            TryDel?.Invoke(obj, e);
        }
        private void Hide(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ResetPassword.Visibility = Visibility.Collapsed;
        }

        public string GetPassword_F()
        {
            return pwb1.Password;
        }

        public string GetPassword_S()
        {
            return pwb2.Password;
        }
    }
}
