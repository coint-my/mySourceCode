using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace testGame
{
    class ParticleArray
    {
        float speed;

        int aliveParticle;
        double timeElapsed;

        public int AliveSpeed
        {
            get { return aliveParticle; }
        }

        int scale;

        public float Speed
        {
            get { return speed; }
        }

        float dx;
        float dy;

        Texture2D texture;

        Vector2 position;

        Rectangle rectPosition;

        public Rectangle RectPosition
        {
            get { return rectPosition; }
            set { rectPosition = value; }
        }

        public bool isAlive;

        public Vector2 Positiion
        {
            get { return position; }
            set { position = value; }
        }

        public ParticleArray(Vector2 _position, Texture2D _texture, int _aliveParticle, int _scale, float _speed, float _dx, float _dy)
        {
            position = _position;
            texture = _texture;
            aliveParticle = _aliveParticle;
            scale = _scale;
            speed = _speed;
            dx = _dx;
            dy = _dy;

            rectPosition = new Rectangle((int)position.X, (int)position.Y, scale, scale);

            isAlive = true;
        }

        public void UpdateP(GameTime gTime)
        {
            timeElapsed += gTime.ElapsedGameTime.TotalMilliseconds;

            if (timeElapsed > aliveParticle)
            {
                timeElapsed = 0;
                isAlive = false;
            }

            rectPosition = new Rectangle((int)position.X, (int)position.Y, scale, scale);
        }

        public void DrawParticle(SpriteBatch sBatch, int x)
        {
            rectPosition.X -= x;
            sBatch.Draw(texture, rectPosition, Color.White);
        }
    }
}
