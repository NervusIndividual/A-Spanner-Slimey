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
        float timer;
        int hero_fps;
        public byte HeroAnimationIndex;
        public string heroState = "idling";
        public string heroDirection = "Right";
        public Rectangle hero_hitbox;
        public Rectangle wrench_hitbox;
        Rectangle[] sourceRectangles;
        
        

        public void hero_init(GraphicsDeviceManager _graphics)
        {
            Position = new Vector2(100, 100);/*(_graphics.PreferredBackBufferWidth / 2, (_graphics.PreferredBackBufferHeight / 2) - 70);*/
            Speed = 125f;
            Velocity = Vector2.Zero;
            timer = 0;
            hero_fps = 200;

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

            foreach(var sprite in sprites) 
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

            hero_animate(gameTime);
        }

        private void Move(GameTime gameTime)
        {
            // MOVEMENT LOGIC
            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Up) || kstate.IsKeyDown(Keys.W))
            {
                Velocity.Y = -Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                _texture = heroTexture_walking;
            }

            if (kstate.IsKeyDown(Keys.Down) || kstate.IsKeyDown(Keys.S))
            {
                Velocity.Y = Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                _texture = heroTexture_walking;
            }

            if (kstate.IsKeyDown(Keys.Left) || kstate.IsKeyDown(Keys.A))
            {
                Velocity.X = -Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                _texture = heroTexture_walking;
                heroDirection = "Left";
            }

            if (kstate.IsKeyDown(Keys.Right) || kstate.IsKeyDown(Keys.D))
            {
                Velocity.X = Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                _texture = heroTexture_walking;
                heroDirection = "Right";
            }

            if (kstate.IsKeyDown(Keys.Space))
            {
                _texture = heroTexture_whacking;
                heroState = "whacking";
            }

            if (kstate.GetPressedKeyCount() == 0)
            {
                if (HeroAnimationIndex >= 3)
                {
                    HeroAnimationIndex = 0;
                }
                _texture = heroTexture_idle;
                heroState = "idling";

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

        public void hero_animate(GameTime gameTime)
        {
            // ANIMATION LOGIC
            if (heroState == "idling")
            {
                if (timer > hero_fps)
                {
                    if (HeroAnimationIndex >= 3)
                    {
                        HeroAnimationIndex = 0;
                    }
                    else
                    {
                        HeroAnimationIndex++;
                    }
                    timer = 0;
                }
                else
                {
                    timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                }
            }
            if (heroState == "walking")
            {
                if (timer > hero_fps)
                {
                    if (HeroAnimationIndex >= 7)
                    {
                        HeroAnimationIndex = 0;
                    }
                    else
                    {
                        HeroAnimationIndex++;
                    }
                    timer = 0;
                }
                else
                {
                    timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                }
            }
            if (heroState == "whacking")
            {
                if (timer > hero_fps)
                {
                    if (HeroAnimationIndex >= 6)
                    {
                        HeroAnimationIndex = 0;
                    }
                    else
                    {
                        HeroAnimationIndex++;
                    }
                    timer = 0;
                }
                else
                {
                    timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                }
            }
        }
    }
}
