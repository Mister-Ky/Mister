using System;

namespace Mister.Framework.BaseClasses
{
    public class Player3D
    {
        public Vector3 Position;

        public Vector3 oldPosition;

        public Texture3D texture;

        public Player3D(Texture3D texture, Vector3 position)
        {
            this.texture = texture;
            Position = position;
            oldPosition = Position;
        }
    }
}
