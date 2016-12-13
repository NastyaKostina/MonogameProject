using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_HSE
{
    class Camera
    {
        private Matrix transform;
        public Matrix Transform { get { return transform; } }

        private Vector2 center;
        private Viewport viewport;

        public Camera(Viewport viewport)
        {
            this.viewport = viewport;
        }

        public void Update(Vector2 position, int x, int y)
        {
            if (position.X < viewport.Width / 2) { center.X = viewport.X / 2; }
            else if (position.X > x - (viewport.Width / 2)) { center.X = x - (viewport.Width / 2); }
            else center.X = position.X;

            // if (position.Y < viewport.Height / 2) { center.Y = viewport.Y / 2; }
            if (position.Y > y - (viewport.Height / 2)) { center.Y = y - (viewport.Height / 2); }
            else center.Y = position.Y;

            transform = Matrix.CreateTranslation(new Vector3(-center.X + (viewport.Width / 2),
                                                             -center.Y + (viewport.Height / 2),
                                                              0));
        }
    }
}
