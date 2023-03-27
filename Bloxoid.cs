using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;



namespace first_mono_game
{
    public class Bloxoid : BoardObject
    {
        private Board board;


        private int rotationTest = 0;
        private int x2;
        private int y2;
        private Vector2 secondPosition;
        protected Texture2D sprite1;
        protected Texture2D sprite2;

        private enum BloxState
        {
            Standing,
            Laying
        }

        private BloxState currentState;

        public Bloxoid(int x, int y, ContentManager contentManager, Board board) : base(x, y)
        {
            sprite1 = contentManager.Load<Texture2D>("Sprites/Player");
            sprite2 = contentManager.Load<Texture2D>("Sprites/BloxoidLaying");
            sprite = sprite1;

            truePosition = new Vector2(x * 8, y * 8);
            x2 = x;
            y2 = y;
            secondPosition = new Vector2(x2 * 8, y2 * 8);
            currentState = BloxState.Standing;

            this.board = board;
        }

        public override void Update(float dt)
        {
            int xStep = 0;
            int yStep = 0;

            if (InputSystem.IsKeyPressed(Keys.Right)) xStep += 1;

            if (InputSystem.IsKeyPressed(Keys.Left)) xStep -= 1;

            if (InputSystem.IsKeyPressed(Keys.Up)) yStep -= 1;

            if (InputSystem.IsKeyPressed(Keys.Down)) yStep += 1;

            if (InputSystem.IsKeyPressed(Keys.D)) rotationTest += 30;
            if (InputSystem.IsKeyPressed(Keys.A)) rotationTest -= 30;

            if (xStep == 0 && yStep == 0) return;

            Console.WriteLine("X : " + xStep + " Y : " + yStep);
            AttemptMove(xStep, yStep);


        }


        public override bool AttemptMove(int xMove, int yMove)
        {

            if (currentState == BloxState.Standing)
            {
                if (board.IsSpaceWalkable(x + xMove, y + yMove) == false) return false;
                if (board.IsSpaceWalkable(x + (xMove * 2), y + (yMove * 2)) == false) return false;

            }
            else
            {
                if (board.IsSpaceWalkable(x + xMove, y + yMove) == false) return false;
                if (board.IsSpaceWalkable(x2 + xMove, y2 + yMove) == false) return false;
            }




            //if (board.IsPositionOutsideBoard(x + xMove, y + yMove) == false) return false;
            if (currentState == BloxState.Standing)
            {
                DoMove(xMove * 2, yMove * 2);
                DoSecondMove(xMove, yMove);

                currentState = BloxState.Laying;
            }
            else if (currentState == BloxState.Laying)
            {

                Vector2 nextMove = new Vector2(xMove, yMove);
                Vector2 SecondPosDir = new Vector2(x2 - x, y2 - y);

                if (nextMove == SecondPosDir)
                {
                    DoMove(xMove * 2, yMove * 2);
                    DoSecondMove(xMove, yMove);

                    currentState = BloxState.Standing;
                }
                else if (nextMove == -SecondPosDir) // Laying and moving away 
                {
                    DoMove(xMove, yMove);
                    DoSecondMove(xMove * 2, yMove * 2);

                    currentState = BloxState.Standing;
                }
                else
                {
                    DoMove(xMove, yMove);
                    DoSecondMove(xMove, yMove);
                    currentState = BloxState.Laying;
                }

            }

            //UpdateSprite();
            return true;
        }

        //private void UpdateSprite()
        //{
        //    if (currentState == BloxState.Standing)
        //    {
        //        sprite = sprite1;
        //    }
        //    else if (currentState == BloxState.Laying)
        //    {
        //        sprite = sprite2;
        //    }
        //}

        private void DoSecondMove(int xMove, int yMove)
        {
            x2 += xMove;
            y2 += yMove;

            secondPosition = new Vector2(x2 * 8, y2 * 8);
        }

        public override void Draw(SpriteBatch batch, Vector2 offset)
        {
            //if (currentState == BloxState.Standing)
            //{
            //    batch.Draw(sprite, truePosition + offset, Color.White);
            //}
            //else
            //{
            //    var origin = new Vector2(sprite.Width / 2, sprite.Height/2);

            //    Vector2 drawPosDiff = secondPosition - truePosition;
            //    Vector2 drawPos = secondPosition + offset;

            //    batch.Draw(sprite, drawPos, null, Color.White, MathHelper.ToRadians(rotationTest), origin, 1, SpriteEffects.None, 0f);
            //}

            //base.Draw(batch, offset);

            batch.Draw(sprite, truePosition + offset, Color.White);

            batch.Draw(sprite, secondPosition + offset, Color.White);
        }
    }
}
