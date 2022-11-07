using Data.EF.Contexts;
using Terminal.Gui;
using TextInterface.UI;

namespace TextInterface
{
    class Program
    {
        public static void Main(string[] args)
        {
            using var app = new MainApp();

            app.StartApplication();
        }
    }
}