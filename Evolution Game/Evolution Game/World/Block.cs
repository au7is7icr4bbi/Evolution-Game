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
    /// </summary>
    public class Block
    {
        public enum bType { AIR, WATER, DIRT, MUD, WOOD, STONE, COAL, CLAY, COPPER, TIN, IRON, SILVER, GOLD };
        private bType type;
        private String fileStr;
        private Vector2 position;
        private int hitsToBreak;
        private int tierLvl;
        private Texture2D texture;
        private BoundingBox box;
        private Game game;

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

            initFileString();
        }

        public Block(Block b)
        {
            game = b.game;
            type = b.type;
            hitsToBreak = b.hitsToBreak;
            tierLvl = b.tierLvl;

            initFileString();
        }

        // sets the variable for the 2 char string that will represent the block in biome file/s
        public void initFileString()
        {
            switch (type)
            {
                case bType.DIRT:
                    fileStr = "db";
                    break;

                case bType.AIR:
                    fileStr = "ab";
                    break;

                case bType.MUD:
                    fileStr = "mb";
                    break;

                case bType.WATER:
                    fileStr = "wb";
                    break;
            }
        }

        public String getFileString()
        {
            return fileStr;
        }

        public void LoadContent()
        {
            if (type != bType.AIR)
                texture = game.Content.Load<Texture2D>("block tex/" + type.ToString().ToLower() + "_tex");
        }

        public void setCoords(float x, float y)
        {
            position.X = x;
            position.Y = y;
        }

        public void Update(GameTime gameTime)
        {
        }

        // draws the block in its set position
        public void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
            {
                spriteBatch.Draw(texture, position, Color.White);
            }
        } 
    }
}
