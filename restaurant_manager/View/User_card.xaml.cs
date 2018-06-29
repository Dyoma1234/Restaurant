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
    public partial class User_card : UserControl
    {
        public int Id { set; get; }
        public User_card()
        {
            InitializeComponent();

        }

        public User_card(int id, string name, string lastname, string login, string password)
        {
            InitializeComponent();
            DataContext = new EditPersonalViewModel(id);
           
        }
    }
}
