using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace A_Spanner_Slimey.Sprites
{
    public class Tree: Sprite
    {
        public static Texture2D full_tree_texture;
        public static Texture2D hit_tree_texture;
        public int health;

        public Tree(Texture2D texture, GraphicsDeviceManager _graphics)
        {
            _texture = texture;
            Random a_random_num = new Random();
            Position = new Vector2(a_random_num.Next(64, _graphics.PreferredBackBufferWidth - 64), a_random_num.Next(128, _graphics.PreferredBackBufferHeight - 128));
        }

        public static void tree_load(ContentManager Content)
        {
            full_tree_texture = Content.Load<Texture2D>("Images\\Objects\\Tree_Full");
            hit_tree_texture = Content.Load<Texture2D>("Images\\Objects\\Tree_Bald");
        }
    }
}
