using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using System.Threading;
using System.Windows;

namespace restaurant_manager.ViewModel
{
  public class StorageViewModel:BaseViewMode
    {
        private bool _can_edit;
        private bool _tablevisible;
        private bool _notfound;
        private bool _isallitemselected;
        private string _search_string;
        private string _searchcombo_string;
        private Product _SelectedItem;

        private Model _model;
        private List<Product> _listproduct;
        private List<Product> _del_listproduct;
        private List<Product> _old_listproduct;
        private Product_category _cur_cat_search;

        static EventWaitHandle _waitHandle = new AutoResetEvent(false);
        public Product_category Cur_cat_search
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
        public string SearchComboString
        {
            set
            {
                _searchcombo_string = value;
                if (string.IsNullOrWhiteSpace(_searchcombo_string) && string.IsNullOrWhiteSpace(_search_string))
                {
                    NotFound = false;

                    ListProduct = _model.db.ProductSet.ToList();
                    UpdateEv?.Invoke(null, null);
                }
                OnPropertyChanged(new PropertyChangedEventArgs("SearchComboString"));
            }
            get
            {
                return _searchcombo_string;
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

                    ListProduct = _model.db.ProductSet.ToList();
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
        private bool _addnew;
        private bool _editmode;
        public List<Product> ListProduct
        {
            set
            {
                _listproduct = value;
                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("ListProduct"));
            }
            get
            {
                return _listproduct;
            }
        }
        public List<Product_category> ListProductCategory { set; get; }
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
        public Product SelectedItem
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

        public StorageViewModel()
        {
            _can_edit = false;
            _model = new Model();
            ListProduct = _model.db.ProductSet.OrderBy(x=>x.Id).ToList();
            AddNew = false;
            ListProductCategory = _model.db.Product_categorySet.ToList(); 
                _del_listproduct = new List<Product>();
            Editmode = true;
            _old_listproduct = ListProduct;


        }
        private DelegateCommand _add_command;
        private DelegateCommand _del_command;
        private DelegateCommand _edit_command;
        private DelegateCommand _save_command;
        private DelegateCommand _search_command;
        private DelegateCommand _endedit_command;

        public bool AddNew
        {
            set
            {
                _addnew = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddNew"));
            }
            get
            {
                return _addnew;
            }

        }
        public bool Editmode
        {
            set
            {
                _editmode = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Editmode"));
            }
            get
            {
                return _editmode;
            }

        }
        public event EventHandler Loading;
        public event EventHandler AddNewProductEvent;
        public bool IsAllItemsSelected
        {
            set
            {
                if (Editmode == false)
                {
                    _isallitemselected = value;
                    foreach (Product item in ListProduct)
                    {
                        item.IsSelected = value;
                        UpdateEv?.Invoke(null, null);
                    }
                    OnPropertyChanged(new PropertyChangedEventArgs("IsAllItemsSelected"));
                    OnPropertyChanged(new PropertyChangedEventArgs("ListProduct"));
                }
            }
            get
            {
                return _isallitemselected;
            }
        }

        public ICommand AddNewProductCommand
        {
            get
            {
                if (_add_command == null)
                {
                    _add_command = new DelegateCommand(AddNewProduct, null);

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
                    _edit_command = new DelegateCommand(Edit, null);

                }
                return _edit_command;
            }
        }
        public ICommand SaveCommand
        {
            get
            {
                if (_save_command == null)
                {
                    _save_command = new DelegateCommand(Save, CanSave);

                }
                return _save_command;
            }
        }
        public ICommand DelProductCommand
        {
            get
            {
                if (_del_command == null)
                {
                    _del_command = new DelegateCommand(DelProduct, CanDelPeoduct);

                }
                return _del_command;
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

        private async void SaveEditedTable(object obj)
        {
            if (SelectedItem != null)
            {
                await _model.db.SaveChangesAsync();
                Product product = _model.db.ProductSet.Where(i => i.Id == SelectedItem.Id).FirstOrDefault();
                if (product == null)
                    return;

                if (product.Name != SelectedItem.Name ||
                    product.Unit != SelectedItem.Unit ||
                    product.count != SelectedItem.count ||
                    product.Product_categoryId != SelectedItem.Product_categoryId)
                {
                    _model.db.ProductSet.Remove(product);
                    _model.db.SaveChanges();
                    _model.db.ProductSet.Add(SelectedItem);
                    _model.db.SaveChanges();
                }

            }
        }

        private bool CanSave(object obj)
        {
            if (_can_edit == true)
            {
                return true;
            }
            return false;
        }
        private async void  Save(object obj)
        {
            Loading.Invoke(obj, null);
            _can_edit = !_can_edit;

            int x = await _model.db.SaveChangesAsync();
            Loading.Invoke(obj, null);
        }
        private void Edit(object obj)
        {
            if (Editmode == true)
            {
                TableVisible = !TableVisible;
                WpfMessageBox.Show("Режим редактирования", "Вы включили режим редактирования.\nВсе изменения будут автоматически сохранены.", MessageBoxButton.OK, MessageBoxImage.Warning);
                TableVisible = !TableVisible;
                Editmode = false;

            }
            else
            {
                Editmode = true;

            }
        }
        private void UpdateListProduct()
        {
            ListProduct = _model.db.ProductSet.ToList();

        }
        private void AddNewProduct(object obj)
        {
            Loading?.Invoke(obj, null);
            AddNewProductEvent?.Invoke(obj, null);
            NotFound = false;
            UpdateListProduct();
            UpdateEv?.Invoke(obj, null);
            Loading?.Invoke(obj, null);

        }
      
        private bool CanDelPeoduct(object obj)
        {
            List<Product> item = ListProduct.Where(i => i.IsSelected == true).ToList();

            if (item.Count() > 0)
                return true;



            return false;
        }
        private async void DelProduct(object obj)
        {

            if (MessageBox.Show("Вы действительно хотите удалить продукт(ы) ?", "Удаление продуктов", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Loading.Invoke(obj, null);
                await Task.Run(() =>
               {
                 List<Product> col=  ListProduct.Where(item => item.IsSelected == true).ToList();
                   ListProduct.RemoveAll(item => item.IsSelected == true);
                   foreach (Product item in col)
                   {
                       _model.db.ProductSet.Remove(item);

                   }
                   _waitHandle.Set();

               });
                _waitHandle.WaitOne();
                _can_edit = true;
                UpdateEv?.Invoke(obj, null);
                Loading?.Invoke(obj, null);
            }
        }
        private bool CanSearch(object obj)
        {
            if (Cur_cat_search != null || (!string.IsNullOrWhiteSpace(SearchString)))
                return true;
            return false;
        }
        private void Search(object obj)
        {
            NotFound = false;


            if (_cur_cat_search != null)
                ListProduct = _model.db.ProductSet.Where(i => i.Product_categoryId == Cur_cat_search.Id).ToList();


            if (!string.IsNullOrWhiteSpace(SearchString))
            {
                ListProduct = _model.db.ProductSet.Where(i => i.Name.Contains(SearchString) | /*i.count.Contains(SearchString) |*/ i.Unit.Contains(SearchString)).ToList();

                if (_cur_cat_search != null)
                {
                    ListProduct = _model.db.ProductSet.Where(i => (i.Name.Contains(SearchString) && i.Product_categoryId == Cur_cat_search.Id)
                    //| (i.count.Contains(SearchString) && i.Product_categoryId == Cur_cat_search.Id)
                    | (i.Unit.Contains(SearchString) && i.Product_categoryId == Cur_cat_search.Id)).ToList();

                }



            }
            if (ListProduct.Count == 0)
            {
                NotFound = true;
            }
            UpdateEv?.Invoke(obj, null);
        }
    }
}
