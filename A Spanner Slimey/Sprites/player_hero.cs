using A_Spanner_Slimey.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;



namespace A_Spanner_Slimey.Sprites
{
    public class player_hero: Sprite
    {
        public static Texture2D heroTexture_idle;
        public static Texture2D heroTexture_walking;
        public static Texture2D heroTexture_whacking;
        public Vector2 to_spot;
        public float timer;
        public int hero_fps;
        public byte HeroAnimationIndex;
        public string heroState = "idling";
        public string heroDirection = "Right";
        public Rectangle hero_hitbox;
        public Rectangle wrench_hitbox;
        Rectangle[] sourceRectangles;
        Animator animator = new Animator();
        
        public void hero_init(GraphicsDeviceManager _graphics)
        {
            Position = new Vector2(100, 100);/*(_graphics.PreferredBackBufferWidth / 2, (_graphics.PreferredBackBufferHeight / 2) - 70);*/
            to_spot = Position;
            Speed = 125f;
            Velocity = Vector2.Zero;
            timer = 0;
            hero_fps = 100;

            _texture = heroTexture_idle;

            #region blah rectangles

            //rectangles for running sprite sheet animations
            sourceRectangles = new Rectangle[8];
            sourceRectangles[0] = new Rectangle(0, 0, 64, 64);
            sourceRectangles[1] = new Rectangle(64, 0, 64, 64);
            sourceRectangles[2] = new Rectangle(0, 64, 64, 64);
            sourceRectangles[3] = new Rectangle(64, 64, 64, 64);
            sourceRectangles[4] = new Rectangle(0, 128, 64, 64);
            sourceRectangles[5] = new Rectangle(64, 128, 64, 64);
            sourceRectangles[6] = new Rectangle(0, 192, 64, 64);
            sourceRectangles[7] = new Rectangle(64, 192, 64, 64);

            #endregion
        }
        public static void hero_load(ContentManager Content)
        {
            heroTexture_idle = Content.Load<Texture2D>("Images\\Spanner\\SP_Idle");
            heroTexture_walking = Content.Load<Texture2D>("Images\\Spanner\\SP_Walking");
            heroTexture_whacking = Content.Load<Texture2D>("Images\\Spanner\\SP_Whacking");
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Move(gameTime);
            animator.hero_animate(gameTime, this);


            foreach (var sprite in sprites) 
            {
                if (sprite == this)
                    continue;

                if((this.Velocity.X > 0 && IsTouchingLeft(sprite)) ||
                    (this.Velocity.X < 0 && IsTouchingRight(sprite)))
                    this.Velocity.X = 0;

                if((this.Velocity.Y > 0 && IsTouchingTop(sprite)) ||
                    (this.Velocity.Y < 0 && IsTouchingBottom(sprite)))
                    this.Velocity.Y = 0;
            }
            
            Position += Velocity;
            Velocity = Vector2.Zero;
            
        }

        private void Move(GameTime gameTime)
        {
            // MOVEMENT LOGIC
            var mousestate = Mouse.GetState();

            if (mousestate.LeftButton == ButtonState.Pressed)
            {
                to_spot = new Vector2((int)mousestate.X, (int)mousestate.Y);
                if(to_spot.X < Position.X)
                {
                    heroDirection = "Left";
                    _texture = heroTexture_walking;
                }
                if(to_spot.X > Position.X)
                {
                    heroDirection = "Right";
                    _texture = heroTexture_walking;
                }
            }

            if (hitbox.Contains(to_spot))
            {
                if (HeroAnimationIndex >= 3)
                {
                    HeroAnimationIndex = 0;
                }
                _texture = heroTexture_idle;
                heroState = "idling";
                Velocity = Vector2.Zero;
                to_spot = Position;
                return;
            }

            if (to_spot.X < Position.X)
            {
                Velocity.X = -Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (to_spot.X > Position.X)
            {
                Velocity.X = Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (to_spot.Y < Position.Y)
            {
                Velocity.Y = -Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (to_spot.Y > Position.Y)
            {
                Velocity.Y = Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            if (heroDirection == "Left")
            {
                _spriteBatch.Draw(
                    _texture,
                    Position,
                    sourceRectangles[HeroAnimationIndex],
                    Color.White,
                    0f,
                    new Vector2(sourceRectangles[HeroAnimationIndex].Width / 2, sourceRectangles[HeroAnimationIndex].Height / 2),
                    Vector2.One,
                    SpriteEffects.FlipHorizontally,
                    0f
                    );
            }
            else if (heroDirection == "Right")
            {
                _spriteBatch.Draw(
                    _texture,
                    Position,
                    sourceRectangles[HeroAnimationIndex],
                    Color.White,
                    0f,
                    new Vector2(sourceRectangles[HeroAnimationIndex].Width / 2, sourceRectangles[HeroAnimationIndex].Height / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    0f
                    );
            }
        }
    }
}
