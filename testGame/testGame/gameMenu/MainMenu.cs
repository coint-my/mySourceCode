using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace testGame.gameMenu
{
    class MainMenu
    {
        KeyboardState kb;
        KeyboardState oldState;

        Texture2D textureTucha1;
        Texture2D textureTucha2;
        Texture2D textureParticle;
        Texture2D textureHero;
        Texture2D textureLogo;
        Texture2D textureNight;

        Rectangle rect;
        Rectangle rectHero;
        Rectangle rectNight;

        List<menuItem> itemList;

        Vector2 tuchaPosition1;
        Vector2 tuchaPosition2;
        Vector2 positionLogo;

        Animated heroMenu;

        SoundEffect soundClap;

        ManagerParticle mParticle;

        int choiseCount;

        bool turnRight;
        bool turnRight2;

        float speedX = 0;
        float speedY = 0;
        float speedMax = 1.5f;

        float speedX2 = 0;
        float speedY2 = 0;
        float speedMax2 = 1.5f;

        Random rand;

        Game1 game;

        public MainMenu(Game1 _game)
        {
            itemList = new List<menuItem>();

            itemList.Add(new menuItem("NewGame", new Vector2(GraphicsDeviceManager.DefaultBackBufferWidth / 2, 200), Color.White));
            itemList.Add(new menuItem("ResumeGame", new Vector2(GraphicsDeviceManager.DefaultBackBufferWidth / 2, 250), Color.White));
            itemList[1].isActive = false;
            itemList.Add(new menuItem("Exit", new Vector2(GraphicsDeviceManager.DefaultBackBufferWidth / 2, 300), Color.White));

            choiseCount = 0;

            game = _game;

            rect = new Rectangle(0, 0, 1100, 100);
            rectHero = new Rectangle(GraphicsDeviceManager.DefaultBackBufferWidth / 2 - 50, 200, 30, 30);

            tuchaPosition1 = new Vector2(200, 400);
            tuchaPosition2 = new Vector2(1000, 250);

            positionLogo = new Vector2(GraphicsDeviceManager.DefaultBackBufferWidth / 3, 50);

            rand = new Random();
        }

        public void UpdateMenu(GameTime gTime)
        {
            kb = Keyboard.GetState();

            UpdateTucha();
            heroMenu.UpdateAnim(gTime, null, null, null);
            mParticle.Update(gTime, rectNight);

            #region LogicMenu

            if (kb.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                choiseCount = choiseCount + 1;
                heroMenu.rect.Y += 50;

                soundClap.Play(1);
            }
            else if (kb.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                choiseCount = choiseCount - 1;
                heroMenu.rect.Y -= 50;

                soundClap.Play(1);
            }
            if (choiseCount < 0)
            {
                choiseCount = 0;
                heroMenu.rect.Y = 200;
            }
            if (choiseCount > 2)
            {
                heroMenu.rect.Y = 300;
                choiseCount = 2;
            }

            if (choiseCount == 0)
                itemList[0].color = Color.Green;
            else
                itemList[0].color = Color.White;
            if (choiseCount == 1 && itemList[1].isActive)
                itemList[1].color = Color.Green;
            else
                itemList[1].color = Color.White;
            if (choiseCount == 2)
                itemList[2].color = Color.Green;
            else
                itemList[2].color = Color.White;

            #endregion

            if ((choiseCount == 0 && itemList[choiseCount].isActive) && kb.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
            {
                game.CreateLevel(game.levelCount = 2);

                game.gameState = GameState.Game;

                itemList[1].isActive = true;
            }
            else if ((choiseCount == 1 && itemList[choiseCount].isActive) && kb.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
            {
                game.gameState = GameState.Game;

                game.musicStop.Resume();
            }
            else if ((choiseCount == 2) && kb.IsKeyDown(Keys.Enter))
                game.Exit();

            oldState = kb;
        }

        private void UpdateTucha()
        {
            #region Tucha1
            tuchaPosition1.X += speedX;
            tuchaPosition1.Y = speedY;

            if (tuchaPosition1.X + textureTucha1.Width < 0)
            {
                turnRight = true;
                speedY = rand.Next(0, 200);
            }
            if (tuchaPosition1.X > game.Width)
            {
                turnRight = false;
                speedY = rand.Next(0, 200);
            }

            if (!turnRight)
                speedX = -speedMax;
            else
                speedX = +speedMax;
            #endregion
            #region Tucha2
            tuchaPosition2.X += speedX2;
            tuchaPosition2.Y = speedY2;

            if (tuchaPosition2.X + textureTucha2.Width < 0)
            {
                turnRight2 = true;
                speedY2 = rand.Next(0, 200);
            }
            if (tuchaPosition2.X > game.Width)
            {
                turnRight2 = false;
                speedY2 = rand.Next(0, 200);
            }

            if (!turnRight2)
                speedX2 = -speedMax2;
            else
                speedX2 = +speedMax2;
            #endregion
        }

        public void ContentLoadMenu(ContentManager content)
        {
            foreach (menuItem item in itemList)
            {
                item.ContentLoad(content);
            }

            textureTucha1 = content.Load<Texture2D>("texture//tucha1");
            textureTucha2 = content.Load<Texture2D>("texture//tucha2");
            textureParticle = content.Load<Texture2D>("texture//llight_3");
            textureLogo = content.Load<Texture2D>("texture//Logo");
            textureHero = content.Load<Texture2D>("ppp");
            textureNight = content.Load<Texture2D>("texture//Single_Particle");

            mParticle = new ManagerParticle(game, 6, 40, 60, 0, 0, 5, 12, 0, 0, textureNight);
            rectNight = new Rectangle(GraphicsDeviceManager.DefaultBackBufferWidth / 3, 50, 500, 80);

            soundClap = content.Load<SoundEffect>("effect//CHIME");

            heroMenu = new Animated(rectHero, textureHero, false, game);
        }

        public void DrawMenu(SpriteBatch sBatch)
        {
            sBatch.Begin();

            sBatch.Draw(textureTucha1, tuchaPosition1, Color.White);
            sBatch.Draw(textureTucha2, tuchaPosition2, Color.White);

            heroMenu.drawMenu(sBatch);

            foreach (menuItem item in itemList)
            {
                item.DrawMenuItem(sBatch);
            }

            sBatch.Draw(textureLogo, positionLogo, Color.White);

            sBatch.End();

            mParticle.DrawManager(sBatch, 0);
        }
    }
}
