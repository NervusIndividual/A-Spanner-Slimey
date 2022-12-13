using A_Spanner_Slimey.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_Spanner_Slimey.Sprites
{
    public class Sprite
    {
        protected Texture2D _texture;

        public Vector2 Position;
        public Vector2 Velocity;
        public Color color = Color.White;
        public float Speed;
        public Input Input;
        public Rectangle hitbox
        { get
            {
                return new Rectangle((int)Position.X+64, (int)Position.Y+64, _texture.Width/2, _texture.Width/2);
            } 
        }

        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {

        }

        public virtual void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(
                _texture,
                Position,
                Color.White
                );
        }
        #region Collision
        protected bool IsTouchingLeft(Sprite sprite)
        {
            return hitbox.Right + Velocity.X > sprite.hitbox.Left &&
                    hitbox.Left < sprite.hitbox.Left &&
                    hitbox.Bottom > sprite.hitbox.Top &&
                    hitbox.Top < sprite.hitbox.Bottom;
        }

        protected bool IsTouchingRight(Sprite sprite)
        {
            return this.hitbox.Left + this.Velocity.X < sprite.hitbox.Right &&
                this.hitbox.Right > sprite.hitbox.Right &&
                this.hitbox.Bottom > sprite.hitbox.Top &&
                this.hitbox.Top < sprite.hitbox.Bottom;
        }

        protected bool IsTouchingTop(Sprite sprite)
        {
            return this.hitbox.Bottom + this.Velocity.Y > sprite.hitbox.Top &&
                this.hitbox.Top < sprite.hitbox.Top &&
                this.hitbox.Right > sprite.hitbox.Left &&
                this.hitbox.Left < sprite.hitbox.Right;
        }

        protected bool IsTouchingBottom(Sprite sprite)
        {
            return this.hitbox.Top + this.Velocity.Y < sprite.hitbox.Bottom &&
                this.hitbox.Bottom > sprite.hitbox.Bottom &&
                this.hitbox.Right > sprite.hitbox.Left &&
                this.hitbox.Left < sprite.hitbox.Right;
        }

        #endregion
    }
}
