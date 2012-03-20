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
    public class Player : Character
    {
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
            items = pInventory;
            position = pPosition;
            box = new BoundingBox();
            physics = new Physics();
            moveSpeed = 80.0f;
            jumpSpeed = 80.0f;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            sprite = new SpriteBatch(Game.GraphicsDevice);

            base.Initialize();
        }

        protected override void LoadContent()
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
                position = physics.horizontalMotion(position, moveSpeed, gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.A))
                position = physics.horizontalMotion(position, -moveSpeed, gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                position = physics.dynamicVerticalMotion(position, jumpSpeed, gameTime);
                jumpSpeed = physics.Velocity;
            }

            if (Keyboard.GetState().IsKeyUp(Keys.Space))
            {
                jumpSpeed = 80.0f;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            sprite.Begin();
            sprite.Draw(texture, position, Color.White);
            sprite.End();
            base.Draw(gameTime);
        }
    }
}
