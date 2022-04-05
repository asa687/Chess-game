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
    public class Knight : ChessPiece
    {

        public Knight() : base()
        {



        }

        public Knight( Texture2D inSpriteTexture, Vector2 inSpritePosition, Color inSpriteColor, Rectangle inSpriteBox, float inScaleFactor, int inValue, bool isDrawn) : base( inSpriteTexture, inSpritePosition, inSpriteColor, inSpriteBox, inScaleFactor, inValue, isDrawn)
        {
            

        }

        public bool ValidKnightMove(Move move, Chessboard board)
        {
            //pawn only moves in one direction, need to select colour amd move
            if (1 == Math.Abs(move.GetEndPoint.X - move.GetStartPoint.X) && 2 == Math.Abs(move.GetEndPoint.Y - move.GetStartPoint.Y) || 2 == Math.Abs(move.GetEndPoint.X - move.GetStartPoint.X) && 1 == Math.Abs(move.GetEndPoint.Y - move.GetStartPoint.Y))
            {
                return true;

            }


            return false;



        }



    }
}
