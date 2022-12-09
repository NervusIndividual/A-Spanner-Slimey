using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace A_Spanner_Slimey
{
    class player_hero
    {
        public Texture2D heroFace;
        public static Texture2D heroTexture_idle;
        public static Texture2D heroTexture_walking;
        public static Texture2D heroTexture_whacking;
        public Vector2 heroPosition;
        public float heroSpeed;
        public float heroVelocity;
        public string heroState = "idling";
        public string heroDirection = "Right";
        public Rectangle hero_hitbox ;
        public Rectangle wrench_hitbox;

        public void hero_init(GraphicsDeviceManager _graphics)
        {
            this.hero_hitbox = new Rectangle(0,0,64,64);
            this.heroPosition = new Vector2(0, 0);/*(_graphics.PreferredBackBufferWidth / 2, (_graphics.PreferredBackBufferHeight / 2) - 70);*/
            this.heroSpeed = 125f;
            this.heroVelocity = 0f;
        }
        public static void hero_load(ContentManager Content) 
        {
            heroTexture_idle = Content.Load<Texture2D>("Images\\Spanner\\SP_Idle");
            heroTexture_walking = Content.Load<Texture2D>("Images\\Spanner\\SP_Walking");
            heroTexture_whacking = Content.Load<Texture2D>("Images\\Spanner\\SP_Whacking");
        }
    }

    class tree
    {
        public Texture2D treeFace;
        public Texture2D dead_treeFace;
        public Texture2D treeTexture_full;
        public Texture2D treeTexture_bald;
        public Vector2 treePosition;
        public Rectangle tree_hitbox;
        public int health = 2;

        public void get_hit()
        {
            if (health == 2) 
            {
                treeFace = dead_treeFace;
                health--;
            }
            if (health <= 1)
            {

            }
        }
    }

    class cabin
    {
        public Texture2D cabinFace;
        public Vector2 cabinPosition;
        public Rectangle cabin_hitbox;

        public void cabin_init(GraphicsDeviceManager _graphics)
        {
            cabin_hitbox = new Rectangle((int)cabinPosition.X, (int)cabinPosition.Y,20,20);
            cabinPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
        }
    }


    class log
    {
        public Texture2D logFace;
        public Vector2 logPosition;
        public Rectangle log_hitbox;
    }

    class blue_slime
    {
        public Texture2D blueSlimeFace;
        public Vector2 blueSlimePosition;
        public Rectangle blueSlime_hitbox;
    }
}
   