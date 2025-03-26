using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.IO;
using testGame.battle;
using testGame.gameMenu;

namespace testGame
{
    public enum GameState
    {
        Game,
        MainMenu,
        GameMenu,
        BattleMenu,
        createLevel,
        GameBonus
    }
    
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        ObjBonusCards bonusHero;
        ObjBonusCards bonusEnemy;

        BatleHero battleHero;

        Battle battle;
        MainMenu mainMenu;

        ManagerParticle mParticleSnow;

        Animated hero;

        public Animated _Heroies
        {
            get { return hero; }
        }

        Bullet bullet;

        TextGame[] textGame;
        TextGame[] gameMenuOptions;
        TextGame textHeroUron;
        Texture2D textureBousAnim;

        HelpBox boxH;

        public HelpBox _Box
        {
            get { return boxH; }
            set { boxH = value; }
        }

        Texture2D text;
        Texture2D textureBlock2;
        Texture2D textureBox;
        Texture2D textureBlock;
        Texture2D backGround;
        Texture2D interfaceRight;
        Texture2D gameM;
        Texture2D textureNext;
        Texture2D stopBlock;
        Texture2D shtopTexture;
        Texture2D fireTexture;
        Texture2D heroTextureDied;
        Texture2D textureSnow;
        Texture2D textureTitleWins;
        Texture2D textureTitleFalse;
        Texture2D textureEnemyBolt;

        KeyboardState kb;
        KeyboardState oldState;

        SoundEffect sound;
        SoundEffect heroDied;
        SoundEffect heroUron;
        SoundEffect heroLiveSound;
        SoundEffect musicLevel_1;
        SoundEffect musicLevel_2;
        SoundEffect musicLevel_3;
        SoundEffect musicLevel_4;
        SoundEffect soundGameMenu;
        SoundEffect soundGameMenuClic;
        public SoundEffectInstance musicStop;

        public GameState gameState = GameState.MainMenu;

        public int Width;
        public int Height;

        public int levelCount;
        int score;
        int heroLive = 2;
        int l = 750, l_2 = 1500, l_3 = 2500;
        int gameMenuCount = 1;
        float uronHero;

        private int scrollX;

        public int levelLench;

        bool[] enemyIsJump;

        List<Blocks> blocks;
        List<Animated> golds;
        List<Animated> enemy;
        List<BatleHero> enemyBattle;
        List<Blocks> shtopList;
        List<Blocks> fireList;
        List<AnimBonus> rectListBonus;
        List<AnimBonus> enemyBoltList;
        ArrayList arrayListStopBox;
        ArrayList arrayListStopPlayBox;

        AnimBonus animFire;
        Rectangle startHeroRect;
        Rectangle beckRect;
        Rectangle interRect;
        public Rectangle rectParticleSnow;
        Vector2 gaReMenu;
        Vector2 positionHeroUron;

        Random rand;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Width = graphics.PreferredBackBufferWidth = 1010;
            Height = graphics.PreferredBackBufferHeight = 600;
            //graphics.IsFullScreen = true;

            battleHero = new BatleHero("Vovan");
            mainMenu = new MainMenu(this);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        public void RangMonster(BatleHero enemyBatle)
        {
            battle = new Battle(battleHero, enemyBatle, this);
            battle.LoadContentBattle(Content);
        }

        public bool ColidesLevel(Rectangle re)
        {
            foreach (Blocks blok in blocks)
            {
                if (re.Intersects(blok.Rect))
                {
                    return true;
                }
            }
            foreach (Blocks bl in arrayListStopBox)
            {
                if (re.Intersects(bl.Rect))
                    return true;
            }
            foreach (var item in shtopList)
            {
                if (re.Intersects(item.Rect) && hero.isColorHero)
                {
                    return true;
                }
            }

            return false;
        }

        public void Scroll(float dx)
        {
            if (scrollX + dx > 0 && scrollX + dx < levelLench - Width + 200)
                scrollX += (int)dx;
        }

