using System;
using System.Collections.Generic;
using first_mono_game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


public class Board : IDrawMe
{
    private ContentManager content;

    private List<BoardObject> objectsOnBoard;

    private readonly Texture2D groundSprite;
    private readonly Texture2D wallSprite;
    private readonly Texture2D goalSprite;
    private readonly Texture2D currentSprite;


    //constants
    public const char PLAYER = 'P';
    public const char GROUND = 'O';
    public const char WALL = 'X';
    public const char GOAL = 'G';
    public const char HOLE = 'H';

    private char[,] board;

    Vector2 boardOffset;

    public Board(ContentManager _content)
    {
        content = _content;
        groundSprite = content.Load<Texture2D>("Sprites/Tile");
        wallSprite = content.Load<Texture2D>("Sprites/Box");
        goalSprite = content.Load<Texture2D>("Sprites/Pickup");
    }

    public int GetBoardWidth() => board.GetLength(0) - 1;
    public int GetBoardHeight() => board.GetLength(1) - 1;
    public Vector2 GetBoardOffset() => boardOffset;



    public bool IsSpaceWalkable(int x, int y)
    {
        if (IsPositionOutsideBoard(x, y)) return false;
        if (board[x, y] == HOLE) return false;
        return board[x, y] != WALL;
    }
    public bool IsPositionOutsideBoard(int x, int y)
    {
        if (x > GetBoardWidth() || x < 0) return true;
        if (y > GetBoardHeight() || y < 0) return true;
        return false;
    }

    private static char[,] RotateArrayClockwise(char[,] src)
    {
        int width;
        int height;
        char[,] dst;

        width = src.GetUpperBound(0) + 1;
        height = src.GetUpperBound(1) + 1;
        dst = new char[height, width];

        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                int newRow;
                int newCol;

                newRow = col;
                newCol = height - (row + 1);

                dst[newCol, newRow] = src[col, row];
            }
        }

        return dst;
    }

    public void SetBoard(char[,] newBoard)
    {
        for (int i = 0; i < 3; i++)
        {
            newBoard = RotateArrayClockwise(newBoard);
        }

        objectsOnBoard = new List<BoardObject>();
        int width = newBoard.GetLength(0);
        int height = newBoard.GetLength(1);
        board = new char[width, height];


        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                board[x, y] = newBoard[x, height - 1 - y];

                switch (board[x, y])
                {
                    case PLAYER:
                        objectsOnBoard.Add(new Bloxoid(x, y, content, this));
                        board[x, y] = GROUND;
                        break;
                }
            }
        }
        boardOffset = new Vector2((board.GetLength(0) * 8) / 2, (board.GetLength(1) * 8) / 2);

    }

    public void Update(float dt)
    {
        for (int i = 0; i < objectsOnBoard.Count; i++)
        {
            objectsOnBoard[i].Update(dt);
        }
    }

    public void Draw(SpriteBatch sprite, Vector2 offset)
    {

        if (board == null) return;
        for (int x = 0; x < board.GetLength(0); x++)
        {
            for (int y = 0; y < board.GetLength(1); y++)
            {
                Vector2 position = new Vector2(x * 8, y * 8);
                position += offset;

                switch (board[x, y])
                {
                    case GROUND:
                      sprite.Draw(groundSprite, position, Color.White);
                        break;
                    case WALL:
                        sprite.Draw(wallSprite, position, Color.White);
                        break;
                    case HOLE:
                        break;
                    case GOAL:
                        sprite.Draw(goalSprite, position, Color.White);
                        break;
                }
            }
        }

        for (int i = 0; i < objectsOnBoard.Count; i++)
        {
            objectsOnBoard[i].Draw(sprite, offset);
        }


    }
}
