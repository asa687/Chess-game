using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chessboard_valuer
{
    public class ChessPiece : Sprite
    {


        int values;
        bool drawn;
        



        public ChessPiece() : base()
        {

        }

        public ChessPiece(Texture2D inSpriteTexture, Vector2 inSpritePosition, Color inSpriteColor, Rectangle inSpriteBox, float inScaleFactor, int inValue, bool isDrawn) :
            base(inSpriteTexture, inSpritePosition, inSpriteColor, inSpriteBox, inScaleFactor)

        {
            values = inValue; 
            
            drawn = isDrawn;

        }  

        public bool CurrentlyDrawn
        {
            get { return drawn;  }
            set { drawn = value; }
        
        }

        public int GetValue
        { 
            get { return values; }
            set { values = value; }
        
        } 

        
        public Rectangle GetBox
        {
            get { return spriteBox; }
            set { spriteBox = value; }

        }  

        public Texture2D GetTexture
        {
            get { return spriteTexture; }
            set { spriteTexture = value;  }
        
        } 

        public Color GetColor
        {
            get { return spriteColor; }
            set { spriteColor = value; }
        
        } 



        public Vector2 GetVector
        {
            get { return spritePosition; }
            set { spritePosition = value; }


        }








    }
}