        public Rectangle getScreenRect(Rectangle getRect)
        {
            Rectangle r = getRect;
            r.Offset(-scrollX, 0);
            return r;
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            mainMenu.ContentLoadMenu(Content);

            textureBlock = Content.Load<Texture2D>("123");
            text = Content.Load<Texture2D>("ppp");
            backGround = Content.Load<Texture2D>("background");
            interfaceRight = Content.Load<Texture2D>("interface");
            gameM = Content.Load<Texture2D>("texture/back");
            textureNext = Content.Load<Texture2D>("texture/next");
            textureBox = Content.Load<Texture2D>("texture/helpBox");
            stopBlock = Content.Load<Texture2D>("texture/stopBlock");
            shtopTexture = Content.Load<Texture2D>("texture/shtop");
            fireTexture = Content.Load<Texture2D>("texture/1111");
            heroTextureDied = Content.Load<Texture2D>("texture/BatleMenuExit");
            textureBlock2 = Content.Load<Texture2D>("texture/1234");
            textureBousAnim = Content.Load<Texture2D>("anim");
            textureTitleWins = Content.Load<Texture2D>("ObjBonusGameContent/textWins");
            textureTitleFalse = Content.Load<Texture2D>("ObjBonusGameContent/textFalse");
            textureEnemyBolt = Content.Load<Texture2D>("textureAnim/Coin");

            textureSnow = Content.Load<Texture2D>("texture/llight_3");

            soundGameMenu = Content.Load<SoundEffect>("effect/arcotensa");
            heroUron = Content.Load<SoundEffect>("effect/boink");
            soundGameMenuClic = Content.Load<SoundEffect>("effect/c7");
            musicLevel_1 = Content.Load<SoundEffect>("music/chimeras_soundtrack_1");
            musicLevel_2 = Content.Load<SoundEffect>("music/chimeras_soundtrack_2");
            musicLevel_3 = Content.Load<SoundEffect>("music/chimeras_soundtrack_3");
            musicLevel_4 = Content.Load<SoundEffect>("music/chimeras_soundtrack_4");

            heroLiveSound = Content.Load<SoundEffect>("effect/autonic");
            heroDied = Content.Load<SoundEffect>("effect/P_PING");
            sound = Content.Load<SoundEffect>("111");

            startHeroRect = new Rectangle(30, 540, 30, 30);

            rectParticleSnow = new Rectangle(100, -450, Width - 50, 500);
            mParticleSnow = new ManagerParticle(this, 25, 2600, 3500, 4, 8, 10, 20, 0.006f, -0.07f, textureSnow);

            boxH = new HelpBox(textureBox, new Rectangle(180, 120, 30, 30), this);
            hero = new Animated(startHeroRect, text, true, this);
            gaReMenu = new Vector2(Width / 2 - gameM.Width / 2, Height / 2 - gameM.Height / 2);

            interRect = new Rectangle(Width - 210, -15, 230, Height + 30);
            beckRect = new Rectangle(0, 0, Width - 200, Height);

            textGame = new TextGame[11];
            gameMenuOptions = new TextGame[8];

            gameMenuOptions[0] = new TextGame("Resume", new Vector2(gaReMenu.X + 160, gaReMenu.Y + 100), Color.White);
            gameMenuOptions[1] = new TextGame("Resume", new Vector2(gaReMenu.X + 170, gaReMenu.Y + 100), Color.Black);
            gameMenuOptions[2] = new TextGame("Restart", new Vector2(gaReMenu.X + 160, gaReMenu.Y + 130),Color.White);
            gameMenuOptions[3] = new TextGame("Restart", new Vector2(gaReMenu.X + 170, gaReMenu.Y + 130), Color.Black);
            gameMenuOptions[4] = new TextGame("MainMenu", new Vector2(gaReMenu.X + 160, gaReMenu.Y + 160), Color.White);
            gameMenuOptions[5] = new TextGame("MainMenu", new Vector2(gaReMenu.X + 170, gaReMenu.Y + 160), Color.Black);
            gameMenuOptions[6] = new TextGame("Exit", new Vector2(gaReMenu.X + 160, gaReMenu.Y + 190), Color.White);
            gameMenuOptions[7] = new TextGame("Exit", new Vector2(gaReMenu.X + 170, gaReMenu.Y + 190), Color.Black);

            textGame[0] = new TextGame("Level", new Vector2(Width - 140, 20), Color.Black);
            textGame[1] = new TextGame(levelCount.ToString(), new Vector2(Width - 80, 20), Color.Black);
            textGame[2] = new TextGame("Score:", new Vector2(Width - 180, 100), Color.Black);
            textGame[3] = new TextGame(score.ToString(), new Vector2(Width - 100, 100), Color.Black);
            textGame[4] = new TextGame("Live", new Vector2(Width - 180, 130), Color.Blue);
            textGame[5] = new TextGame(heroLive.ToString(), new Vector2(Width - 100, 130), Color.Blue);
            textGame[6] = new TextGame("Options", new Vector2(gaReMenu.X + gameM.Width / 2 - 40,
            gaReMenu.Y + 40), Color.White);
            textGame[7] = new TextGame("Bullet", new Vector2(Width - 180, 300), Color.Black);
            textGame[9] = new TextGame("HP", new Vector2(Width - 180, 155), Color.Blue);
            textGame[10] = new TextGame(" / ", new Vector2(Width - 70, 155), Color.Blue);

            textHeroUron = new TextGame(uronHero.ToString(), Vector2.Zero, Color.White);
            textHeroUron.isScrollX = true;
            textHeroUron.ContentLoad(Content);

            score = 0;
            //CreateLevel(levelCount);

            bullet = new Bullet(text, new Rectangle(hero.rect.X, hero.rect.Y, 50, 50),
            new Rectangle(0, 52, 50, 50), this);
            textGame[8] = new TextGame(bullet.MaxBall.ToString(), new Vector2(Width - 100, 300), Color.Black);

            foreach (TextGame item in gameMenuOptions)
            {
                item.ContentLoad(Content);
            }
        }

