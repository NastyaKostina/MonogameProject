using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_HSE
{
    class Player:Sprite
    {
        public Vector2 move { get; set; }
        private Vector2 oldPosition;
        //public List<SoundEffect> sound_effects;
        public List<int> Score = new List<int>();

        public Player(Texture2D texture, Vector2 position, SpriteBatch spritebatch //List<SoundEffect> sounds
            )
        : base(texture, position, spritebatch)
        {
            //sound_effects = sounds;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardAction();
            GravityEffect();
            move -= move * new Vector2(.1f, .1f);
            PossibleMoving(gameTime);
            StopMovingIfBlocked();
        }

        private void KeyboardAction() //movements
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Left)) { move += -Vector2.UnitX * 0.45f; }
            if (keyboardState.IsKeyDown(Keys.Right)) { move += Vector2.UnitX * 0.45f; }
            if (keyboardState.IsKeyDown(Keys.Space) && IsOnFirmGround()) { move = -Vector2.UnitY * 32;/* sound_effects[0].Play();  подняли его вверх */ }
        }
        private void GravityEffect()
        {
            move += Vector2.UnitY * .65f;
        }

        private void UpdatePosition(GameTime gameTime)
        {
            Sprite_vector += move * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 15;
        }

        private void PossibleMoving(GameTime gameTime)
        {
            oldPosition = Sprite_vector;
            UpdatePosition(gameTime);
            Sprite_vector = Board.CurrentBoard.WhereCanIGetTo(oldPosition, Sprite_vector, rectangle);
        }

        public bool IsOnFirmGround() //проверяем что стоим на земле
        {
            Rectangle onePixelLower = rectangle;
            onePixelLower.Offset(0, 1);
            return !Board.CurrentBoard.HasSpaceForRectangle(onePixelLower);
        }

        private void StopMovingIfBlocked()
        {
            Vector2 lastMovement = Sprite_vector - oldPosition;
            if (lastMovement.X == 0) { move *= Vector2.UnitY; }
            if (lastMovement.Y == 0) { move *= Vector2.UnitX; }
        }

    }
}
