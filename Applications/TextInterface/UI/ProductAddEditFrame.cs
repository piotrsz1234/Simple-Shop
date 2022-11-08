using System;
using System.Globalization;
using System.Threading.Tasks;
using Autofac;
using Common.Bll.Helpers;
using Common.Bll.Services.Enums;
using Common.Bll.Services.Interfaces;
using Data.Dto.Dtos;
using Data.Dto.Models;
using Terminal.Gui;

namespace TextInterface.UI
{
    public class ProductAddEditFrame : FrameView
    {
        private Label _lblName;
        private Label _lblBarcode;
        private Label _lblPrice;
        private Label _lblAgeRestricted;
        private Label _lblCountable;
        private Label _lblVATPercentage;
        private Label _lblError;

        private Button _btnAdd;
        private Button _btnSave;
        private Button _btnDelete;

        private TextField _txtName;
        private TextField _txtBarcode;
        private TextField _txtPrice;
        private CheckBox _cbxAgeRestricted;
        private CheckBox _cbxCountable;
        private TextField _txtVATPercentage;

        private ProductDto? _current;

        private IProductService _productService;
        
        public ProductAddEditFrame()
        {
            _productService = AutofacConfig.GetContainer().Resolve<IProductService>();
            SetLabels();
            SetInputs();
            
            _lblError = new Label()
            {
                X = 0,
                Y = Pos.Bottom(_btnSave) + 2,
                Width = Dim.Fill(),
                TextAlignment = TextAlignment.Centered,
                ColorScheme = new ColorScheme() {
                    Normal = Terminal.Gui.Attribute.Make(Color.Red, Color.Blue)
                }
            };
            
            Add(_lblError);
            
            InitControls();
            InitEvents();
        }

        private void SetLabels()
        {
            _lblName = new Label()
            {
                X = 0,
                Y = 2,
                Text = "Name: ",
                Width = 6,
            };

            _lblBarcode = new Label()
            {
                X = 0,
                Y = Pos.Bottom(_lblName) + 1,
                Text = "Barcode: "
            };

            _lblPrice = new Label()
            {
                X = 0,
                Y = Pos.Bottom(_lblBarcode) + 1,
                Text = "Price: "
            };
            
            _lblAgeRestricted = new Label()
            {
                X = 0,
                Y = Pos.Bottom(_lblPrice) + 1,
                Text = "Age restricted: "
            };

            _lblCountable = new Label()
            {
                X = 0,
                Y = Pos.Bottom(_lblAgeRestricted) + 1,
                Text = "Countable: "
            };

            _lblVATPercentage = new Label()
            {
                X = 0,
                Y = Pos.Bottom(_lblCountable) + 1,
                Text = "VAT percentage: "
            };

            Add(_lblName, _lblBarcode, _lblPrice, _lblAgeRestricted, _lblCountable, _lblVATPercentage);
        }

        private void SetInputs()
        {
            _btnAdd = new Button()
            {
                X = Pos.Center(),
                Y = 0,
                Text = "Add new"
            };

            _btnSave = new Button()
            {
                X = Pos.Center(),
                Y = Pos.Bottom(_lblVATPercentage) + 2,
                Text = "Save"
            };

            _btnDelete = new Button()
            {
                X = Pos.Center(),
                Y = _btnSave.Y + 2,
                Text = "Delete",
                Visible = false
            };
            
            Add(_btnAdd);

            _txtName = new TextField()
            {
                X = Pos.Right(_lblAgeRestricted),
                Y = _lblName.Y,
                Width = Dim.Fill()
            };

            _txtBarcode = new TextField()
            {
                X = Pos.Right(_lblAgeRestricted),
                Y = _lblBarcode.Y,
                Width = Dim.Fill()
            };
            
            _txtPrice = new TextField()
            {
                X = Pos.Right(_lblAgeRestricted),
                Y = _lblPrice.Y,
                Width = Dim.Fill(),
            };

            _cbxAgeRestricted = new CheckBox()
            {
                X = Pos.Right(_lblAgeRestricted),
                Y = _lblAgeRestricted.Y
            };

            _cbxCountable = new CheckBox()
            {
                X = Pos.Right(_lblAgeRestricted),
                Y = _lblCountable.Y
            };
            
            _txtVATPercentage = new TextField()
            {
                X = Pos.Right(_lblAgeRestricted),
                Y = _lblVATPercentage.Y,
                Width = Dim.Fill(),
            };

            Add(_txtName, _txtBarcode, _txtPrice,_cbxAgeRestricted, _cbxCountable, _txtVATPercentage, _btnSave, _btnDelete);
        }

