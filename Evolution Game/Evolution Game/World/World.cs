  using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
    public class World : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private String name;
        private int height;
        private int width;
        private List<Biome> biomes;
        private List<Spawn> playerSpawns;
        private float cameraPosition;
        private Player player;
        private SpriteBatch spriteBatch;

        public World(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
            player = new Player(Game);
        }

        // world height and width must be a multiple of 15 in order to work
        public World(Game game, int wHeight, int wWidth, SpriteBatch batch, Player pl)
            : base(game)
        {
            name = "World 1";
            height = wHeight;
            width = wWidth;
            spriteBatch = batch;

            biomes = new List<Biome>();
            playerSpawns = new List<Spawn>();
            player = pl;
        }

        // generates an entirely new world
        public void generateNewWorld(Game game, int wWidth, int wHeight, SpriteBatch batch)
        {
            name = "World 1";
            height = wHeight;
            width = wWidth;
            biomes = new List<Biome>();
            playerSpawns = new List<Spawn>();
            spriteBatch = batch;

            // adds each biome to the world
            addBiomes();

            // generate the blocks for each biome
            foreach (Biome b in biomes)
            {
                b.generateBiome();
            }

            // write the biomes to file
            worldFileWriter();

            // clears the biomes stored in memory so that duplicates dont appear when the .wld file is loaded
            biomes.Clear();
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            //player.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            player.LoadContent();
            foreach (Biome b in biomes)
            {
                b.LoadContent();
            }
        }

        // added biomes width and height MUST be a MULTIPLE of 15 AND 4, otherwise there are calculation problems
        // they should also be approximately (screenWidth / 4) pixels wide and (screenHeight / 4) pixels high
        public void addBiomes()
        {
            // Normal Atmosphere biomes
            // where y = 2
            biomes.Add(new Biome(this.Game, Biome.nameId.NORMAL, Biome.typeId.ATMOS, new Vector2(0, 2), 360, 300,
                new Vector2(180, 5), false));

            biomes.Add(new Biome(this.Game, Biome.nameId.NORMAL, Biome.typeId.ATMOS, new Vector2(1, 2), 360, 300,
                new Vector2(540, 5), false));

            biomes.Add(new Biome(this.Game, Biome.nameId.NORMAL, Biome.typeId.ATMOS, new Vector2(1, 2), 360, 300,
                new Vector2(900, 5), false));

            biomes.Add(new Biome(this.Game, Biome.nameId.NORMAL, Biome.typeId.ATMOS, new Vector2(1, 2), 360, 300,
                new Vector2(1260, 5), false));

            biomes.Add(new Biome(this.Game, Biome.nameId.NORMAL, Biome.typeId.ATMOS, new Vector2(1, 2), 360, 300,
                new Vector2(1620, 5), false));

            // where y = 1
            biomes.Add(new Biome(this.Game, Biome.nameId.NORMAL, Biome.typeId.ATMOS, new Vector2(0,1), 360, 300,
                new Vector2(180, 305), false));

            biomes.Add(new Biome(this.Game, Biome.nameId.NORMAL, Biome.typeId.ATMOS, new Vector2(1, 1), 360, 300,
                new Vector2(540, 305), false));

            biomes.Add(new Biome(this.Game, Biome.nameId.NORMAL, Biome.typeId.ATMOS, new Vector2(1, 1), 360, 300,
                new Vector2(900, 305), true));

            biomes.Add(new Biome(this.Game, Biome.nameId.NORMAL, Biome.typeId.ATMOS, new Vector2(1, 1), 360, 300,
                new Vector2(1260, 305), false));

            biomes.Add(new Biome(this.Game, Biome.nameId.NORMAL, Biome.typeId.ATMOS, new Vector2(1, 1), 360, 300,
                new Vector2(1620, 305), false));

            // Normal Ground Biomes           
            // where y = 0
            biomes.Add(new Biome(this.Game, Biome.nameId.NORMAL, Biome.typeId.GROUND, new Vector2(0,0), 360, 300,
                new Vector2(180, 605), false));

            biomes.Add(new Biome(this.Game, Biome.nameId.NORMAL, Biome.typeId.GROUND, new Vector2(1,0), 360, 300,
                new Vector2(540, 605), false));

            biomes.Add(new Biome(this.Game, Biome.nameId.NORMAL, Biome.typeId.GROUND, new Vector2(2, 0), 360, 300,
                new Vector2(900, 605), false));

            biomes.Add(new Biome(this.Game, Biome.nameId.NORMAL, Biome.typeId.GROUND, new Vector2(3, 0), 360, 300,
                            new Vector2(1260, 605), false));

            biomes.Add(new Biome(this.Game, Biome.nameId.NORMAL, Biome.typeId.GROUND, new Vector2(4, 0), 360, 300,
                            new Vector2(1620, 605), false));
            
            // where y = -1
            biomes.Add(new Biome(this.Game, Biome.nameId.NORMAL, Biome.typeId.GROUND, new Vector2(0, -1), 360, 300,
                new Vector2(180, 905), false));

            biomes.Add(new Biome(this.Game, Biome.nameId.NORMAL, Biome.typeId.GROUND, new Vector2(1, -1), 360, 300,
                new Vector2(540, 905), false));

            biomes.Add(new Biome(this.Game, Biome.nameId.NORMAL, Biome.typeId.GROUND, new Vector2(2, -1), 360, 300,
                new Vector2(900, 905), false));

            biomes.Add(new Biome(this.Game, Biome.nameId.NORMAL, Biome.typeId.GROUND, new Vector2(3, -1), 360, 300,
                            new Vector2(1260, 905), false));

            biomes.Add(new Biome(this.Game, Biome.nameId.NORMAL, Biome.typeId.GROUND, new Vector2(4, -1), 360, 300,
                            new Vector2(1620, 905), false));

        }

        public void worldFileWriter()
        {
            StreamWriter sw = new StreamWriter("../../../../Evolution GameContent/world data/" + name + ".wld");

            sw.WriteLine(width + "," + height);
            sw.WriteLine(biomes.Count);

            foreach (Biome b in biomes)
            {
                sw.WriteLine(b.getName() + "," + b.getType() + "," + b.getSegment().X + ","
                    + b.getSegment().Y + "," + b.getWidth() + "," + b.getHeight() + ","
                    + b.getPosition().X + "," + b.getPosition().Y + "," + b.spawnHere());

                if (b.spawnHere())
                {
                    player = new Player(Game, 100, 0, new Inventory(Game), new Spawn(Game, b, b.getPosition(), true));
                }
            }
            sw.Close();

            Console.WriteLine("World file " + name + ".wld written successfully");

            player.writePlayerFile();
        }

        // loads a .wld file which contains data on the worlds structure
        public void loadWorld()
        {
            int biomeCount = 0;
            char[] delim = new char[2];
            delim[0] = ',';

            StreamReader sr = new StreamReader("../../../../Evolution GameContent/world data/" + name + ".wld");
            string[] temp = sr.ReadLine().Split(delim);
            
            width = Convert.ToInt32(temp[0]);
            height = Convert.ToInt32(temp[1]);

            biomeCount = Convert.ToInt32(sr.ReadLine());

            for (int i = 0; i < biomeCount; i++)
            {
                temp = sr.ReadLine().Split(delim);

                biomes.Add(new Biome(Game, (Biome.nameId)Convert.ToInt32(temp[0]), (Biome.typeId)Convert.ToInt32(temp[1]),  // this line loads biome Game, name, type 
                    new Vector2(Convert.ToInt32(temp[2]), Convert.ToInt32(temp[3])),                                        // segment.X, segment.Y
                    Convert.ToInt32(temp[4]), Convert.ToInt32(temp[5]),                                                     // width, height
                    new Vector2(Convert.ToInt32(temp[6]), Convert.ToInt32(temp[7])), Convert.ToBoolean(temp[8])));          // position.X, position.Y, playerSpawn

                //biomes[i].printBiome(); //debug code
            }
            sr.Close();

            Console.WriteLine("World file " + name + ".wld read successfully");

            // load in stuff that exists in the world
            player.loadPlayerFile();
            loadBiomes(); // loads in all biome segments that exist in the players viewport              
        }

        public void loadBiomes()
        {
            // load in biome data
            foreach (Biome b in biomes)
            {
                Biome.nameId tempN = (Biome.nameId)b.getName();
                String n = tempN.ToString().ToLower();

                Biome.typeId tempT = (Biome.typeId)b.getType();
                String t = tempT.ToString().ToLower();

                b.loadBiome(n + "_" + t);

                if (b.spawnHere())
                {
                    player.setSpawn(new Spawn(Game, b, new Vector2(100, 300), true));
                    player.setCurrentBiome(b);
                }
            }

            LoadContent();
        }

        private void unloadBiomes()
        {

        }

        private void ScrollCamera(Viewport viewport)
        {
            const float ViewMargin = 0.5f;

            // Calculate the edges of the screen.
            float marginWidth = viewport.Width * ViewMargin;
            float marginLeft = cameraPosition + marginWidth;
            float marginRight = cameraPosition + viewport.Width - marginWidth;

            // Calculate how far to scroll when the player is near the edges of the screen.
            float cameraMovement = 0.0f;
            if (player.Position.X < marginLeft)
                cameraMovement = player.Position.X - marginLeft;
            else if (player.Position.X > marginRight)
                cameraMovement = player.Position.X - marginRight;

            // Update the camera position, but prevent scrolling off the ends of the level.
            float maxCameraPosition = 15 * width - viewport.Width;
            cameraPosition = MathHelper.Clamp(cameraPosition + cameraMovement, 0.0f, maxCameraPosition);
        }  

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            player.Update(gameTime);

            foreach (Biome b in biomes)
                b.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            ScrollCamera(spriteBatch.GraphicsDevice.Viewport);
            Matrix cameraTransform = Matrix.CreateTranslation(-cameraPosition, 0.0f, 0.0f);
            
            // draws things in the world using the spriteBatch
            spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, null, null, null, null, cameraTransform); 
                foreach (Biome b in biomes)
                {
                    b.Draw(spriteBatch, cameraPosition);
                }

                player.Draw(spriteBatch);
            spriteBatch.End();     
        }
    }
}
