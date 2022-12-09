using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace A_Spanner_Slimey
{
    public class Game1 : Game
    {
        float timer;
        int hero_fps;
        
        Rectangle[] sourceRectangles;

        byte HeroAnimationIndex;

        player_hero player = new player_hero();
        tree rand_tree = new tree();
        cabin the_house = new cabin();
        Random a_random_num = new Random();

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.PreferredBackBufferHeight = 900;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            rand_tree.treePosition = new Vector2(a_random_num.Next(64, _graphics.PreferredBackBufferWidth - 64), a_random_num.Next(128, _graphics.PreferredBackBufferHeight - 128));
            player.hero_init(_graphics);
            the_house.cabin_init(_graphics);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            timer = 0;
            hero_fps = 125;

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

            player_hero.hero_load(Content);
            the_house.cabinFace = Content.Load<Texture2D>("Images\\Objects\\Log_cabin");
            rand_tree.treeFace = rand_tree.treeTexture_full = Content.Load<Texture2D>("Images\\Objects\\Tree_Full");
            rand_tree.treeTexture_bald = Content.Load<Texture2D>("Images\\Objects\\Tree_Bald");
        }

    protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

        // TODO: Add your update logic here

        // MOVEMENT LOGIC
        var kstate = Keyboard.GetState();

        if (kstate.IsKeyDown(Keys.Up) || kstate.IsKeyDown(Keys.W))
        {
            player.heroPosition.Y -= player.heroSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            player.heroFace = player_hero.heroTexture_walking;
        }

        if (kstate.IsKeyDown(Keys.Down) || kstate.IsKeyDown(Keys.S))
        {
            player.heroPosition.Y += player.heroSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            player.heroFace = player_hero.heroTexture_walking;
        }

        if (kstate.IsKeyDown(Keys.Left) || kstate.IsKeyDown(Keys.A))
        {
            player.heroPosition.X -= player.heroSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            player.heroFace = player_hero.heroTexture_walking;
            player.heroDirection = "Left";
        }

        if (kstate.IsKeyDown(Keys.Right) || kstate.IsKeyDown(Keys.D))
        {
            player.heroPosition.X += player.heroSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            player.heroFace = player_hero.heroTexture_walking;
            player.heroDirection = "Right";
        }

        if (kstate.IsKeyDown(Keys.Space))
        {
            player.heroFace = player_hero.heroTexture_whacking;
            player.heroState = "whacking";
        }

        if (kstate.GetPressedKeyCount() == 0)
        {
            if (HeroAnimationIndex >= 3)
            {
                HeroAnimationIndex = 0;
            }
            player.heroFace = player_hero.heroTexture_idle;
            player.heroState = "idling";

        }
        // ANIMATION LOGIC
        if (player.heroState == "idling")
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
        if (player.heroState == "walking")
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
        if (player.heroState == "whacking")
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

        //Check Collision

        /*if (player.hero_hitbox.X < the_house.cabin_hitbox.X + the_house.cabin_hitbox.Width &&
            player.hero_hitbox.X + player.hero_hitbox.Width > the_house.cabin_hitbox.X &&
            player.hero_hitbox.Y < the_house.cabin_hitbox.Y + the_house.cabin_hitbox.Height &&
            player.hero_hitbox.Y + player.hero_hitbox.Height > the_house.cabin_hitbox.Y)
                {
                    player.heroSpeed = 0;
                }*/

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();

        //Tree Draw
        _spriteBatch.Draw(
            rand_tree.treeFace,
            rand_tree.treePosition,
            null,
            Color.White,
            0f,
            new Vector2(rand_tree.treeFace.Width / 2, rand_tree.treeFace.Height / 2),
            Vector2.One,
            SpriteEffects.None,
            0f
            );

        //Hero Draw
        if (player.heroDirection == "Left")
        {
            _spriteBatch.Draw(
                player.heroFace,
                player.heroPosition,
                sourceRectangles[HeroAnimationIndex],
                Color.White,
                0f,
                new Vector2(sourceRectangles[HeroAnimationIndex].Width / 2, sourceRectangles[HeroAnimationIndex].Height / 2),
                Vector2.One,
                SpriteEffects.FlipHorizontally,
                0f
                );
        }
        else if (player.heroDirection == "Right")
        {
                _spriteBatch.Draw(
                player.heroFace,
                    player.heroPosition,
                    sourceRectangles[HeroAnimationIndex],
                    Color.White,
                    0f,
                    new Vector2(sourceRectangles[HeroAnimationIndex].Width / 2, sourceRectangles[HeroAnimationIndex].Height / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    0f
                    );
        }
        //Cabin Draw
        _spriteBatch.Draw(
            the_house.cabinFace,
            the_house.cabinPosition,
            null,
            Color.White,
            0f,
            new Vector2(the_house.cabinFace.Width / 2, the_house.cabinFace.Height / 2),
            Vector2.One,
            SpriteEffects.None,
            0f
            );

        _spriteBatch.End();
        base.Draw(gameTime);
        }
    }
}