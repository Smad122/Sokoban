using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sokoban
{
    internal abstract class CreatureBase : ICreature
    {
        public Vector2 Position { get; protected set; }

        public Point Size { get; protected set; }

        public abstract Vector2 GetPosition();

        public abstract Point GetSize();

        public abstract void SetPosition(Vector2 pos);

        public abstract Texture2D Texture { get; }
    }
}
