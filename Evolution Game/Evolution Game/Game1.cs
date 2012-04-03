using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Evolution_Game
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        World homeWorld;
        Menu mainMenu;
        Player player;
        Texture2D cursor;
        MouseState mouse;
        bool noDraw;

        // performance code, used to determine frames per second and lag
        private float fps;
        private float updateInterval = 1.0f;
        private float timeSinceLastUpdate = 0.0f;
        private float framecount = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1366;
            graphics.PreferredBackBufferHeight = 768;

            graphics.SynchronizeWithVerticalRetrace = false;
            IsFixedTimeStep = false;

            graphics.IsFullScreen = false;

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
            // Create a new SpriteBatch, which can be used to draw textures.
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            noDraw = false;

            player = new Player(this, 100, 0, new Inventory(this), new Spawn(this));

            homeWorld = new World(this, 0, 0, spriteBatch, player);
            homeWorld.DrawOrder = 0;

            mainMenu = new Menu(this);
            mainMenu.DrawOrder = 1;
 
            // add the various component classes to components
            Components.Add(homeWorld);
            Components.Add(mainMenu);

            // add the mouse cursor
            cursor = Content.Load<Texture2D>("UI tex/cursor");
            mouse = Mouse.GetState();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            KeyboardState keys = Keyboard.GetState();

            if (keys.IsKeyDown(Keys.Escape))
                this.Exit();

            mouse = Mouse.GetState();
            int mouseX = mouse.X;
            int mouseY = mouse.Y;


            // main menu display logic
            if (mouse.LeftButton == ButtonState.Pressed && !noDraw)
            {
                if (mouseX >= 625 && mouseX <= 625 + 200)
                {
                    if (mouseY >= 350 && mouseY <= 350+50)
                    {
                        noDraw = true;
                        homeWorld.generateNewWorld(this, 20000, 20000, spriteBatch);
                        homeWorld.loadWorld();
                    }
                    else if (mouseY >= 450 && mouseY <= 450 + 50)
                    {
                        noDraw = true;
                        homeWorld.loadWorld();
                    }
                    else if (mouseY >= 600 && mouseY <= 600 + 50)
                    {
                        this.Exit();
                    }
                }
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            // code calculates the frames per second and writes it in the window title, NOT within the viewport
            float elapsed =
            (float)gameTime.ElapsedGameTime.TotalSeconds;
            framecount++;
            timeSinceLastUpdate += elapsed;

            if(timeSinceLastUpdate > updateInterval)
            {
                fps = framecount/timeSinceLastUpdate;
                Window.Title = "FPS: " + fps.ToString() + "     RT: " +
                gameTime.ElapsedGameTime.TotalSeconds.ToString() + "     GT: " +
                gameTime.ElapsedGameTime.TotalSeconds.ToString();

                framecount = 0;
                timeSinceLastUpdate -= updateInterval;
            }

            // draw components first as mouse cursor is drawn over everything
            base.Draw(gameTime);

            // draws the in-game mouse cursor
            spriteBatch.Begin();
            spriteBatch.Draw(cursor, new Vector2(mouse.X, mouse.Y), Color.White);
            spriteBatch.End();
        }
    }
}
