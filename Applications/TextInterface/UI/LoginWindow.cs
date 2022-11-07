using Autofac;
using Terminal.Gui;
using Attribute = Terminal.Gui.Attribute;

namespace TextInterface.UI
{
    public class LoginWindow : Window
    {
        public LoginWindow()
        {
            Title = "Login";

            var codeLabel = new Label() {
                X = 3,
                Y = 3,
                Text = "Code: ",
            };

            var codePassField = new TextField() {
                X = Pos.Right(codeLabel),
                Y = codeLabel.Y,
                Width = Dim.Sized(12),
            };

            codePassField.KeyPress += args => {
                if (args.KeyEvent.Key == Key.Enter) {
                    Login(codePassField.Text?.ToString());
                }
            };

            var incorrectLoginLabel = new Label() {
                X = Pos.Left(codeLabel),
                Y = codePassField.Y + 1,
                TextAlignment = TextAlignment.Centered,
                Width = Dim.Sized(18)
            };
            
            incorrectLoginLabel.ColorScheme = new ColorScheme() {
                Normal = Attribute.Make(Color.Red, Color.Blue)
            } ;
            
            var loginButton = new Button() {
                X = Pos.Left(codeLabel) + 2,
                Y = codeLabel.Y + 3,
                Width = Dim.Sized(14),
                Text = "Login"
            };

            loginButton.Clicked += () => Login(codePassField.Text?.ToString());
            
            EventManager.OnLoginFailed += () => {
                incorrectLoginLabel.Text = "Incorrect code";
            };
            
            Add(codeLabel, codePassField, loginButton, incorrectLoginLabel);
        }

        private void Login(string? code)
        {
            EventManager.RaiseRequestLoginEvent(code);
        }
    }
}