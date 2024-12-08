using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sokoban
{
    internal class Wall : CreatureBase
    {
        public static Texture2D SharedTexture { get; set; }

        public static Point SharedSize { get; set; } = new Point(50, 50);

        public Wall(Vector2 pos)
        {
            Position = pos;
        }

        public override Vector2 GetPosition() => Position;

        public override void SetPosition(Vector2 pos)
        {
            throw new System.ArgumentException("Position of a wall can only be set in the constructor.");
        }

        public override Point GetSize() => SharedSize;

        public override Texture2D Texture => SharedTexture;
    }
}
