using Sokoban;
using System.Windows.Forms;
using System;

namespace Sokoban
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var menuForm = new Menu())
            {
                if (menuForm.ShowDialog() == DialogResult.OK)
                {
                    var playerName = menuForm.PlayerName;
                    var levelPath = menuForm.PathToLevel;

                    if (levelPath == null) throw new ArgumentException("Path can't be null");

                    using (var game = new SokobanGame())
                    {
                        game.LoadPlayerName(playerName);

                        if (!string.IsNullOrEmpty(levelPath))
                        {
                            game.LoadLevel(levelPath);
                        }

                        game.Run();
                    }
                }
            }
        }
    }
}
