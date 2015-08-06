using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Puzzle
    {
        public List<Group> Rows { get; }
        public List<Group> Columns { get; }
        public List<Group> Grids { get; }
        public List<Tile> Tiles { get; }
        public List<string> Solutions { get; }

        public Puzzle()
        {
            Rows = new List<Group>();
            Columns = new List<Group>();
            Grids = new List<Group>();
            Tiles = new List<Tile>();
            Solutions = new List<string>();
        }

        public Puzzle(string filename)
        {
            Rows = new List<Group>();
            Columns = new List<Group>();
            Grids = new List<Group>();
            Tiles = new List<Tile>();
            Solutions = new List<string>();

            int rowIndex = 0;
            using (StreamReader sr = new StreamReader(filename))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    Group row = new Group();

                    for (int i = 0; i < 9; i++)
                    {
                        Tile tile = new Tile();
                        Tiles.Add(tile);

                        char c = line[i];
                        if (c == 'B')
                        {
                            tile.Given = false;
                        }
                        else
                        {
                            tile.Given = true;
                            tile.Value = c - 48; // Convert to "face value" of char; 0 is ascii 48.
                        }

                        row.Tiles.Add(tile);
                        tile.RowIndex = rowIndex;
                    }

                    Rows.Add(row);
                    rowIndex++;
                }
            }

            // Should have all rows at this point; assign tiles in rows to columns
            for (int col = 0; col < 9; col++)
            {
                Group column = new Group();

                for (int row = 0; row < 9; row++)
                {
                    Tile tile = Rows[row].Tiles[col];
                    column.Tiles.Add(tile);
                    tile.ColIndex = col;
                }

                Columns.Add(column);
            }

            // Create the grids.
            for (int gridRow = 0; gridRow < 3; gridRow++)
            {
                for (int gridCol = 0; gridCol < 3; gridCol++)
                {
                    Group grid = new Group();
                    Grids.Add(grid);
                }
            }

            
            // Assign tiles to the grids.
            // There's probably an elegant way to do what comes next, but I'm dumb...
            Grids[0].Tiles.Add(Rows[0].Tiles[0]);
            Grids[0].Tiles.Add(Rows[0].Tiles[1]);
            Grids[0].Tiles.Add(Rows[0].Tiles[2]);
            Grids[0].Tiles.Add(Rows[1].Tiles[0]);
            Grids[0].Tiles.Add(Rows[1].Tiles[1]);
            Grids[0].Tiles.Add(Rows[1].Tiles[2]);
            Grids[0].Tiles.Add(Rows[2].Tiles[0]);
            Grids[0].Tiles.Add(Rows[2].Tiles[1]);
            Grids[0].Tiles.Add(Rows[2].Tiles[2]);

            Grids[1].Tiles.Add(Rows[0].Tiles[3]);
            Grids[1].Tiles.Add(Rows[0].Tiles[4]);
            Grids[1].Tiles.Add(Rows[0].Tiles[5]);
            Grids[1].Tiles.Add(Rows[1].Tiles[3]);
            Grids[1].Tiles.Add(Rows[1].Tiles[4]);
            Grids[1].Tiles.Add(Rows[1].Tiles[5]);
            Grids[1].Tiles.Add(Rows[2].Tiles[3]);
            Grids[1].Tiles.Add(Rows[2].Tiles[4]);
            Grids[1].Tiles.Add(Rows[2].Tiles[5]);

            Grids[2].Tiles.Add(Rows[0].Tiles[6]);
            Grids[2].Tiles.Add(Rows[0].Tiles[7]);
            Grids[2].Tiles.Add(Rows[0].Tiles[8]);
            Grids[2].Tiles.Add(Rows[1].Tiles[6]);
            Grids[2].Tiles.Add(Rows[1].Tiles[7]);
            Grids[2].Tiles.Add(Rows[1].Tiles[8]);
            Grids[2].Tiles.Add(Rows[2].Tiles[6]);
            Grids[2].Tiles.Add(Rows[2].Tiles[7]);
            Grids[2].Tiles.Add(Rows[2].Tiles[8]);

            Grids[3].Tiles.Add(Rows[3].Tiles[0]);
            Grids[3].Tiles.Add(Rows[3].Tiles[1]);
            Grids[3].Tiles.Add(Rows[3].Tiles[2]);
            Grids[3].Tiles.Add(Rows[4].Tiles[0]);
            Grids[3].Tiles.Add(Rows[4].Tiles[1]);
            Grids[3].Tiles.Add(Rows[4].Tiles[2]);
            Grids[3].Tiles.Add(Rows[5].Tiles[0]);
            Grids[3].Tiles.Add(Rows[5].Tiles[1]);
            Grids[3].Tiles.Add(Rows[5].Tiles[2]);

            Grids[4].Tiles.Add(Rows[3].Tiles[3]);
            Grids[4].Tiles.Add(Rows[3].Tiles[4]);
            Grids[4].Tiles.Add(Rows[3].Tiles[5]);
            Grids[4].Tiles.Add(Rows[4].Tiles[3]);
            Grids[4].Tiles.Add(Rows[4].Tiles[4]);
            Grids[4].Tiles.Add(Rows[4].Tiles[5]);
            Grids[4].Tiles.Add(Rows[5].Tiles[3]);
            Grids[4].Tiles.Add(Rows[5].Tiles[4]);
            Grids[4].Tiles.Add(Rows[5].Tiles[5]);

            Grids[5].Tiles.Add(Rows[3].Tiles[6]);
            Grids[5].Tiles.Add(Rows[3].Tiles[7]);
            Grids[5].Tiles.Add(Rows[3].Tiles[8]);
            Grids[5].Tiles.Add(Rows[4].Tiles[6]);
            Grids[5].Tiles.Add(Rows[4].Tiles[7]);
            Grids[5].Tiles.Add(Rows[4].Tiles[8]);
            Grids[5].Tiles.Add(Rows[5].Tiles[6]);
            Grids[5].Tiles.Add(Rows[5].Tiles[7]);
            Grids[5].Tiles.Add(Rows[5].Tiles[8]);

            Grids[6].Tiles.Add(Rows[6].Tiles[0]);
            Grids[6].Tiles.Add(Rows[6].Tiles[1]);
            Grids[6].Tiles.Add(Rows[6].Tiles[2]);
            Grids[6].Tiles.Add(Rows[7].Tiles[0]);
            Grids[6].Tiles.Add(Rows[7].Tiles[1]);
            Grids[6].Tiles.Add(Rows[7].Tiles[2]);
            Grids[6].Tiles.Add(Rows[8].Tiles[0]);
            Grids[6].Tiles.Add(Rows[8].Tiles[1]);
            Grids[6].Tiles.Add(Rows[8].Tiles[2]);

            Grids[7].Tiles.Add(Rows[6].Tiles[3]);
            Grids[7].Tiles.Add(Rows[6].Tiles[4]);
            Grids[7].Tiles.Add(Rows[6].Tiles[5]);
            Grids[7].Tiles.Add(Rows[7].Tiles[3]);
            Grids[7].Tiles.Add(Rows[7].Tiles[4]);
            Grids[7].Tiles.Add(Rows[7].Tiles[5]);
            Grids[7].Tiles.Add(Rows[8].Tiles[3]);
            Grids[7].Tiles.Add(Rows[8].Tiles[4]);
            Grids[7].Tiles.Add(Rows[8].Tiles[5]);

            Grids[8].Tiles.Add(Rows[6].Tiles[6]);
            Grids[8].Tiles.Add(Rows[6].Tiles[7]);
            Grids[8].Tiles.Add(Rows[6].Tiles[8]);
            Grids[8].Tiles.Add(Rows[7].Tiles[6]);
            Grids[8].Tiles.Add(Rows[7].Tiles[7]);
            Grids[8].Tiles.Add(Rows[7].Tiles[8]);
            Grids[8].Tiles.Add(Rows[8].Tiles[6]);
            Grids[8].Tiles.Add(Rows[8].Tiles[7]);
            Grids[8].Tiles.Add(Rows[8].Tiles[8]);

            for (int grid = 0; grid < 9; grid++)
            {
                for (int tile = 0; tile < 9; tile++)
                {
                    // Save grid reference for easy access elsewhere.
                    Grids[grid].Tiles[tile].GridIndex = grid;
                }
            }
        }

        public void Display()
        {
            for (int row = 0; row < 9; row++)
            {
                Console.WriteLine("{0}{1}{2}{3}{4}{5}{6}{7}{8}",
                    new object[]
                    {
                        Rows[row].Tiles[0].GetDisplayText(),
                        Rows[row].Tiles[1].GetDisplayText(),
                        Rows[row].Tiles[2].GetDisplayText(),
                        Rows[row].Tiles[3].GetDisplayText(),
                        Rows[row].Tiles[4].GetDisplayText(),
                        Rows[row].Tiles[5].GetDisplayText(),
                        Rows[row].Tiles[6].GetDisplayText(),
                        Rows[row].Tiles[7].GetDisplayText(),
                        Rows[row].Tiles[8].GetDisplayText()
                    });
            }
        }

        public void Solve()
        {
            Console.WriteLine("Initializing possible tile values...");
            InitPossibleValues();
            Console.WriteLine("Computing row solutions...");
            ComputeRowSolutions();
            Console.WriteLine("Checking solutions against each other...");
            CheckSolutions();
        }

        private void InitPossibleValues()
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    Tile tile = Rows[row].Tiles[col];
                    if (tile.Given)
                        continue;

                    for (int value = 1; value < 10; value++)
                    {
                        List<int> givenRowValues = Rows[row].GetGivenValues();
                        if (givenRowValues.Contains(value))
                            continue;

                        List<int> givenColValues = Columns[col].GetGivenValues();
                        if (givenColValues.Contains(value))
                            continue;

                        List<int> givenGridValues = Grids[tile.GridIndex].GetGivenValues();
                        if (givenGridValues.Contains(value))
                            continue;

                        // If made it this far, then possible value not in row, col, or grid.  Save it.
                        tile.PossibleValues.Add(value);
                    }
                }
            }
        }

        private void ComputeRowSolutions()
        {
            // The most elegant method ever written right here.

            int aIdx = 0;
            int bIdx = 0;
            int cIdx = 0;
            int dIdx = 0;
            int eIdx = 0;
            int fIdx = 0;
            int gIdx = 0;
            int hIdx = 0;
            int iIdx = 0;

            List<int> dupList = new List<int>();

            // Just a few loops needed.
            for (int row = 0; row < 9; row++)
            {
                do
                {
                    do // If you say do-do real fast, sounds like poop.
                    {
                        do
                        {
                            do  // Need more loops.
                            {
                                do
                                {
                                    do
                                    {
                                        do  // Still a few more loops.
                                        {
                                            do
                                            {
                                                do
                                                {
                                                    // There, that's not nested too deeply.  Looks nice.

                                                    // More ugliness...
                                                    dupList.Clear();

                                                    int a = Rows[row].Tiles[0].Given == true ? Rows[row].Tiles[0].Value : Rows[row].Tiles[0].PossibleValues[aIdx];
                                                    int b = Rows[row].Tiles[1].Given == true ? Rows[row].Tiles[1].Value : Rows[row].Tiles[1].PossibleValues[bIdx];
                                                    int c = Rows[row].Tiles[2].Given == true ? Rows[row].Tiles[2].Value : Rows[row].Tiles[2].PossibleValues[cIdx];
                                                    int d = Rows[row].Tiles[3].Given == true ? Rows[row].Tiles[3].Value : Rows[row].Tiles[3].PossibleValues[dIdx];
                                                    int e = Rows[row].Tiles[4].Given == true ? Rows[row].Tiles[4].Value : Rows[row].Tiles[4].PossibleValues[eIdx];
                                                    int f = Rows[row].Tiles[5].Given == true ? Rows[row].Tiles[5].Value : Rows[row].Tiles[5].PossibleValues[fIdx];
                                                    int g = Rows[row].Tiles[6].Given == true ? Rows[row].Tiles[6].Value : Rows[row].Tiles[6].PossibleValues[gIdx];
                                                    int h = Rows[row].Tiles[7].Given == true ? Rows[row].Tiles[7].Value : Rows[row].Tiles[7].PossibleValues[hIdx];
                                                    int i = Rows[row].Tiles[8].Given == true ? Rows[row].Tiles[8].Value : Rows[row].Tiles[8].PossibleValues[iIdx];

                                                    dupList.Add(a);
                                                    if (!dupList.Contains(b))
                                                        dupList.Add(b);
                                                    if (!dupList.Contains(c))
                                                        dupList.Add(c);
                                                    if (!dupList.Contains(d))
                                                        dupList.Add(d);
                                                    if (!dupList.Contains(e))
                                                        dupList.Add(e);
                                                    if (!dupList.Contains(f))
                                                        dupList.Add(f);
                                                    if (!dupList.Contains(g))
                                                        dupList.Add(g);
                                                    if (!dupList.Contains(h))
                                                        dupList.Add(h);
                                                    if (!dupList.Contains(i))
                                                        dupList.Add(i);

                                                    if (dupList.Count == 9)
                                                    {
                                                        Rows[row].Solutions.Add(
                                                            string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}",
                                                                a,
                                                                b,
                                                                c,
                                                                d,
                                                                e,
                                                                f,
                                                                g,
                                                                h,
                                                                i)
                                                        );
                                                    }

                                                    iIdx++;
                                                } while (Rows[row].Tiles[8].PossibleValues.Count > iIdx);
                                                iIdx = 0;
                                                hIdx++;
                                            } while (Rows[row].Tiles[7].PossibleValues.Count > hIdx);
                                            hIdx = 0;
                                            gIdx++;
                                        } while (Rows[row].Tiles[6].PossibleValues.Count > gIdx);
                                        gIdx = 0;
                                        fIdx++;
                                    } while (Rows[row].Tiles[5].PossibleValues.Count > fIdx);
                                    fIdx = 0;
                                    eIdx++;
                                } while (Rows[row].Tiles[4].PossibleValues.Count > eIdx);
                                eIdx = 0;
                                dIdx++;
                            } while (Rows[row].Tiles[3].PossibleValues.Count > dIdx);
                            dIdx = 0;
                            cIdx++;
                        } while (Rows[row].Tiles[2].PossibleValues.Count > cIdx);
                        cIdx = 0;
                        bIdx++;
                    } while (Rows[row].Tiles[1].PossibleValues.Count > bIdx);
                    bIdx = 0;
                    aIdx++;
                } while (Rows[row].Tiles[0].PossibleValues.Count > aIdx);
                aIdx = 0;
            }
        }

        private void CheckSolutions()
        {
            int aIdx = 0;
            int bIdx = 0;
            int cIdx = 0;
            int dIdx = 0;
            int eIdx = 0;
            int fIdx = 0;
            int gIdx = 0;
            int hIdx = 0;
            int iIdx = 0;

            do
            {
                do
                {
                    do
                    {
                        do
                        {
                            do
                            {
                                do
                                {
                                    do
                                    {
                                        do
                                        {
                                            do
                                            {
                                                Rows[0].AssignValues(Rows[0].Solutions[aIdx]);
                                                Rows[1].AssignValues(Rows[1].Solutions[bIdx]);
                                                Rows[2].AssignValues(Rows[2].Solutions[cIdx]);
                                                Rows[3].AssignValues(Rows[3].Solutions[dIdx]);
                                                Rows[4].AssignValues(Rows[4].Solutions[eIdx]);
                                                Rows[5].AssignValues(Rows[5].Solutions[fIdx]);
                                                Rows[6].AssignValues(Rows[6].Solutions[gIdx]);
                                                Rows[7].AssignValues(Rows[7].Solutions[hIdx]);
                                                Rows[8].AssignValues(Rows[8].Solutions[iIdx]);

                                                int failed = 0;

                                                for (int col = 0; col < 9; col++)
                                                {
                                                    if (!Columns[col].CheckContraints())
                                                    {
                                                        failed++;
                                                        break;
                                                    }
                                                }

                                                if (failed == 0)
                                                {
                                                    for (int grid = 0; grid < 9; grid++)
                                                    {
                                                        if (!Grids[grid].CheckContraints())
                                                        {
                                                            failed++;
                                                            break;
                                                        }
                                                    }
                                                }

                                                if (failed == 0)
                                                {
                                                    string solution = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}",
                                                        Rows[0].Solutions[aIdx],
                                                        Rows[1].Solutions[bIdx],
                                                        Rows[2].Solutions[cIdx],
                                                        Rows[3].Solutions[dIdx],
                                                        Rows[4].Solutions[eIdx],
                                                        Rows[5].Solutions[fIdx],
                                                        Rows[6].Solutions[gIdx],
                                                        Rows[7].Solutions[hIdx],
                                                        Rows[8].Solutions[iIdx]
                                                        );

                                                    Solutions.Add(solution);
                                                }

                                                iIdx++;
                                            } while (Rows[8].Solutions.Count > iIdx);
                                            iIdx = 0;
                                            hIdx++;
                                        } while (Rows[7].Solutions.Count > hIdx);
                                        hIdx = 0;
                                        gIdx++;
                                    } while (Rows[6].Solutions.Count > gIdx);
                                    gIdx = 0;
                                    fIdx++;
                                } while (Rows[5].Solutions.Count > fIdx);
                                fIdx = 0;
                                eIdx++;
                            } while (Rows[4].Solutions.Count > eIdx);
                            eIdx = 0;
                            dIdx++;
                        } while (Rows[3].Solutions.Count > dIdx);
                        dIdx = 0;
                        cIdx++;
                    } while (Rows[2].Solutions.Count > cIdx);
                    cIdx = 0;
                    bIdx++;
                } while (Rows[1].Solutions.Count > bIdx);
                bIdx = 0;
                aIdx++;
            } while (Rows[0].Solutions.Count > aIdx);
        }
    }
}
