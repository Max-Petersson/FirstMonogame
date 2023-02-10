using System;
using first_mono_game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
public class Board : IDrawMe
{
	private readonly Texture2D ground;
	private readonly Texture2D goal;
	//constants
	public const char PLAYER = 'P';
	public const char GROUND = 'X';
	public const char GOAL = 'G';
	public const char HOLE = 'H';

	char[,] level;

	

    public Board(ContentManager content)
    {
		
    }
    public void LoadLevel(char[,] level)
	{

	}

    public void Draw(SpriteBatch sprite, Vector2 offset)
    {
        throw new NotImplementedException();
    }
}
