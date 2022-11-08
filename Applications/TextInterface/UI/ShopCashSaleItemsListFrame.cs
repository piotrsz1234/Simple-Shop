using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Data.Dto.Models;
using Terminal.Gui;

namespace TextInterface.UI
{
    public class ShopCashSaleItemsListFrame : FrameView
    {
        private List<AddSaleProductModel> _currentItems;

        private ListView _mainListView;
        private Button _btnRemove;
        private Label _lblForProductName;
        private Label _lblProductName;
        private Label _lblForCount;
        private Label _lblCount;

        public ShopCashSaleItemsListFrame()
        {
            _currentItems = new List<AddSaleProductModel>();

            _mainListView = new ListView()
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill(7)
            };

            _mainListView.SelectedItemChanged += args => { _btnRemove.Visible = _currentItems.Count > 0;
                DisplayCurrent();
            };

            InitControls();
            
            Add(_mainListView, _btnRemove);

            InitEvents();
        }

        private void DisplayCurrent()
        {
            if (_currentItems.Count == 0) return;
            
            var currentItem = _currentItems.ElementAt(_mainListView.SelectedItem);
            _lblCount.Text = currentItem.Count.ToString();
            _lblProductName.Text = currentItem.ProductName;
        }

        private void InitControls()
        {
            _lblForProductName = new Label()
            {
                X = 1,
                Y = Pos.AnchorEnd() - 5,
                Text = "Name: "
            };

            _lblForCount = new Label()
            {
                X = 0,
                Y = Pos.Bottom(_lblForProductName),
                Text = "Count: "
            };

            _lblProductName = new Label()
            {
                X = Pos.Right(_lblForCount),
                Y = _lblForProductName.Y,
                Width = Dim.Fill()
            };

            _lblCount = new Label()
            {
                X = Pos.Right(_lblForCount),
                Y = _lblForCount.Y,
                Width = Dim.Fill()
            };

            _btnRemove = new Button()
            {
                X = Pos.Center(),
                Y = Pos.Bottom(_lblCount) + 2,
                Text = "Delete",
                Visible = false
            };

            _btnRemove.Clicked += () =>
            {
                _currentItems.RemoveAt(_mainListView.SelectedItem);
                ReloadListView();
            };

            Add(_lblForProductName, _lblForCount, _lblProductName, _lblCount);
        }

        private void InitEvents()
        {
            EventManager.OnAddItemToBasket += (model) =>
            {
                if (_currentItems.Any(x => x.ProductId == model.ProductId))
                {
                    var item = _currentItems.First(x => x.ProductId == model.ProductId);
                    item.Count += model.Count;
                    ReloadListView();
                    return;
                }

                _currentItems.Add(model);
                ReloadListView();
            };
            
            EventManager.OnDiscardBasket += () =>
            {
                _currentItems = new List<AddSaleProductModel>();
                ReloadListView();
            };
        }

        private void ReloadListView()
        {
            _mainListView.Clear();
            if (_currentItems.Count == 0) _btnRemove.Visible = false;
            _mainListView.SetSource(_currentItems.Select(x => x.ProductName).ToList());
        }

        internal decimal GetTotalCost()
        {
            return _currentItems.Sum(x => x.PricePerOne * x.Count);
        }

        internal IReadOnlyCollection<AddSaleProductModel> GetItems()
        {
            return _currentItems;
        }
    }
}