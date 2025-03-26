using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace testGame
{
    public class HelpBox
    {
        Texture2D texture;

        float gravityDown = 0.3f;
        float ySpeed;

        public bool getOutSite;
        public bool isBoxUp;
        public bool isBoxTurn;

        public Rectangle GetRectBox;

        public Rectangle heroRectangle;

        Game1 _game;
        Animated _hero;

        public HelpBox(Texture2D text, Rectangle pos, Game1 game)
        {
            texture = text;
            GetRectBox = pos;
            _game = game;
        }

        public void IsRun(bool isR)
        {
            getOutSite = isR;
        }
        public bool asIntersect(Rectangle r)
        {
            if (GetRectBox.Intersects(r))
                return true;
            return false;
        }

        private void UpdateGravity(GameTime gameTime)
        {
            _hero = _game._Heroies;

            #region gravityBox

            Rectangle r = GetRectBox;
            Rectangle he = new Rectangle(_hero.rect.X - 3, _hero.rect.Y, 36, 30);

            if (!isBoxUp)
            {
                ySpeed = ySpeed + gravityDown * gameTime.ElapsedGameTime.Milliseconds * 5f * 0.01f;

                r.Offset(0, (int)ySpeed);
            }
            else
            {
                r = new Rectangle(_hero.rect.X, _hero.rect.Y - 30, 30, 30);
                
            }

            if (r.Intersects(he))
            {
                if (_hero.isRuning && !_game.ColidesLevel(r))
                {
                    if (getOutSite)
                        r.Offset((int)1, 0);
                    else
                        r.Offset(-(int)1, 0);
                }
            }

            if (!_game.ColidesLevel(r) && !r.Intersects(_hero.rect))
            {
                GetRectBox = r;
            }
            else
            {
                isBoxUp = false;
                ySpeed = 0;
            }

            #endregion


        }

        public bool HeroIntersectBox()
        {
            Rectangle r = _hero.rect;

            r = new Rectangle(heroRectangle.X - 8, heroRectangle.Y, 46, 30);

            if (r.Intersects(GetRectBox))
                return true;
            return false;
        }

        public void UpdateBox(GameTime gameTime)
        {
            UpdateGravity(gameTime);

            if (isBoxTurn)
                GetRectBox = heroRectangle;
        }

        public void DrawBox(SpriteBatch spriteBatch)
        {
            Rectangle r = _game.getScreenRect(GetRectBox);
            spriteBatch.Draw(texture, r, Color.White);
        }
    }
}
