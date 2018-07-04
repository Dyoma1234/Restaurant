using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;

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
        private string _secretword;
        private string _p1;
        private string _p2;
        private string _cur_secretword;
        public int Id { set; get; }
        private HashingPassword hashing;
        private IPasswordChanget pwb;


        private string old_name;
        private string old_lastname;
        private string old_phone;
        private string old_pos;
        private string old_fullname;
        private string old_login;
        private string old_password;
        private bool _resetvis;


        public List<Staff_Pos> staff_Pos { set; get; }
        public Staff_Pos Cur_pos { set; get; }
        public event EventHandler DelVis;
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
        public string SecretWord
        {
            set
            {
                _secretword = value;
                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("SecretWord"));
            }
            get
            {
                return _secretword;
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
        public bool ResetVis
        {
            set
            {
                _resetvis = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ResetVis"));
            }
            get
            {
                return _resetvis;
            }
        }

        public event EventHandler DelUserEvent;

        public EditPersonalViewModel()
        {
         _model = new Model();
            ResetVis = false;
            _p1 = string.Empty;
            _p2 = string.Empty;
         old_name =_name;
         old_lastname = _lastname;
         old_phone = _phone;
         old_pos = _pos;
         old_fullname = _fullname;
            hashing = new HashingPassword();


           FullName = (_name +" "+ _lastname);
        }
        public EditPersonalViewModel(int id)
        {
            
            _model = new Model();
            ResetVis = false;

            _p1 = string.Empty;
            _p2 = string.Empty;
            hashing = new HashingPassword();
            Id = id;
            Staff item = _model.db.StaffSet.Find(Id);

            if (item.Id == Id) 
            {

                _name = item.FirstName;
                _lastname = item.LastName;
                _phone = item.phone_number;
                _login = item.login;
                _password = item.password;
                Cur_pos = item.Staff_Pos;
                _pos = item.Staff_Pos.Position;
                _cur_secretword = item.secret_word;
                
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
        private DelegateCommand _deluser_command;

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

        public ICommand DelUserCommand
        {
            get
            {
                if (_deluser_command == null)
                {
                    _deluser_command = new DelegateCommand(DelPersonal, CanDelPersonal);

                }
                return _deluser_command;
            }
        }

        private bool CanDelPersonal(object obj)
        {
            if (obj != null)
            {
                int search_id = (int)obj;
                if (search_id == Cur_session.Id)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        private async void DelPersonal(object obj)
        {
            if (obj != null)
            {
                
                    int search_id = (int)obj;
                    var remove = _model.db.StaffSet.Find(search_id);
                    if (remove == null)
                        return;

                DelVis?.Invoke(obj, null);
                if (MessageBoxResult.Yes==WpfMessageBox.Show("Удаление персонала", "Вы действительно хотите удалить \""+remove.FirstName+" "+remove.LastName+"\" ?",MessageBoxButton.YesNo,MessageBoxImage.Warning))
                {
                    DelVis?.Invoke(obj, null);
                    _model.db.StaffSet.Remove(remove);
                    await _model.db.SaveChangesAsync();
                    DelUserEvent?.Invoke(null, null);
                    return;
                }
                DelVis?.Invoke(obj, null);
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
            DelVis?.Invoke(obj, null);
            if (MessageBoxResult.Yes == WpfMessageBox.Show("Редактирование персонала", "Вы действительно хотите сохранить редактирование ?", MessageBoxButton.YesNo, MessageBoxImage.Warning))
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
                        FullName = (_name + " " + _lastname);
                        Pos = Cur_pos.Position;
                        break;
                    }
                }
                await _model.db.SaveChangesAsync();
            }
            DelVis?.Invoke(obj, null);

        }
        private DelegateCommand _PasswordChangedCommand;
        private DelegateCommand _reset_command;
        private DelegateCommand _ShowResetCommand;
        private DelegateCommand _CloseResetCommand;
        public ICommand CloseResetCommand
        {
            get
            {
                if (_CloseResetCommand == null)
                {
                    _CloseResetCommand = new DelegateCommand(CloseReset, null);
                }
                return _CloseResetCommand;
            }
        }

        private void CloseReset(object obj)
        {
            SecretWord = string.Empty;
            ResetVis = false;
            pwb?.ClearPassword();
        }

        public ICommand ShowResetCommand
        {
            get
            {
                if (_ShowResetCommand == null)
                {
                     _ShowResetCommand=new DelegateCommand(OpenReset,null);
                }
                return _ShowResetCommand;
            }
        }

        private void OpenReset(object obj)
        {
            ResetVis = true;
        }

        public ICommand PasswordChangedCommand
        {
            get
            {
                if (_PasswordChangedCommand == null)
                {
                    _PasswordChangedCommand = new DelegateCommand(FpChanget, CanFpChanget);
                }
                return _PasswordChangedCommand;
            }
        }

        private bool CanFpChanget(object obj)
        {
            return true;
        }

        public ICommand ResetCommand
        {
            get
            {
                if (_reset_command == null)
                {
                    _reset_command = new DelegateCommand(Reset, CanReset);
                }
                return _reset_command;
            }
            
        }

        private bool CanReset(object obj)
        {
            if (string.IsNullOrWhiteSpace(_secretword))
                return false;
            if (string.IsNullOrWhiteSpace(_p1))
                return false;
            if (string.IsNullOrWhiteSpace(_p2))
                return false;
            if (_p1 != _p2)
                return false;

            return true;
        }

        private void Reset(object obj)
        {
            var UserReset = _model.db.StaffSet.Where(i => i.Id == Id).FirstOrDefault();
            if (UserReset == null)
                return;
            if (UserReset.secret_word == SecretWord ||UserReset.secret_word== hashing.HashPassword(SecretWord))
            {
                _model.db.StaffSet.Remove(UserReset);
                _model.db.SaveChanges();
                UserReset.password = hashing.HashPassword(_p1);
                _model.db.StaffSet.Add(UserReset);
                _model.db.SaveChanges();
                _p1 = string.Empty;
                _p2 = string.Empty;
                SecretWord = string.Empty;
                DelVis?.Invoke(obj, null);
                WpfMessageBox.Show("Изменение пароля", "Пароль успешно изменен.", MessageBoxType.Information);
                DelVis?.Invoke(obj, null);
                ResetVis = false;
                pwb?.ClearPassword();
            }
            else
            {
                DelVis?.Invoke(obj, null);
                WpfMessageBox.Show("Изменение пароля", "Секретное слово неправильное.", MessageBoxType.Information);
                DelVis?.Invoke(obj, null);
            }

        }

        private void FpChanget(object obj)
            {
            pwb = (obj as IPasswordChanget);
            if (pwb == null)
                return;
            _p1 = pwb.GetPassword_F();
            _p2 = pwb.GetPassword_S();


        }
    }
}
