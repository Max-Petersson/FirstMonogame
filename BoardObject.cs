using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace first_mono_game;

public abstract class BoardObject : IDrawMe, IUpdate
{
    protected Vector2 truePosition;
    protected Texture2D sprite;
    protected int x;
    protected int y;

    protected BoardObject(int x, int y)
    {
        this.x = x;
        this.y = y;
        truePosition  = new Vector2(x * 8, y * 8);
    }

    public int GetX() => x;
    public int GetY() => y;


    public abstract bool AttemptMove(int xMove, int yMove);

    protected void DoMove(int xMove,int yMove)
    {
        x += xMove;
        y += yMove;

        truePosition = new Vector2(x * 8, y * 8);
    }


    public virtual void Draw(SpriteBatch batch, Vector2 offset)
    {
        batch.Draw(sprite,truePosition + offset, Color.White);
    }

    public virtual void Update(float dt)
    {
    }
}