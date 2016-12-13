using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_HSE
{
    class Sprite
    {
        public Texture2D Sprite_texture { get; set; }
        public Vector2 Sprite_vector { get; set; }
        public SpriteBatch Sb { get; set; }

        public Sprite(Texture2D sprite_texture, Vector2 sprite_vector, SpriteBatch sb)
        {
            Sprite_vector = sprite_vector;
            Sprite_texture = sprite_texture;
            Sb = sb;
        }

        public virtual void Draw()
        {
            Sb.Draw(Sprite_texture, Sprite_vector, Color.White);
        }

        public Rectangle rectangle
        {
            get
            {
                return new Rectangle((int)Sprite_vector.X, (int)Sprite_vector.Y,
                          Sprite_texture.Width, Sprite_texture.Height);
            }
        }
        #region Collision
        public bool IsLaterally(Sprite sprite)
        {
            return this.rectangle.Right == sprite.rectangle.Left || this.rectangle.Left == sprite.rectangle.Right;
        }
        public bool IsTop(Sprite sprite)
        {
            return this.rectangle.Top <= sprite.rectangle.Bottom && this.rectangle.Intersects(sprite.rectangle);
        }
        #endregion
    }
}
