using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sokoban
{
    internal class Container : CreatureBase
    {
        public static Texture2D SharedTexture { get; set; }
        public static Point SharedSize { get; private set; } = new Point(50, 50);

        public Container(Vector2 pos)
        {
            Position = pos;
        }

        public override Vector2 GetPosition() => Position;

        public override void SetPosition(Vector2 pos) => Position = pos;

        public override Point GetSize() => SharedSize;

        public override Texture2D Texture => SharedTexture;
    }
}

