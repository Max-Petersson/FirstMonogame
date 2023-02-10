using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace first_mono_game;

public interface IDrawMe
{
    public void Draw(SpriteBatch sprite, Vector2 offset);
}