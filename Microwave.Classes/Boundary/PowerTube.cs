using System;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class PowerTube : IPowerTube
    {
        private IOutput myOutput;

        private bool IsOn = false;

        private int MaxPower;
        private int CurrentPower = 0;

        public PowerTube(IOutput output, int maxPower)
        {
            myOutput = output;
            this.MaxPower = maxPower;
        }

        public void TurnOn(int power)
        {
            if (power < 1 || 100 < power)
            {
                throw new ArgumentOutOfRangeException("power", power, $"Must be between 1% and 100% (incl.)");
            }

            if (IsOn)
            {
                throw new ApplicationException("PowerTube.TurnOn: is already on");
            }
            //calculate power to use
            CurrentPower = (MaxPower * power) / 100;
            myOutput.OutputLine($"PowerTube works with {CurrentPower}W");
            IsOn = true;
        }

        public void TurnOff()
        {
            if (IsOn)
            {
                myOutput.OutputLine($"PowerTube turned off");
            }

            IsOn = false;
        }
    }
}