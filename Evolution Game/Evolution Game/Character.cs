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
    public abstract class Character : Microsoft.Xna.Framework.DrawableGameComponent
    {
        protected int id;
        protected int health;
        protected int mana;
        protected Inventory items;
        protected Texture2D texture;
        protected Vector2 position;
        protected SpriteBatch sprite;
        protected Physics physics;
        protected BoundingBox box;
        protected float moveSpeed;
        protected float jumpSpeed;

        public Character(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
