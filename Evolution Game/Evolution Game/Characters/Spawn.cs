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
    public class Spawn : Microsoft.Xna.Framework.GameComponent
    {
        Game g;
        Biome spawnLocation;
        Vector2 position;
        bool defaultSpawn;

        public Spawn(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        public Spawn(Game game, Biome biome, Vector2 pos, bool defSpawn)
            :base(game)
        {
            spawnLocation = biome;
            position = pos;
            defaultSpawn = defSpawn;
            g = game;
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

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }

        // get/set methods
        public void setPosition(Vector2 pos)
        {
            position = pos;
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public Biome getBiome()
        {
            return spawnLocation;
        }
    }
}
