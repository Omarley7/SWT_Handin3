using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class Buzzer : IBuzzer
    {
        private readonly IOutput myOutput;
        public void ShortBeep()
        {
            myOutput.OutputLine("Beep!");
        }

        public void ThreeShortBeeps()
        {
            for (int i = 0; i < 3; i++)
                myOutput.OutputLine("Beep!");
        }

        public Buzzer(IOutput output)
        {
            myOutput = output;
        }

    }
}
