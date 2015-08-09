using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSolve
{
    public delegate void PuzzleSolvedHandler();

    public partial class MainForm : Form
    {
        public event PuzzleSolvedHandler PuzzleSolved;

        public MainForm()
        {
            InitializeComponent();
        }

        private void SolveButton_Click(object sender, EventArgs e)
        {
            int[,] input = new int[9, 9];

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    string key = string.Format("TB{0}{1}", row, col);
                    if (Controls.IndexOfKey(key) < 0)
                        continue;

                    TextBox tb = Controls[key] as TextBox;
                    if (tb != null)
                    {
                        if (!string.IsNullOrEmpty(tb.Text))
                        {
                            input[row, col] = int.Parse(tb.Text);
                        }
                        else
                        {
                            input[row, col] = 0;
                        }
                    }
                }
            }

            _puzzle = new Puzzle(input);

            ThreadStart start = new ThreadStart(SolvePuzzle);
            Thread t = new Thread(start);
            t.Start();

            SolveButton.Text = "";
            SolveButton.Enabled = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                TextBox tb = control as TextBox;
                if (tb != null)
                    tb.Clear();
            }

            // "Temporary" code to load a default puzzle.
            _puzzle = new Puzzle("Puzzle1.txt");
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (!_puzzle.Rows[row].Tiles[col].Given)
                        continue;

                    string key = string.Format("TB{0}{1}", row, col);
                    if (Controls.IndexOfKey(key) < 0)
                        continue;

                    TextBox tb = Controls[key] as TextBox;
                    if (tb != null)
                    {
                        tb.Text = _puzzle.Rows[row].Tiles[col].Value.ToString();
                    }
                }
            }

            this.PuzzleSolved += MainForm_PuzzleSolved;
        }

        private void MainForm_PuzzleSolved()
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    
                    string key = string.Format("TB{0}{1}", row, col);
                    if (Controls.IndexOfKey(key) < 0)
                        continue;

                    TextBox tb = Controls[key] as TextBox;
                    if (tb != null)
                    {
                        tb.Invoke((MethodInvoker)delegate { tb.Text = _puzzle.Solutions[0][row, col].ToString(); });
                    }
                }
            }

            SolveButton.Invoke((MethodInvoker)delegate { SolveButton.Text = "SOLVE"; SolveButton.Enabled = true; });
        }

        private void SolvePuzzle()
        {
            _puzzle.Solve();
            PuzzleSolved();
        }

        private Puzzle _puzzle;
    }
}
