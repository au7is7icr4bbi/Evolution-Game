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
    public abstract class Character
    {
        protected int id;
        protected int health;
        protected int mana;
        protected Inventory items;
        protected List<Texture2D> textures;
        protected Vector2 position;
        public Vector2 Position { get { return position; } }
        protected Physics physics;
        protected BoundingBox box;
        protected float moveSpeed;
        protected float jumpSpeed;
        protected Game game;
        protected Biome currentBiome;
        protected Spawn spawn;

        public Character()
        {
        }

        public virtual void Initialize()
        {
            items.Initialize();
        }

        public virtual void LoadContent()
        {
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}
