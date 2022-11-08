using Autofac;
using Common.Bll.Services.Interfaces;
using Terminal.Gui;

namespace TextInterface.UI
{
    public sealed class UserListWindow : Window
    {
        private readonly UserListFrame _userListFrame;
        private readonly UserDetailsFrame _userDetailsFrame;

        public UserListWindow()
        {
            _userListFrame = new UserListFrame()
            {
                X = 0,
                Y = 0,
                Width = Dim.Percent(50),
                Height = Dim.Fill()
            };

            _userDetailsFrame = new UserDetailsFrame()
            {
                X = Pos.Right(_userListFrame),
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };
            
            Add(_userListFrame, _userDetailsFrame);
            InitEvents();
        }

        private void InitEvents()
        {
            KeyDown += args =>
            {
                if (args.KeyEvent.Key == Key.Esc)
                    EventManager.RaiseGoBackEvent(this);
            };
        }
    }
}