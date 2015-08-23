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
            ClearPuzzle();

            this.PuzzleSolved += MainForm_PuzzleSolved;

            LoadSamplePuzzle(@"SamplePuzzzles\EasyPuzzle.txt");
        }

        private void MainForm_PuzzleSolved()
        {
            if (_puzzle.Solutions.Count == 0)
                return;

            SolutionCountLabel.Invoke((MethodInvoker)delegate { SolutionCountLabel.Text = string.Format("1 of {0}", _puzzle.Solutions.Count()); });

            _selectedSolutionIndex = 0;
            DisplaySolution(_selectedSolutionIndex);
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char[] allowedChars = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '\b' };
            if (!allowedChars.Contains(e.KeyChar))
                e.Handled = true;
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            ClearPuzzle();
        }

        private void PreviousSolutionButton_Click(object sender, EventArgs e)
        {
            _selectedSolutionIndex -= 1;

            if (_selectedSolutionIndex < 0)
            {
                _selectedSolutionIndex++;
                return;
            }

            DisplaySolution(_selectedSolutionIndex);
        }

        private void NextSolutionButton_Click(object sender, EventArgs e)
        {
            _selectedSolutionIndex++;

            if (_selectedSolutionIndex >= _puzzle.Solutions.Count)
            {
                _selectedSolutionIndex -= 1;
                return;
            }

            DisplaySolution(_selectedSolutionIndex);
        }

        private void PuzzlesMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ctrl = sender as ToolStripMenuItem;
            if (ctrl == null)
                return;

            if (ctrl.Tag == null)
                return;

            ClearPuzzle();

            string puzzleType = ctrl.Tag.ToString();
            switch (puzzleType.ToLower())
            {
                case "easy":
                    LoadSamplePuzzle(@"SamplePuzzzles\EasyPuzzle.txt");
                    break;
                case "medium":
                    LoadSamplePuzzle(@"SamplePuzzzles\MediumPuzzle.txt");
                    break;
                case "hard":
                    LoadSamplePuzzle(@"SamplePuzzzles\HardPuzzle.txt");
                    break;
                default:
                    return;
            }
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SolvePuzzle()
        {
            _puzzle.Solve();
            PuzzleSolved();
        }

        private void DisplaySolution(int solutionIndex)
        {
            if (_puzzle.Solutions.Count == 0)
                return;

            if (solutionIndex > _puzzle.Solutions.Count - 1)
                return;

            if (solutionIndex < 0)
                return;

            SolutionCountLabel.Text = string.Format("{0} of {1}", solutionIndex + 1, _puzzle.Solutions.Count);

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
                        tb.Invoke((MethodInvoker)delegate { tb.Text = _puzzle.Solutions[solutionIndex][row, col].ToString(); });
                        if (!_puzzle.Rows[row].Tiles[col].Given)
                            tb.Invoke((MethodInvoker)delegate { tb.ForeColor = Color.Red; });
                        else
                            tb.Invoke((MethodInvoker)delegate { tb.ForeColor = Color.Black; });
                    }
                }
            }

            SolveButton.Invoke((MethodInvoker)delegate { SolveButton.Text = "SOLVE"; SolveButton.Enabled = true; });
        }

        private void ClearPuzzle()
        {
            foreach (Control control in Controls)
            {
                TextBox tb = control as TextBox;
                if (tb != null)
                {
                    tb.Clear();
                    tb.ForeColor = Color.Black;
                }
            }

            SolutionCountLabel.Text = "0";
            _selectedSolutionIndex = -1;
        }

        private void LoadSamplePuzzle(string filename)
        {
            // "Temporary" code to load a default puzzle.
            _puzzle = new Puzzle(filename);
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
        }

        private Puzzle _puzzle;
        private int _selectedSolutionIndex = 0;
    }
}
