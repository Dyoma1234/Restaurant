using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
namespace restaurant_manager.ViewModel
{
    class MenuViewModel : BaseViewMode
    {
        private bool _isallitemselected;
        private Model _model;
        private List<Dishes> _disheslist;
        private List<Dishes_categories> _categories;
        private Dishes_categories _cur_cat_search;
        private string _search_string;
        private string _searchcombo_string;
        private bool _editmode;
        private bool _tablevisible;
        private bool _addvisible;
        private bool _helpbtnvisibility;
        private bool _notfound;
        private bool _addnewdish;
        private string _addnewname;
        private string _addnewcat;
        private string _addnewweight;
        private string _addnewpric;
        private Dishes _SelectedItem;
        public bool AddNewDish
        {
            set
            {
                _addnewdish = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddNewDish"));
            }
            get
            {
                return _addnewdish;
            }
        }
        public bool TableVisible
        {
            set
            {
                _tablevisible = value;
                OnPropertyChanged(new PropertyChangedEventArgs("TableVisible"));
            }
            get
            {
                return _tablevisible;
            }
        }
        public bool AddVisible
        {
            set
            {
                _addvisible = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddVisible"));
            }
            get
            {
                return _addvisible;
            }
        }
        public bool HelpBtnVisibility
        {
            set
            {
                _helpbtnvisibility = value;
                OnPropertyChanged(new PropertyChangedEventArgs("HelpBtnVisibility"));
            }
            get
            {
                return _helpbtnvisibility;
            }
        }
        public bool NotFound
        {
            set
            {
                _notfound = value;
                OnPropertyChanged(new PropertyChangedEventArgs("NotFound"));
            }
            get
            {
                return _notfound;
            }
        }

        public string SearchComboString
        {
            set
            {
                _searchcombo_string = value;
                if (string.IsNullOrWhiteSpace(_searchcombo_string) && string.IsNullOrWhiteSpace(_search_string))
                {
                    NotFound = false;

                    DishesList = _model.db.DishesSet.ToList();
                    UpdateEv?.Invoke(null, null);
                }
                OnPropertyChanged(new PropertyChangedEventArgs("SearchComboString"));
            }
            get
            {
                return _searchcombo_string;
            }
        }
        public bool EditMode
        {
            set
            {
                _editmode = value;
                OnPropertyChanged(new PropertyChangedEventArgs("EditMode"));
            }
            get
            {
                return _editmode;
            }
        }
        public string SearchString
        {
            set
            {
                _search_string = value;
                if (string.IsNullOrWhiteSpace(_searchcombo_string) && string.IsNullOrWhiteSpace(_search_string))
                {
                    NotFound = false;

                    DishesList = _model.db.DishesSet.ToList();
                    UpdateEv?.Invoke(null, null);
                }
                OnPropertyChanged(new PropertyChangedEventArgs("SearchString"));
            }
            get
            {
                return _search_string;
            }
        }
        public event EventHandler UpdateEv;
        public List<Dishes_categories> Categories
        {
            set
            {
                _categories = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Categories"));
            }
            get
            {
                return _categories;
            }
        }
        public Dishes_categories Cur_cat_search
        {
            set
            {
                _cur_cat_search = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Cur_cat_search"));
            }
            get
            {
                return _cur_cat_search;
            }
        }
        public List<Dishes> DishesList
        {
            set
            {
                _disheslist = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DishesList"));
            }
            get {
                return _disheslist;
            }
        }
        public List<Dishes_categories> DishesCategoriesList { set; get; }
        public Dishes SelectedItem
        {
            set
            {
                _SelectedItem = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedItem"));
            }
            get
            {
                return _SelectedItem;
            }
        }
        public bool IsAllItemsSelected
        {
            set
            {
                if (EditMode == false)
                {
                    _isallitemselected = value;
                    foreach (Dishes item in DishesList)
                    {
                        item.IsSelected = value;
                        UpdateEv?.Invoke(null, null);
                    }
                    OnPropertyChanged(new PropertyChangedEventArgs("IsAllItemsSelected"));
                    OnPropertyChanged(new PropertyChangedEventArgs("DishesList"));
                }
            }
            get
            {
                return _isallitemselected;
            }
        }

