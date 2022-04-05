using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chessboard_valuer
{
    public class Bishop : ChessPiece
    {
        public Bishop() : base()
        {



        }

        public Bishop( Texture2D inSpriteTexture, Vector2 inSpritePosition, Color inSpriteColor, Rectangle inSpriteBox, float inScaleFactor, int inValue,bool isDrawn) : base( inSpriteTexture, inSpritePosition, inSpriteColor, inSpriteBox, inScaleFactor, inValue,isDrawn)
        {
            

        }

        public bool ValidBishopMove(Move move, Chessboard chessboard)
        {
            bool valid = false;
            if (Math.Abs(move.GetEndPoint.X - move.GetStartPoint.X) == Math.Abs(move.GetEndPoint.Y - move.GetStartPoint.Y))
            {
                valid = true;

            }
            if (valid && move.GetEndPoint.X - move.GetStartPoint.X > 0 && move.GetEndPoint.Y - move.GetStartPoint.Y > 0)
            {
                for (int i = 1; i < move.GetEndPoint.X - move.GetStartPoint.X; i++)
                {
                    if (chessboard.pawn.ContainsKey(new Point(move.GetStartPoint.X + i, move.GetStartPoint.Y + i)) || chessboard.rook.ContainsKey(new Point(move.GetStartPoint.X + i, move.GetStartPoint.Y + i)) || chessboard.knight.ContainsKey(new Point(move.GetStartPoint.X + i, move.GetStartPoint.Y + i)) || chessboard.bishop.ContainsKey(new Point(move.GetStartPoint.X + i, move.GetStartPoint.Y + i)) || chessboard.king.ContainsKey(new Point(move.GetStartPoint.X + i, move.GetStartPoint.Y + i)) || chessboard.queen.ContainsKey(new Point(move.GetStartPoint.X + i, move.GetStartPoint.Y + i)))
                    {
                        return false;
                    
                    }
                
                }
            
            
            }

            else if (valid && move.GetEndPoint.X - move.GetStartPoint.X > 0 && move.GetEndPoint.Y - move.GetStartPoint.Y < 0)
            {
                for (int i = 1; i < move.GetEndPoint.X - move.GetStartPoint.X; i++)
                {
                    if (chessboard.pawn.ContainsKey(new Point(move.GetStartPoint.X + i, move.GetStartPoint.Y - i)) || chessboard.rook.ContainsKey(new Point(move.GetStartPoint.X + i, move.GetStartPoint.Y - i)) || chessboard.knight.ContainsKey(new Point(move.GetStartPoint.X + i, move.GetStartPoint.Y - i)) || chessboard.bishop.ContainsKey(new Point(move.GetStartPoint.X + i, move.GetStartPoint.Y - i)) || chessboard.king.ContainsKey(new Point(move.GetStartPoint.X + i, move.GetStartPoint.Y - i)) || chessboard.queen.ContainsKey(new Point(move.GetStartPoint.X + i, move.GetStartPoint.Y -i)))
                    {
                        return false;

                    }

                }


            }

            else if (valid && move.GetEndPoint.X - move.GetStartPoint.X < 0 && move.GetEndPoint.Y - move.GetStartPoint.Y > 0)
            {
                for (int i = 1; i < move.GetEndPoint.Y - move.GetStartPoint.Y; i++)
                {
                    if (chessboard.pawn.ContainsKey(new Point(move.GetStartPoint.X - i, move.GetStartPoint.Y + i)) || chessboard.rook.ContainsKey(new Point(move.GetStartPoint.X - i, move.GetStartPoint.Y + i)) || chessboard.knight.ContainsKey(new Point(move.GetStartPoint.X - i, move.GetStartPoint.Y + i)) || chessboard.bishop.ContainsKey(new Point(move.GetStartPoint.X - i, move.GetStartPoint.Y + i)) || chessboard.king.ContainsKey(new Point(move.GetStartPoint.X - i, move.GetStartPoint.Y + i)) || chessboard.queen.ContainsKey(new Point(move.GetStartPoint.X - i, move.GetStartPoint.Y + i)))
                    {
                        return false;

                    }

                }


            }

            else if (valid && move.GetEndPoint.X - move.GetStartPoint.X < 0 && move.GetEndPoint.Y - move.GetStartPoint.Y < 0)
            {
                for (int i = 1; i < Math.Abs(move.GetEndPoint.X - move.GetStartPoint.X); i++)
                {
                    if (chessboard.pawn.ContainsKey(new Point(move.GetStartPoint.X - i, move.GetStartPoint.Y - i)) || chessboard.rook.ContainsKey(new Point(move.GetStartPoint.X - i, move.GetStartPoint.Y - i)) || chessboard.knight.ContainsKey(new Point(move.GetStartPoint.X - i, move.GetStartPoint.Y - i)) || chessboard.bishop.ContainsKey(new Point(move.GetStartPoint.X - i, move.GetStartPoint.Y - i)) || chessboard.king.ContainsKey(new Point(move.GetStartPoint.X - i, move.GetStartPoint.Y - i)) || chessboard.queen.ContainsKey(new Point(move.GetStartPoint.X - i, move.GetStartPoint.Y - i)))
                    {
                        return false;

                    }

                }


            }



            return valid;



        }




    }
}
