using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Chessboard_valuer
{
    class Square : Sprite
    {
        public Square() : base()
        {

        }

        public Square(Texture2D inSpriteTexture, Vector2 inSpritePosition, Color inSpriteColor, Rectangle inSpriteBox, float inScaleFactor) :
            base(inSpriteTexture, inSpritePosition, inSpriteColor, inSpriteBox, inScaleFactor)

        {


        }
        public Rectangle SquareTexture
        {
            get { return spriteBox; }
            set { spriteBox = value; }




        }

        public bool IsOccupied { get; set; } = false;



    }
}
