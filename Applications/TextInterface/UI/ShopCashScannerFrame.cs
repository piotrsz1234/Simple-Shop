using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Common.Bll.Helpers;
using Common.Bll.Services.Interfaces;
using Data.Dto.Dtos;
using Data.Dto.Models;
using Terminal.Gui;

namespace TextInterface.UI
{
    public sealed class ShopCashScannerFrame : FrameView
    {
        private Label _lblError;
        private Label _lblSearch;
        private TextField _txtSearch;
        private ListView _mainListView;
        private TextField _txtCount;
        private TextField _txtBarcode;
        private readonly Label _lblCount;
        private readonly Label _lblBarcode;
        private readonly Label _lblIsOfAge;
        private readonly CheckBox _cbxIsOfAge;

        private IReadOnlyCollection<ProductDto> _currentSearch;

        private IProductService _productService;
        private readonly Button _btnScan;

        public ShopCashScannerFrame()
        {
            _currentSearch = Array.Empty<ProductDto>();
            
            var container = AutofacConfig.GetContainer();
            _productService = container.Resolve<IProductService>();
            
            _lblSearch = new Label()
            {
                X = 0, 
                Y = 0,
                Text = "Search: "
            };

            _txtSearch = new TextField()
            {
                X = Pos.Right(_lblSearch),
                Y = _lblSearch.Y,
                Width = Dim.Fill()
            };

            _mainListView = new ListView()
            {
                X = 0,
                Y = Pos.Bottom(_lblSearch),
                Width = Dim.Fill(),
                Height = Dim.Fill(8)
            };

            _lblCount = new Label()
            {
                X = 0,
                Y = Pos.AnchorEnd() - 2,
                Text = "Count: "
            };
            
            _txtCount = new TextField()
            {
                X = Pos.Right(_lblCount) + 1,
                Y = Pos.AnchorEnd() - 2,
                Width = Dim.Sized(15)
            };

            _lblBarcode = new Label()
            {
                X = Pos.Right(_txtCount) + 1,
                Y = _txtCount.Y,
                Text = "Barcode: "
            };
            
            _txtBarcode = new TextField()
            {
                X = Pos.Right(_lblBarcode) + 1,
                Y = _lblBarcode.Y,
                Width = Dim.Sized(20)
            };

            _txtSearch.TextChanged += oldValue =>
            {
                if (string.IsNullOrWhiteSpace(_txtSearch.Text.ToString()))
                {
                    _currentSearch = Array.Empty<ProductDto>();
                    return;
                }

                _currentSearch = _productService.Search(new BrowseProductsModel()
                {
                    Text = _txtSearch.Text.ToString()
                });

                ReloadListView();
            };
            
            _btnScan = new Button()
            {
                X = Pos.Right(_txtBarcode) + 2,
                Y = _txtBarcode.Y,
                Text = "Scan"
            };

            _lblError = new Label()
            {
                X = 0,
                Y = Pos.Bottom(_btnScan),
                Width = Dim.Fill(),
                TextAlignment = TextAlignment.Centered,
                ColorScheme = new ColorScheme()
                {
                    Normal = Terminal.Gui.Attribute.Make(Color.Red, Color.Blue)
                }
            };
            
            Add(_lblSearch, _txtSearch, _mainListView, _lblCount, _txtCount, _lblBarcode, _txtBarcode, _btnScan, _lblError);

            InitControls();

            EventManager.OnDiscardBasket += () =>
            {
                _txtSearch.Text = string.Empty;
                _txtBarcode.Text = string.Empty;
                _txtCount.Text = string.Empty;
                _currentSearch = Array.Empty<ProductDto>();
            };
        }

        private void InitControls()
        {
            _mainListView.SelectedItemChanged += args =>
            {
                if(_currentSearch.Count == 0) return;

                var currentProduct = _currentSearch.ElementAt(_mainListView.SelectedItem);

                _txtCount.Text = currentProduct.IsCountable ? "1" : "";

                _txtBarcode.Text = currentProduct.Barcode;
            };

            _btnScan.Clicked += () =>
            {
                AddItem();
            };

            _txtBarcode.KeyDown += args =>
            {
                if (args.KeyEvent.Key == Key.Enter)
                {
                    AddItem();
                }
            };
            
            _txtCount.KeyDown += args =>
            {
                if (args.KeyEvent.Key == Key.Enter)
                {
                    AddItem();
                }
            };

            _txtCount.TextChanged += oldValue =>
            {
                if (!Extensions.IsUDecimal(_txtCount.Text.ToString()))
                    _txtCount.Text = oldValue;
            };
        }

        private void AddItem()
        {
            _lblError.Text = string.Empty;
            if (string.IsNullOrWhiteSpace(_txtBarcode.Text.ToString()) || string.IsNullOrWhiteSpace(_txtCount.Text.ToString()))
            {
                _lblError.Text = "No barcode or amount provided";
                return;
            }

            var currentProduct = _productService.GetOneByBarcode(_txtBarcode.Text.ToString());

            if (currentProduct is null)
            {
                _lblError.Text = "Wrong barcode!";
                return;
            }

            if (currentProduct.IsCountable && !Extensions.IsInteger(_txtCount.Text.ToString()))
            {
                _lblError.Text = "Product is countable";
                return;
            }

            if (!currentProduct.IsCountable && !Extensions.IsUDecimal(_txtCount.Text.ToString()))
            {
                _lblError.Text = "Product should have amount";
                return;
            }

            EventManager.RaiseAddItemToBasketEvent(new AddSaleProductModel()
            {
                Count = decimal.Parse(_txtCount.Text.ToString()),
                IsAgeRestricted = currentProduct.IsAgeRestricted,
                ProductId = currentProduct.Id,
                ProductName = currentProduct.Name,
                PricePerOne = currentProduct.Price
            });

            _txtCount.Text = _txtBarcode.Text = string.Empty;
            
            _txtBarcode.SetFocus();
        }
        
        private void ReloadListView()
        {
            _mainListView.SetSource(_currentSearch.Select(x => x.Name).ToArray());
        }
    }
}