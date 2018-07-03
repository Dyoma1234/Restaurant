using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows;
using restaurant_manager.Interfaces;
namespace restaurant_manager.ViewModel
{
    class AddUserViewModel:BaseViewMode
    {
        private Model _model;
        private ICreateUserList createUser;
        private string _name;
        private string _surname;
        private string _email;
        private string _phone;
        private string _login;
        private string _password_f;
        private string _password_s;
        private string _cur_posstr;
        public Staff_Pos _cur_pos;
        public List<Staff_Pos> Pos { set; get; }
        public Staff_Pos Cur_pos { set { _cur_pos = value; OnPropertyChanged(new PropertyChangedEventArgs("Cur_pos")); } get => _cur_pos; }
        public string Cur_posStr { set { _cur_posstr = value; OnPropertyChanged(new PropertyChangedEventArgs("Cur_posStr")); } get => _cur_posstr; }
        private HashingPassword hashing;


        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Name"));
            }
        }
        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                _surname = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Surname"));
            }
        }
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Email"));
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
                OnPropertyChanged(new PropertyChangedEventArgs("Phone"));
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
                OnPropertyChanged(new PropertyChangedEventArgs("Login"));
            }
        }
        public string Password_f
        {
            get
            {
                return _password_f;
            }
            set
            {
                _password_f = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Password_f"));
            }
        }
        public string Password_s
        {
            get
            {
                return _password_s;
            }
            set
            {
                _password_s = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Password_s"));
            }
        }

        private DelegateCommand _add_command;



        public ICommand AddNewUserCommand
        {
            get
            {
                if (_add_command == null)
                {
                    _add_command = new DelegateCommand(AddUser, CanAddUser);

                }
                return _add_command;
            }
        }

        private bool CanAddUser(object obj)
        {
            if (string.IsNullOrWhiteSpace(_name))
                return false;
            if (string.IsNullOrWhiteSpace(_surname))
                return false;
            if (string.IsNullOrWhiteSpace(_phone))
                return false;
            if (string.IsNullOrWhiteSpace(_login))
                return false;
            if (string.IsNullOrWhiteSpace(_password_f))
                return false;
            if (string.IsNullOrWhiteSpace(_password_s))
                return false;
            if (string.IsNullOrWhiteSpace(_cur_posstr))
                return false;
            if (_password_f.Count()<4|| _password_f.Count()>16)
                return false;
            if (_password_s.Count() < 4 || _password_s.Count() > 16)
                return false;
            if (_login.Count() < 4 || _login.Count() > 16)
                return false;
            if (_password_f != _password_s)
                return false;
            return true;
        }

        private async void  AddUser(object obj)
        {
           var find = _model.db.StaffSet.Where(Staff => Staff.login == _login)
                    .FirstOrDefault();
            if (find == null)
            {

                Staff temp = new Staff();
                temp.FirstName = _name;
                temp.LastName = _surname;
                temp.login = _login;
                temp.password = hashing.HashPassword(Password_f);
                temp.phone_number = _phone;
                temp.Staff_Pos = Cur_pos;
                temp.Staff_PosId = Cur_pos.Id;
                _model.db.StaffSet.Add(temp);
                await _model.db.SaveChangesAsync();
                WpfMessageBox.Show("Добавить нового сотрудника", "Новый пользователь добавлен!", MessageBoxButton.OK, MessageBoxImage.Warning);

                find = _model.db.StaffSet.Where(Staff => Staff.login == _login)
                    .FirstOrDefault();
                if (find != null)
                {
                    createUser.AddUser(find.Id);
                }
                clear();
            }
            else
            {
                

                WpfMessageBox.Show( "Добавить нового сотрудника", "Пользователь с таким логином уже существует",MessageBoxButton.OK,MessageBoxImage.Warning);
            }

        }
        private void clear()
        {
            Name = string.Empty;
            Surname = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;
            Login = string.Empty;
            Password_f = string.Empty;
            Password_s = string.Empty;

        }
        public AddUserViewModel()
        {
            _model = new Model();
            Pos = _model.db.Staff_PosSet.ToList();
            Cur_pos = null;
            hashing = new HashingPassword();
        }
        public AddUserViewModel(ICreateUserList cul)
        {
            _model = new Model();
            Pos = _model.db.Staff_PosSet.ToList();
            Cur_pos = null;
            createUser = cul;
            hashing = new HashingPassword();

        }


        private DelegateCommand _PasswordChangedCommand;

        public ICommand PasswordChangedCommand
        {
            get
            {
                if (_PasswordChangedCommand == null)
                {
                    _PasswordChangedCommand = new DelegateCommand(FPChanget, null);
                }
                return _PasswordChangedCommand;
            }
        }

        private void FPChanget(object obj)
        {
            IPasswordChanget item = (obj as IPasswordChanget);
            if (item == null)
                return;
            Password_f = item.GetPassword_F();
            Password_s= item.GetPassword_S();


        }
    }
}
