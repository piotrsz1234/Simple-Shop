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
                "2. Open cash register mode",
                "3. Logout"
            });

            menu.OpenSelectedItem += args => {
                switch (args.Item) {
                    case 0:
                        EventManager.RaiseShowProductListEvent();
                        break;
                    case 1:
                        break;
                    case 2:
                        EventManager.RaiseRequestLogoutEvent();
                        break;
                }
            };
            
            Add(menu);
        }
        
        //private void 
    }
}