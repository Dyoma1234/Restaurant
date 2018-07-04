using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
namespace restaurant_manager.ViewModel
{
    class CheckViewModel:BaseViewMode
    {
        private string _sum;
        public string Sum
        {
            set
            {
                _sum = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Sum"));
            }
            get
            {
                return _sum;
            }
        }
        public event EventHandler Update;
        private Model _model;
        private List<Dishes> _menulist;
        private List<Dishes> _checkmenulist;
        private Check _check;
        private string _searchstr;
        public Check Check
        {
            set
            {
                _check = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Check"));
            }
            get
            {
                return _check;
            }
        }
        private Dishes _selectedmenudish;
        private Dishes _CheckSelectedItem;
        public List<Dishes> CheckMenuList
        {
            set
            {
                _checkmenulist = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CheckMenuList"));
            }
            get
            {
                return _checkmenulist;
            }
        }


        public string SearchStr
        {
            set
            {
                _searchstr = value;
                if (string.IsNullOrWhiteSpace(_searchstr))
                {
                    MenuList = _model.db.DishesSet.ToList();

                }
                else
                {
                    MenuList = _model.db.DishesSet.Where(i => i.Name.Contains(_searchstr) | i.Price.Contains(_searchstr) | i.Dishes_categories.Name.Contains(_searchstr) | i.Weight.Contains(_searchstr)).ToList();


                    OnPropertyChanged(new PropertyChangedEventArgs("MenuList"));
                    OnPropertyChanged(new PropertyChangedEventArgs("SearchStr"));
                    Update?.Invoke(null, null);
                }
            }
            get
            {
                return _searchstr;
            }
        }
        public List<Dishes> MenuList
        {
            set
            {
                _menulist = value;
                OnPropertyChanged(new PropertyChangedEventArgs("MenuList"));
            }
            get
            {
                return _menulist;
            }
        }
        public Dishes SelectedMenuDish
        {
            set
            {
                _selectedmenudish = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedMenuDish"));
            }
            get
            {
                return _selectedmenudish;
            }
        }
        public Dishes CheckSelectedItem
        {
            set
            {
                _CheckSelectedItem = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CheckSelectedItem"));
            }
            get
            {
                return _CheckSelectedItem;
            }
        }

        private DelegateCommand _AddDishToCheckCommand;
        private DelegateCommand _EraseDishFromCheck;
        private DelegateCommand _ClearCurCheck;
        public ICommand ClearCurCheck
        {
            get
            {
                if (_ClearCurCheck == null)
                {
                    _ClearCurCheck = new DelegateCommand(ClearCur, null);
                }
                return _ClearCurCheck;
            }
        }

        private void ClearCur(object obj)
        {
           if(WpfMessageBox.Show("Очистить чек","Вы действительно хотите сбросить чек ?", MessageBoxType.ConfirmationWithYesNo) == System.Windows.MessageBoxResult.Yes)
            {

            }
        }

        public ICommand AddDishToCheckCommand
        {
            get
            {
                if (_AddDishToCheckCommand == null)
                {
                    _AddDishToCheckCommand = new DelegateCommand(AddDishToCHek, null);
                }
                return _AddDishToCheckCommand;
            }
        }
        public ICommand EraseDishFromCheck
        {
            get
            {
                if (_EraseDishFromCheck == null)
                {
                    _EraseDishFromCheck = new DelegateCommand(EraseFromCheck, CanEraseFromCheck);
                }
                return _EraseDishFromCheck;
            }
        }

        private bool CanEraseFromCheck(object obj)
        {
           if(CheckSelectedItem==null)
                return false;

            return true;
        }

        private void EraseFromCheck(object obj)
        {
            Dishes temp = CheckSelectedItem;
            CheckSelectedItem = null;
            CheckMenuList.Remove(temp);
            GetSum();
            Check.Dishes = CheckMenuList;
            OnPropertyChanged(new PropertyChangedEventArgs("CheckMenuList"));
            OnPropertyChanged(new PropertyChangedEventArgs("Check"));
            Update?.Invoke(obj, null);
        }

        private void AddDishToCHek(object obj)
        {
            if (SelectedMenuDish == null)
                return;

            CheckMenuList.Add(SelectedMenuDish);
            GetSum();
            Check.Dishes = CheckMenuList;
            OnPropertyChanged(new PropertyChangedEventArgs("CheckMenuList"));
            OnPropertyChanged(new PropertyChangedEventArgs("Check"));
            Update?.Invoke(obj, null);
        }
        private void GetSum()
        {
            double sum = 0;
            foreach (var item in CheckMenuList)
            {
                sum += double.Parse(item.Price);
            }
            Sum = sum.ToString();
        }
        public CheckViewModel()
        {
            _model = new Model();
            MenuList = _model.db.DishesSet.ToList();
            Check = new Check();
            CheckMenuList = new List<Dishes>();
            Sum = "0";

        }
    }
}
