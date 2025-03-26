using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace testGame.gameMenu
{
    class menuItem
    {
        string name;

        public Color color;

        Vector2 position;

        SpriteFont font;

        public bool isActive;

        public menuItem(string _name, Vector2 _position, Color _color)
        {
            name = _name;
            position = _position;
            color = _color;

            isActive = true;
        }

        public void ContentLoad(ContentManager content)
        {
            font = content.Load<SpriteFont>("fontMenu");
        }

        public void DrawMenuItem(SpriteBatch sBatch)
        {
            if (!isActive)
                color = Color.Gray;

            sBatch.DrawString(font, name, position, color);
        }
    }
}
