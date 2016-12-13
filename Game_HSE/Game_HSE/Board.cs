using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_HSE
{
    class Board
    {
        public Block[,] blocks { get; set; }
        public int columns { get; set; }
        public int rows { get; set; }
        public Texture2D block_texture { get; set; }
        private SpriteBatch sb { get; set; }
        public static Board CurrentBoard { get; private set; }

        public Board(SpriteBatch sb, Texture2D block_texture, int columns, int rows)
        {
            this.sb = sb;
            this.block_texture = block_texture;
            this.columns = columns;
            this.rows = rows;
            blocks = new Block[columns, rows];
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    Vector2 position = new Vector2(i * block_texture.Width, j * block_texture.Height);
                    blocks[i, j] = new Block(block_texture, position, sb, false);
                }
            }
            WhichAreBlocked();
            Board.CurrentBoard = this;
        }

        private void WhichAreBlocked()
        {
            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if ((y == 9 && ((x > 7 && x <= 23) || (x >= 25 && x <= 37) || (x >= 40 && x <= 73) || (x >= 78 && x <= 87))) || // ground
                        ((x == 83) || (x == 7)) || // walls
                        (y == 4 && (x >= 9 && x <= 13)) || // 1
                        (y == 6 && (x >= 16 && x <= 18)) || // 2
                        (y == 6 && (x >= 25 && x <= 29)) || // 3
                        (y == 4 && (x >= 31 && x <= 35)) || // 4
                        (y == 6 && (x >= 43 && x <= 47)) || // 5
                        (y == 4 && (x >= 51 && x <= 53)) || // 6
                        (y == 2 && (x >= 56 && x <= 59)) || // 7
                        (y == 6 && (x >= 65 && x <= 67)) || // 8
                        (y == 6 && (x >= 71 && x <= 74)) || // 9
                        (y == 4 && (x >= 76 && x <= 78))    // 10

                        )
                    {
                        blocks[x, y].blocked = true;
                    }
                }
            }
        }

        public void Draw()
        {
            foreach (var block in blocks)
            {
                block.Draw();
            }
        }

        public bool HasSpaceForRectangle(Rectangle rectangleToCheck)
        {
            foreach (var block in blocks)
            {
                if (block.blocked && block.rectangle.Intersects(rectangleToCheck))
                {
                    return false;
                }
            }
            return true;
        }

        public Vector2 WhereCanIGetTo(Vector2 originalPosition, Vector2 destination, Rectangle boundingRectangle)
        {
            Vector2 movementToTry = destination - originalPosition;
            Vector2 furthestAvailableLocationSoFar = originalPosition;
            int numberOfStepsToBreakMovementInto = (int)(movementToTry.Length() * 2) + 1;
            Vector2 oneStep = movementToTry / numberOfStepsToBreakMovementInto;

            for (int i = 1; i <= numberOfStepsToBreakMovementInto; i++)
            {
                Vector2 positionToTry = originalPosition + oneStep * i;
                Rectangle newBoundary =
                    CreateRectangle(positionToTry, boundingRectangle.Width, boundingRectangle.Height);
                if (HasSpaceForRectangle(newBoundary)) { furthestAvailableLocationSoFar = positionToTry; }
                else
                {
                    bool isDiagonalMove = movementToTry.X != 0 && movementToTry.Y != 0;
                    if (isDiagonalMove)
                    {
                        int stepsLeft = numberOfStepsToBreakMovementInto - (i - 1);

                        Vector2 remainingHorizontalMovement = oneStep.X * Vector2.UnitX * stepsLeft;
                        Vector2 finalPositionIfMovingHorizontally = furthestAvailableLocationSoFar + remainingHorizontalMovement;
                        furthestAvailableLocationSoFar =
                            WhereCanIGetTo(furthestAvailableLocationSoFar, finalPositionIfMovingHorizontally, boundingRectangle);

                        Vector2 remainingVerticalMovement = oneStep.Y * Vector2.UnitY * stepsLeft;
                        Vector2 finalPositionIfMovingVertically = furthestAvailableLocationSoFar + remainingVerticalMovement;
                        furthestAvailableLocationSoFar =
                            WhereCanIGetTo(furthestAvailableLocationSoFar, finalPositionIfMovingVertically, boundingRectangle);
                    }
                    break;
                }
            }
            return furthestAvailableLocationSoFar;
        }

        private Rectangle CreateRectangle(Vector2 positionToMove, int width, int height)
        {
            return new Rectangle((int)positionToMove.X, (int)positionToMove.Y, width, height);
        }
    }
}
