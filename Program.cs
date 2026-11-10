using System;
using System.Windows.Forms;

namespace BicycleMushroomIdentification
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            new LoginForm().Show();
            Application.Run();
        }
    }
}