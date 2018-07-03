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
        public event EventHandler InitStorageEvent;
        public event EventHandler InitMenuEvent;
        public event EventHandler InitBookingEvent;
        public event EventHandler CloseEv;

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
        private DelegateCommand _storage_command;
        private DelegateCommand _menu_command;
        private DelegateCommand _booking_command;
        private DelegateCommand _close_command;


        public ICommand ShowBookingCommand
        {
            get
            {
                if (_booking_command == null)
                {
                    _booking_command = new DelegateCommand(ShowBooking, null);
                }
                return _booking_command;
            }
        }

        private void ShowBooking(object obj)
        {
            InitBookingEvent?.Invoke(obj, null);
        }

        public ICommand ShowStorageCommand
        {
            get
            {

                if (_storage_command == null)
                {
                    _storage_command = new DelegateCommand(ShowStorage, null);

                }
                return _storage_command;
            }

        }
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
        public ICommand ShowMenuCommand
        {
            get
            {
                if (_menu_command == null)
                {
                    _menu_command = new DelegateCommand(ShowMenu, null);

                }
                return _menu_command;
            }
        }
        public ICommand CloseCommand
        {
            get
            {
                if (_close_command == null)
                {
                    _close_command = new DelegateCommand(Close, null);
                }
                return _close_command;
            }
        }

        private void Close(object obj)
        {
            Cur_session.Close_session();
            CloseEv?.Invoke(obj, null);
        }

        private void ShowMenu(object obj)
        {
            InitMenuEvent?.Invoke(obj, null);
        }

        private void ShowPersonal(object obj)
        {
            ICreateUserList awnd = obj as ICreateUserList;
            if ( awnd == null)
            {
                return;
            }
            awnd.InitUserList();
            foreach (var item in _model.db.StaffSet)

            {
                awnd.AddUser(item.Id);
            }
        }
        private void ShowStorage(object obj)
        {
            InitStorageEvent?.Invoke(obj, null);
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


