using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace restaurant_manager.ViewModel
{
    class EditPersonalViewModel : BaseViewMode
    {
        private Model _model;
        private string _name;
        private string _lastname;
        private string _phone;
        private string _pos;
        private string _fullname;
        private string _login;
        private string _password;
        public int Id { set; get; }


        private string old_name;
        private string old_lastname;
        private string old_phone;
        private string old_pos;
        private string old_fullname;
        private string old_login;
        private string old_password;

        
        public List<Staff_Pos> staff_Pos { set; get; }
        public Staff_Pos Cur_pos { set; get; } 
         
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                FullName = (old_name + ' ' + old_lastname);
                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("Name"));
                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("FullName"));

            }
        }
        public string LastName
        {
            get
            {
                return _lastname;
            }
            set
            {
                _lastname = value;
                FullName = (old_name + ' ' + old_lastname);
                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("LastName"));
                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("FullName"));

            }
        }
        public string FullName
        {
            get
            {
                return _fullname;
            }
            set
            {
                _fullname = value;
                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("FullName"));
            }
        }

        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;
                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("Phone"));
            }
        }
        public string Pos
        {
            get
            {
                return _pos;
            }
            set
            {
                _pos = value;
                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("Pos"));
            }
        }
        public string Login
        {
            get
            {
                return _login;
            }
            set
            {
                _login = value;
                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("Login"));

            }
        }
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("Password"));
            }
        }
        public EditPersonalViewModel()
        {
         _model = new Model();
         old_name=_name;
         old_lastname = _lastname;
         old_phone = _phone;
         old_pos = _pos;
         old_fullname = _fullname;



        FullName = (_name +" "+ _lastname);
        }
        public EditPersonalViewModel(int id)
        {
            
            _model = new Model();
            Id = id;
            foreach (var item in _model.db.StaffSet)
            {
                if (item.Id == Id)
                {
                    _name = item.FirstName;
                    _lastname = item.LastName;
                    _phone = item.phone_number;
                    _login = item.login;
                    _password = item.password;
                    Cur_pos = item.Staff_Pos;
                    _pos = item.Staff_Pos.Position;
                }
            }

            old_name = _name;
            old_lastname = _lastname;
            old_phone = _phone;
            old_pos = _pos;
            old_fullname = _fullname;
            old_login = _login;
            old_password = _password;

            staff_Pos = _model.db.Staff_PosSet.ToList();
            
            FullName = (_name + " " + _lastname);
           
        }

        private DelegateCommand _save_command;


        public ICommand SaveCommand
        {
            get
            {
                if (_save_command == null)
                {
                    _save_command = new DelegateCommand(SaveNewPropertys, CanSaveNewPropertys);

                }
                return _save_command;
            }
        }

        private bool CanSaveNewPropertys(object obj)
        {
            if (old_name != _name ||
                old_lastname != _lastname||
                 old_phone != _phone||
                 old_login!=_login||
                 old_password!=_password||
                 old_pos!=Cur_pos.Position
                )
            {
                if (_name == string.Empty ||
               _lastname == string.Empty ||
                _phone == string.Empty ||
                _login == string.Empty ||
                _password == string.Empty ||
                _pos == string.Empty
               )
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        private async   void SaveNewPropertys(object obj)
        {
            foreach (var item in _model.db.StaffSet)
            {
                if (item.Id == Id)
                {


                    item.FirstName = Name;

                    item.LastName = _lastname;

                    item.login = Login;

                    item.password = Password;

                    item.phone_number = Phone;

                    item.Staff_Pos = Cur_pos;


                    old_name = _name;
                    old_lastname = _lastname;
                    old_phone = _phone;
                    old_pos = Cur_pos.Position;
                    old_fullname = _fullname;
                    old_login = _login;
                    old_password = _password;

                    break;
                }
            }
         await   _model.db.SaveChangesAsync();

        }
    }
}
