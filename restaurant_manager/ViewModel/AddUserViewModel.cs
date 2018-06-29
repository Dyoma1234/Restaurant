using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;
using System.Threading.Tasks;
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

        public List<Staff_Pos> Pos { set; get; }
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
            if (_name == string.Empty||
                _surname == string.Empty ||
                _email == string.Empty ||
                _phone == string.Empty ||
                _login == string.Empty ||
                _password_f == string.Empty ||
                _password_s == string.Empty ||
                Cur_pos == null )
            {
                return false;
            }
            else if (_password_f != _password_s)
            {
                return false;
            }

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
                temp.password = _password_f;
                temp.phone_number = _phone;
                temp.Staff_Pos = Cur_pos;
                temp.Staff_PosId = Cur_pos.Id;
                _model.db.StaffSet.Add(temp);
                await _model.db.SaveChangesAsync();
                Imsg sender = obj as Imsg;
                if (sender == null)
                    return;

                sender.ShowInfo("Новый пользователь добавлен!", "Добавить нового сотрудника");
                find = _model.db.StaffSet.Where(Staff => Staff.login == _login)
                    .FirstOrDefault();
                if (find != null)
                {
                    createUser.AddUser(find.Id, find.FirstName, find.LastName, find.login, find.password);
                }
                clear();
            }
            else
            {
                Imsg sender = obj as Imsg;
                if (sender == null)
                    return;

                sender.ShowWarning("Пользователь с таким логином уже существует", "Добавить нового сотрудника");
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
        }
        public AddUserViewModel(ICreateUserList cul)
        {
            _model = new Model();
            Pos = _model.db.Staff_PosSet.ToList();
            Cur_pos = null;
            createUser = cul;
        }
    }
}
