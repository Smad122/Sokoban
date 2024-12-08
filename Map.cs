using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Sokoban
{
    internal class Map
    {
        public string[] Game {get; set; }

        public Point Size { get; }

        public Map(string path) 
        {
            var maxLength = 0;
            var lines = File.ReadAllLines(path);
            var gameList = new List<string>();

            foreach (var line in lines)
            {
                gameList.Add(line);

                if(maxLength < line.Length)
                    maxLength = line.Length;
            }

            Size = new Point(maxLength, gameList.Count);

            Game = gameList.ToArray();
        }
    }
}