        private void InitControls()
        {
            _txtPrice.TextChanged += oldValue =>
            {
                if (!Extensions.IsMoneyFormat(_txtPrice.Text.ToString()?.Trim()))
                    _txtPrice.Text = oldValue;
            };

            _txtVATPercentage.TextChanged += oldValue =>
            {
                if (!Extensions.IsInteger(_txtVATPercentage.Text.ToString()?.Trim()))
                    _txtVATPercentage.Text = oldValue;
            };

            _btnAdd.Clicked += () =>
            {
                _current = null;
                LoadDetailsToForm();
            };

            _btnSave.Clicked += () =>
            {
                SaveProduct();
            };

            _btnDelete.Clicked += () =>
            {
                DeleteProduct();
            };
        }

        private void InitEvents()
        {
            EventManager.OnShowProductDetails += product =>
            {
                _current = product;
                LoadDetailsToForm();
            };
        }

        private void LoadDetailsToForm()
        {
            if (_current is null)
            {
                _txtName.Text = "";
                _txtBarcode.Text = "";
                _txtPrice.Text = "";
                _cbxAgeRestricted.Checked = false;
                _cbxCountable.Checked = false;
                _txtVATPercentage.Text = "";
                _btnDelete.Visible = false;
                
                return;
            }

            _txtName.Text = _current.Name;
            _txtBarcode.Text = _current.Barcode;
            _txtPrice.Text = _current.Price.ToString(CultureInfo.InvariantCulture);
            _cbxAgeRestricted.Checked = _current.IsAgeRestricted;
            _cbxCountable.Checked = _current.IsCountable;
            _txtVATPercentage.Text = _current.VATPercentage.ToString(CultureInfo.InvariantCulture);
            _btnDelete.Visible = true;
        }
        
        private void SaveProduct()
        {
            var product = new AddEditProductModel()
            {
                Id = _current?.Id,
                Name = _txtName.Text.ToString(),
                Barcode = _txtBarcode.Text.ToString(),
                Price = !string.IsNullOrWhiteSpace(_txtPrice.Text.ToString()) ? decimal.Parse(_txtPrice.Text.ToString()) : Decimal.Zero,
                IsCountable = _cbxCountable.Checked,
                IsAgeRestricted = _cbxAgeRestricted.Checked,
                VATPercentage = !string.IsNullOrWhiteSpace(_txtVATPercentage.Text.ToString()) ? int.Parse(_txtVATPercentage.Text.ToString()) : 0
            };

            var result = _productService.AddEditProduct(product);

            switch (result)
            {
                case AddEditProductResult.Ok:
                    _current = null;
                    _lblError.Text = string.Empty;
                    LoadDetailsToForm();
                    EventManager.RaiseProductSavedEvent();
                    break;
                case AddEditProductResult.BarcodeAlreadyExists:
                    _lblError.Text = "Product with such barcode already exists";
                    break;
                case AddEditProductResult.Error:
                    _lblError.Text = "Error while saving. Check is all fields are filled!";
                    break;
            }
        }
        
        private void DeleteProduct()
        {
            if (_current != null && _productService.RemoveProduct(_current.Id))
            {
                EventManager.RaiseProductSavedEvent();
                _lblError.Text = string.Empty;
                _current = null;
                LoadDetailsToForm();
            }
            else
            {
                _lblError.Text = "Error while removing product";
            }
        }
    }
}