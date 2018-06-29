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

namespace restaurant_manager
{
    /// <summary>
    /// Логика взаимодействия для PopUpBtn.xaml
    /// </summary>
    public partial class PopUpBtn : UserControl
    {
       private ICreateUserList rel;
        public PopUpBtn()
        {
            InitializeComponent();
        }
        public PopUpBtn(ICreateUserList cul)
        {
            InitializeComponent();
            rel = cul;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddUserWnd wnd = new AddUserWnd(rel);
            wnd.ShowDialog();
        }
    }
}
