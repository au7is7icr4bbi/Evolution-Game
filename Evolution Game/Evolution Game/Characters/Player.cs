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
using Evolution_Game.Characters;


namespace Evolution_Game
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Player : Character
    {
        public Player(Game g)
        {
            // TODO: Construct any child components here
            game = g;
            moveSpeed = 100.0f;
            jumpSpeed = 50.0f;
        }

        public Player(Game g, int pHealth, int pMana, Inventory pInventory, Vector2 pos)
        {
            health = pHealth;
            mana = pMana;
            items = pInventory;
            position = pos;
            spawn = null;
            box = new BoundingBox();
            physics = new Physics();
            moveSpeed = 100.0f;
            jumpSpeed = 50.0f;
            game = g;
        }

        public Player(Game g, int pHealth, int pMana, Inventory pInventory, Spawn pSpawn)
        {
            health = pHealth;
            mana = pMana;
            items = pInventory;
            position = pSpawn.getPosition();
            spawn = pSpawn;
            box = new BoundingBox();
            physics = new Physics();
            moveSpeed = 100.0f;
            jumpSpeed = 50.0f;
            game = g;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
        }

        public override void LoadContent()
        {
            texture = game.Content.Load<Texture2D>("player tex/player_tex");
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
                jumpSpeed = 100.0f;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        // get/set methods
        public float getSpawnPosX()
        {
            return spawn.getPosX();
        }

        public float getSpawnPosY()
        {
            return spawn.getPosY();
        }
    }
}
