using System;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Unit
{
    [TestFixture]
    public class CookControllerTest
    {
        private CookController uut;

        private IUserInterface ui;
        private ITimer timer;
        private IDisplay display;
        private IPowerTube powerTube;

        [SetUp]
        public void Setup()
        {
            ui = Substitute.For<IUserInterface>();
            timer = Substitute.For<ITimer>();
            display = Substitute.For<IDisplay>();
            powerTube = Substitute.For<IPowerTube>();

            uut = new CookController(timer, display, powerTube, ui);
        }

        [Test]
        public void StartCooking_ValidParameters_TimerStarted()
        {
            uut.StartCooking(50, 60);

            timer.Received().Start(60);
        }

        [Test]
        public void StartCooking_ValidParameters_PowerTubeStarted()
        {
            uut.StartCooking(50, 60);

            powerTube.Received().TurnOn(50);
        }

        [Test]
        public void Cooking_TimerTick_DisplayCalled()
        {
            uut.StartCooking(50, 60);

            timer.TimeRemaining.Returns(115);
            timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);

            display.Received().ShowTime(1, 55);
        }

        [Test]
        public void Cooking_TimerExpired_PowerTubeOff()
        {
            uut.StartCooking(50, 60);

            timer.Expired += Raise.EventWith(this, EventArgs.Empty);

            powerTube.Received().TurnOff();
        }

        [Test]
        public void Cooking_TimerExpired_UICalled()
        {
            uut.StartCooking(50, 60);

            timer.Expired += Raise.EventWith(this, EventArgs.Empty);

            ui.Received().CookingIsDone();
        }

        [Test]
        public void Cooking_Stop_PowerTubeOff()
        {
            uut.StartCooking(50, 60);
            uut.Stop();

            powerTube.Received().TurnOff();
        }
        #region SigurdsTests
        //Tilføj cooking test her med timer hvor der fjernes før cooking
        [TestCase(3,7)]
        [TestCase(6,4)]
        [TestCase(1,9)]
        [TestCase(0,10)]
        [TestCase(10,0)]
        [TestCase(12, 0)]
        public void Cooking_RemoveTimeBeforeCooking_AssertThatTimeAdded(int startTime, int newTime)
        {
            //Arrange
            ITimer temptimer;
            temptimer = new Microwave.Classes.Boundary.Timer();
            CookController uut2 = new CookController(temptimer, display, powerTube);

            //Act
            for (int i = 0; i <= 10; i++)
            {
                uut2.AddTime();
            }
            for (int i = 0; i <= startTime; i++)
            {
                uut2.RemoveTime();
            }
            uut.StartCooking(1,startTime);

            //Assert
            Assert.That(temptimer.TimeRemaining, Is.EqualTo(newTime*60));
        }

        [TestCase(3, 4)]
        [TestCase(6, 7)]
        [TestCase(1, 2)]
        [TestCase(0, 1)]
        [TestCase(10, 11)]
        [TestCase(12, 13)]
        public void Cooking_AddTimeWhileCooking_AssertThatTimeAdded(int startTime, int newTime)
        {
            //Arrange
            ITimer temptimer;
            temptimer = new Microwave.Classes.Boundary.Timer();
            CookController uut2 = new CookController(temptimer, display, powerTube);

            //Act
            uut.StartCooking(1, 1);
            for (int i = 0; i <= startTime; i++)
            {
                uut2.AddTime();
            }
            
            //Assert
            Assert.That(temptimer.TimeRemaining, Is.EqualTo(newTime * 60));
        }
        //Tilføj cooking test her med timer hvor der fjernes under cooking
        [TestCase(3, 7)]
        [TestCase(6, 4)]
        [TestCase(1, 9)]
        [TestCase(0, 10)]
        [TestCase(10, 0)]
        [TestCase(12, 0)]
        public void Cooking_RemoveTimeWhileCooking_AssertThatTimeAdded(int startTime, int newTime)
        {
            //Arrange
            ITimer temptimer;
            temptimer = new Microwave.Classes.Boundary.Timer();
            CookController uut2 = new CookController(temptimer, display, powerTube);

            //Act
            uut.StartCooking(1, startTime);

            for (int i = 0; i <= 10; i++)
            {
                uut2.AddTime();
            }
            for (int i = 0; i <= startTime; i++)
            {
                uut2.RemoveTime();
            }

            //Assert
            Assert.That(temptimer.TimeRemaining, Is.EqualTo(newTime * 60));
        }
        #endregion
    } //Lav en tester til userinterface
}