using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace restaurant_manager.ViewModel
{
    class AddNewProductViewModel: BaseViewMode
    {
        public event EventHandler AddNew;
        private Model _model;
        private DelegateCommand _save_command;
        
        public List<Product_category> Categories
        {
            set
            {
                _categories = value;
                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("Categories"));
            }
            get
            {
                return _categories;
            }
        }
        public Product_category Cur_cat
        {
            set
            {
                _cur_cat = value;
                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("Cur_cat"));
            }
            get
            {
                return _cur_cat;
            }
        }
        private string _name;
        private string _count;
        private string _type;
        private List<Product_category> _categories;
        private Product_category _cur_cat;
        private string _Cur_catstr;
        public string Cur_catstr
        {
            set
            {
                _Cur_catstr = value;
                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("Cur_catstr"));
            }
            get
            {
                return _Cur_catstr;
            }
        }
        public string Name
        {
            set
            {
                _name = value;
                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("Name"));

            }
            get
            {
                return _name;
            }
        }
  
        public string Count
        {
            set
            {
                _count = value;
                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("Count"));

            }
            get
            {
                return _count;
            }
        }
        public string Type
        {
            set
            {
                _type = value;
                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("Type"));

            }
            get
            {
                return _type;
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
        private void Clear()
        {
            Name = string.Empty;
            Count = string.Empty;
            Cur_cat = null;
            Type = string.Empty;
            Cur_catstr = string.Empty;
        }
        public AddNewProductViewModel()
        {
            _model = new Model();
            Categories = _model.db.Product_categorySet.ToList();
            
        }
        private bool CanSave(object obj)
        {

            if (string.IsNullOrWhiteSpace(_name))
                return false;
            if(string.IsNullOrWhiteSpace(_count))
                return false;

            if (string.IsNullOrWhiteSpace(_type))
                return false;
            if (string.IsNullOrWhiteSpace(_Cur_catstr))
                return false;
            return true;
        }
        private async void Save(object obj)
        {
            Product temp = new Product();
            temp.Name = _name;
            long i= new long();
            long.TryParse (_count, out i);
            temp.count = i;
            temp.Unit = _type;
            temp.Product_category = Cur_cat;
            _model.db.ProductSet.Add(temp);
            await _model.db.SaveChangesAsync();
            AddNew?.Invoke(obj, null);
            Clear();
            WpfMessageBox.Show("Добавление", "Продукты успешно добавлены!", MessageBoxType.Information);

        }

    }
}
