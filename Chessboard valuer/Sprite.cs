using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chessboard_valuer
{
    public class Sprite
    {
        public Texture2D spriteTexture;
        public Vector2 spritePosition;
        public Color spriteColor;
        public Rectangle spriteBox;
        public float scaleFactor;

        public Sprite()
        {


        }

        public Sprite(Texture2D inSpriteTexture, Vector2 inSpritePosition, Color inSpriteColor, Rectangle inSpriteBox, float inScaleFactor)
        {

            spriteTexture = inSpriteTexture;
            spritePosition = inSpritePosition;
            spriteColor = inSpriteColor;
            spriteBox = inSpriteBox;
            scaleFactor = inScaleFactor;


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteTexture, spritePosition, spriteColor);
        }

        public void DrawString(SpriteBatch spriteBatch, SpriteFont spriteFont, string text)
        {
            spriteBatch.DrawString(spriteFont, text, spritePosition, spriteColor);

        }

        public virtual void Update(KeyboardState instate, GraphicsDeviceManager inGraphics)
        {

        }


        

    }
}
