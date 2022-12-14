using A_Spanner_Slimey.Sprites;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_Spanner_Slimey.Models
{
    public class Animator
    {
        public void hero_animate(GameTime gameTime, player_hero player)
        { 
            // ANIMATION LOGIC
            if (player.heroState == "idling")
            {
                if (player.timer > player.hero_fps)
                {
                    if (player.HeroAnimationIndex >= 3)
                    {
                        player.HeroAnimationIndex = 0;
                    }
                    else
                    {
                        player.HeroAnimationIndex++;
                    }
                    player.timer = 0;
                }
                else
                {
                    player.timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                }
            }
            if (player.heroState == "walking")
            {
                if (player.timer > player.hero_fps)
                {
                    if (player.HeroAnimationIndex >= 7)
                    {
                        player.HeroAnimationIndex = 0;
                    }
                    else
                    {
                        player.HeroAnimationIndex++;
                    }
                    player.timer = 0;
                }
                else
                {
                    player.timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                }
            }
            if (player.heroState == "whacking")
            {
                if (player.timer > player.hero_fps)
                {
                    if (player.HeroAnimationIndex >= 6)
                    {
                        player.HeroAnimationIndex = 0;
                    }
                    else
                    {
                        player.HeroAnimationIndex++;
                    }
                    player.timer = 0;
                }
                else
                {
                    player.timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                }
            }
        }
    }
}
