using System;
using Autofac;
using Common.Bll.Services.Enums;
using Common.Bll.Services.Interfaces;
using Data.Dto.Dtos;
using Data.Dto.Models;
using Terminal.Gui;

namespace TextInterface.UI
{
    public sealed class UserDetailsFrame : FrameView
    {
        private Button _btnAdd;
        private Button _btnSave;
        private Button _btnDelete;
        
        private Label _lblFirstName;
        private Label _lblLastName;
        private Label _lblCodeName;
        private Label _lblIsAdmin;
        private Label _lblError;

        private TextField _txtFirstName;
        private TextField _txtLastName;
        private TextField _txtCodeName;
        private CheckBox _cbxIsAdmin;

        private UserDto? _currentUser;
        private IUserService _userService;
        
        public UserDetailsFrame()
        {
            _userService = AutofacConfig.GetContainer().Resolve<IUserService>();
            
            _btnAdd = new Button()
            {
                X = Pos.Center(),
                Y = 0,
                Text = "Add User"
            };

            _btnAdd.Clicked += () =>
            {
                _currentUser = null;
                LoadUserDetails();
            };
            
            Add(_btnAdd);
            
            SetLabels();
            SetInputs();

            _btnSave = new Button()
            {
                X = Pos.Center(),
                Y = Pos.Bottom(_lblIsAdmin) + 4,
                Text = "Save"
            };

            _btnSave.Clicked += () =>
            {
                SaveUser();
            };
            
            _btnDelete = new Button()
            {
                X = Pos.Center(),
                Y = Pos.Bottom(_btnSave) + 1,
                Text = "Delete",
                Visible = false
            };
            
            _btnDelete.Clicked += () =>
            {
                DeleteUser();
            };
            
            Add(_btnSave, _btnDelete);
            
            InitEvents();
        }

        private void SetLabels()
        {
            _lblFirstName = new Label()
            {
                X = 0,
                Y = 2,
                Text = "First name: "
            };
            
            _lblLastName = new Label()
            {
                X = 0,
                Y = Pos.Bottom(_lblFirstName) + 1,
                Text = "Last name: "
            };
            
            _lblCodeName = new Label()
            {
                X = 0,
                Y = Pos.Bottom(_lblLastName) + 1,
                Text = "Code: "
            };
            
            _lblIsAdmin = new Label()
            {
                X = 0,
                Y = Pos.Bottom(_lblCodeName) + 1,
                Text = "Is admin: "
            };

            _lblError = new Label()
            {
                X = 0,
                Y = Pos.Bottom(_lblIsAdmin) + 2,
                Width = Dim.Fill(),
                TextAlignment = TextAlignment.Centered,
                ColorScheme = new ColorScheme() {
                    Normal = Terminal.Gui.Attribute.Make(Color.Red, Color.Blue)
                }
            };
            
            Add(_lblFirstName, _lblLastName, _lblCodeName, _lblIsAdmin, _lblError);
        }

        private void SetInputs()
        {
            var xOffset = Pos.Right(_lblFirstName);
            
            _txtFirstName = new TextField()
            {
                X = xOffset,
                Y = _lblFirstName.Y,
                Width = Dim.Fill()
            };
            _txtLastName = new TextField()
            {
                X = xOffset,
                Y = _lblLastName.Y,
                Width = Dim.Fill()
            };
            _txtCodeName = new TextField()
            {
                X = xOffset,
                Y = _lblCodeName.Y,
                Width = Dim.Fill()
            };
            
            _cbxIsAdmin = new CheckBox()
            {
                X = xOffset,
                Y = _lblIsAdmin.Y
            };
            
            Add(_txtFirstName, _txtLastName, _txtCodeName, _cbxIsAdmin);
        }

        private void InitEvents()
        {
            EventManager.OnShowUserDetails += user =>
            {
                _currentUser = user;
                LoadUserDetails();
            };
        }

        private void LoadUserDetails()
        {
            _lblError.Text = string.Empty;
            
            if (_currentUser is null)
            {
                _txtFirstName.Text = string.Empty;
                _txtLastName.Text = string.Empty;
                _txtCodeName.Text = string.Empty;
                _cbxIsAdmin.Checked = false;
                _btnDelete.Visible = false;
                return;
            }

            _btnDelete.Visible = _currentUser.Id != MainApp.User?.Id;
            
            _txtFirstName.Text = _currentUser.FirstName;
            _txtLastName.Text = _currentUser.LastName;
            _txtCodeName.Text = _currentUser.LoginCode;
            _cbxIsAdmin.Checked = _currentUser.IsAdmin;
        }

        private void SaveUser()
        {
            var model = new AddEditUserModel()
            {
                Id = _currentUser?.Id,
                FirstName = _txtFirstName.Text.ToString(),
                LastName = _txtLastName.Text.ToString(),
                LoginCode = _txtCodeName.Text.ToString(),
                IsAdmin = _cbxIsAdmin.Checked
            };

            var result = _userService.AddEditUser(model);

            switch (result)
            {
                case AddEditUserResult.Ok:
                    _lblError.Text = string.Empty;
                    EventManager.RaiseUserSavedEvent();
                    break;
                case AddEditUserResult.NameFieldsEmpty:
                    _lblError.Text = "You left some fields empty!";
                    break;
                case AddEditUserResult.CodeNameIsAlreadyUsed:
                    _lblError.Text = "Such code name is already in use!";
                    break;
                default:
                    _lblError.Text = "Unexpected error occured!";
                    break;
            }
        }

        private void DeleteUser()
        {
            if (_currentUser != null && _userService.RemoveUser(_currentUser.Id))
            {
                _lblError.Text = string.Empty;
                EventManager.RaiseUserSavedEvent();
                
                return;
            }

            _lblError.Text = "Unexpected error occured";
        }
    }
}