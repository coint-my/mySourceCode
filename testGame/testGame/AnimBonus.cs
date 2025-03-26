using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace testGame
{
    class AnimBonus
    {
        Texture2D texture;

        Rectangle rect;

        public Rectangle RectBonus
        {
            get { return rect; }
        }

        int width;
        int height;

        int frameCount;
        int frames = 0;
        int currentFrame = 0;
        int wInt = 0;
        int hInt = 0;
        double timeElapsed = 0;
        double timeFrame;
        int widthLench = 0;
        int heightLench = 0;

        public bool isAlive;

        public AnimBonus(Texture2D _texture, Rectangle _rect, int _frameCount, int _heightLench, int _widthLench, double _timeFrame, int _width, int _height)
        {
            texture = _texture;
            rect = _rect;
            frameCount = _frameCount;
            heightLench = _heightLench;
            widthLench = _widthLench;
            timeFrame = _timeFrame;
            width = _width;
            height = _height;
            isAlive = true;
        }

        public void UpdateAnim(GameTime gameTime)
        {
            timeElapsed += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timeElapsed > timeFrame)
            {
                timeElapsed = 0;

                if (wInt >= widthLench)
                {
                    hInt++;
                    wInt = 0;
                }
                if (frames >= frameCount)
                {
                    frames = 0;
                    wInt = 0;
                    hInt = 0;
                }

                wInt++;
                frames++;
            }
        }
        public void DrawAnim(SpriteBatch spriteBatch, int scroll)
        {
            if (isAlive)
            {
                Rectangle r = new Rectangle(wInt * width, hInt * height, width, height);
                Rectangle rBonus = new Rectangle(rect.X - scroll, rect.Y, rect.Width, rect.Height);
                spriteBatch.Draw(texture, rBonus, r, Color.White);
            }
            else
            {
                rect = new Rectangle();
            }
        }
    }
}
