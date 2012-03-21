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
        private float cameraPosition;
        private Player player;
        private SpriteBatch spriteBatch;

        public World(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        public World(Game game, int wHeight, int wWidth, SpriteBatch batch, Player p)
            : base(game)
        {
            height = wHeight;
            width = wWidth;
            biomes = new List<Biome>();
            spriteBatch = batch;
            player = p;
            addBiomes();
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            player.Initialize();

            base.Initialize();
        }

        // added biomes MUST be a MULTIPLE of 15, otherwise there are calculation problems
        public void addBiomes()
        {
            biomes.Add(new Biome(this.Game, Biome.nameId.NORMAL, Biome.typeId.ATMOS, new Vector2(1,1), 2700, 1500,
                new Vector2(0, 0)));
            biomes.Add(new Biome(this.Game, Biome.nameId.NORMAL, Biome.typeId.GROUND, new Vector2(1,2), 2700, 300,
                new Vector2(1366 / 2.0f, 650)));
        }

        protected override void LoadContent()
        {
            player.LoadContent();
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
            player.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            ScrollCamera(spriteBatch.GraphicsDevice.Viewport);
            Matrix cameraTransform = Matrix.CreateTranslation(-cameraPosition, 0.0f, 0.0f);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, cameraTransform);
            
            foreach (Biome b in biomes)
            {
                b.Draw(spriteBatch, cameraPosition);
            }
            player.Draw(spriteBatch);
            spriteBatch.End();
            
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
    }
}
