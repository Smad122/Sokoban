using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Sokoban
{
    internal interface ICreature
    {
        Vector2 GetPosition();

        Point GetSize();

        void SetPosition(Vector2 pos);


        Texture2D Texture { get; }
    }
}
