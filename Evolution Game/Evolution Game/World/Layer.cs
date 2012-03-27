using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Evolution_Game
{
    class Layer
    {
        private Texture2D texture;
        private float scrollRate;
        public Biome owner;
        public Layer(ContentManager Content, String file, float scrollSpeed, Biome o)
        {
            // Assumes each layer only has 3 segments.
            texture = Content.Load<Texture2D>(file);
            owner = o;

            scrollRate = scrollSpeed;
        }

        public void Draw(SpriteBatch spriteBatch, float cameraPosition)
        {
            // Assume each segment is the same width.
            int segmentWidth = texture.Width;

            // Calculate which segments to draw and how much to offset them.
            float x = cameraPosition * scrollRate;
            int leftSegment = (int)Math.Floor(x / segmentWidth);
            x = (x / segmentWidth - leftSegment) * -segmentWidth;
            spriteBatch.Draw(texture, new Vector2(owner.getPosition().X - (owner.getWidth() / 2.0f), 0), 
                null, Color.White, 0.0f, new Vector2(0, 0.0f), 2.0f, SpriteEffects.None, 1.0f);
        }
    }
}
