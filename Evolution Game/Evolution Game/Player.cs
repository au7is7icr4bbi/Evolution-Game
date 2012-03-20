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
    public class Player : Microsoft.Xna.Framework.GameComponent
    {
        private int health;
        private int mana;
        private Inventory inventory;
        private Texture2D texture;
        private const int moveSpeed = 2;
        private const int jumpHeight = 20;
        private Vector2 position;
        private SpriteBatch sprite;

        public Player(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        public Player(Game game, int pHealth, int pMana, Inventory pInventory, Vector2 pPosition)
            : base(game)
        {
            health = pHealth;
            mana = pMana;
            inventory = pInventory;
            position = pPosition;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        public void LoadContent()
        {
            sprite = new SpriteBatch(Game.GraphicsDevice);
            texture = Game.Content.Load<Texture2D>("player tex/player_tex");
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            if (Keyboard.GetState().IsKeyDown(Keys.D))
                position.X += moveSpeed;

            if (Keyboard.GetState().IsKeyDown(Keys.A))
                position.X -= moveSpeed;

            base.Update(gameTime);
        }

        public void Draw(SpriteBatch sprite)
        {
            //sprite.Begin();
                sprite.Draw(texture, position, Color.White);
            //sprite.End();
        }
    }
}
