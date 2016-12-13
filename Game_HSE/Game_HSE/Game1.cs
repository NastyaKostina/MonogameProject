using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game_HSE
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        

        const int ground_level = 576;

        private Player player;
        private Board board;
        private Camera camera;

        private SpriteFont debugfont;
        


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth = 960;
            graphics.PreferredBackBufferHeight = 640;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            camera = new Camera(GraphicsDevice.Viewport);
            //debugfont = Content.Load<SpriteFont>("DebugFont");
            //Song music = Content.Load<Song>("music");
            //MediaPlayer.Play(music);
            //SoundEffect jump = Content.Load<SoundEffect>("jump");
            // sounds.Add(jump);

            Texture2D player_texture = Content.Load<Texture2D>("студент.png");
            player = new Player(player_texture, new Vector2(550, ground_level - player_texture.Height), spriteBatch //sounds
                );

            Texture2D block_texture = Content.Load<Texture2D>("блок.png");
            board = new Board(spriteBatch, block_texture, 87, 10);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            base.Update(gameTime);
            player.Update(gameTime);
            camera.Update(player.Sprite_vector, board.columns * 64, board.rows * 64);
            // TODO: Add your update logic here
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //string helloWords = string.Format("Created by\nNastya Kostina\nand\nVasiliy Sdobnov\n\n\nWe are so glad\nyou decided to play\nthis disaster.\n\n\nSpace - Jump\n<- - Move Left\n-> - Move Right");//\nWe are so glad\nyou decided to play\nthis disaster.\nPress right and left\n\to replace Mario.\nSpace for jumping.");

            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred,
                              BlendState.AlphaBlend,
                              null, null, null, null,
                              camera.Transform);
            base.Draw(gameTime);
            board.Draw();
            //spriteBatch.DrawString(debugfont, helloWords, new Vector2(-400, 40), Color.White);
            player.Draw();
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
