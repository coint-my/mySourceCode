using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace testGame
{
    class TextGame
    {
        private string name;
        private Vector2 position;

        public Vector2 PositionText
        {
            get { return position; }
            set { position = value; }
        }

        public bool isVisible;
        public bool isScrollX;

        Color color;

        public Color TextColor
        {
            get { return color; }
            set { color = value; }
        }

        SpriteFont font;

        public TextGame(string name, Vector2 pos, Color col)
        {
            this.name = name;
            this.position = pos;
            this.color = col;

            this.isVisible = true;
            this.isScrollX = false;
        }

        public void ContentLoad(ContentManager content)
        {
            font = content.Load<SpriteFont>("font");
        }

        public void UpdateText(string text)
        {
            this.name = text;
        }

        public void DrawFont(SpriteBatch spriteBatch, int _scrollX)
        {
            if (isVisible && !isScrollX)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(font, name, position, color);
                spriteBatch.End();
            }
            if (isScrollX && isVisible)
            {
                Vector2 p = new Vector2(position.X - _scrollX, position.Y);

                spriteBatch.Begin();
                spriteBatch.DrawString(font, name, p, color);
                spriteBatch.End();
            }
        }
    }
}
