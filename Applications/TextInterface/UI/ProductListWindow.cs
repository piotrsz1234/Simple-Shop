using System;
using System.Collections.Generic;
using Autofac;
using Common.Bll.Services.Interfaces;
using Data.Dto.Dtos;
using Data.Dto.Models;
using System.Data;
using System.Linq;
using Terminal.Gui;

namespace TextInterface.UI
{
    public class ProductListWindow : Window
    {
        private readonly IProductService _productService;
        private IReadOnlyCollection<ProductDto>? _currentProducts;

        public ProductListWindow()
        {
            _productService = AutofacConfig.GetContainer().Resolve<IProductService>();
            _currentProducts = _productService.Search(new BrowseProductsModel());
            
            var leftFrame = new FrameView() {
                X = 0,
                Y = 1,
                Width = Dim.Percent(50),
                Height = Dim.Fill()
            };

            var searchLabel = new Label() {
                X = 0,
                Y = 1,
                Text = "Search: "
            };

            var txtSearch = new TextField() {
                X = Pos.Right(searchLabel),
                Y = searchLabel.Y,
                Width = Dim.Fill()
            };

            var mainListView = new ListView() {
                X = 0,
                Y = searchLabel.Y + 1,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            txtSearch.TextChanged += text => {
                Search(txtSearch, mainListView);
            };

            mainListView.SelectedItemChanged += args =>
            {
                EventManager.RaiseShowProductDetailsEvent(_currentProducts.ElementAt(args.Item));
            };
            
            RedrawTable(mainListView);
            
            leftFrame.Add(searchLabel, txtSearch, mainListView);
            Add(leftFrame);

            var rightFrame = new ProductAddEditFrame() {
                X = Pos.Right(leftFrame),
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };
            
            Add(rightFrame);

            EventManager.OnProductSaved += () =>
            {
                Search(txtSearch, mainListView);
            };

            this.KeyDown += args =>
            {
                if (args.KeyEvent.Key == Key.Esc)
                {
                    EventManager.RaiseGoBackEvent(this);
                }
            };
        }

        private void RedrawTable(ListView listView)
        {
            if (_currentProducts is null) {
                listView.SetSource(Array.Empty<string>());
                return;
            }

            listView.SetSource(_currentProducts.Select(x => x.Name).ToArray());
        }

        private void Search(TextField txtSearch, ListView mainListView)
        {
            _currentProducts = _productService.Search(new BrowseProductsModel() {
                    Text = txtSearch.Text?.ToString() ?? string.Empty
                }
            );

            RedrawTable(mainListView);
        }
    }
}