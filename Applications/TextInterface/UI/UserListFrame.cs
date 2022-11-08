using System.Collections.Generic;
using System.Linq;
using Autofac;
using Common.Bll.Services.Interfaces;
using Data.Dto.Dtos;
using Terminal.Gui;

namespace TextInterface.UI
{
    public sealed class UserListFrame : FrameView
    {
        private Label _lblSearch;
        private TextField _txtSearch;
        private ListView _mainListView;
        
        private readonly IUserService _userService;
        private IReadOnlyCollection<UserDto> _currentUsers;

        public UserListFrame()
        {
            _userService = AutofacConfig.GetContainer().Resolve<IUserService>();
            
            _lblSearch = new Label() {
                X = 0,
                Y = 1,
                Text = "Search: "
            };
            
            _txtSearch = new TextField() {
                X = Pos.Right(_lblSearch),
                Y = _lblSearch.Y,
                Width = Dim.Fill()
            };

            _txtSearch.TextChanged += oldValue =>
            {
                LoadUsers();
            };

            _mainListView = new ListView() {
                X = 0,
                Y = _lblSearch.Y + 1,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            _mainListView.SelectedItemChanged += args =>
            {
                if(_currentUsers is null) return;
                
                var user = _currentUsers.ElementAt(args.Item);
                EventManager.RaiseShowUserDetails(user);
            };
            
            Add(_lblSearch, _txtSearch, _mainListView);
            
            InitEvents();
            LoadUsers();
        }

        private void InitEvents()
        {
            EventManager.OnUserSaved += () =>
            {
                LoadUsers();
            };
        }

        private void LoadUsers()
        {
            _currentUsers = _userService.Search(_txtSearch.Text.ToString());

            var items = _currentUsers.Select(x => $"{x.FirstName} {x.LastName}").ToList();
            
            _mainListView.SetSource(items);
        }
    }
}