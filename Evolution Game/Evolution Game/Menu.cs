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
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Menu : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private MouseState mouse;
        private SpriteBatch sprite;
        Texture2D menu_back;
        Texture2D new_game;
        Texture2D load_game;
        Texture2D exit_game;
        private bool noDraw;

        public Menu(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
            sprite = new SpriteBatch(game.GraphicsDevice);          
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            
            noDraw = false;
            mouse = Mouse.GetState();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            menu_back = Game.Content.Load<Texture2D>("menu tex/menu_background");
            new_game = Game.Content.Load<Texture2D>("menu tex/newgame_tex");
            load_game = Game.Content.Load<Texture2D>("menu tex/loadgame_tex");
            exit_game = Game.Content.Load<Texture2D>("menu tex/exitgame_tex");

            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            updateMouse();

            base.Update(gameTime);
        }

        public void updateMouse()
        {
            mouse = Mouse.GetState();
            int mouseX = mouse.X;
            int mouseY = mouse.Y;

            if (mouse.LeftButton == ButtonState.Pressed)
            {
                if (mouseX >= 625 && mouseX <= 625 + 200)
                {
                    if (mouseY >= 350 && mouseY <= 350+50)
                    {
                        beginNewGame();
                    }
                    else if (mouseY >= 450 && mouseY <= 450 + 50)
                    {
                        loadGame();
                    }
                    else if (mouseY >= 600 && mouseY <= 600 + 50)
                    {
                        Game.Exit();
                    }
                }
            }
        }

        public void beginNewGame()
        {
            noDraw = true;
        }

        public void loadGame()
        {
            noDraw = true;
        }

        public override void Draw(GameTime gameTime)
        {
            if (!noDraw)
            {
                sprite.Begin();
                sprite.Draw(menu_back, new Vector2(0, 0), Color.White);
                sprite.Draw(new_game, new Vector2(625, 350), Color.White);
                sprite.Draw(load_game, new Vector2(625, 450), Color.White);
                sprite.Draw(exit_game, new Vector2(625, 600), Color.White);
                sprite.End();
            }

            base.Draw(gameTime);
        }
    }
}
