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
    public class World : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private int height;
        private int width;
        private int numBlocks;
        private List<Biome> biomes;

        public World(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        public World(Game game, int wHeight, int wWidth)
            : base(game)
        {
            height = wHeight;
            width = wWidth;
            biomes = new List<Biome>();
            addBiomes();
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

        // added biomes MUST be a MULTIPLE of 15, otherwise there are calculation problems
        public void addBiomes()
        {
            biomes.Add(new Biome(this.Game, Biome.nameId.NORMAL, Biome.typeId.ATMOS, 2700, 1500,
                new Vector2(0, 0)));
            biomes.Add(new Biome(this.Game, Biome.nameId.NORMAL, Biome.typeId.GROUND, 2700, 300,
                new Vector2(1366 / 2.0f, 650)));
        }

        protected override void LoadContent()
        {
            foreach (Biome b in biomes)
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

        public override void Draw(GameTime gameTime)
        {
            foreach (Biome b in biomes)
            {
                b.Draw();
            }
        }
    }
}
