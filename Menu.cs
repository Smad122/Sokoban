using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Sokoban
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
            listView1.Items.Clear();
        }

        private DateTime ParseDateTime(string dateTimeString)
        {
            DateTime parsedDateTime;
            if (DateTime.TryParse(dateTimeString, out parsedDateTime))
            {
                return parsedDateTime;
            }

            else
            {
                return DateTime.MinValue;
            }
        }

        private void GetLeaderboard()
        {
            if (!File.Exists("Leaderboard.txt"))
                File.Create("Leaderboard.txt").Dispose();

            else
            {
                var lines = File.ReadAllLines("Leaderboard.txt")
                   .Where(line => line.Contains(PathToLevel)) 
                   .Select(line => new
                   {
                       filePath = line.Split(' ')[0], 
                       name = line.Split(' ')[1],
                       time = ParseDateTime(line.Split(' ')[2]) 
                   })
                   .OrderBy(entry => entry.time) 
                   .ToList();

                listView1.Items.Clear();

                foreach (var line in lines)
                {
                    string displayText = $"{line.name} {line.time}";
                    listView1.Items.Add(new ListViewItem(displayText));
                }
            }
        }

        public string PlayerName { get; set; } = "NoName";

        public string PathToLevel { get; set; }

        private void Start_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Map file | *.txt";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    PathToLevel = openFileDialog.FileName;
                    MessageBox.Show("Success");
                    textBox1.Text = PathToLevel;
                    GetLeaderboard();
                }
            }
        }


        private void ChangeName_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0)
            {
                PlayerName = textBox2.Text;
                textBox3.Text = "Hello, " + PlayerName;
            }
        }
    }
}
