using Autofac;
using Common.Bll.Services.Interfaces;
using Data.Dto.Dtos;
using Data.Dto.Models;
using System.Data;
using Terminal.Gui;

namespace TextInterface.UI
{
    public class ProductListWindow : Window
    {
        private readonly IProductService _productService;
        private IReadOnlyCollection<ProductDto>? _currentProducts;

        public ProductListWindow()
        {
            _currentProducts = Array.Empty<ProductDto>();
            _productService = AutofacConfig.GetContainer().Resolve<IProductService>();

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
                X = 2,
                Y = searchLabel.Y + 1,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            txtSearch.TextChanged += async text => {
                _currentProducts = _productService.Search(new BrowseProductsModel() {
                        Text = txtSearch.Text?.ToString() ?? string.Empty
                    }
                    );

                RedrawTable(mainListView);
            };

            leftFrame.Add(searchLabel, txtSearch, mainListView);
            Add(leftFrame);

            var rightFrame = new ProductAddEditFrame() {
                X = Pos.Right(leftFrame),
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };
            Add(rightFrame);
        }

        private void RedrawTable(ListView listView)
        {
            if (_currentProducts is null) {
                listView.SetSource(Array.Empty<string>());
                return;
            }

            listView.SetSource(_currentProducts.Select(x => x.Name).ToArray());
        }
    }
}