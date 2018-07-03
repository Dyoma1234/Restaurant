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
    /// Логика взаимодействия для HelperBtnStorage.xaml
    /// </summary>
    public partial class HelperBtnStorage : UserControl
    {
        public HelperBtnStorage()
        {
            InitializeComponent();
        }
        public HelperBtnStorage(  StorageViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
