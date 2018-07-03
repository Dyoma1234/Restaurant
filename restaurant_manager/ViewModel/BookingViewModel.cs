using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows;
using System.Text.RegularExpressions;

namespace restaurant_manager.ViewModel
{
    class BookingViewModel:BaseViewMode
    {
        private Model _model;
        private int _selectTablIndex;
        private DateTime _date;
        private List<Reservation> _reslist;
        private Reservation _old_cur_item;
        private Reservation _cur_item;
        private bool _addvis;
        private bool _editvis;
        private bool _bgvis;
        private string _searchstring;

        public event EventHandler Update;
        private Tables _seltable;
        public Tables Selected_Table
        {
            set
            {
                _seltable = value;
             
                OnPropertyChanged(new PropertyChangedEventArgs("Selected_Table"));
               
            }
            get
            {
                return _seltable;
            }
        }

        private List<Tables> _tblist;
        public List<Tables> TbList
        {
            set
            {
                _tblist = value;
                OnPropertyChanged(new PropertyChangedEventArgs("TbList"));
            }
            get
            {
                return _tblist;
            }
        }
        private string _editfullname;
        public string EditFullName
        {
            set
            {
                _editfullname = value;
                OnPropertyChanged(new PropertyChangedEventArgs("EditFullName"));
            }
            get
            {
                return _editfullname;
            }
        }
        private string _editphone;
        public string EditPhone
        {
            set
            {
                _editphone = value;
                OnPropertyChanged(new PropertyChangedEventArgs("EditPhone"));
            }
            get
            {
                return _editphone;
            }
        }
        private DateTime _editdate;
        public DateTime EditDate
        {
            set
            {
                _editdate = value;
                OnPropertyChanged(new PropertyChangedEventArgs("EditDate"));
            }
            get
            {
                return _editdate;
            }
        }
        private int _edittable;
        public int EditTable
        {
            set
            {
                _edittable = value;
                OnPropertyChanged(new PropertyChangedEventArgs("EditTable"));
            }
            get
            {
                return _edittable;
            }
        }
        private Tables _editseltables;
        public Tables EditSelected_Table
        {
            set
            {
                _editseltables = value;

                OnPropertyChanged(new PropertyChangedEventArgs("EditSelected_Table"));

            }
            get
            {
                return _editseltables;
            }
        }


        private string _addfullname;
        public string AddFullName
        {
            set
            {
                _addfullname = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddFullName"));
            }
            get
            {
                return _addfullname;
            }
        }
        private string _addphone;
        public string AddPhone
        {
            set
            {
                _addphone = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddPhone"));
            }
            get
            {
                return _addphone;
            }
        }
      



        private DateTime _adddate;
        public DateTime AddDate
        {
            set
            {
                _adddate = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddDate"));
            }
            get
            {
                return _adddate;
            }
        }
       
        private string _addtablestr;
        public string AddTableStr
        {
            set
            {
                _addtablestr = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddTableStr"));
            }
            get
            {
                return _addtablestr;
            }
        }
        private Tables _addseltables;
        public Tables AddSelected_Table
        {
            set
            {
                _addseltables = value;

                OnPropertyChanged(new PropertyChangedEventArgs("AddSelected_Table"));

            }
            get
            {
                return _addseltables;
            }
        }

