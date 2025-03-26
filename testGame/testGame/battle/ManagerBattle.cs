using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace testGame.battle
{
    class ManagerBattle
    {
        BatleHero hero;
        BatleHero enemy;

        TextGame textHpHero;
        TextGame textHpEnemy;

        KeyboardState kbState;
        KeyboardState oldState;

        Texture2D textureBullet;
        Texture2D textureHpAttak;
        Texture2D textureEnemyBullet;
        Texture2D textureStartBattle;

        float xSpeed = 1.2f;
        float ySpeed = 2.5f;

        int hp1;
        int hp2;
        int rand;
        int randMiss;
        int randMissSravnenie;
        int randEnemyMiss;
        int randEnemySravnenie;
        int enemyDamage;
        float timeElapsed;
        float yEnemyBullet;
        float g = 0.3f;

        Vector2 StartHeroBullet;
        Vector2 RunPosition;
        Vector2 HpAttak;
        Vector2 enemyBulletPosition;
        Vector2 enemyHpAttak;

        Random randHpAttak;
        Random randHpMiss;

        string strMiss;

        bool isAlive;
        bool isAliveHpAttak;
        bool isEnemyBullet;
        bool isEnemyHpAttak;
        bool startBattle;

        public int countXP;

        SoundEffect soundDie_1;
        SoundEffect soundLevelUp;
        SoundEffect soundHeroUron;

        public ManagerBattle(BatleHero _hero, BatleHero _enemy)
        {
            hero = _hero;
            enemy = _enemy;

            StartHeroBullet = new Vector2(GetHeroRect.X + 15, GetHeroRect.Y + 25);

            textHpHero = new TextGame("", HpAttak, Color.Black);
            textHpEnemy = new TextGame("", enemyHpAttak, Color.Black);

            isAlive = false;
            isAliveHpAttak = false;

            randHpAttak = new Random();
            randHpMiss = new Random();

            randMiss = randHpMiss.Next(1, 11 - (int)enemy.LevelHero);
            randEnemyMiss = randHpMiss.Next(1, 11 - (int)hero.LevelHero);

            enemyHpAttak = new Vector2(hero.RectPosition.X - 20, hero.RectPosition.Y - 20);

            startBattle = false;
        }

        public void LoadContentBattle(ContentManager content)
        {
            hero.LoadContent(content);
            enemy.LoadContent(content);

            textureBullet = content.Load<Texture2D>("texture/bullet");
            textureHpAttak = content.Load<Texture2D>("texture/hpAttak");
            textureEnemyBullet = content.Load<Texture2D>("texture/enemyZ");
            soundDie_1 = content.Load<SoundEffect>("effect/Die1");
            soundLevelUp = content.Load<SoundEffect>("effect/Hallelu");
            soundHeroUron = content.Load<SoundEffect>("effect/ho_tmaj");
            textureStartBattle = content.Load<Texture2D>("texture/StartBattle");

            textHpHero.ContentLoad(content);
            textHpEnemy.ContentLoad(content);
        }

        public void UpdateBattle(GameTime gameTime)
        {
            kbState = Keyboard.GetState();

            hero.UpdateBattle(gameTime);
            enemy.UpdateBattle(gameTime);

            if (startBattle)
            {
                if (!hero.isAliveHero == false)
                {
                    if (IsEnemyFail())
                    {
                        countXP = 0;

                        #region LogicAttakHero

                        if (!isAlive && hero.HP > 0)
                        {
                            if (kbState.IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space))
                            {
                                isAlive = true;
                                RunPosition = StartHeroBullet;
                                //hp1 = HpUron(hero.LevelHero, hero.AttakHero, enemy.DefensHero);
                            }
                        }
                        else
                        {
                            RunPosition.X += gameTime.ElapsedGameTime.Milliseconds * xSpeed * (float)Math.Sin(45) * 0.03f;
                            xSpeed += 0.8f;

                            if (enemy.RectPosition.Intersects(new Rectangle((int)RunPosition.X - 30, (int)RunPosition.Y, 30, 30)))
                            {
                                isAlive = false;
                                xSpeed = 1.2f;
                                HpAttak = new Vector2(enemy.RectPosition.X - 20, enemy.RectPosition.Y);
                                isAliveHpAttak = true;
                                rand = randHpAttak.Next(hero.Damage[0], hero.Damage[1]);
                                //randMiss = randHpMiss.Next(1, (int)enemy.DefensHero);
                                hp1 = HpHeroUron(hero.LevelHero, hero.AttakHero, enemy.DefensHero);

                                enemy.HP -= hp1;
                            }
                        }
                        if (isAliveHpAttak)
                        {
                            HpAttak.Y -= ySpeed * (float)Math.Cos(1);

                            strMiss = hp1 == 0 ? " Miss" : hp1 + " HP";
                            textHpHero.UpdateText(strMiss);

                            Vector2 newVector = new Vector2(HpAttak.X + 20, HpAttak.Y + 15);
                            textHpHero.PositionText = newVector;

                            enemy.Text(isAliveHpAttak);

                            if (HpAttak.Y < 400)
                            {
                                isAliveHpAttak = false;
                            }
                        }

                        #endregion

                        #region LogicAttakEnemy

                        timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

                        if (timeElapsed > 5 && enemy.HP != 0)
                        {
                            isEnemyBullet = true;
                            timeElapsed = 0;
                            yEnemyBullet = 20;
                        }

                        if (!isEnemyBullet)
                        {
                            enemyBulletPosition = new Vector2(enemy.RectPosition.X, enemy.RectPosition.Y + 30);
                            yEnemyBullet = 0;
                        }
                        else
                        {
                            yEnemyBullet = yEnemyBullet - g * gameTime.ElapsedGameTime.Milliseconds * 0.15f;

                            enemyBulletPosition.X -= 4.5f * 3.6f;
                            enemyBulletPosition.Y -= yEnemyBullet;

                            Rectangle r = new Rectangle((int)enemyBulletPosition.X + 30, (int)enemyBulletPosition.Y, 30, 30);

                            if (r.Intersects(hero.RectPosition))
                            {
                                isEnemyBullet = false;

                                enemyDamage = randHpAttak.Next(enemy.Damage[0], enemy.Damage[1]);

                                hp2 = EnemyHpUron(enemy.AttakHero, hero.DefensHero);

                                isEnemyHpAttak = true;

                                hero.HP -= hp2;

                                soundHeroUron.Play(1.0f);
                            }
                        }

                        if (isEnemyHpAttak)
                        {
                            enemyHpAttak.Y -= ySpeed * 0.6f;

                            Vector2 vector = new Vector2(enemyHpAttak.X + 20, enemyHpAttak.Y + 15);
                            string str = hp2 == 0 ? " Miss" : hp2 + " HP";
                            textHpEnemy.UpdateText(str);

                            textHpEnemy.PositionText = vector;

                            if (enemyHpAttak.Y < 400)
                                isEnemyHpAttak = false;
                            hero.Text(isEnemyHpAttak);
                        }
                        else
                        {
                            enemyHpAttak = new Vector2(hero.RectPosition.X - 20, hero.RectPosition.Y - 20);
                        }

                        #endregion

                        if (enemy.HP <= 0)
                        {
                            soundDie_1.Play(10, 0, 0, false);
                        }
                        if (hero.HP <= 0)
                        {
                            soundDie_1.Play(10, 0, 0, false);
                        }
                    }
                    else
                    {
                        countXP = enemy.xperienceHero;
                    }
                    IsEnemyFail();

                    if (hero.xperienceHero > hero.OldXP)
                    {
                        soundLevelUp.Play(15, 0, 0, false);
                        hero.LevelUp(hero.LevelHero + 1);
                    }
                }
            }
            else
            {
                if (kbState.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
                {
                    startBattle = true;
                }

            }
            oldState = kbState;
        }

        public bool IsEnemyFail()
        {
            if (!enemy.isAliveHero)
                return false;
            return true;
        }

        private int HpHeroUron(int heroLevel, float heroAttak, float enemyDefens)
        {
            float hp = (float)Math.Floor((heroAttak * 2.5f * rand) / enemyDefens);

            if (randMiss == randMissSravnenie)
            {
                randMiss = randHpMiss.Next(1, 11 - (int)enemy.LevelHero);
                randMissSravnenie = 0;
                return 0;
            }

            randMissSravnenie++;

            return (int)hp;
        }

        private int EnemyHpUron(float enemyAttak, float heroDefens)
        {
            float hp = (float)Math.Floor((enemyAttak * 2.5f * enemyDamage) / heroDefens);

            if (randEnemyMiss == randEnemySravnenie)
            {
                randEnemyMiss = randHpMiss.Next(1, 11 - (int)hero.LevelHero);
                randEnemySravnenie = 0;
                return 0;
            }

            randEnemySravnenie++;

            return (int)hp;
        }

        public Rectangle GetHeroRect
        {
            get { return hero.RectPosition; }
        }

        public void DrawBattle(SpriteBatch spriteBatch)
        {
            Vector2 newVector = isAlive ? RunPosition : StartHeroBullet;

            hero.DrawHeroies(spriteBatch);
            enemy.DrawHeroies(spriteBatch);

            spriteBatch.Begin();
            spriteBatch.Draw(textureBullet, newVector, Color.White);

            if (isAliveHpAttak)
                spriteBatch.Draw(textureHpAttak, new Rectangle((int)HpAttak.X, (int)HpAttak.Y, 100, 50), Color.White);
            if (isEnemyHpAttak)
                spriteBatch.Draw(textureHpAttak, new Rectangle((int)enemyHpAttak.X, (int)enemyHpAttak.Y, 100, 50), Color.White);
            if (isEnemyBullet)
                spriteBatch.Draw(textureEnemyBullet, enemyBulletPosition, Color.White);
            if (!startBattle)
            {
                spriteBatch.Draw(textureStartBattle, new Vector2(1000 / 2 - 100, 600 / 2 - 100), Color.White);
            }

            spriteBatch.End();
            if (isAliveHpAttak)
                textHpHero.DrawFont(spriteBatch, 0);
            if (isEnemyHpAttak)
                textHpEnemy.DrawFont(spriteBatch, 0);
        }
    }
}
