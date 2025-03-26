using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;

namespace testGame
{
    class ManagerParticle
    {
        List<ParticleArray> list;

        Game1 game;

        Texture2D texture;

        int aliveMin;
        int aliveMax;
        int scaleMin;
        int scaleMax;

        float dx;
        float dy;
        int speedMin;
        int speedMax;

        byte array;

        Random rand;

        public ManagerParticle(Game1 _game, byte _array, int _aliveMin,int _aliveMax,int _speedMin, int _speedMax,int _scaleMin
            ,int _scaleMax ,float _dx, float _dy, Texture2D _texture)
        {
            game = _game;

            rand = new Random();

            array = _array;
            aliveMin = _aliveMin;
            aliveMax = _aliveMax;
            speedMin = _speedMin;
            speedMax = _speedMax;
            scaleMin = _scaleMin;
            scaleMax = _scaleMax;
            dx = _dx;
            dy = _dy; 
            texture = _texture;

            CreateParticle();
        }

        public void CreateParticle()
        {
            list = new List<ParticleArray>();
            Rectangle Rect = new Rectangle();

            for (int i = 0; i < array; i++)
            {
                Vector2 p = new Vector2(rand.Next(Rect.Left, Rect.Right),
                                        rand.Next(Rect.Top, Rect.Bottom));

                int alivePartRand = rand.Next(aliveMin, aliveMax);

                int speedPart = rand.Next(speedMin, speedMax);

                int scalePart = rand.Next(scaleMin, scaleMax);

                ParticleArray part = new ParticleArray(p, texture, alivePartRand, scalePart, speedPart, dx, dy);

                list.Add(part);
            }
        }

        public void AliveCollection()
        {
            foreach (ParticleArray item in list)
            {
                item.isAlive = true;
            }
        }

        public void UpdateOnly(GameTime gameTime, Rectangle rectStart)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].UpdateP(gameTime);

                if (list[i].isAlive)
                {
                    if (dy != 0)
                    {
                        Vector2 p = list[i].Positiion;

                        float y = dy * gameTime.ElapsedGameTime.Milliseconds * list[i].Speed;
                        float x = dx * gameTime.ElapsedGameTime.Milliseconds * list[i].Speed;

                        p.Y -= y;
                        p.X -= x;

                        list[i].Positiion = p;
                    }
                }
                else
                {
                    Vector2 p = new Vector2(rand.Next(rectStart.Left, rectStart.Right),
                                           rand.Next(rectStart.Top, rectStart.Bottom));
                    list[i].Positiion = p;

                    int scaleRect = rand.Next(scaleMin, scaleMax);

                    list[i].RectPosition = new Rectangle((int)p.X, (int)p.Y, scaleRect, scaleRect);
                }
            }
        }

        public void Update(GameTime gameTime, Rectangle rectStart)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].UpdateP(gameTime);

                if (list[i].isAlive)
                {
                    if (dy != 0)
                    {
                        Vector2 p = list[i].Positiion;

                        float y = dy * gameTime.ElapsedGameTime.Milliseconds * list[i].Speed;
                        float x = dx * gameTime.ElapsedGameTime.Milliseconds * list[i].Speed;

                        p.Y -= y;
                        p.X -= x;

                        list[i].Positiion = p;
                    }
                }
                else
                {
                    Vector2 p = new Vector2(rand.Next(rectStart.Left, rectStart.Right),
                                           rand.Next(rectStart.Top, rectStart.Bottom));
                    list[i].Positiion = p;

                    int scaleRect = rand.Next(scaleMin, scaleMax);

                    list[i].RectPosition = new Rectangle((int)p.X, (int)p.Y, scaleRect, scaleRect);

                    list[i].isAlive = true;
                }
            }
        }

        public void DrawManager(SpriteBatch sBatch, int x)
        {
            sBatch.Begin();

            foreach (ParticleArray item in list)
            {
                if (item.isAlive)
                    item.DrawParticle(sBatch, x);
            }

            sBatch.End();
        }
    }
}
