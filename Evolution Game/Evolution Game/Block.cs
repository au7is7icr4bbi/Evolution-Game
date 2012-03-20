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
    public class Block
    {
        public enum bType { AIR, WATER, DIRT, MUD, WOOD, STONE, COAL, CLAY, COPPER, TIN, IRON, SILVER, GOLD };
        private bType type;
        private Vector2 position;
        private int hitsToBreak;
        private int tierLvl;
        private Texture2D texture;
        private BoundingBox box;
        private Game game;
        SpriteBatch sprite;

        public Block(Game g)
        {
            // TODO: Construct any child components here
            game = g;
        }

        public Block(Game g, bType blockType, Vector2 pos, int hits, int tier)
        {
            game = g;
            type = blockType;
            position = pos;
            hitsToBreak = hits;
            tierLvl = tier;
            sprite = new SpriteBatch(game.GraphicsDevice);
        }

        public void LoadContent()
        {
            if (type != bType.AIR)
                texture = game.Content.Load<Texture2D>("block tex/" + type.ToString().ToLower() + "_tex");
        }

        public void Update(GameTime gameTime)
        {
        }

        // draws the block in its set position
        public void Draw()
        {
            if (texture != null)
            {
                sprite.Begin();
                sprite.Draw(texture, position, Color.White);
                sprite.End();
            }
        }
    }
}
