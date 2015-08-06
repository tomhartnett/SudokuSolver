using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Tile
    {
        public bool Given { get; set; }
        public int Value { get; set; }
        public List<int> PossibleValues { get; }
        public int RowIndex { get; set; }
        public int ColIndex { get; set; }
        public int GridIndex { get; set; }

        public Tile()
        {
            PossibleValues = new List<int>();
        }

        public string GetDisplayText()
        {
            if (Value > 0)
            {
                return Value.ToString();
            }
            else
            {
                return " ";
            }
        }
    }
}
