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
        public void ShortBeep(int times)
        {
            if (times >= 0)
            {
                for (int i = 0; i < times; i++)
                myOutput.OutputLine("Beep!");
            }
            else
            {
                throw new ApplicationException("Number of beeps can't be negative");
            }
        }

        public Buzzer(IOutput output)
        {
            myOutput = output;
        }

    }
}
