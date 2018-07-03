using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Controls;
using System.Security.Cryptography;

namespace restaurant_manager.ViewModel
{
    public class LoginViewModel : BaseViewMode
    {
        private string _login;
        private Model _model;
        private bool _isloading;
        static EventWaitHandle _waitHandle = new AutoResetEvent(false);
        public bool LoadVisible
        {
            set
            {
                _isloading = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadVisible"));
            }
            get
            {
                return _isloading;
            }
        }
        private HashingPassword hashing;

        public string Login
        {
            get
            {
                return _login;
            }
            set
            {
                _login = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Login"));
            }
        }

        public string Password { get; set; }
        private DelegateCommand _login_commaned;


        public LoginViewModel()
        {
            _model = new Model();
            LoadVisible = false ;
            hashing = new HashingPassword();
        }
        public ICommand LoginCommand
        {
            get
            {
                if (_login_commaned == null)
                {
                    _login_commaned = new DelegateCommand(MainWindowShow, CanMainWindowShow);

                }
                return _login_commaned;
            }
        }
        private async void MainWindowShow(object obj)
        {
            ILogin temp_obj = obj as ILogin;
            if (temp_obj == null)
                return;
            LoadVisible = true;
            _waitHandle = new AutoResetEvent(false);
            await Task.Run(() =>
            {

               
                    foreach (var item in _model.db.StaffSet)
                    {
                        if (item.login == Login &&( item.password==temp_obj.GetPassword()|| item.password == hashing.HashPassword(temp_obj.GetPassword())))
                        {


                            Cur_session.New_session(item.Id, item.login, item.password, item.FirstName, item.LastName, item.Staff_Pos.Position, item.phone_number);
                            _waitHandle.Set();

                            break;
                        }
                    }
                    _waitHandle.Set();
            });

            _waitHandle.WaitOne();
            if(Cur_session.Id == -1)
            {
                WpfMessageBox.Show("Ошибка авторизации", "Пользователь с таким логином или паролем не найден.", 0, MessageBoxImage.Warning);
                LoadVisible = false;
                return;
            }
            LoadVisible = false;
            _waitHandle.Close();
            AdminWnd adminWnd = new AdminWnd();
            adminWnd.Show();
            temp_obj.Close();
        }
        private bool CanMainWindowShow(object obj)
        {
            ILogin temp_obj = obj as ILogin;
            if (temp_obj == null)
                return false;
            if (string.IsNullOrWhiteSpace(Login) || Login.Length < 4 || Login.Length>16)
                return false;
            if (string.IsNullOrWhiteSpace(temp_obj.GetPassword())|| temp_obj.GetPassword().Length < 4 || temp_obj.GetPassword().Length > 16)
                return false;


            return true;


        }
      
     
    }
}


