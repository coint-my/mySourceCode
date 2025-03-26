using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace testGame
{
    class Bullet
    {
        Texture2D texture;

        public Rectangle rect;
        Rectangle rectangle;
        public Rectangle rectFalse;

        int dx = 5;

        public bool isAlive;
        bool isBullet;

        Game1 _game;

        public Bullet(Texture2D text, Rectangle rectBullet, Rectangle re, Game1 game)
        {
            this.rect = rectBullet;
            this.texture = text;
            this._game = game;
            this.rectangle = re;
            this.MaxBall = 5;

            this.rect = rectFalse = new Rectangle(-10, -10, 10, 10);

            isAlive = false;
        }

        public void ShotBullet(bool shot)
        {
            isBullet = shot;
        }

        public int MaxBall { get; set; }

        public void UpdateBullet(GameTime gameTime)
        {
            if (isAlive)
            {
                if (isBullet)
                {
                    rect.Offset(dx, 0);

                    if (_game.ColidesLevel(rect))
                    {
                        rect = rectFalse;

                        isAlive = false;
                    }
                }
                else
                {
                    rect.Offset(-dx, 0);

                    if (_game.ColidesLevel(rect))
                    {
                        rect = rectFalse;

                        isAlive = false;
                    }
                }
            }
        }

        public void DrawBullet(SpriteBatch spriteBath)
        {
            spriteBath.Begin();
            Rectangle r = _game.getScreenRect(rect);
            spriteBath.Draw(texture, r, rectangle, Color.White);
            spriteBath.End();
        }
    }
}
