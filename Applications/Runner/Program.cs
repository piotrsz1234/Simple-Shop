
using System.Diagnostics;

namespace Runner
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Which interface you wish to run?");
            Console.WriteLine("1. Web");
            Console.WriteLine("2. Console");
            var result = Console.ReadLine();

            var processes = new List<Process>();
            
            if (result != null && result.Trim() == "1") {
                var fileName = Path.Combine(Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.IndexOf(@"Applications")),
                    @"Applications\GraphicInterface");
                var startInfo = new ProcessStartInfo("dotnet.exe");
                startInfo.WorkingDirectory = fileName;
                startInfo.ArgumentList.Add("run");
                processes.Add(Process.Start(startInfo));

                var browserStartInfo = new ProcessStartInfo {
                    UseShellExecute = true,
                    FileName = "http://localhost:5257"
                };

                processes.Add(Process.Start(browserStartInfo));
            } else {
                TextInterface.Program.Main(args);
            }

            Console.ReadLine();
            
            Process.GetCurrentProcess().Exited += (sender, eventArgs) => {
                foreach (var item in processes) {
                    item.Kill();
                }
            };
        }
    }
}