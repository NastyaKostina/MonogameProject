using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_HSE
{
    class Block:Sprite
    {
        public bool blocked { get; set; }

        public Block(Texture2D Sprite_texture, Vector2 Sprite_vector, SpriteBatch sb, bool blocked)
            : base(Sprite_texture, Sprite_vector, sb)
        {
            this.blocked = blocked;
        }


        public override void Draw()
        {
            if (blocked)
            { base.Draw(); }
        }
    }
}
