using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Group
    {
        public List<Tile> Tiles { get; set; }
        public List<string> Solutions { get; }

        public Group()
        {
            Tiles = new List<Tile>();
            Solutions = new List<string>();
        }

        public List<int> GetGivenValues()
        {
            List<int> givenValues = new List<int>();
            foreach (Tile tile in Tiles)
            {
                if (tile.Given)
                {
                    givenValues.Add(tile.Value);
                }
            }
            return givenValues;
        }

        public void AssignValues(string valueList)
        {
            for (int i = 0; i < 9; i++)
            {
                Tiles[i].Value = valueList[i] - 48;
            }
        }

        public bool CheckContraints()
        {
            //List<int> dupList = new List<int>();

            //for (int i = 0; i < 9; i++)
            //{
            //    if (!dupList.Contains(Tiles[i].Value))
            //        dupList.Add(Tiles[i].Value);
            //}

            //return dupList.Count == 9;

            //var list = Tiles.Select(t => t.Value);
            //var query = list.GroupBy(x => x)
            //    .Where(g => g.Count() > 1)
            //    .Select(y => y.Key)
            //    .ToList();

            //if (query.Count > 0)
            //    return false;
            //else
            //    return true;

            int[] values = new int[9];
            values[0] = Tiles[0].Value;
            values[1] = Tiles[1].Value;
            values[2] = Tiles[2].Value;
            values[3] = Tiles[3].Value;
            values[4] = Tiles[4].Value;
            values[5] = Tiles[5].Value;
            values[6] = Tiles[6].Value;
            values[7] = Tiles[7].Value;
            values[8] = Tiles[8].Value;

            if (values.Contains(1) &&
                values.Contains(2) &&
                values.Contains(3) &&
                values.Contains(4) &&
                values.Contains(5) &&
                values.Contains(6) &&
                values.Contains(7) &&
                values.Contains(8) &&
                values.Contains(9))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
