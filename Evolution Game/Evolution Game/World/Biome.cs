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
        Vector2 segment;
        private int width;
        private int height;
        private Vector2 position;
        private List<Block> blocktypes;
        private List<Block> blocks;
        private List<int> spawnPercent;
        private Layer[] layers;
        static Random r = new Random();
        Game game;

        public Biome(Game g)
        {
            // TODO: Construct any child components here
            game = g;
        }

        public Biome(Game g, nameId bName, typeId bType, Vector2 bSegment, int bWidth, int bHeight, Vector2 bPosition)
        {
            layers = new Layer[1];
            layers[0] = new Layer(g.Content, "biome tex/" + name + "_Layer0", 0.8f);
            //layers[1] = new Layer(g.Content, "biome tex/" + name + "Layer1", 0.5f);
            //layers[2] = new Layer(g.Content, "biome tex/" + name + "Layer2", 0.8f);
            game = g;
            name = bName;
            type = bType;
            blocktypes = new List<Block>();
            blocks = new List<Block>();
            spawnPercent = new List<int>();
            segment = bSegment;
            width = bWidth;
            height = bHeight;
            position = bPosition;
            
            generateBiome(); // generates the biome based on data
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
                            blocktypes.Add(new Block(game, Block.bType.AIR, new Vector2()));
                            blocktypes.Add(new Block(game, Block.bType.DIRT, new Vector2()));
                            blocktypes.Add(new Block(game, Block.bType.WATER, new Vector2()));
                            blocktypes.Add(new Block(game, Block.bType.MUD, new Vector2()));

                            // set percentage liklihood of spawning blocks within the biome
                            // they are in the same order that the blocks above were added
                            spawnPercent.Add(5);
                            spawnPercent.Add(80);
                            spawnPercent.Add(10);
                            spawnPercent.Add(5);
                        break;

                        case Biome.typeId.ATMOS:
                            blocktypes.Add(new Block(game, Block.bType.AIR, new Vector2()));
                            spawnPercent.Add(100);

                        break;
                    }
                break;
            }
        }

        public void LoadContent()
        {
            foreach (Block b in blocks)
            {
                b.LoadContent();
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

            biomeFileWriter();
        }

        // write the generated biome data to a text file
        public void biomeFileWriter()
        {
            StreamWriter sw = new StreamWriter("../../../../Evolution GameContent/world data/biome data/" +
                name.ToString().ToLower() + "_" + type.ToString().ToLower() + ".bio");

            sw.WriteLine(width / 15.0f + "," + height / 15.0f); // write the current number of blocks in the biome

            // write in each block
            for (int x = 0; x < blocks.Count; x++)
            {
                if (x % width == 0)
                {
                    sw.Write("\n");
                }
                sw.Write(blocks[x].getFileString() + "|");
            }
            sw.Close();
        }

        // reads in biome data file
        public void loadBiome(String fileName)
        {
            int numBlWide = 0;
            int numBlHigh = 0;
            int y = 0;
            char[] delim = new char[2];
            delim[0] = '|';
            delim[1] = ',';

            StreamReader sr = new StreamReader("../../../../Evolution GameContent/world data/biome data/" + fileName);
            try
            {
                String[] temp = sr.ReadLine().Split(delim);
                numBlWide = Convert.ToInt32(temp[0]);
                numBlHigh = Convert.ToInt32(temp[1]);
            }
            catch (FormatException e)
            {
                Console.WriteLine("Invalid File Format");
            }

            for (String temp = null; (temp = sr.ReadLine()) != null; )
            {
                String[] items = temp.Split(delim);

                for (int x = 0; x < items.Length; x++)
                {
                    Block newBlock = new Block(items[x], new Vector2(x * 15, y));
                    blocks.Add(newBlock);
                }
                y += 15;
            }
            sr.Close();
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

        public String getName()
        {
            return name.ToString();
        }

        public String getType()
        {
            return type.ToString();
        }

        public int getWidth()
        {
            return width;
        }

        public int getHeight()
        {
            return height;
        }

        public Vector2 getPosition()
        {
            return position;
        }

        // calls the block draw method to render the blocks on screen
        public void Draw(SpriteBatch spriteBatch, float cameraPosition)
        {
            for (int i = 0; i < layers.Length; ++i)
                layers[i].Draw(spriteBatch, cameraPosition);
            foreach (Block b in blocks)
                b.Draw(spriteBatch);
        }
    }
}