        public string AddNewName
        {
            set
            {
                _addnewname = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddNewName"));
            }
            get
            {

                return _addnewname;
            }
        }
        public string AddNewCat
        {
            set
            {
                _addnewcat = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddNewCat"));
            }
            get
            {

                return _addnewcat;
            }
        }
        public string AddNewWeight
        {
            set
            {
                _addnewweight = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddNewWeight"));
            }
            get
            {

                return _addnewweight;
            }
        }
        public string AddNewPric
        {
            set
            {
                _addnewpric = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddNewPric"));
            }
            get
            {

                return _addnewpric;
            }
        }


        private DelegateCommand _endedit_command;
        private DelegateCommand _search_command;
        private DelegateCommand _del_menu_command;
        private DelegateCommand _edit_command;
        private DelegateCommand _addnewdish_command;
        private DelegateCommand _adddish_command;
        private DelegateCommand _closeadddishes_command;


        public ICommand EndEdit
        {
            get
            {
                if (_endedit_command == null)
                {
                    _endedit_command = new DelegateCommand(SaveEditedTable, null);

                }
                return _endedit_command;
            }
        }
        public ICommand SearchCommand
        {
            get
            {
                if (_search_command == null)
                {
                    _search_command = new DelegateCommand(Search, CanSearch);
                }
                return _search_command;
            }
        }
        public ICommand DelMenuCommand
        {
            get
            {
                if (_del_menu_command == null)
                {
                    _del_menu_command = new DelegateCommand(Del, CanDel);
                }
                return _del_menu_command;
            }
        }
        public ICommand EditMenuCommand
        {
            get
            {
                if (_edit_command == null)
                {
                    _edit_command = new DelegateCommand(EditModeC, null);
                }
                return _edit_command;
            }
        } 
        public ICommand AddNewDishCommand
        {
            get
            {
                if (_addnewdish_command == null)
                {
                    _addnewdish_command = new DelegateCommand(AddNewDishC, null);
                }
                return _addnewdish_command;
            }
        }
        public ICommand CloseAddDishesCommand
        {
            get
            {
                if (_closeadddishes_command == null)
                {
                    _closeadddishes_command = new DelegateCommand(CloseAddDish, null);
                }
                return _closeadddishes_command;
            }
        }

        

        public ICommand AddDishCommand
        {
            get
            {
                if (_adddish_command == null)
                {
                    _adddish_command = new DelegateCommand(CrateNewDish, CanCrateNewDish);
                }
                return _adddish_command;
            }
        }

        private bool CanCrateNewDish(object obj)
        {
            return (!(string.IsNullOrWhiteSpace(AddNewName) || string.IsNullOrWhiteSpace(AddNewCat) || string.IsNullOrWhiteSpace(AddNewWeight) || string.IsNullOrWhiteSpace(AddNewPric)));
        
        }

        private async void CrateNewDish(object obj)
        {
            Dishes new_dish = new Dishes();
            new_dish.Name = AddNewName;
            new_dish.Dishes_categories = _model.db.Dishes_categoriesSet.FirstOrDefault(i => i.Name == AddNewCat);
            new_dish.Dishes_categoriesId = _model.db.Dishes_categoriesSet.FirstOrDefault(i => i.Name == AddNewCat).Id;
            new_dish.Weight = AddNewWeight;
            new_dish.Price = AddNewPric;
            new_dish.IsSelected = false;




                _model.db.DishesSet.Add(new_dish);
                await _model.db.SaveChangesAsync();

                DishesList.Add(new_dish);
                OnPropertyChanged(new PropertyChangedEventArgs("DishesList"));
                Clear();
              WpfMessageBox.Show("Добавление", "Новое блюдо успешно добавлено в меню", MessageBoxType.Information);
                TableVisible = false;
                AddVisible = false;
                HelpBtnVisibility = true;
                UpdateEv?.Invoke(obj, null);

        }

