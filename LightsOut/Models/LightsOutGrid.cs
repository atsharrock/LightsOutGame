using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightsOut.Models
{
    /// <summary>
    /// A grid of n x n lights.
    /// </summary>
    public class LightsOutGrid
    {
        /// <summary>
        /// The Size of the Grid
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Returns if the puzzle has been completed.
        /// </summary>
        public bool Complete { get; set; }

        /// <summary>
        /// Returns if the game has started.
        /// </summary>
        public bool GameStarted { get; set; }

        /// <summary>
        /// A grid of lights. The index of the list of lights refers to the row number. The index of the lights within these lists refer to the column number.
        /// </summary>
        public List<List<Light>> Lights { get; set; }

        public LightsOutGrid()
        {
            Size = 5;
            Lights = new List<List<Light>>();
            for (var i = 0; i < Size; i++)
            {
                var lights = new List<Light>();
                for (var j = 0; j < Size; j++)
                {
                    var light = new Light(i, j);
                    lights.Add(light);
                }
                Lights.Add(lights);
            }
        }

        /// <summary>
        /// Toggles the selected light and its adjacent lights on/off. Then checks win conditions.
        /// </summary>
        /// <param name="light">The light that has been clicked</param>
        public void ToggleLights(Light light)
        {
            light.Toggle();

            var row = light.GridRow;
            var col = light.GridColumn;

            if (row - 1 >= 0) Lights[row-1][col].Toggle();
            if (row + 1 <= Size - 1) Lights[row+1][col]?.Toggle();
            if (col - 1 >= 0) Lights[row][col-1]?.Toggle();
            if (col + 1 <= Size - 1) Lights[row][col+1]?.Toggle();

            CheckWinConditions();
        }

        /// <summary>
        /// Checks and sets Complete to true if all the lights on a grid are off.
        /// </summary>
        public void CheckWinConditions()
        {
            Complete = !Lights.Any(rows => rows.Any(lights => lights.On));
            if (Complete) GameStarted = false;
        }

        /// <summary>
        /// Starts the game by turning some lights on.
        /// </summary>
        public void Start()
        {
            Reset();
            GameStarted = true;
            var random = new Random();
            foreach (var row in Lights)
            {
                foreach (var light in row)
                {
                    if (random.Next(1,3) % 2 == 0)
                    {
                        light.Toggle();
                    }
                }
            }
        }

        /// <summary>
        /// Resets the game. Completed is false and all the lights in the grid are switched off.
        /// </summary>
        public void Reset()
        {
            Complete = false;
            Lights.ForEach(r => r.ForEach(l => l.On = false));
        }

        /// <summary>
        /// For testing purposes, sets up a grid that allows a win condition with one click.
        /// </summary>
        public void StartOneClickFinish()
        {
            Reset();
            GameStarted = true;
            Lights[0][0].Toggle();
            Lights[0][1].Toggle();
            Lights[1][0].Toggle();
        }
    }
}
