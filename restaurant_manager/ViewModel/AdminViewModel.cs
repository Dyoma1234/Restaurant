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
    public class AdminViewModel : BaseViewMode
    {
        // public Model _model;
        private string _fullname;
        private string _phonenumber;
        private string _pagename;
        private Model _model;


        public string FullName
        {
            get
            {
                return _fullname;
            }
            set
            {
                _fullname = value;
                OnPropertyChanged(new PropertyChangedEventArgs("FullName"));
            }
        }
        public string PhoneNumber
        {
            get
            {
                return _phonenumber;
            }
            set
            {
                _phonenumber = value;
                OnPropertyChanged(new PropertyChangedEventArgs("PhoneNumber"));
            }
        }
        public string PageName
        {
            get
            {
                return _pagename;
            }
            set
            {
                _pagename = value;
                OnPropertyChanged(new PropertyChangedEventArgs("PageName"));
            }
        }

        public string Password { get; set; }
        public AdminViewModel()
        {
            FullName = Cur_session.Name + " " + Cur_session.LastName;
            PhoneNumber = Cur_session.PhoneNumber;
            _model = new Model();

        }

        private DelegateCommand _personal_command;


      
        public ICommand ShowPersonalCommand
        {
            get
            {
                if (_personal_command == null)
                {
                    _personal_command = new DelegateCommand(ShowPersonal, null);

                }
                return _personal_command;
            }
        }
        private void ShowPersonal(object obj)
        {
            ICreateUserList awnd = obj as ICreateUserList;
            if ( awnd == null)
            {
                return;
            }
            awnd.InintNav();
            foreach (var item in _model.db.StaffSet)

            {
                awnd.AddUser(item.Id, item.FirstName, item.LastName, item.login, item.password);
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


