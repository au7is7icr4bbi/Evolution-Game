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
    public class Biome : Microsoft.Xna.Framework.GameComponent
    {
        public enum nameId { NORMAL, JUNGLE, DESERT, OCEAN, HELL, VOLCANIC, SNOW };
        public enum typeId { ATMOS, GROUND, MIDGROUND, LOWGROUND };
        private nameId name;
        private typeId type;
        private int width;
        private int height;
        private Vector2 position;
        private List<Block.bType> blocktypes;
        private List<Block> blocks;
        private List<int> spawnPercent;
        static Random r = new Random();

        public Biome(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        public Biome(Game game, nameId bName, typeId bType, int bWidth, int bHeight, Vector2 bPosition)
            : base(game)
        {
            name = bName;
            type = bType;
            blocktypes = new List<Block.bType>();
            blocks = new List<Block>();
            spawnPercent = new List<int>();
            width = bWidth;
            height = bHeight;
            position = bPosition;

            generateBiome();
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here\

            base.Initialize();
        }

        public void setBlockTypes()
        {
            switch (name)
            {
                // for the normal biome...
                case Biome.nameId.NORMAL:
                    switch (type)
                    {
                        // at ground level
                        case Biome.typeId.GROUND:
                            blocktypes.Add(Block.bType.AIR);
                            blocktypes.Add(Block.bType.DIRT);
                            blocktypes.Add(Block.bType.WATER);
                            blocktypes.Add(Block.bType.MUD);

                            // set percentage liklihood of spawning blocks within the biome
                            // they are in the same order that the blocks above were added
                            spawnPercent.Add(5);
                            spawnPercent.Add(80);
                            spawnPercent.Add(10);
                            spawnPercent.Add(5);
                        break;

                        case Biome.typeId.ATMOS:
                            blocktypes.Add(Block.bType.AIR);
                            spawnPercent.Add(100);

                        break;
                    }
                break;
            }
        }

        // generates a biome filled with the specified blocks
        public void generateBiome()
        {
            setBlockTypes();
            for (int x = 0; x < width; x += 15)
            {
                for (int y = 0; y < height; y += 15)
                {
                    Block block = new Block(this.Game, blocktypes.ElementAt(generateBlock()), 
                        new Vector2(x, Game.GraphicsDevice.DisplayMode.Height - y), 5, 1);

                    blocks.Add(block);
                }   
            }
        }

        // generates a random number
        static int generateRandomNumber()
        { 
              int n = r.Next(100);
              return n;
        }

        // generates a random block from the blockTypes list
        public int generateBlock()
        {
            int n = generateRandomNumber();
            int currTotal = 0;

            if (n < spawnPercent[0])
            {
                n = 0;
                return n;
            }

            currTotal = spawnPercent[0];
            for (int i = 1; i < spawnPercent.Count; i++)
            {
                currTotal += spawnPercent[i];
                if (n > spawnPercent[i - 1] && n < currTotal)
                {
                    n = i;
                    return n;
                }
            }

            return 0;
        }

        public void LoadContent()
        {
            foreach (Block b in blocks)
            {
                b.LoadContent();
            }
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

        // calls the block draw method to render the blocks on screen
        public void Draw(SpriteBatch sprite)
        {
            foreach (Block b in blocks)
            {
                b.Draw(sprite);
            }
        }
    }
}