        public int SelectTableIndex
        {
            set
            {
                _selectTablIndex = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectTableIndex"));

                if (_selectTablIndex != -1)
                {
                    ResList = _model.db.ReservationSet.Where(i => i.Time.Day == _date.Day &&
                     i.Time.Month == _date.Month &&
                     i.Time.Year == _date.Year&&
                     i.TablesSet.Num==_selectTablIndex).ToList();
                }
                else
                {
                    ResList = _model.db.ReservationSet.Where(i => i.Time.Day == _date.Day &&
                    i.Time.Month == _date.Month &&
                    i.Time.Year == _date.Year).ToList();
                }


            }
            get
            {
                return _selectTablIndex;
            }
        }
        public List<Reservation> ResList
        {
            set
            {
                _reslist = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ResList"));
            }
            get
            {
                return _reslist;
            }
        }
        public Reservation Cur_Item
        {
            set
            {
                _cur_item = value;
                _old_cur_item = _cur_item;
                OnPropertyChanged(new PropertyChangedEventArgs("Cur_Item"));
            }
            get
            {
                return _cur_item;
            }
        }
        public DateTime Date
        {
            set
            {
                _date = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Date"));
                ListUpdate();

            }
            get
            {
                return _date;
            }
        }
        public bool BgVis
        {
            set
            {
                _bgvis = value;
                OnPropertyChanged(new PropertyChangedEventArgs("BgVis"));
            }
            get
            {
                return _bgvis;
            }
        }
        public bool AddNewVis
        {
            set
            {
                _addvis = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddNewVis"));
            }
            get
            {
                return _addvis;
            }
        }
        public bool EditVis
        {
            set
            {
                _editvis = value;
                OnPropertyChanged(new PropertyChangedEventArgs("EditVis"));
            }
            get
            {
                return _editvis;
            }
        }
        public string SearchString
        {
            set
            {
                _searchstring = value;
                if (string.IsNullOrWhiteSpace(_searchstring))
                {
                    ListUpdate();
                }
                else
                {
                    ResList = _model.db.ReservationSet.Where(i => i.GuestsSet.FullName.Contains(SearchString) || i.GuestsSet.PhoneNumber.Contains(_searchstring)).ToList();
                    Update?.Invoke(null, null);
                }
                OnPropertyChanged(new PropertyChangedEventArgs("SearchString"));
            }
            get
            {
                return _searchstring;
            }
        }

        private DelegateCommand _selecttable_command;
        private DelegateCommand _add_command;
        private DelegateCommand _edit_command;
        private DelegateCommand _del_command;
        private DelegateCommand _close_command;
        private DelegateCommand _editbooking_command;
        private DelegateCommand _addnew_command;

        public ICommand SelectTableCommand
        {
            get
            {
                if (_selecttable_command == null)
                {
                    _selecttable_command = new DelegateCommand(UpdateSelect, null);
                  
                }
                return _selecttable_command;
            }
        }
        public ICommand AddCommand
        {
            get
            {
                if (_add_command==null)
                {
                    _add_command = new DelegateCommand(Add, null);
                }
                return _add_command;
            }
        }
        public ICommand EditCommand
        {
            get
            {
                if (_edit_command == null)
                {
                    _edit_command = new DelegateCommand(Edit, CanEdit);
                }
                return _edit_command;
            }
        }
        public ICommand DelCommand
        {
            get
            {
                if (_del_command == null)
                {
                    _del_command = new DelegateCommand(Del, CanDel);
                }
                return _del_command;
            }
        }
        public ICommand CloseCommand
        {
            get
            {
                if (_close_command == null)
                {
                    _close_command = new DelegateCommand(Hide, null);

                }
                return _close_command;
            }
        }
        public ICommand AddNewBookingCommand
        {
            get
            {
                if (_addnew_command == null)
                {
                    _addnew_command = new DelegateCommand(SaveNewBooking, CanSaveNewBooking);

                }
                return _addnew_command;
            }
        }
        public ICommand EditBookingCommand
        {
            get
            {
                if (_editbooking_command == null)
                {
                    _editbooking_command = new DelegateCommand(SaveEdit, CanSaveEdit);
                }
                return _editbooking_command;
            }
            
        }


        private bool CanEdit(object obj)
        {
            if (Cur_Item != null)
                return true;

            return false;
        }

        private void Edit(object obj)
        {
            EditFullName = Cur_Item.GuestsSet.FullName;
            EditPhone = Cur_Item.GuestsSet.PhoneNumber;
            EditDate = Cur_Item.Time;

            EditSelected_Table = Cur_Item.TablesSet;

            BgVis = true;
            EditVis = true;
        }

        private void ListUpdate()
        {
            if (_selectTablIndex != -1)
            {
                ResList = _model.db.ReservationSet.Where(i => i.Time.Day == _date.Day &&
                 i.Time.Month == _date.Month &&
                 i.Time.Year == _date.Year &&
                 i.TablesSet.Num == _selectTablIndex).ToList();
            }
            else
            {
                ResList = _model.db.ReservationSet.Where(i => i.Time.Day == _date.Day &&
                i.Time.Month == _date.Month &&
                i.Time.Year == _date.Year).ToList();
            }


        }
        private void Add(object obj)
        {
            BgVis = true;
            AddNewVis = true;
            AddDate = DateTime.Now;
            AddSelected_Table = TbList.First();
            
        }

