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
        bool plSpawn;

        public Biome(Game g)
        {
            // TODO: Construct any child components here
            game = g;
        }

        public Biome(Game g, nameId bName, typeId bType, Vector2 bSegment, int bWidth, int bHeight, Vector2 bPosition, bool plSpawnHere)
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
            plSpawn = plSpawnHere;
        }


        // detmines what blocks are initially in the different biomes, blocktypes should be added in the order from most common to least common
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
                            blocktypes.Add(new Block(game, Block.bType.DIRT, new Vector2()));
                            blocktypes.Add(new Block(game, Block.bType.AIR, new Vector2()));
                            blocktypes.Add(new Block(game, Block.bType.WATER, new Vector2()));
                            blocktypes.Add(new Block(game, Block.bType.MUD, new Vector2()));

                            // set percentage liklihood of spawning blocks within the biome
                            // they are in the same order that the blocks above were added
                            spawnPercent.Add(80);
                            spawnPercent.Add(5);
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

            float endX = position.X + (width / 2.0f);
            float endY = position.Y + (height / 2.0f);
            int i = 0;

            for (float startX = position.X - (width / 2.0f); startX < endX; startX += 15)
            {
                for (float startY = position.Y - (height / 2.0f); startY < endY; startY += 15)
                {
                    Block block = new Block(blocktypes.ElementAt(0));
                    blocks.Add(block);
                    blocks.ElementAt(i).setCoords(startX, startY);
                    i++;
                }
            }

            generateBlocks();

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
                if (x % (width / 15.0f) == 0)
                {
                    sw.WriteLine();
                }
                sw.Write(blocks[x].getFileString() + "|");
            }
            sw.Close();

            Console.WriteLine("Biome file " + name.ToString().ToLower() + "_" + type.ToString().ToLower() + ".bio written successfully");
        }

        // reads in biome data file
        public void loadBiome(String fileName)
        {
            int numBlWide = 0;
            int numBlHigh = 0;
            float startX = position.X - (width / 2.0f);
            float startY = position.Y - (height / 2.0f);
            char[] delim = new char[3];
            delim[0] = '|';
            delim[1] = ',';
            delim[2] = '\n';

            StreamReader sr = new StreamReader("../../../../Evolution GameContent/world data/biome data/" + fileName);
            try
            {
                String[] temp = sr.ReadLine().Split(delim);
                numBlWide = Convert.ToInt32(temp[0]);
                numBlHigh = Convert.ToInt32(temp[1]);

                for (String temp1; (temp1 = sr.ReadLine()) != null; )
                {
                    String[] items = temp1.Split(delim);
                    startX = position.X - (width / 2.0f); 
                    
                    for (int x = 0; x < items.Length; x++)
                    {
                        Block newBlock = new Block(game, items[x], new Vector2(startX, startY));
                        newBlock.setCoords(startX, startY); // dont know why, but without this blocks dont draw correctly, doing x * 15 on the new vector above has no effect
                        blocks.Add(newBlock);

                        //Console.WriteLine(startX + "\t" + startY); // debug code
                        startX += 15;
                    }
                    startY += 15;
                }

            }
            catch (FormatException e)
            {
                Console.WriteLine("Invalid biome File Format");
            }

            sr.Close();

            Console.WriteLine("Biome file " + name.ToString().ToLower() + "_" + type.ToString().ToLower() + ".bio read successfully");
        }

        // generates a random number
        static int generateRandomNumber(int min, int max)
        {
            int n = r.Next(min, max);
            return n;
        }

        // generates a random block from the blockTypes list
        public int generateBlocks()
        {
            // positioning vars
            int startX = (int)position.X - (width / 2);
            int endX = (int)position.X + (width / 2);
            int startY = (int)position.Y - (height / 2);
            int endY = (int)position.Y + (height / 2);

            int numBlocks = (width / 15) * (height / 15);
            int n = generateRandomNumber(0, blocks.Count-1);
            int groupWidth = 0;
            int groupHeight = 0;
            List<int> groupPos= new List<int>();
            List<int> typeIndexes = new List<int>();

            for (int i = 0; i < n; i++)
            {
                groupPos.Add(generateRandomNumber(0, numBlocks));
                typeIndexes.Add(generateRandomNumber(0, blocktypes.Count));
            }

            for (int i = 0; i < groupPos.Count; i++)
            {
                int index = (width / 15) * (height / 15);
                Vector2 pos = blocks[groupPos[i]].Position;

                groupWidth = generateRandomNumber(20, 30);
                groupHeight = generateRandomNumber(20, 30);

                // ignore the primary type as it has already been added
                if (typeIndexes[i] != 0)
                {
                    // draws a block at the picked position
                    blocks[groupPos[i]] = new Block(game, blocktypes[typeIndexes[i]].Type, pos);

                    // adds a random sized cluster of a blocktype to the  world along the x-axis
                    for (int j = 0; j < groupWidth; j++)
                    {
                        // draws a block to the right of the picked position
                        if (i + j < groupPos.Count && groupPos[i + j] < blocks.Count)
                        {
                            pos = blocks[groupPos[i + j]].Position;
                            blocks[groupPos[i + j]] = new Block(game, blocktypes[typeIndexes[i]].Type, pos);
                        }

                        // draws a block to the left of the picked position
                        if (i - j >= 0 && groupPos[i - j] >= 0)
                        {
                            pos = blocks[groupPos[i - j]].Position;
                            blocks[groupPos[i - j]] = new Block(game, blocktypes[typeIndexes[i]].Type, pos);
                        }
                    }

                    // adds a random sized cluster of a blocktype to the  world along the y-axis
                    for (int j = 0; j < groupHeight; j++)
                    {
                        blocks[groupPos[i]] = new Block(game, blocktypes[typeIndexes[i]].Type, pos);

                        // draws a block to the above of the picked position
                        if (i + width < groupPos.Count && groupPos[i + width] < blocks.Count)
                        {
                            pos = blocks[groupPos[i + width]].Position;
                            blocks[groupPos[i + j]] = new Block(game, blocktypes[typeIndexes[i]].Type, pos);
                        }

                        // draws a block to the below of the picked position
                        if (i - width >= 0 && groupPos[i - width] >= 0)
                        {
                            pos = blocks[groupPos[i - j]].Position;
                            blocks[groupPos[i - j]] = new Block(game, blocktypes[typeIndexes[i]].Type, pos);
                        }
                    }
                }

            }

            return 0;
        }

        // calls the block draw method to render the blocks on screen
        public void Draw(SpriteBatch spriteBatch, float cameraPosition)
        {
            for (int i = 0; i < layers.Length; ++i)
                layers[i].Draw(spriteBatch, cameraPosition);
            
            foreach (Block b in blocks)
                b.Draw(spriteBatch);
        }

        // get and set methods
        public int getName()
        {
            return (int)name;
        }

        public int getType()
        {
            return (int)type;
        }

        public Vector2 getSegment()
        {
            return segment;
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

        public bool spawnHere()
        {
            return plSpawn;
        }

        //debug code
        public void printBiome()
        {
            Console.WriteLine(position.X + "\t" + position.Y + "\t" + spawnHere());
        }
    }

}
