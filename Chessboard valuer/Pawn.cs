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
    public class Pawn : ChessPiece
    {

        
        public Pawn() : base()
        {



        }

        public Pawn( Texture2D inSpriteTexture, Vector2 inSpritePosition, Color inSpriteColor, Rectangle inSpriteBox, float inScaleFactor, int inValue, bool isDrawn) : base( inSpriteTexture, inSpritePosition, inSpriteColor, inSpriteBox, inScaleFactor, inValue, isDrawn)
        {
           

        }

        public bool ValidPawnMove(Move move, Chessboard chessboard)
        {
            // note pawn capture rules are incorrect
            bool valid = false;
            if ((1 == (move.GetEndPoint.Y - move.GetStartPoint.Y)) && spriteColor == Color.Black && (0 == (move.GetEndPoint.X - move.GetStartPoint.X)) || (2 == (move.GetEndPoint.Y - move.GetStartPoint.Y) && move.GetStartPoint.Y == 1 && spriteColor == Color.Black && (0 == (move.GetEndPoint.X - move.GetStartPoint.X))) || ((1 == (move.GetEndPoint.Y - move.GetStartPoint.Y)) && spriteColor == Color.Black && (1 == Math.Abs(move.GetEndPoint.X - move.GetStartPoint.X)) && (chessboard.pawn.ContainsKey(move.GetEndPoint) || chessboard.rook.ContainsKey(move.GetEndPoint) || chessboard.knight.ContainsKey(move.GetEndPoint) || chessboard.bishop.ContainsKey(move.GetEndPoint) || chessboard.king.ContainsKey(move.GetEndPoint) || chessboard.queen.ContainsKey(move.GetEndPoint))))
            {
                
                valid = true;

            }
            else if ((-1 == (move.GetEndPoint.Y - move.GetStartPoint.Y)) && spriteColor == Color.White && (0 == (move.GetEndPoint.X - move.GetStartPoint.X)) || (-2 == (move.GetEndPoint.Y - move.GetStartPoint.Y) && move.GetStartPoint.Y == 6 && spriteColor == Color.White && (0 == (move.GetEndPoint.X - move.GetStartPoint.X))) || ((-1 == (move.GetEndPoint.Y - move.GetStartPoint.Y)) && spriteColor == Color.White && (1 == Math.Abs(move.GetEndPoint.X - move.GetStartPoint.X)) && (chessboard.pawn.ContainsKey(move.GetEndPoint) || chessboard.rook.ContainsKey(move.GetEndPoint) || chessboard.knight.ContainsKey(move.GetEndPoint) || chessboard.bishop.ContainsKey(move.GetEndPoint) || chessboard.king.ContainsKey(move.GetEndPoint) || chessboard.queen.ContainsKey(move.GetEndPoint))))
            {
                
                valid =  true;

            }


            if (move.GetStartPoint.X == move.GetEndPoint.X && valid == true)
            {
                if (move.GetStartPoint.Y > move.GetEndPoint.Y)
                {
                    int i = 1;
                    if (chessboard.pawn.ContainsKey(new Point(move.GetEndPoint.X, move.GetStartPoint.Y - i)) || chessboard.rook.ContainsKey(new Point(move.GetEndPoint.X, move.GetStartPoint.Y - i)) || chessboard.knight.ContainsKey(new Point(move.GetEndPoint.X, move.GetStartPoint.Y - i)) || chessboard.bishop.ContainsKey(new Point(move.GetEndPoint.X, move.GetStartPoint.Y - i)) || chessboard.king.ContainsKey(new Point(move.GetEndPoint.X, move.GetStartPoint.Y - i)) || chessboard.queen.ContainsKey(new Point(move.GetEndPoint.X, move.GetStartPoint.Y - i)))
                    {
                        return false;

                    }

                    

                }
                else if (move.GetStartPoint.Y < move.GetEndPoint.Y)
                {

                    int i = 1;
                    if (chessboard.pawn.ContainsKey(new Point(move.GetEndPoint.X, move.GetStartPoint.Y + i)) || chessboard.rook.ContainsKey(new Point(move.GetEndPoint.X, move.GetStartPoint.Y + i)) || chessboard.knight.ContainsKey(new Point(move.GetEndPoint.X, move.GetStartPoint.Y + i)) || chessboard.bishop.ContainsKey(new Point(move.GetEndPoint.X, move.GetStartPoint.Y + i)) || chessboard.king.ContainsKey(new Point(move.GetEndPoint.X, move.GetStartPoint.Y + i)) || chessboard.queen.ContainsKey(new Point(move.GetEndPoint.X, move.GetStartPoint.Y + i)))
                    {
                        return false;

                    }

                    

                }


            }









            return valid;



        }



    }
}
