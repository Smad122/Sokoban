using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sokoban
{
    internal class Key : CreatureBase, IActivatable
    {
        public static Texture2D SharedTexture { get; set; }

        public bool Activation { get; set; }

        public static Point SharedSize { get; private set; } = new Point(50, 50);

        public Key(Vector2 pos)
        {
            Activation = false;
            Position = pos;
        }

        public override Vector2 GetPosition() => Position;

        public override void SetPosition(Vector2 pos) => Position = pos;

        public override Point GetSize() => SharedSize;

        public override Texture2D Texture => SharedTexture;
    }

    public interface IActivatable
    {
        bool Activation { get; set; }
    }
}