        private void AddNewDishC(object obj)
        {
            TableVisible = true;
            AddVisible = true;
            HelpBtnVisibility = false;
        }
        private void CloseAddDish(object obj)
        {
            TableVisible = false;
            AddVisible = false;
            HelpBtnVisibility = true;
            Clear();

        }
        private void Clear()
        {
            AddNewName = string.Empty;
            AddNewPric = string.Empty;
            AddNewWeight = string.Empty;
            AddNewCat = string.Empty;
        }
        private void EditModeC(object obj)
        {
            if (EditMode == true)
            {
                TableVisible = !TableVisible;
                WpfMessageBox.Show("Режим редактирования", "Вы включили режим редактирования.\nВсе изменения будут автоматически сохранены.", MessageBoxButton.OK, MessageBoxImage.Warning);
                TableVisible = !TableVisible;
              
                EditMode = false;

            }
            else
            {
                EditMode = true;
       
            }
        }

        private bool CanDel(object obj)
        {
            List<Dishes> item = DishesList.Where(i => i.IsSelected == true).ToList();

            if (item.Count() > 0)
                return true;



            return false;
        }

        private async void Del(object obj)
        {
            TableVisible = true;
            HelpBtnVisibility = false;
            if (MessageBoxResult.Yes == WpfMessageBox.Show("Удаление", "Вы дейстивтельно хотите удалить выбранные элементы?\nОтменить это действие будет не возможно.", MessageBoxButton.YesNo, MessageBoxImage.Warning))
            {
                HelpBtnVisibility = true;
                EditMode = true;
                var DelList = DishesList.FindAll(i => i.IsSelected == true);
                foreach (var item in DelList)
                {
                    _model.db.DishesSet.Remove(item);
                    DishesList.Remove(item);
                }

                await _model.db.SaveChangesAsync();
                OnPropertyChanged(new PropertyChangedEventArgs("DishesList"));
                EditMode = false;
                TableVisible = false;
                UpdateEv(obj, null);

            }
            else
            {
                HelpBtnVisibility = true;
                TableVisible = false;

            }
        }

        private bool CanSearch(object obj)
        {
            if (Cur_cat_search != null||(!string.IsNullOrWhiteSpace(SearchString)))
                return true;
            return false;
        }

        private void Search(object obj)
        {
            NotFound = false;


            if (_cur_cat_search != null)
                    DishesList = _model.db.DishesSet.Where(i => i.Dishes_categoriesId == Cur_cat_search.Id).ToList();
            
           
                if (!string.IsNullOrWhiteSpace(SearchString))
                {
                    DishesList = _model.db.DishesSet.Where(i => i.Name.Contains(SearchString) | i.Price.Contains(SearchString) | i.Weight.Contains(SearchString)).ToList();

                    if (_cur_cat_search != null)
                    {
                        DishesList = _model.db.DishesSet.Where(i => (i.Name.Contains(SearchString) && i.Dishes_categoriesId == Cur_cat_search.Id) 
                        | (i.Price.Contains(SearchString) && i.Dishes_categoriesId == Cur_cat_search.Id )
                        | (i.Weight.Contains(SearchString) && i.Dishes_categoriesId == Cur_cat_search.Id)).ToList();

                    }



                }
            if(DishesList.Count == 0)
            {
                NotFound = true;
            }
            UpdateEv?.Invoke(obj, null);
        }

        private async  void SaveEditedTable(object obj)
        {
            if (SelectedItem != null)
            {
                await _model.db.SaveChangesAsync();
                Dishes dishes = _model.db.DishesSet.Where(i => i.Id == SelectedItem.Id).FirstOrDefault();
                if (dishes == null)
                    return;

                if (dishes.Name != SelectedItem.Name ||
                    dishes.Price != SelectedItem.Price ||
                    dishes.Weight != SelectedItem.Weight ||
                    dishes.Dishes_categoriesId != SelectedItem.Dishes_categoriesId)
                {
                    _model.db.DishesSet.Remove(dishes);
                    _model.db.SaveChanges();
                    _model.db.DishesSet.Add(SelectedItem);
                    _model.db.SaveChanges();
                }
                
            }
        }

        public MenuViewModel()
        {
            _model = new Model();
            DishesList = _model.db.DishesSet.ToList();
            DishesCategoriesList = _model.db.Dishes_categoriesSet.ToList();
            IsAllItemsSelected = false;
            EditMode = true;
            HelpBtnVisibility = true;
            TableVisible = false;
            AddNewDish = false ;
            AddVisible = false;
            NotFound = false;
            AddNewPric = string.Empty;

        }
    }
}
