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
    public class Inventory
    {
        private int slots;
        private int armourSlots;
        private List<Item> items;

        public Inventory(Game game)
        {
            // TODO: Construct any child components here
            items = new List<Item>();
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public void Initialize()
        {
            // TODO: Add your initialization code here
            items = new List<Item>();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
        }

        // get/set methods
        public void addItem(int i)
        {
            
        }

        public int getTotalItems()
        {
            return items.Count;
        }

        // gets the specified item from the list
        public int getItem(int index)
        {
            return items[index].getId();
        }
    }
}
