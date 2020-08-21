using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SimulatorEngine.Contents;
using SimulatorEngine.Interface;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace SimulatorEngine
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameSE : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GUI GUI;

        public GameSE()
        {
            graphics = new GraphicsDeviceManager(this);
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


            IsMouseVisible = true;

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

            //Load Texures
            Textures.Add("MainBackground", Content.Load<Texture2D>("Images/Backgrounds/Main"));
            Textures.Add("AdditionalPanelBackgound", Content.Load<Texture2D>("Images/Backgrounds/AdditionalPanel"));
            Textures.Add("Broken", Content.Load<Texture2D>("Images/BrokenTexture"));


            GUI = new GUI(new Size(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), GraphicsDevice, spriteBatch);

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


        MouseState LastMouseState = new MouseState();
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                //Debug.WriteLine($"Click to X:{mouseState.X}, Y:{mouseState.Y}");

                //GUI.additionalPanel.Drawer.OffsetsVector = new Vector2(
                //    x: mouseState.X - GUI.additionalPanel.Drawer.OffsetsRectangle.Size.X,
                //    y: mouseState.Y - GUI.additionalPanel.Drawer.OffsetsRectangle.Size.Y);

            }

            

            foreach (Content content in Contents.Content.Contents)
            {
                Rectangle Collider = new Rectangle(content.Drawer.OffsetsVector.ToPoint(), content.Drawer.OffsetsRectangle.Size);
                if (content.Drawer.IsVisible && Collider.Contains(mouseState.X, mouseState.Y))
                {
                    content.MouseHandler(mouseState, LastMouseState);
                }
            }


            LastMouseState = mouseState;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            GUI.Draw();

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
