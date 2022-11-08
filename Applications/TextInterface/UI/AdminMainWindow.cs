using System.Collections.Generic;
using Terminal.Gui;

namespace TextInterface.UI
{
    public class AdminMainWindow : Window
    {
        public AdminMainWindow()
        {
            Title = "Admin main menu";

            var menu = new ListView() {
                X = 2,
                Y = 2,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };
            
            menu.SetSource(new List<string>() {
                "1. Edit product list",
                "2. Manage user list",
                "3. Open cash register mode",
                "4. Logout"
            });

            menu.OpenSelectedItem += args => {
                switch (args.Item) {
                    case 0:
                        EventManager.RaiseShowProductListEvent();
                        break;
                    case 1:
                        EventManager.RaiseShowUserListEvent();
                        break;
                   default:
                        EventManager.RaiseRequestLogoutEvent();
                        break;
                }
            };
            
            Add(menu);
        }
        
        //private void 
    }
}