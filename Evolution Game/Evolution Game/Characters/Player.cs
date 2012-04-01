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
using System.IO;


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

        public Player(Game g, int pHealth, int pMana, Inventory pInventory, Spawn pSpawn)
        {
            id = 0;
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
            base.LoadContent();
        }

        public void writePlayerFile()
        {
            StreamWriter sw = new StreamWriter("../../../../Evolution GameContent/world data/player data/player_" +
                id + ".plr");
            
            sw.WriteLine(id);
            sw.WriteLine(health +"," + mana);

            if (items.getTotalItems() == 0) // if there are no items in the inventory write "-1" 
            {
                sw.WriteLine("-1");
            }
            else // else write the id's of each item in the order the inventory stores them
            {
                for (int i = 0; i < items.getTotalItems(); i++)
                {
                    if (i != 0)
                        sw.Write(",");

                    sw.Write(items.getItem(i));
                }
            }

            sw.WriteLine(spawn.getPosition().X + "," + spawn.getPosition().Y);

            // writes ther players spawn biomes data
            sw.WriteLine(spawn.getBiome().getName().ToString() + "," + spawn.getBiome().getType().ToString() + "," +
                spawn.getBiome().getSegment().X + "," + spawn.getBiome().getSegment().Y + "," + 
                spawn.getBiome().getWidth() + "," + spawn.getBiome().getHeight() + "," +
                spawn.getBiome().getPosition().X + "," + spawn.getBiome().getPosition().Y);

            sw.Close();

            Console.WriteLine("Player file player_" + id + ".plr written successfully");
        }

        public void loadPlayerFile()
        {
            String[] temp;
            char[] delim = new char[2];
            delim[0] = ',';

            StreamReader sr = new StreamReader("../../../../Evolution GameContent/world data/player data/player_" +
                id + ".plr");

            temp = sr.ReadLine().Split(delim);
            id = Convert.ToInt32(temp[0]);

            temp = sr.ReadLine().Split(delim);
            health = Convert.ToInt32(temp[0]);
            mana = Convert.ToInt32(temp[1]);

            temp = sr.ReadLine().Split(delim);

            if (temp[0] == "-1")
            {
                items = null;
            }
            else
            {
                for (int i = 0; i < temp.Length; i++)
                {
                    items.addItem(Convert.ToInt32(temp[i]));
                }
            }

            temp = sr.ReadLine().Split(delim);
            spawn.setPosition(new Vector2(Convert.ToInt32(temp[0]), Convert.ToInt32(temp[1])));
            position = spawn.getPosition(); // players position begins at default spawn when a level is loaded

            temp = sr.ReadLine().Split(delim);
            currentBiome = new Biome(game, (Biome.nameId)Convert.ToInt32(temp[0]), (Biome.typeId)Convert.ToInt32(temp[1]),
                new Vector2(Convert.ToInt32(temp[2]), Convert.ToInt32(temp[3])), Convert.ToInt32(temp[4]), Convert.ToInt32(temp[5]),
                new Vector2(Convert.ToInt32(temp[6]), Convert.ToInt32(temp[7])), true);

            spawn.setBiome(currentBiome); // spawn biome and current biome are the same when the game is loaded

            sr.Close();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
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
        public void setSpawn(Spawn newSpawn)
        {
            spawn = newSpawn;
        }

        public void setCurrentBiome(Biome b)
        {
            currentBiome = b;
        }

        public float getSpawnPosition()
        {
            return spawn.getPosition().X;
        }

        public Vector2 getPlayerPos()
        {
            return position;
        }

        // gets the biome that the player is currently located in
        public Biome getCurrentBiome()
        {
            return currentBiome;
        }
    }
}
