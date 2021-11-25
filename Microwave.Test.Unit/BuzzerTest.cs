using System;
using System.Collections.Generic;
using System.Text;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Unit
{
    [TestFixture]
    public class BuzzerTest
    {
        private Buzzer uut;
        private IOutput output;

        [SetUp]
        public void Setup()
        {
            output = Substitute.For<IOutput>();
            uut = new Buzzer(output);
        }

        [Test]
        public void BuzzThreeTimes()
        {
            uut.ShortBeep(3);
            output.Received(3).OutputLine(Arg.Is<string>(str => str.Contains("Beep!")));
        }

        [Test]
        public void SortBeep_NegativeParam_ExceptionThrown()
        {
            Assert.Throws<ApplicationException>(() => uut.ShortBeep(-1));
        }

        [Test]
        public void ShortBeep_ZeroParam_NoBeeps()
        {
            uut.ShortBeep(0);
            output.Received(0);
        }
    }
}
