using System;

namespace Mister.Framework.BaseClasses
{
    public class Player2D
    {
        public Vector2 Position;
        public Vector2 oldPosition;

        public Texture2D texture;

        public Player2D(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            Position = position;
            oldPosition = Position;
        }
    }
}
