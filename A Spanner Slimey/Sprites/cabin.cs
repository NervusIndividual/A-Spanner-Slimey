using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_Spanner_Slimey.Sprites
{
    public class Cabin: Sprite
    {
        public static Texture2D cabin_texture;

        public Cabin(Texture2D texture, GraphicsDeviceManager _graphics) 
        {
            Position = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            _texture = texture;
        }

        public static void cabin_load(ContentManager Content)
        {
            cabin_texture = Content.Load<Texture2D>("Images\\Objects\\Log_cabin");
        }
    }
}
