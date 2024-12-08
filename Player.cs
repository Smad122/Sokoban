using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sokoban
{
    internal class Player : CreatureBase
    {
        public static Texture2D[] Textures { get; set; }

        public static Point SharedSize { get; set; } 

        public Point CurrentFrame { get; set; }

        public Point SpriteSize { get; }

        public int FrameWidth { get; }
        public int FrameHeight { get; }

        public int Speed { get; }

        public static Movement CurrentMove { get; set; }

        public enum Movement
        {
            Stay, Left, Right, Up, Down
        }

        public Player(Vector2 startPos)
        {
            Position = startPos;

            SharedSize = new Point(20, 40);
            CurrentFrame = new Point(0, 0);
            SpriteSize = new Point(6, 0);

            FrameWidth = 20;
            FrameHeight = 40;

            Speed = 5;

            CurrentMove = Movement.Stay;
        }

        public override Vector2 GetPosition() => Position;

        public override Point GetSize() => SharedSize;

        public override void SetPosition(Vector2 pos)
        {
            Position = pos;
        }

        public override Texture2D Texture => Textures[(int)CurrentMove];
    }
}