        public void CreateLevel(int levelCount)
        {
            blocks = new List<Blocks>();
            golds = new List<Animated>();
            enemy = new List<Animated>();
            arrayListStopBox = new ArrayList();
            arrayListStopPlayBox = new ArrayList();
            enemyBattle = new List<BatleHero>();
            shtopList = new List<Blocks>();
            fireList = new List<Blocks>();
            rectListBonus = new List<AnimBonus>();
            enemyBoltList = new List<AnimBonus>();
            rand = new Random();

            int x = 0;
            int y = 0;

            string[] str = File.ReadAllLines("Content/level" + levelCount + ".txt");

            scrollX = 0;

            levelLench = str[0].Length * 30;

            if (musicStop != null)
                musicStop.Stop(true);

            int playContent = rand.Next(0, 2);

            switch (levelCount)
            {
                case 1:
                    musicStop = musicLevel_1.Play(0.08f, 0, 0, true);
                    hero.rect = startHeroRect;
                    scrollX = 0;
                    boxH.GetRectBox = new Rectangle(180, 120, 30, 30);

                    enemyBattle.Add(new BatleHero("Harder", 1, 1, 2, 2.4f, 75, 220, 2, 6));
                    enemyBattle.Add(new BatleHero("Goro", 1, 1, 2.5f, 2, 62, 155, 4, 5));
                    enemyBattle.Add(new BatleHero("Wolf", 1, 1, 2.2f, 1.2f, 68, 130, 2, 5));
                    enemyBattle.Add(new BatleHero("Halk", 1, 1, 2.6f, 1.3f, 48, 190, 3, 6));

                    enemyBattle[0].LoadContent(Content);
                    enemyBattle[1].LoadContent(Content);
                    enemyBattle[2].LoadContent(Content);
                    enemyBattle[3].LoadContent(Content);
                    break;
                case 2:

                    enemyBattle.Add(new BatleHero("Harder", 1, 2, 3.4f, 4.8f, 130, 325, 4, 7));
                    enemyBattle.Add(new BatleHero("Spaider", 1, 2, 3.2f, 5.2f, 110, 295, 6, 8));
                    enemyBattle.Add(new BatleHero("Bersecer", 1, 2, 6f, 6.3f, 145, 485, 4, 9));
                    enemyBattle.Add(new BatleHero("Zombie", 1, 2, 3.2f, 5.2f, 120, 300, 3, 6));
                    enemyBattle.Add(new BatleHero("Scelet", 1, 2, 2.8f, 4.2f, 80, 100, 3, 4));

                    battleHero.LevelUp(levelCount);

                    bonusHero = new ObjBonusCards(5, 50, 20, this, 1, 0.2, 100, 5);
                    bonusEnemy = new ObjBonusCards(5, 50, 20, this, 700, 0.2, 100, 3);
                    bonusEnemy.isControlLogic = false;

                    animFire = new AnimBonus(Content.Load<Texture2D>("textureAnim//fire"), new Rectangle(hero.rect.X, hero.rect.Y - 30, 30, 80),
                        19, 2, 7, 50, 54, 72);

                    foreach (var item in enemyBattle)
                    {
                        item.LoadContent(Content);
                    }

                    boxH.GetRectBox = new Rectangle(120, 30, 30, 30);
                    musicStop = musicLevel_2.Play(0.5f, 0, 0, true);
                    break;
                case 3:
                    musicStop = musicLevel_3.Play(0.5f, 0, 0, true);
                    break;
                default:
                    musicStop = musicLevel_4.Play(1, 0, 0, true);
                    break;
            }

            foreach (string item in str)
            {
                foreach (char ch in item)
                {
                    Rectangle rect = new Rectangle(x, y, 30, 30);

                    if (ch == 'x')
                    {
                        if (levelCount <= 3)
                        {
                            Blocks block = new Blocks(rect, textureBlock, this);
                            blocks.Add(block);
                        }
                        else
                        {
                            Blocks block = new Blocks(rect, textureNext, this);
                            blocks.Add(block);
                        }
                    }
                    if (ch == 'y')
                    {
                        Animated a = new Animated(text, rect, 52, 50, 208, 5, false, 7, 150, this, 0);
                        golds.Add(a);
                    }
                    if (ch == 'e')
                    {
                        Animated e = new Animated(text, rect, 52, 50, 52 * 16, 5, true, 14, 150, this, 1.99f);
                        enemy.Add(e);
                        enemyIsJump = new bool[enemy.Count];
                    }
                    if (ch == 'z')
                    {
                        Blocks block = new Blocks(rect, stopBlock, this);
                        arrayListStopBox.Add(block);
                    }
                    if (ch == 'q')
                    {
                        arrayListStopPlayBox.Add(new Blocks(new Rectangle(x + 5, y + 25, 20, 20), stopBlock, this));
                    }
                    if (ch == 'p')
                    {
                        Blocks blok = new Blocks(rect, shtopTexture, this);
                        blok.R(rand.Next(58, 65));
                        shtopList.Add(blok);
                    }
                    if (ch == 'f')
                    {
                        Rectangle r = new Rectangle(rect.X, rect.Y - 20, 30, 30 + 20);
                        Blocks bloko = new Blocks(r, fireTexture, this);
                        fireList.Add(bloko);
                    }
                    if (ch == 'c')
                    {
                        blocks.Add(new Blocks(rect, textureBlock2, this));
                    }
                    if (ch == '1')
                    {
                        Rectangle rBonus = new Rectangle(rect.X - 12, rect.Y - 12, 50, 50);
                        rectListBonus.Add(new AnimBonus(textureBousAnim, rBonus, 18, 5, 3, 120, 108, 108));
                    }
                    if (ch == '2')
                    {
                        Rectangle rBonus = new Rectangle(rect.X - 10, rect.Y - 10, 50, 50);
                        enemyBoltList.Add(new AnimBonus(textureEnemyBolt, rBonus, 18, 3, 5, 120, 40, 40));
                    }

                    x += 30;
                }
                x = 0;
                y += 30;
            }

            foreach (TextGame item in textGame)
            {
                item.ContentLoad(Content);
            }
            foreach (BatleHero item in enemyBattle)
            {
                item.LoadContent(Content);
            }

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private void GameMenuUpdate()
        {
            kb = Keyboard.GetState();

            textGame[6].isVisible = true;

            if (kb.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                soundGameMenu.Play();

                if (gameMenuCount < 2)
                {
                    gameMenuCount = 1;
                }
                else
                    gameMenuCount = gameMenuCount - 1;
            }
            else if (kb.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                soundGameMenu.Play();

                if (gameMenuCount > 3)
                    gameMenuCount = 4;
                else
                    gameMenuCount = gameMenuCount + 1;
            }

            if (kb.IsKeyDown(Keys.Enter) && gameMenuCount == 1)
            {
                soundGameMenuClic.Play();
                musicStop.Play();
                gameState = GameState.Game;
            }
            if (kb.IsKeyDown(Keys.Enter) && gameMenuCount == 4)
                this.Exit();

            if (kb.IsKeyDown(Keys.Enter) && gameMenuCount == 2 && oldState.IsKeyUp(Keys.Enter))
            {
                gameState = GameState.Game;
                CreateLevel(levelCount);
            }
            if (kb.IsKeyDown(Keys.Enter) && gameMenuCount == 3)
                gameState = GameState.MainMenu;

            oldState = kb;
        }

        public void EnemyDate(Rectangle[] re)
        {
            for(int i = 0; i < enemy.Count; i++)
            {
                re[i] = enemy[i].rect;
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (gameState == GameState.Game)
            {
                kb = Keyboard.GetState();

                foreach (var item in rectListBonus)
                {
                    item.UpdateAnim(gameTime);
                }

                if (battleHero.HP <= 0)
                {
                    if (kb.IsKeyDown(Keys.Enter))
                    {
                        gameState = GameState.createLevel;
                    }
                }
                else
                    UpdateMethod(gameTime);
            }
            else if (gameState == GameState.GameMenu)
            {
                GameMenuUpdate();
            }
            else if (gameState == GameState.BattleMenu)
            {
                battle.Update(gameTime);
            }
            else if (gameState == GameState.createLevel)
            {
                CreateLevel(levelCount);
                battleHero = new BatleHero("Vovan");
                gameState = GameState.Game;
                hero.rect = startHeroRect;
                battleHero.LevelUp(levelCount);
                musicStop.Play();
            }
            else if (gameState == GameState.MainMenu)
            {
                mParticleSnow.Update(gameTime, rectParticleSnow);

                mainMenu.UpdateMenu(gameTime);
            }
            else if (gameState == GameState.GameBonus)
            {
                if (!bonusHero.isTimeLevel)
                {
                    bonusHero.UpdateCards(gameTime);
                    bonusEnemy.UpdateCards(gameTime);
                }
                else
                {
                    if (Mouse.GetState().RightButton == ButtonState.Pressed)
                    {
                        gameState = GameState.Game;
                        musicStop.Play();
                        if (bonusHero.scoreNumber > bonusEnemy.scoreNumber && bonusHero.isTimeLevel)
                        {
                            int s = battleHero.HP + 50 % battleHero.OldHP;
                            if (s > battleHero.OldHP)
                            {
                                s = battleHero.OldHP;
                            }
                            battleHero.HP = s;
                            positionHeroUron.X = hero.rect.X;
                            positionHeroUron.Y = hero.rect.Y;
                            textHeroUron.PositionText = positionHeroUron;
                            textHeroUron.UpdateText(" + " + 50 + " HP");
                        }
                    }
                }
            }

            base.Update(gameTime);
        }

        private void LiveUp(int live)
        {
            if (live >= l)
            {
                l = 20000;
                heroLive = heroLive + 1;
                heroLiveSound.Play();
            }
            else if (live >= l_2)
            {
                l_2 = 20000;
                heroLive = heroLive + 1;
                heroLiveSound.Play();
            }
            else if (live >= l_3)
            {
                l_3 = 20000;
                heroLive = heroLive + 1;
                heroLiveSound.Play();
            }
        }

        public  bool EnemyFullDown(Rectangle re, bool isRigth, bool isJ)
        {
            Rectangle rectEnemy = re;

            if (isRigth && !isJ)
            {
                rectEnemy.Offset(10, 10);
            }
            else if (!isRigth && !isJ)
            {
                rectEnemy.Offset(-10, 10);
            }

            if (!ColidesLevel(rectEnemy))
                return true;

            return false;
        }

        private bool HeroIsLineEnemy(Rectangle reEnemy)
        {
            Rectangle re = new Rectangle(hero.rect.X - Width, hero.rect.Y, Width + Width, hero.rect.Height);

            if (re.Intersects(reEnemy))
                return true;

            return false;
        }

        private int ColidesBonusGame(Rectangle rectBonus)
        {
            for (int i = 0; i < rectListBonus.Count; i++)
            {
                if (rectBonus.Intersects(rectListBonus[i].RectBonus))
                {
                    return i;
                }
            }
            return -1;
        }

        private bool HeroisSideEnemy(Rectangle reEnemy)
        {
            if (hero.rect.Right > reEnemy.Right)
                return true;

            return false;
        }

        private bool UpdateUronHero(Rectangle rectObject)
        {
            foreach (var item in fireList)
            {
                if (item.Rect.Intersects(rectObject) && hero.isColorHero)
                {
                    return true;
                }
            }
            foreach (var item in shtopList)
            {
                if (item.Rect.Intersects(rectObject) && hero.isColorHero)
                {
                    return true;
                }
            }
            return false;
        }

        private void UpdateMethod(GameTime gameTime)
        {
            kb = Keyboard.GetState();
            if (animFire != null)
                animFire.UpdateAnim(gameTime);
            foreach (var item in fireList)
            {
                item.UpdateBlocksFire(gameTime);
            }
            foreach (var item in shtopList)
            {
                item.UpdateBlocks(gameTime);
            }
            foreach (AnimBonus item in enemyBoltList)
            {
                item.UpdateAnim(gameTime);
            }
            #region UpdateUron

            if (UpdateUronHero(hero.rect))
            {
                uronHero = uronHero - 0.5f;

                positionHeroUron = new Vector2(hero.rect.X, hero.rect.Y);
            }
            else
            {
                battleHero.HP += (int)uronHero;

                positionHeroUron.Y -= 1.5f;

                textHeroUron.PositionText = positionHeroUron;

                if (uronHero != 0)
                {
                    heroUron.Play(1);
                    textHeroUron.UpdateText("" + uronHero.ToString() + " HP");
                    hero.isColorHero = false;
                }
                uronHero = 0;
            }

            #endregion
            //обновления текста в игре
            textGame[1].UpdateText(levelCount.ToString());
            textGame[3].UpdateText(score.ToString());
            textGame[5].UpdateText(heroLive.ToString());
            textGame[8].UpdateText(bullet.MaxBall.ToString());
            textGame[9].UpdateText("HP        " + battleHero.HP);
            textGame[10].UpdateText(" / " + battleHero.OldHP);
            //обновления героя
            hero.UpdateAnim(gameTime, arrayListStopBox, arrayListStopPlayBox, enemyBattle);
            boxH.UpdateBox(gameTime);
            //проверка на опции
            if (kb.IsKeyDown(Keys.Escape))
            {
                musicStop.Pause();
                soundGameMenuClic.Play();
                gameState = GameState.GameMenu;
            }
            //обновлять золото
            foreach (Animated item in golds)
            {
                item.UpdateAnim(gameTime, arrayListStopBox, arrayListStopPlayBox, enemyBattle);
            }
            //цыкл противников на прыжок, ход влево,и право, ипроверка пересичения с героям.
            for (int i = 0; i < enemy.Count; i++)
            {
                enemy[i].UpdateAnim(gameTime, arrayListStopBox, arrayListStopPlayBox, enemyBattle);
                // выбор сложности противника
                if (i == 1)
                    enemy[1].isEnemyHard = true;
                // проверка противника с верхней или нижней границей экрана на прыжок
                if (enemy[i].rect.Y < 50)
                    enemyIsJump[i] = false;
                else if (enemy[i].rect.Y > Height - 150)
                    enemyIsJump[i] = true;

                // перемищение героя после столкновения с противником
                if (hero.rect.Intersects(enemy[i].rect))
                {
                    hero.rect = startHeroRect;
                    heroDied.Play();
                    if (heroLive > 0)
                        heroLive--;
                }
                #region Enemy
                // если противник сложный то...
                if (enemy[i].isEnemyHard)
                {
                    if (!HeroIsLineEnemy(enemy[i].rect))
                    {
                        if (enemy[i].isRuningRigth)
                        {
                            enemy[i].RunRight(true);

                            if (enemyIsJump[i])
                            {
                                if (EnemyFullDown(enemy[i].rect, true, enemy[i].isJump))
                                    enemy[i].Jump();
                            }
                        }
                        else
                        {
                            enemy[i].RunRight(false);

                            if (enemyIsJump[i])
                            {
                                if (EnemyFullDown(enemy[i].rect, false, enemy[i].isJump))
                                    enemy[i].Jump();
                            }
                        }
                    }
                    else
                    {
                        if (HeroisSideEnemy(enemy[i].rect))
                        {
                            enemy[i].RunRight(true);

                            if (enemyIsJump[i])
                            {
                                if (EnemyFullDown(enemy[i].rect, true, enemy[i].isJump))
                                    enemy[i].Jump();
                            }
                        }
                        else
                        {
                            enemy[i].RunRight(false);

                            if (enemyIsJump[i])
                            {
                                if (EnemyFullDown(enemy[i].rect, false, enemy[i].isJump))
                                    enemy[i].Jump();
                            }
                        }
                    }
                }
                // если противник не сложный то...
                else
                {
                    if (enemy[i].isRuningRigth)
                    {
                        enemy[i].RunRight(true);

                        if (enemyIsJump[i])
                        {
                            if (EnemyFullDown(enemy[i].rect, true, enemy[i].isJump))
                                enemy[i].Jump();
                        }
                    }
                    else
                    {
                        enemy[i].RunRight(false);

                        if (enemyIsJump[i])
                        {
                            if (EnemyFullDown(enemy[i].rect, false, enemy[i].isJump))
                                enemy[i].Jump();
                        }
                    }
                }
            }
                #endregion

            // проверка пересичения золота, и героя, и создание уровня
            for (int i = 0; i < golds.Count; i++)
            {
                if (hero.rect.Intersects(golds[i].rect))
                {
                    golds.Remove(golds[i]);
                    sound.Play();
                    score = score + 34;
                    LiveUp(score);

                    if (golds.Count == 0)
                    {
                        levelCount++;
                        hero.rect = startHeroRect;
                        CreateLevel(levelCount);
                    }
                }
            }
            if (ColidesBonusGame(hero.rect) >= 0)
            {
                gameState = GameState.GameBonus;

                bonusHero = new ObjBonusCards(5, 50, 20, this, 1, 0.2, 60, 5);
                bonusEnemy = new ObjBonusCards(5, 50, 20, this, 700, 0.2, 60, 5);
                bonusEnemy.isControlLogic = false;

                bonusHero.ContentLoad(Content);
                bonusEnemy.ContentLoad(Content);

                rectListBonus[ColidesBonusGame(hero.rect)].isAlive = false;

                musicStop.Stop();
            }

            // контроль героя или управления героям

            if (kb.IsKeyDown(Keys.Left))
            {
                boxH.getOutSite = false;
                hero.RunRight(false);
                hero.isRuning = true;
            }

            else if (kb.IsKeyDown(Keys.Right))
            {
                boxH.getOutSite = true;
                hero.RunRight(true);
                hero.isRuning = true;
            }
            else
                hero.isRuning = false;

            if (kb.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                if (boxH.HeroIntersectBox() && hero.isHeroUp())
                {
                    boxH.isBoxUp = true;
                    boxH.GetRectBox = new Rectangle(hero.rect.X, hero.rect.Y - 30, 30, 30);
                }
            }

            // проверка если есть ещё пули или выстрелить
            if (kb.IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space))
            {
                hero.Jump();
            }

            Window.Title = hero.elapseSecond.ToString();

            if (kb.IsKeyDown(Keys.B) && oldState.IsKeyUp(Keys.B))
            {
                boxH.GetRectBox = hero.rect;
                boxH.isBoxUp = false;
            }
            if (kb.IsKeyDown(Keys.C) && oldState.IsKeyUp(Keys.C))
            {
                boxH.isBoxUp = false;
            }
            if (kb.IsKeyDown(Keys.V) && oldState.IsKeyUp(Keys.V))
            {
                if (boxH.HeroIntersectBox() && hero.isHeroUp())
                {
                    boxH.isBoxUp = true;
                    boxH.GetRectBox = new Rectangle(hero.rect.X, hero.rect.Y - 30, 30, 30);
                }
            }

            boxH.heroRectangle = hero.rect;
            hero.RectBox = boxH.GetRectBox;

            Rectangle rectHero = getScreenRect(hero.rect);

            if (rectHero.Right > Width / 2 + 15)
                Scroll((float)2 * gameTime.ElapsedGameTime.Milliseconds * 0.09f);
            if (rectHero.Left < Width / 3)
                Scroll(-(float)2 * gameTime.ElapsedGameTime.Milliseconds * 0.09f);

            oldState = kb;
        }

        protected override void Draw(GameTime gameTime)
        {
            if (gameState == GameState.Game)
            {
                gameDraw();
            }

            else if (gameState == GameState.GameMenu)
            {
                GraphicsDevice.Clear(Color.Black);
                spriteBatch.Begin();
                spriteBatch.Draw(gameM, gaReMenu, Color.White);
                spriteBatch.End();

                textGame[6].DrawFont(spriteBatch, 0);

                if (gameMenuCount == 1)
                    gameMenuOptions[1].DrawFont(spriteBatch,0);
                else
                    gameMenuOptions[0].DrawFont(spriteBatch,0);

                if (gameMenuCount == 2)
                    gameMenuOptions[3].DrawFont(spriteBatch,0);
                else
                    gameMenuOptions[2].DrawFont(spriteBatch,0);

                if (gameMenuCount == 3)
                    gameMenuOptions[5].DrawFont(spriteBatch,0);
                else
                    gameMenuOptions[4].DrawFont(spriteBatch,0);

                if (gameMenuCount == 4)
                    gameMenuOptions[7].DrawFont(spriteBatch,0);
                else
                    gameMenuOptions[6].DrawFont(spriteBatch,0);
            }
            else if (gameState == GameState.BattleMenu)
            {
                GraphicsDevice.Clear(Color.Firebrick);

                foreach (BatleHero item in enemyBattle)
                {
                    item.DrawHeroies(spriteBatch);
                }
                battle.DarwBattle(spriteBatch);
            }
            else if (gameState == GameState.MainMenu)
            {
                GraphicsDevice.Clear(Color.DimGray);

                mParticleSnow.DrawManager(spriteBatch, 0);
                mainMenu.DrawMenu(spriteBatch);
            }
            else if (gameState == GameState.GameBonus)
            {
                spriteBatch.Begin();
                bonusHero.DrawFon(spriteBatch);
                bonusHero.DrawBonusCards(spriteBatch);
                bonusEnemy.DrawBonusCards(spriteBatch);

                if (bonusHero.scoreNumber > bonusEnemy.scoreNumber && bonusHero.isTimeLevel)
                {
                    spriteBatch.Draw(textureTitleWins, new Vector2(Width / 2 - textureTitleWins.Width / 2,
                        Height / 2 - textureTitleWins.Height / 2), Color.White);
                }
                else if (bonusHero.scoreNumber < bonusEnemy.scoreNumber && bonusHero.isTimeLevel)
                {
                    spriteBatch.Draw(textureTitleFalse, new Vector2(Width / 2 - textureTitleWins.Width / 2,
                        Height / 2 - textureTitleWins.Height / 2), Color.White);
                }
                spriteBatch.End();

                bonusHero.DrawParticle(spriteBatch);
            }

            base.Draw(gameTime);
        }

        private void gameDraw()
        {
            mParticleSnow.DrawManager(spriteBatch, scrollX);

            spriteBatch.Begin();

            spriteBatch.Draw(backGround, beckRect, Color.White);

            foreach (AnimBonus item in rectListBonus)
            {
                item.DrawAnim(spriteBatch, scrollX);
            }
            foreach (AnimBonus item in enemyBoltList)
            {
                item.DrawAnim(spriteBatch, scrollX);
            }

            foreach (Blocks bl in arrayListStopBox)
            {
                bl.DrawBlocks(spriteBatch);
            }
            foreach (Blocks bl in arrayListStopPlayBox)
            {
                bl.DrawBlocks(spriteBatch);
            }

            foreach (var item in shtopList)
            {
                item.DrawBlocks(spriteBatch);
            }

            foreach (var item in fireList)
            {
                item.DrawBlocks(spriteBatch);
            }

            foreach (Blocks bloc in blocks)
            {
                bloc.DrawBlocks(spriteBatch);
            }
            foreach (Animated item in golds)
            {
                item.DrawAnim(spriteBatch);
            }
            foreach (Animated item in enemy)
            {
                item.DrawAnim(spriteBatch);
            }

            boxH.DrawBox(spriteBatch);
            hero.DrawAnim(spriteBatch);

            if (battleHero.HP < 1)
            {
                spriteBatch.Draw(heroTextureDied, new Vector2(Width / 2 - heroTextureDied.Width/2,Height / 2 - heroTextureDied.Height / 2), Color.White);
            }

            spriteBatch.End();

            foreach (var item in fireList)
            {
                item.DrawParticleBlockFire(spriteBatch, scrollX);   
            }

            if (bullet.isAlive)
                bullet.DrawBullet(spriteBatch);

            textGame[6].isVisible = false;

            spriteBatch.Begin();
            spriteBatch.Draw(interfaceRight, interRect, Color.White);
            if (animFire != null)
                animFire.DrawAnim(spriteBatch, scrollX);
            spriteBatch.End();

            foreach (TextGame t in textGame)
            {
                t.DrawFont(spriteBatch, 0);
            }

            textHeroUron.DrawFont(spriteBatch, scrollX);
        }
    }
}
