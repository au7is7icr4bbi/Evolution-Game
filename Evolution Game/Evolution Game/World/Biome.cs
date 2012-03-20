using System;
using System.IO; // allows the writing of biome data to a text file
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
    public class Biome
    {
        public enum nameId { NORMAL, JUNGLE, DESERT, OCEAN, HELL, VOLCANIC, SNOW };
        public enum typeId { ATMOS, GROUND, MIDGROUND, LOWGROUND };
        private nameId name;
        private typeId type;
        private int width;
        private int height;
        private Vector2 position;
        private List<Block> blocktypes;
        private List<Block> blocks;
        private List<int> spawnPercent;
        static Random r = new Random();
        Game game;

        public Biome(Game g)
        {
            // TODO: Construct any child components here
            game = g;
        }

        public Biome(Game g, nameId bName, typeId bType, int bWidth, int bHeight, Vector2 bPosition)
        {
            game = g;
            name = bName;
            type = bType;
            blocktypes = new List<Block>();
            blocks = new List<Block>();
            spawnPercent = new List<int>();
            width = bWidth;
            height = bHeight;
            position = bPosition;
            generateBiome();
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
                            blocktypes.Add(new Block(game, Block.bType.AIR, new Vector2(), 5, 1));
                            blocktypes.Add(new Block(game, Block.bType.DIRT, new Vector2(), 5, 1));
                            blocktypes.Add(new Block(game, Block.bType.WATER, new Vector2(), 5, 1));
                            blocktypes.Add(new Block(game, Block.bType.MUD, new Vector2(), 5, 1));

                            // set percentage liklihood of spawning blocks within the biome
                            // they are in the same order that the blocks above were added
                            spawnPercent.Add(5);
                            spawnPercent.Add(80);
                            spawnPercent.Add(10);
                            spawnPercent.Add(5);
                        break;

                        case Biome.typeId.ATMOS:
                            blocktypes.Add(new Block(game, Block.bType.AIR, new Vector2(), 5, 1));
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

            float endX = position.X + width / 2.0f;
            float endY = position.Y + height / 2.0f;
            int i = 0;

            for (float startX = position.X - width / 2.0f; startX < endX; startX += 15)
            {
                for (float startY = position.Y - height / 2.0f; startY < endY; startY += 15)
                {
                    Block block = new Block(blocktypes.ElementAt(generateBlock()));
                    blocks.Add(block);
                    blocks.ElementAt(i).setCoords(startX, startY);
                    i++;
                }
            }
        }

        // write the generated biome data to a text file
        /*
        public void BiomeFileWriter
        {
            StreamWriter sw = new StreamWriter(name + ".txt" );
            {
                tw.WriteLine();
                tw.close();
            }
        }
        */

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

        // calls the block draw method to render the blocks on screen
        public void Draw()
        {
            foreach (Block b in blocks)
            {
                b.Draw();
            }
        }
    }
}
