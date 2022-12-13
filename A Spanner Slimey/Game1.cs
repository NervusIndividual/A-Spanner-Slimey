using A_Spanner_Slimey.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace A_Spanner_Slimey
{
    public class Game1 : Game
    {
        
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private List<Sprite> _sprites;

        private player_hero player;
        private Tree rand_tree;
        private Cabin the_house;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            player_hero.hero_load(Content);
            Tree.tree_load(Content);
            Cabin.cabin_load(Content);
            player = new player_hero();
            rand_tree = new Tree(Tree.full_tree_texture, _graphics);
            the_house = new Cabin(Cabin.cabin_texture, _graphics);
            player.hero_init(_graphics);
            
            _sprites = new List<Sprite> {
                player, rand_tree, the_house
                };  
        }

    protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (var sprite in _sprites)
                sprite.Update(gameTime, _sprites);

            player.hero_animate(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            foreach (var sprite in _sprites)
            {
                sprite.Draw(_spriteBatch);
            }
    
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}