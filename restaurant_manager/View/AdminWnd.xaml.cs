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
using restaurant_manager.View;


namespace restaurant_manager
{
    /// <summary>
    /// Логика взаимодействия для AdminWnd.xaml
    /// </summary>
    public partial class AdminWnd : Window, ICreateUserList
    {
        private Grid _btnSearch;

        public AdminWnd()
        {

            InitializeComponent();
            AdminViewModel vm = new AdminViewModel();
            vm.InitStorageEvent += InitStorage;
            vm.InitMenuEvent += InitMenu;
            vm.InitBookingEvent += InitBooking;
            vm.CloseEv += Close;
            DataContext = vm;
            _btnSearch = SearchBtn;
            InitBooking(this, null);
        }

        private void InitBooking(object sender, EventArgs e)
        {
            Clear();
            AddNavHeader("Зал");
            BookingViewModel vm = new BookingViewModel();
            Booking wnd = new Booking();
            vm.Update += wnd.Update;
            wnd.DataContext = vm;
            bkk.Children.Add(wnd);
            bkk.Visibility = Visibility.Visible;

        }

        private void InitMenu(object sender, EventArgs e)
        {
            Clear();
            AddNavHeader("Меню");
            AddMenuGrid();
            SearchBtn.Visibility = Visibility.Visible;
            Table_content.Visibility = Visibility.Visible;
            HelperBtn.Visibility = Visibility.Visible;
            SearchPanel.Visibility = Visibility.Visible;
            Nav.Children.Add(_btnSearch);
           
        }
        public void InitStorage(object sender, EventArgs e)
        {
            Clear();
            AddNavHeader("Склад");
            AddStorage();
            SearchBtn.Visibility = Visibility.Visible;
            Table_content.Visibility = Visibility.Visible;
            HelperBtn.Visibility = Visibility.Visible;
            SearchPanel.Visibility = Visibility.Visible;
            Nav.Children.Add(_btnSearch);

        }
        private void AddMenuGrid()
        {
            MenuViewModel vm = new MenuViewModel();
            MenuTable table = new MenuTable();
            SearchMenuPanel panel = new SearchMenuPanel();
            MenuHelpBtn btn = new MenuHelpBtn();
            vm.UpdateEv += table.Refresh;
            btn.DataContext = vm;
            table.DataContext = vm;
            panel.DataContext = vm;
            Table_content.Children.Insert(0,table);
            SearchPanel.Children.Add(panel);
            HelperBtn.Children.Add(btn);

        }
        
        
            
        
        private void BtnOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            bg.Visibility = Visibility.Visible;
            BtnOpenMenu.Visibility = Visibility.Collapsed;
            BtnCloseMenu.Visibility = Visibility.Visible;
            FullName.Visibility = Visibility.Visible;
            PhoneNumber.Visibility = Visibility.Visible;
        }

        private void BtnCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            bg.Visibility = Visibility.Collapsed;
            BtnOpenMenu.Visibility = Visibility.Visible;
            BtnCloseMenu.Visibility = Visibility.Collapsed;
            FullName.Visibility = Visibility.Collapsed;
            PhoneNumber.Visibility = Visibility.Collapsed;
        }
      

        public void AddStorage()
        {
            StorageViewModel vm = new StorageViewModel();
            vm.Loading += Loading;

            UserTable table = new UserTable();
            SearchStoragePanel search = new SearchStoragePanel();
            vm.UpdateEv += table.Refresh;
            vm.AddNewProductEvent += CreateAddProductWnd;
            HelperBtnStorage helperBtn = new HelperBtnStorage();
            table.DataContext = vm;
            helperBtn.DataContext = vm;
            search.DataContext = vm;
            Table_content.Children.Add(table);
            HelperBtn.Children.Add(helperBtn);
            SearchPanel.Children.Add(search);


        }
        public void Loading(object obj, EventArgs args)
        {
            if (LoadLogin.Visibility == Visibility.Visible)
            {
                LoadLogin.Visibility = Visibility.Collapsed;
                return;
            }
            if (LoadLogin.Visibility == Visibility.Collapsed)
            {
                LoadLogin.Visibility = Visibility.Visible;
                return;
            }
        }
        public void Stels(object obj,EventArgs e)
        {
            if (BgContent.Visibility == Visibility.Visible)
            {
                BgContent.Visibility = Visibility.Collapsed;
                return;
            }
            if (BgContent.Visibility == Visibility.Collapsed)
            {
                BgContent.Visibility = Visibility.Visible;
                return;
            }
        }
        public void AddUser(int id)
        {
            User_card NewUsCard = new User_card(id);
            NewUsCard.TryDel += Stels;
            Users_content.Children.Add(NewUsCard);

        }
        public void Clear()
        {
            Users_content.Children.Clear();
            Table_content.Children.Clear();
            Nav.Children.Clear();
            HelperBtn.Children.Clear();
            SearchPanel.Children.Clear();
            bkk.Children.Clear();
            foreach (UIElement item in Content.Children)
            {
                item.Visibility = Visibility.Collapsed;
            }
        }
        void AddNavHeader(string str)
        {
            TextBlock tb = new TextBlock();
            tb.Text = str;
            tb.HorizontalAlignment = HorizontalAlignment.Center;
            tb.VerticalAlignment = VerticalAlignment.Center;
            tb.Foreground = new SolidColorBrush(Colors.White);
            tb.FontSize = 16;
            Nav.Children.Add(tb);
        }
        public void InitUserList()
        {
            Nav.Children.Clear();
            Clear();
            AddNavHeader("Персонал");
            Nav.Children.Add(new PopUpBtn(this));
            User_container.Visibility = Visibility.Visible;


        }

        private void bg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BtnOpenMenu.Visibility = Visibility.Visible;
            BtnCloseMenu.Visibility = Visibility.Collapsed;
            FullName.Visibility = Visibility.Collapsed;
            PhoneNumber.Visibility = Visibility.Collapsed;
        }

        public void EraseUser(int id)
        {
            foreach (User_card item in Users_content.Children)
            {
                if (item.Id == id)
                {
                    Users_content.Children.Remove(item);
                    break;
                }

            }

        }
        private void CreateAddProductWnd(object obj, EventArgs e){

            AddProduct AddProductWnd = new AddProduct();
            AddProductWnd.ShowDialog();

        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void BtnOpenSearchMenu_Click(object sender, RoutedEventArgs e)
        {
            BtnCloseSearch.Visibility = Visibility.Visible;
            BtnOpenSearch.Visibility = Visibility.Collapsed;
        }

        private void BtnCloseSearchMenu_Click(object sender, RoutedEventArgs e)
        {
            BtnCloseSearch.Visibility = Visibility.Collapsed;
            BtnOpenSearch.Visibility = Visibility.Visible;
        }

        public void Close(object sender, EventArgs e)
        {
            MainBG.Visibility = Visibility.Visible;
            if(WpfMessageBox.Show("Выход","Вы действительно хотите выйти из учетной записи ?", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                MainBG.Visibility = Visibility.Collapsed;
                LoginViewModel vm = new LoginViewModel();
                LoginWnd wnd = new LoginWnd();
                wnd.DataContext = vm;
                wnd.Show();
                this.Close();
            }
            MainBG.Visibility = Visibility.Collapsed;
        }
    }
}
