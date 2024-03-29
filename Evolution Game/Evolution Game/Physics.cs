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
    public class Physics
    {
        private const float gravity = -100.0f;
        private float nextVelocity;
        public float Velocity { get { return nextVelocity; } }
        public Physics()
        {
            // TODO: Construct any child components here
            nextVelocity = 0.0f;
        }

        public Vector2 dynamicVerticalMotion(Vector2 startPos, float velocity, GameTime gameTime)
        {
            Vector2 endPos = new Vector2();
            endPos.X = startPos.X;
            endPos.Y = startPos.Y - (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds - 0.5f * gravity * (float)gameTime.ElapsedGameTime.TotalSeconds * (float)gameTime.ElapsedGameTime.TotalSeconds);
            nextVelocity = velocity + gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            return endPos;
        }

        public Vector2 horizontalMotion(Vector2 startPos, float velocity, GameTime gameTime)
        {
            Vector2 endPos = new Vector2();
            endPos.X = startPos.X + velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            endPos.Y = startPos.Y;
            return endPos;
        }
    }
}
