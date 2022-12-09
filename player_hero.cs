using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace A_Spanner_Slimey;

class player_hero
{
    public Texture2D heroFace;
    public Texture2D heroTexture_idle;
    public Texture2D heroTexture_walking;
    public Texture2D heroTexture_chopping;
    public Vector2 heroPosition;
    public float heroSpeed;
    public string heroState = "idling";
    public string heroDirection = "Right";
}