using System.Collections.Generic;
using Terminal.Gui;

namespace TextInterface.UI
{
    public class RegularUserMainWindow : Window
    {
        public RegularUserMainWindow()
        {
            Title = "User main menu";
            
            var menu = new ListView() {
                X = 2,
                Y = 2,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };
            
            menu.SetSource(new List<string>() {
                "1. Open cash register mode",
                "2. Logout"
            });

            menu.OpenSelectedItem += args => {
                switch (args.Item) {
                    case 0:
                        EventManager.RaiseCashRegisterModeEvent();
                        break;
                    default:
                        EventManager.RaiseRequestLogoutEvent();
                        break;
                }
            };
            
            Add(menu);
        }
    }
}