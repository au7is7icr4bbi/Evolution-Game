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
    public class Block : Microsoft.Xna.Framework.GameComponent
    {
        public enum bType { AIR, WATER, DIRT, MUD, WOOD, STONE, COAL, CLAY, COPPER, TIN, IRON, SILVER, GOLD };
        private bType type;
        private Vector2 position;
        private int hitsToBreak;
        private int tierLvl;
        private Texture2D texture;
        private BoundingBox box;
        SpriteBatch sprite;

        public Block(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        public Block(Game game, bType blockType, Vector2 pos, int hits, int tier) : base(game)
        {
            type = blockType;
            position = pos;
            hitsToBreak = hits;
            tierLvl = tier;  
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
            if (type != bType.AIR)
                texture = Game.Content.Load<Texture2D>("block tex/" + type.ToString().ToLower() + "_tex");
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }

        // draws the block in its set position
        public void Draw(SpriteBatch sprite)
        {
            if (texture != null)
            {
                //sprite.Begin();
                sprite.Draw(texture, position, Color.White);
                //sprite.End();
            }
        }
    }
}
