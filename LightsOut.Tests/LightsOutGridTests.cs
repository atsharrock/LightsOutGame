using Microsoft.VisualStudio.TestTools.UnitTesting;
using LightsOut.Models;
using System.Linq;

namespace LightsOut.Tests
{
    [TestClass]
    public class LightsOutGridTests
    {
        [TestMethod]
        public void GridIsFiveByFive_ReturnsTrue()
        {
            var lightsOutGrid = new LightsOutGrid();
            var rowCount = lightsOutGrid.Lights.Count() == 5;

            Assert.IsTrue(rowCount);

            var colOneCount = lightsOutGrid.Lights[0].Count() == 5;
            var colTwoCount = lightsOutGrid.Lights[1].Count() == 5;
            var colThreeCount = lightsOutGrid.Lights[2].Count() == 5;
            var colFourCount = lightsOutGrid.Lights[3].Count() == 5;
            var colFiveCount = lightsOutGrid.Lights[4].Count() == 5;

            Assert.IsTrue(colOneCount && colTwoCount && colThreeCount && colFourCount && colFiveCount);
        }

        [TestMethod]
        public void StartGame_LightsAreOn_ReturnsTrue()
        {
            var lightsOutGrid = new LightsOutGrid();
            lightsOutGrid.Start();
            var rowsWithALightOn = lightsOutGrid.Lights.Select(r => r.Any(l => l.On)).Any();

            Assert.IsTrue(rowsWithALightOn);
        }

        [TestMethod]
        public void GameIsNotComplete_ReturnsFalse()
        {
            var lightsOutGrid = new LightsOutGrid();
            lightsOutGrid.Start();

            Assert.IsFalse(lightsOutGrid.Complete);
        }

        [TestMethod]
        public void GameIsComplete_ReturnsTrue()
        {
            var lightsOutGrid = new LightsOutGrid();

            // create a game with a one click finish.
            lightsOutGrid.StartOneClickFinish();

            // grab the light to be clicked that should trigger a win condition.
            var light = lightsOutGrid.Lights[0][0];

            lightsOutGrid.ToggleLights(light);

            Assert.IsTrue(lightsOutGrid.Complete);
        }
    }
}
