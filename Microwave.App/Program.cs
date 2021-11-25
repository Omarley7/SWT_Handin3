using System;
using System.Threading;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;

namespace Microwave.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Running program: 700W powertube, 1 button push on time and button");
            int powerTubeMaxPower = 700;

            #region init microoven
            Button startCancelButton = new Button();
            Button powerButton = new Button();
            Button timeButton = new Button();
            Button negativeTimeButton = new Button();

            Door door = new Door();
            Output output = new Output();
            Display display = new Display(output);
            PowerTube powerTube = new PowerTube(output, powerTubeMaxPower);
            Light light = new Light(output);
            Buzzer buzzer = new Buzzer(output);
            Microwave.Classes.Boundary.Timer timer = new Classes.Boundary.Timer();
            CookController cooker = new CookController(timer, display, powerTube);
            UserInterface ui = new UserInterface(powerButton, timeButton, startCancelButton, negativeTimeButton, door, display, light, cooker, buzzer);
            
            // Finish the double association
            cooker.UI = ui;
            #endregion
            // Simulate a simple sequence
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();
            // The simple sequence should now run

            System.Console.WriteLine("Press enter to continue");
            // Wait for input
            System.Console.ReadLine();

            Console.WriteLine("Running program: 1000W powertube, 4 button push on time and button, will push time button after 10 seconds negative time button after 30 seconds");
            int power1000 = 1000;

            #region init microoven

            PowerTube powerTube1 = new PowerTube(output, power1000);
            CookController cooker1 = new CookController(timer, display, powerTube1);
            UserInterface ui1 = new UserInterface(powerButton, timeButton, startCancelButton, negativeTimeButton, door, display, light, cooker1, buzzer);

            // Finish the double association
            cooker1.UI = ui1;
            #endregion
            // Simulate a simple sequence
            powerButton.Press();
            powerButton.Press();
            powerButton.Press();
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();
            Thread t = new Thread(()=> { Thread.Sleep(10000); });
            t.Start();
            t.Join();
            timeButton.Press();
            Thread t1 = new Thread(() => { Thread.Sleep(10000); });
            t1.Start();
            t1.Join();
            negativeTimeButton.Press();
            // The simple sequence should now run

            System.Console.WriteLine("Press enter to continue");
            // Wait for input
            System.Console.ReadLine();
        }
    }
}
