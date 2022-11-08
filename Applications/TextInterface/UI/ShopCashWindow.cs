using System.Threading;
using Autofac;
using Common.Bll.Helpers;
using Common.Bll.Services.Enums;
using Common.Bll.Services.Interfaces;
using Terminal.Gui;

namespace TextInterface.UI
{
    public sealed class ShopCashWindow : Window
    {
        private ShopCashSaleItemsListFrame _saleItemsListFrame;
        private ShopCashScannerFrame _scannerFrame;

        private Button _btnDiscard;
        private Button _btnSave;

        private TextField _txtMoneyGiven;
        private Label _lblForChange;
        private Label _lblChange;
        private Label _lblMoneyGiven;
        private readonly ISaleService _saleService;
        private readonly Label _lblError;

        public ShopCashWindow()
        {
            _saleService = AutofacConfig.GetContainer().Resolve<ISaleService>();

            _saleItemsListFrame = new ShopCashSaleItemsListFrame()
            {
                X = 0,
                Y = 0,
                Width = Dim.Percent(30),
                Height = Dim.Fill()
            };

            _scannerFrame = new ShopCashScannerFrame()
            {
                X = Pos.Right(_saleItemsListFrame),
                Y = 0,
                Width = Dim.Percent(70),
                Height = Dim.Fill(6)
            };

            _lblMoneyGiven = new Label()
            {
                X = Pos.Right(_saleItemsListFrame) + 2,
                Y = Pos.AnchorEnd(5),
                Text = "Money given: "
            };

            _txtMoneyGiven = new TextField()
            {
                X = Pos.Right(_lblMoneyGiven),
                Y = _lblMoneyGiven.Y,
                Width = Dim.Sized(12)
            };

            _txtMoneyGiven.TextChanged += oldValue =>
            {
                if (!Extensions.IsMoneyFormat(_txtMoneyGiven.Text.ToString()))
                    _txtMoneyGiven.Text = oldValue;

                RecalculateChange();
            };

            _lblForChange = new Label()
            {
                X = _lblMoneyGiven.X,
                Y = Pos.Bottom(_lblMoneyGiven) + 1,
                Text = "Change: "
            };

            _lblChange = new Label()
            {
                X = Pos.Right(_lblForChange),
                Y = _lblForChange.Y,
                Width = 12
            };

            _btnSave = new Button()
            {
                X = Pos.Right(_txtMoneyGiven) + 7,
                Y = _txtMoneyGiven.Y,
                Text = "Save"
            };

            _btnDiscard = new Button()
            {
                X = Pos.Right(_txtMoneyGiven) + 6,
                Y = _lblForChange.Y,
                Text = "Discard"
            };

            _lblError = new Label()
            {
                X = _lblForChange.X,
                Y = _lblForChange.Y + 2,
                Width = Dim.Fill(),
                ColorScheme = new ColorScheme()
                {
                    Normal = Terminal.Gui.Attribute.Make(Color.Red, Color.Blue)
                }
            };

            Add(_saleItemsListFrame, _scannerFrame, _lblMoneyGiven, _txtMoneyGiven, _lblForChange, _lblChange, _btnSave,
                _btnDiscard, _lblError);

            EventManager.OnAddItemToBasket += model =>
            {
                Thread.Sleep(100);
                RecalculateChange();
            };

            KeyDown += args =>
            {
                if (args.KeyEvent.Key == Key.Esc)
                    EventManager.RaiseGoBackEvent(this);
            };

            _btnDiscard.Clicked += () => EventManager.RaiseDiscardBasketEvent();

            _btnSave.Clicked += () =>
            {
                var items = _saleItemsListFrame.GetItems();

                var result = _saleService.SaveSale(items, MainApp.User.Id);
                if (result == SaveSaleResult.Ok)
                {
                    EventManager.RaiseDiscardBasketEvent();
                    _lblError.Text = string.Empty;
                }
                else
                {
                    _lblError.Text = "Unexpected error occured";
                }
            };
        }

        private void RecalculateChange()
        {
            var stringMoneyGiven = _txtMoneyGiven.Text.ToString();
            var moneyGiven = string.IsNullOrWhiteSpace(stringMoneyGiven)
                ? decimal.Zero
                : decimal.Parse(stringMoneyGiven);

            var totalCost = _saleItemsListFrame.GetTotalCost();

            _lblChange.Text = totalCost > moneyGiven ? "Insufficient" : (moneyGiven - totalCost).ToString();
        }
    }
}