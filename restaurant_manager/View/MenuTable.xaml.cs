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
    /// Логика взаимодействия для MenuTable.xaml
    /// </summary>
    public partial class MenuTable : UserControl
    {
        public MenuTable()
        {
            InitializeComponent();
            MenuViewModel vm = new MenuViewModel();
            DataContext = vm;
        }
        public void Refresh(object obj, EventArgs e)
        {
            Table.Items.Refresh();
        }

        private void AddNewPric_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            bool approvedDecimalPoint = false;

            if (e.Text == ".")
            {
                if (!((TextBox)sender).Text.Contains("."))
                    approvedDecimalPoint = true;
            }

            if (!(char.IsDigit(e.Text, e.Text.Length - 1) || approvedDecimalPoint))
                e.Handled = true;
        }

        private void Table_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {

        }
    }
}
