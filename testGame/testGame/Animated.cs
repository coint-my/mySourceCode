using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections;
using Microsoft.Xna.Framework.Audio;

namespace testGame
{
    public class Animated
    {
        Texture2D animTexture;

        public Rectangle rect;
        public Rectangle RectBox { get; set; }

        Color colorHero;

        public Color ColorHero
        {
            get { return colorHero; }
            set { colorHero = value; }
        }

        int width;
        int height;

        private int FrameCount;

        int frameTime = 100;
        int timeElapsed;
        int currentFrame;
        int HeightTexture;
        double colorCount = 0;

        public float xSpeed = 3;
        float ySpeed;
        float maxSpeed = 10;
        float g = 0.41f;

        public bool isJump;
        bool isGravity;
        public bool isRuning;
        public bool isRuningRigth;
        public bool isEnemyHard;
        public bool isColorHero;
        private bool isColorVar;

        public float elapseSecond;

        Random rand = new Random();
        SoundEffect soundPlayBattle;

        Game1 _game;

        readonly int startCurrentFrame;

        public Animated(Rectangle re, Texture2D texture, bool gravity, Game1 game)
        {
            this.isGravity = gravity;
            this._game = game;
            this.rect = re;
            this.animTexture = texture;
            width = 52;
            height = 50;
            FrameCount = 12;
            startCurrentFrame = 2;
            currentFrame = startCurrentFrame;
            HeightTexture = 0;

            soundPlayBattle = game.Content.Load<SoundEffect>("effect/1111");
        }

        public Animated(Texture2D text, Rectangle re, int widt, int heigh, int heightTexture, int startCurrentF,
            bool gravity, int frameC, int timeCountFrame, Game1 game, float xSp)
        {
            this.isGravity = gravity;
            this.animTexture = text;
            this.rect = re;
            this.startCurrentFrame = startCurrentF;
            this.FrameCount = frameC;
            this.width = widt;
            this.height = heigh;
            this.HeightTexture = heightTexture;
            currentFrame = startCurrentFrame;
            this.frameTime = timeCountFrame;
            this._game = game;
            this.xSpeed = xSp;

            isEnemyHard = false;
        }

        public void RunRight(bool isR)
        {
            Rectangle heroRect = rect;

            if (!isR)
            {
                heroRect.Offset(-(int)xSpeed, 0);
      
                if (heroRect.Left > 0 && !_game.ColidesLevel(heroRect) && !_game._Box.GetRectBox.Intersects(heroRect))
                {
                    isRuningRigth = false;
                    rect = heroRect;
                }
                else
                    isRuningRigth = true;
            }
            else if (isR)
            {
                heroRect.Offset((int)xSpeed, 0);

                if (heroRect.Right < _game.levelLench + 200 && !_game.ColidesLevel(heroRect) && !_game._Box.GetRectBox.Intersects(heroRect))
                {
                    isRuningRigth = true;
                    rect = heroRect;
                }
                else
                    isRuningRigth = false;
            }

            isRuning = true;
        }

        public bool isHeroUp()
        {
            Rectangle hero = rect;

            hero.Offset(0, -30);

            if (!_game.ColidesLevel(hero))
            {
                return true;
            }
            return false;
        }

        public void Gravity(GameTime gameTime)
        {
            Rectangle grRect = rect;

            ySpeed = ySpeed - g * gameTime.ElapsedGameTime.Milliseconds * 10 * 0.009f;
            float dy = ySpeed * gameTime.ElapsedGameTime.Milliseconds * 10 * 0.009f;

            grRect.Offset(0, -(int)dy);

            if (!_game.ColidesLevel(grRect) && !heroRectIntBoxDown(grRect))
            {
                rect = grRect;
            }
            else
            {
                ySpeed = 0;
                isJump = false;
            }

            elapseSecond += (float)gameTime.ElapsedGameTime.TotalSeconds * 1f;
        }

        public bool intersectHeroRect(Rectangle r)
        {
            if (r.Intersects(rect))
                return true;
            return false;
        }

        private bool heroRectIntBoxDown(Rectangle r)
        {
            Rectangle re = r;
            if (_game._Box.GetRectBox.Y < re.Y)
                re.Offset(0, 30);
            else
                re.Offset(0, 0);

            if (_game._Box.GetRectBox.Intersects(re))
            {
                return true;
            }
            return false;
        }

        public void Jump()
        {
            if (!isJump)
            {
                ySpeed = maxSpeed;
                isJump = true;
            }
        }

        public void UpdateAnim(GameTime gameTime, ArrayList gates, ArrayList playGate, List<BatleHero> listEnemyBatle)
        {
            timeElapsed += gameTime.ElapsedGameTime.Milliseconds;

            UpdateColor(gameTime);

            if (timeElapsed > frameTime)
            {
                timeElapsed = 0;
                if (currentFrame > FrameCount)
                    currentFrame = startCurrentFrame;
                currentFrame += 1 % FrameCount;
            }

            if (isGravity)
            {
                Gravity(gameTime);

                if (!isRuning)
                    frameTime = 100;
                else
                    frameTime = 15;

                for (int i = 0; i < playGate.Count; i++)
                {
                    if (rect.Intersects(((Blocks)playGate[i]).Rect) && isColorHero)
                    {
                        _game.RangMonster(listEnemyBatle[i]);
                        _game.gameState = GameState.BattleMenu;
                        listEnemyBatle.RemoveAt(i);
                        playGate.RemoveAt(i);
                        gates.RemoveAt(i);
                        soundPlayBattle.Play(1);
                    }
                }
            }
        }

        private void UpdateColor(GameTime gameTime)
        {
            if (isColorHero)
                colorHero = Color.White;
            else
            {
                Color color = colorHero;

                colorCount += gameTime.ElapsedGameTime.TotalSeconds;

                int turn = 0;

                if (isColorVar)
                    turn += 12;
                else if (!isColorVar)
                    turn -= 15;

                if (color.A > 240)
                    isColorVar = false;
                if (color.A < 1)
                    isColorVar = true;

                color.A += (byte)turn;
                color.B = 255;
                color.G = 255;
                color.R = 255;

                colorHero = color;

                if (colorCount > 2)
                {
                    colorCount = 0;
                    isColorHero = true;
                }
            }
        }

        public void DrawAnim(SpriteBatch sBatch)
        {
            Rectangle re = new Rectangle(width * currentFrame, HeightTexture, width, height);
            Rectangle r = _game.getScreenRect(rect);
            sBatch.Draw(animTexture, r, re, !isGravity ? Color.White : colorHero);
        }
        public void drawMenu(SpriteBatch sBatch)
        {
            Rectangle re = new Rectangle(width * currentFrame, HeightTexture, width, height);
            sBatch.Draw(animTexture, rect, re, Color.White);
        }
    }
}
