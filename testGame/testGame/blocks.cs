using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace testGame
{
    public class Blocks
    {
        Texture2D block;

        public Rectangle Rect { get; set; }

        Rectangle rectStart;

        ManagerParticle mParticleFire;

        int count = 0;

        int _rand;
        int speed = 4;

        double elapsedTime;

        Game1 _game;

        bool blocksIsAlive;

        public Blocks(Rectangle rect, Texture2D texture, Game1 game)
        {
            this.Rect = this.rectStart = rect;
            this.block = texture;

            this._game = game;

            blocksIsAlive = false;

            elapsedTime = 0;

            mParticleFire = new ManagerParticle(_game, 5, 150, 230, 3, 6, 15, 30, 0, 0.04f, texture);
        }

        public void R(int _r)
        {
            _rand = _r;
        }

        public void UpdateBlocks(GameTime gTime)
        {
            Rectangle r = Rect;
            Rectangle re = Rect;

            r.Offset(0, -1);
            re.Offset(0, 1);

            count++;

            if (count < 31)
            {
                Rect = r;
            }
            else
            {
                if (count > _rand)
                {
                    Rect = re;

                    if (count > _rand + 29)
                        count = 0;
                }
            }
        }

        public void UpdateBlocksFire(GameTime gTime)
        {
            Rectangle re = new Rectangle(Rect.X, Rect.Y, Rect.Width / 2, Rect.Height);

            mParticleFire.Update(gTime, re);

            if (blocksIsAlive)
            {
                Rectangle r = Rect;

                r.Offset(0, speed);

                if (_game.ColidesLevel(r))
                {
                    blocksIsAlive = false;
                    Rect = rectStart;
                }
                else
                {
                    Rect = r;
                }
            }
            else
            {
                elapsedTime += gTime.ElapsedGameTime.TotalSeconds;

                if (elapsedTime > 1.1)
                {
                    elapsedTime = 0;
                    blocksIsAlive = true;
                }
            }
        }

        public void DrawParticleBlockFire(SpriteBatch sBatch, int scrollX)
        {
            mParticleFire.DrawManager(sBatch, scrollX);
        }

        public void DrawBlocks(SpriteBatch sBatch)
        {
            Rectangle r = _game.getScreenRect(Rect);
            sBatch.Draw(block, r, Color.White);
        }
    }
}