        private bool CanSaveNewBooking(object obj)
        {
           string pattern = @"^\+\d{2}\(\d{3}\)\d{3}-\d{2}-\d{2}$";
            Regex rgx = new Regex(pattern);
            Match match = rgx.Match(AddPhone);
            if (!match.Success)
                return false;


            if (string.IsNullOrWhiteSpace((_addfullname ?? "").ToString()))
                return false;

            if (AddDate < DateTime.Now)
                return false;

            return true;

        }

        private void SaveNewBooking(object obj)
        {

            Guests guests = _model.db.GuestsSet.Where(i => i.PhoneNumber == AddPhone).FirstOrDefault();
            if (guests != null)
            {
                _model.db.GuestsSet.Remove(guests);
                _model.db.SaveChanges();
            }
            if (guests == null)
            {
                guests = new Guests();
                guests.PhoneNumber = AddPhone;
                guests.FullName = AddFullName;
                _model.db.GuestsSet.Add(guests);
            }
            guests.FullName = AddFullName;
            _model.db.GuestsSet.Add(guests);
            Reservation reservation = new Reservation();
            reservation.GuestsSet = guests;
            reservation.TablesSet = AddSelected_Table;
            reservation.Time = AddDate;

            _model.db.ReservationSet.Add(reservation);

            _model.db.SaveChanges();

            ListUpdate();
            Update?.Invoke(obj, null);

            WpfMessageBox.Show("Добавление", "Бронь успешно добавлена!", MessageBoxButton.OK, MessageBoxImage.Information);
            Hide(obj);
        }

        private bool CanSaveEdit(object obj)
        {
            if (Cur_Item != null && _old_cur_item != null)
            {
                if (EditTable != _old_cur_item.TablesSet.Num ||
                EditDate != _old_cur_item.Time ||
                EditPhone != _old_cur_item.GuestsSet.PhoneNumber ||
                EditFullName != _old_cur_item.GuestsSet.FullName)
                {
                    if (string.IsNullOrWhiteSpace(EditFullName) || string.IsNullOrWhiteSpace(EditPhone) || EditDate == null || EditTable < 1 || EditTable > 8)
                    {
                        return false;
                    }
                    return true;
                }
            }




            return false;
        }

        private  void SaveEdit(object obj)
        {
            if (WpfMessageBox.Show("Редактирование", "Вы действительно хотите сохранить изменения?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                _model.db.ReservationSet.Remove(Cur_Item);
                _model.db.SaveChanges();
                Cur_Item.Time = EditDate;
                Cur_Item.TablesSet = EditSelected_Table;
                Guests guests = _model.db.GuestsSet.Where(i => i.PhoneNumber == EditPhone).FirstOrDefault();
                if (guests != null)
                {
                    _model.db.GuestsSet.Remove(guests);
                    _model.db.SaveChanges();
                }
                if (guests == null)
                {
                    guests = new Guests();
                    guests.PhoneNumber = EditPhone;
                    guests.FullName = EditFullName;
                    _model.db.GuestsSet.Add(guests);
                }
                guests.FullName = EditFullName;
                _model.db.GuestsSet.Add(guests);
                Cur_Item.GuestsSet = guests;

                _model.db.ReservationSet.Add(Cur_Item);
                _model.db.SaveChanges();

                ListUpdate();
                Update?.Invoke(obj, null);

                WpfMessageBox.Show("Редактирование", "Редактирование успешно сохранено!", MessageBoxButton.OK, MessageBoxImage.Information);
                Hide(obj);
            }
            
        }

        private void Hide(object obj)
        {
            BgVis = false;
            EditVis = false;
            AddNewVis = false;
        }

        private bool CanDel(object obj)
        {
            if (Cur_Item != null)
                return true;

            return false;
        }

        private async void Del(object obj)
        {
            BgVis = true;
            if (WpfMessageBox.Show("Удаление", "Вы действительно хотите удалить бронь?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                _model.db.ReservationSet.Remove(Cur_Item);
                ResList.Remove(Cur_Item);
                Cur_Item = null;
                OnPropertyChanged(new PropertyChangedEventArgs("ResList"));
                Update?.Invoke(obj, null);
             await   _model.db.SaveChangesAsync();
            }
            BgVis = false;
        }

        private void UpdateSelect(object obj)
        {
            if(obj!=null)
            SelectTableIndex =int.Parse( obj.ToString());
        }


        public BookingViewModel()
        {
            _model = new Model();
            ResList = _model.db.ReservationSet.ToList();
            Date = DateTime.Now;
            TbList = _model.db.TablesSet.ToList();
             SelectTableIndex = -1;
            AddPhone = string.Empty;
        }

    }
}
