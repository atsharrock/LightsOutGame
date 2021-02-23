using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightsOut.Models
{
    public class Light
    {
        public bool On { get; set; }
        public int GridRow { get; set; }
        public int GridColumn { get; set; }

        public Light(int row, int col)
        {
            On = false;
            GridRow = row;
            GridColumn = col;
        }

        public void Toggle()
        {
            On = !On;
        }
    }
}
