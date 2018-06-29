using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Controls;

namespace restaurant_manager.ViewModel
{
    public class LoginViewModel : BaseViewMode
    {
        // public Model _model;
        private string _login;
        private Model _model;


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

        private void MainWindowShow(object obj)
        {
            ILogin temp_obj = obj as ILogin;
            if (temp_obj == null)
                return;
            
            foreach ( var item in _model.db.StaffSet)
            {
               if( item.login == Login&&item.password== temp_obj.GetPassword())
                {
                    
                    
                    Cur_session.New_session(item.login,item.password,item.FirstName,item.LastName,item.Staff_Pos.Position,item.phone_number);
                    AdminWnd adminWnd = new AdminWnd();
                    adminWnd.Show();
                    temp_obj.Close();

                    break;
                }
            }
           
        }
        private bool CanMainWindowShow(object obj)
        {
            if (obj != null)
            {
               
            }
            return true;
        }
      
     
    }
}


