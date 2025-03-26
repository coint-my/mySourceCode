using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Collections;
using testGame.battle;

namespace testGame
{
    public class BatleHero
    {
        Texture2D heroTexture;
        Texture2D textureAtribute;

        Rectangle positionAttribute;
        Rectangle position;

        public Rectangle RectPosition
        {
            get { return position; }
        }

        TextGame[] text;

        string nameHero;

        float attackHero;

        public float AttakHero
        { get { return attackHero; } }

        float defenseHero;

        public float DefensHero
        { get { return defenseHero; } }

        public int xperienceHero;

        int liveHero;

        public int HP
        {
            get { return liveHero; }
            set 
            {
                liveHero = value;
            }
        }

        int[] damage;
        public int[] Damage
        { get { return damage; } }

        int levelHero;

        public int LevelHero
        { get { return levelHero; } }

        int oldXP;
        public int OldXP
        { get { return oldXP; } }

        int oldLive;
        public int OldHP
        {
            get { return oldLive; }
        }

        int enemyInt;

        public bool isAliveHero;

        const float attC = 1.8f;
        const float defC = 3.2f;
        const int HpC = 87;
        const int dam1 = 3;
        const int dam2 = 5;

        public BatleHero(string _name)
        {
            nameHero = _name;
            levelHero = 1;
            attackHero = attC;
            defenseHero = defC;
            liveHero = HpC;
            xperienceHero = 0;
            damage = new int[2];
            damage[0] = dam1;
            damage[1] = dam2;

            oldXP = 658;
            oldLive = liveHero;

            enemyInt = 0;

            positionAttribute = new Rectangle(5, 5, 140, 250);
            position = enemyInt < 1 ? new Rectangle(50, 500, 54, 100)
                                   : new Rectangle(900, 500, 54, 100);

            text = new TextGame[7];
            LoadAttribute();

            isAliveHero = true;
        }
        public BatleHero(string _name, int _enemy, int _level, float _attack, float _defens, int _live,
            int _xp, int _damege1, int _damage2)
        {
            nameHero = _name;
            this.levelHero = _level;
            this.attackHero = _attack;
            this.defenseHero = _defens;
            this.liveHero = _live;
            this.xperienceHero = _xp;
            this.enemyInt = _enemy;
            damage = new int[2];
            damage[0] = _damege1;
            damage[1] = _damage2;

            oldXP = 480;
            oldXP = oldXP * _level;
            oldLive = liveHero;

            positionAttribute = new Rectangle(600, 5, 150, 250);

            text = new TextGame[7];
            LoadAttribute();

            isAliveHero = true;
        }

        public void LevelUp(int level)
        {
            levelHero = level;
            attackHero = attC * level;
            damage[0] = dam1 * level;
            damage[1] = (damage[0] + level + 2);
            defenseHero = defC * level;
            oldLive = HpC * level;
            oldXP = oldXP * level;

            liveHero = oldLive;
            xperienceHero = 0;
        }

        private void LoadAttribute()
        {
            text[0] = new TextGame("Hero : " + nameHero.ToString(), 
                new Vector2(positionAttribute.X + 150, positionAttribute.Y + 5), Color.White);
            text[1] = new TextGame("Level      : " + levelHero.ToString(), 
                new Vector2(positionAttribute.X + 150, positionAttribute.Y + 41), Color.White);

            text[2] = new TextGame("Attack    : " + attackHero.ToString(), 
                new Vector2(positionAttribute.X + 150, positionAttribute.Y + 77), Color.White);
            text[3] = new TextGame("Defens   : " + defenseHero.ToString(), 
                new Vector2(positionAttribute.X + 150, positionAttribute.Y + 149), Color.White);
            text[4] = new TextGame("HP       : " + liveHero.ToString(),
                new Vector2(positionAttribute.X + 150, positionAttribute.Y + 185), Color.White);  
            text[5] = new TextGame("XP         : " + xperienceHero.ToString(),
                new Vector2(positionAttribute.X + 150, positionAttribute.Y + 221), Color.White);
            text[6] = new TextGame("Damage : " + damage[0] + " - " + damage[1],
                new Vector2(positionAttribute.X + 150, positionAttribute.Y + 113), Color.White);
        }

        public void LoadContent(ContentManager content)
        {
            heroTexture = content.Load<Texture2D>("battle//hero" + enemyInt);
            textureAtribute = content.Load<Texture2D>("texture/atribute");

            foreach (TextGame t in text)
            {
                t.ContentLoad(content);
            }

            position = enemyInt < 1 ? new Rectangle(50, 500, heroTexture.Width, heroTexture.Height)
                : new Rectangle(900, 500, heroTexture.Width, heroTexture.Height);
        }

        public void UpdateBattle(GameTime gameTime)
        {
            if (liveHero < 1)
            {
                isAliveHero = false;
            }

            text[1].UpdateText("Level     : " + levelHero.ToString());
            text[2].UpdateText("Attack   : " + attackHero.ToString());
            text[6].UpdateText("Damage : " + damage[0] + " - " + damage[1]);
            text[3].UpdateText("Defens  : " + defenseHero.ToString());
            text[4].UpdateText("HP        : " + liveHero.ToString() + " / " + oldLive.ToString());
            text[4].TextColor = Color.Blue;
            text[5].UpdateText("XP        : " + xperienceHero.ToString() + " / " + oldXP.ToString());
        }

        public void Text(bool flag)
        {
            if (!flag)
            {
                text[4].TextColor = Color.Blue;
                text[4].UpdateText("HP        : " + liveHero.ToString() + " / " + oldLive.ToString());
            }
            else
            {
                text[4].TextColor = Color.Red;
                text[4].UpdateText("HP        : " + liveHero.ToString() + " / " + oldLive.ToString());
            }
        }

        public void DrawHeroies(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(heroTexture, positionAttribute, Color.White);
            spriteBatch.Draw(heroTexture, position, Color.White);
            spriteBatch.Draw(textureAtribute, new Rectangle(positionAttribute.X + 140, positionAttribute.Y, 
                200, 250), Color.White);
            spriteBatch.End();

            foreach (TextGame t in text)
            {
                t.DrawFont(spriteBatch, 0);
            }
        }
    }
}
