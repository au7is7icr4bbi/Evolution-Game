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
    public class UserInterface
    {
        Texture2D cursor;
        Texture2D health_tex;
        Texture2D mana_tex;
        Game g;

        public UserInterface(Game game)
        {
            // TODO: Construct any child components here
           
            g = game;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public void Initialize()
        {
            // TODO: Add your initialization code here
            health_tex = g.Content.Load<Texture2D>("UI tex/health");
            mana_tex = g.Content.Load<Texture2D>("UI tex/mana");
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
        }

        public void Draw(SpriteBatch sprite, int health, int mana, Vector2 playerPos)
        {
            int numHearts = health / 10;

            for (int i = 0; i < numHearts; i++)
            {
                sprite.Draw(health_tex, new Vector2(playerPos.X + (i*30), 30), Color.White);
            }
        }
    }
}
