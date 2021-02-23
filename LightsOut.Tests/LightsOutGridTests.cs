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
        public void StartGame_GameIsNotComplete_ReturnsFalse()
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

            // "click" the light.
            lightsOutGrid.ToggleLights(light);

            Assert.IsTrue(lightsOutGrid.Complete);
        }

        [TestMethod]
        public void AdjacentLightsToggle_ReturnsTrue()
        {
            var lightsOutGrid = new LightsOutGrid();
            
            // get the middle light.
            var middleLight = lightsOutGrid.Lights[2][2];
            // "click" the light.
            lightsOutGrid.ToggleLights(middleLight);

            // [0][0] [0][1] [0][2] [0][3] [0][4]
            // [1][0] [1][1] [1][2] [1][3] [1][4]
            // [2][0] [2][1] [2][2] [2][3] [2][4]
            // [3][0] [3][1] [3][2] [3][3] [3][4]
            // [4][0] [4][1] [4][2] [4][3] [4][4]

            // the middle light and its adjacent lights should now be on.
            Assert.IsTrue(lightsOutGrid.Lights[2][2].On); // middle light
            Assert.IsTrue(lightsOutGrid.Lights[1][2].On); // above
            Assert.IsTrue(lightsOutGrid.Lights[3][2].On); // below
            Assert.IsTrue(lightsOutGrid.Lights[2][1].On); // left
            Assert.IsTrue(lightsOutGrid.Lights[2][3].On); // right
        }
    }
}
