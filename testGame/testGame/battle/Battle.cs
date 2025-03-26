using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections;
using Microsoft.Xna.Framework.Audio;

namespace testGame.battle
{
    public class Battle
    {
        Texture2D textureWin;
        Texture2D textureBattleExit;

        BatleHero hero;
        BatleHero enemy;

        ManagerBattle mBattle;
        TextGame textXP;

        Vector2 positionTextXP;

        Game1 game;

        Rectangle screenRect;
        Rectangle XPRext;

        SoundEffect musicPlay;
        SoundEffectInstance music;

        int countXp;

        public Battle(BatleHero _hero, BatleHero _enemy, Game1 _game)
        {
            game = _game;
            hero = _hero;
            enemy = _enemy;

            mBattle = new ManagerBattle(_hero, _enemy);

            screenRect = new Rectangle(game.Width / 2 - 100, game.Height / 2 - 100, 200, 200);
            positionTextXP = new Vector2(game.Width / 2 - 20, game.Height / 2 + 100);

            textXP = new TextGame("", positionTextXP, Color.White);

            XPRext = new Rectangle(game.Width / 3 - 100, game.Height / 3 + 25, 10, 10);
        }

        public void LoadContentBattle(ContentManager content)
        {
            mBattle.LoadContentBattle(content);

            Random r = new Random();

            textureWin = content.Load<Texture2D>("texture/screenWin");
            textureBattleExit = content.Load<Texture2D>("texture/BatleMenuExit");

            musicPlay = content.Load<SoundEffect>("ObjBonusGameContent/" + "COMBAT" + r.Next(0, 2).ToString());
            music = musicPlay.Play(0.5f);

            game.musicStop.Pause();

            textXP.ContentLoad(content);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState kb = Keyboard.GetState();

            mBattle.UpdateBattle(gameTime);

            if (!mBattle.IsEnemyFail())
            {
                UpdateTextXP(mBattle.countXP, kb);
                textXP.UpdateText(countXp + " XP");

                if (kb.IsKeyDown(Keys.Enter) && !textXP.isVisible)
                {
                    game.gameState = GameState.Game;
                    music.Stop();
                    game.musicStop.Resume();
                }
            }
            else
            {
                countXp = 0;
            }
            if (!hero.isAliveHero)
            {
                if (kb.IsKeyDown(Keys.Escape))
                    game.Exit();
                if (kb.IsKeyDown(Keys.Enter))
                {
                    music.Stop();
                    game.gameState = GameState.createLevel;
                }
            }
        }

        public void UpdateTextXP(int textXPEnemy, KeyboardState kb)
        {
            if (countXp < textXPEnemy)
            {
                countXp++;
            }
            else
            {
                if (positionTextXP.X > game.Width / 3 - 100)
                    positionTextXP.X -= 1.45f;

                if (positionTextXP.Y > game.Height / 3 + 25)
                    positionTextXP.Y -= 1;

                textXP.PositionText = positionTextXP;

                Rectangle r = new Rectangle((int)positionTextXP.X, (int)positionTextXP.Y, 10, 10);

                if (r.Intersects(XPRext))
                {
                    XPRext.Offset(0, 500);
                    textXP.isVisible = false;
                    hero.xperienceHero += textXPEnemy;
                }
            }
            if (kb.IsKeyDown(Keys.Enter))
                countXp = textXPEnemy;
        }

        public void DarwBattle(SpriteBatch spriteBatch)
        {
            mBattle.DrawBattle(spriteBatch);

            if (!mBattle.IsEnemyFail())
            {
                spriteBatch.Begin();
                spriteBatch.Draw(textureWin, screenRect, Color.Coral);
                spriteBatch.End();

                textXP.DrawFont(spriteBatch, 0);
            }
            if (!hero.isAliveHero)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(textureBattleExit, new Vector2(1000 / 2 - 100, 600 / 2 - 100), Color.White);
                spriteBatch.End();
            }
        }
    }
}
