using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace testGame
{
    class ObjBonusCards
    {
        public List<ObjBonus> objType;

        public List<ObjBonus> ListObject
        {
            get { return objType; }
            set { objType = value; }
        }

        BonusManagerGame manager;
        ManagerParticle managerParticle;

        Texture2D texture;
        Texture2D textureEffect;
        Texture2D textureBackground;
        Texture2D textureWins;
        Texture2D textureFalse;

        SpriteFont scoreFont;

        MouseState mouse;
        MouseState oldState;
        KeyboardState kb;

        SoundEffect soundMusic;
        SoundEffect soundEffect;
        SoundEffect soundEffectYes;
        SoundEffect soundEffectColum;
        SoundEffectInstance soundM;

        Color color = new Color();
        Color timeColor = new Color();

        byte[,] fields;
        byte colum;
        int positionObj;
        int pressMouse;
        int[] mainCoordinate = new int[4];
        int cout = 0;
        int dyColor = 0;
        int numberObjList = 0;
        double timeLevelColor = 0;

        public int scoreNumber = 0;

        int coordinateStart;
        int stringX;
        int xCoutInt = 0;

        byte scaleObj;
        byte speedObj;

        double timeSpan;
        double timeVal;
        double time;
        double timeLevel;

        bool isButton = false;
        bool isColor = true;
        public bool isTimeLevel = false;

        public bool isControlLogic = true;

        Random rand;
        Rectangle r;
        Vector2 positionScore;
        NameObj nameObjEnter;
        Game1 game;
        Rectangle rectBack;

        public ObjBonusCards(byte _colum, byte _scaleObj, byte _speedObj, Game1 _game, int _coordinateStart, double _timeVal, double _timeStart, int _enemySpan)
        {
            game = _game;
            timeVal = _timeVal;
            timeLevel = _timeStart;
            coordinateStart = _coordinateStart;

            rand = new Random();
            timeSpan = _enemySpan;

            fields = new byte[_colum, colum];

            colum = _colum;

            positionObj = scaleObj = _scaleObj;

            speedObj = _speedObj;

            r = new Rectangle(50, 50, 50, 50);
            rectBack = new Rectangle(0, 0, game.Width, game.Height);

            objType = new List<ObjBonus>();
        }

        public Texture2D TextureWinsInFalse(bool isWins)
        {
            if (isWins)
                return textureWins;
            else
                return textureFalse;
        }

        public void ContentLoad(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("ppp");

            textureEffect = Content.Load<Texture2D>("Light");

            int m = rand.Next(0, 2);

            scoreFont = Content.Load<SpriteFont>("fontScore");
            textureBackground = Content.Load<Texture2D>("ObjBonusGameContent//background");
            soundMusic = Content.Load<SoundEffect>("ObjBonusGameContent//" + "COMBAT" + m.ToString());
            soundEffect = Content.Load<SoundEffect>("ObjBonusGameContent//COIN");
            soundEffectYes = Content.Load<SoundEffect>("ObjBonusGameContent//FLUB");
            soundEffectColum = Content.Load<SoundEffect>("ObjBonusGameContent//JUMP_ON");
            textureWins = Content.Load<Texture2D>("ObjBonusGameContent//textWins");
            textureFalse = Content.Load<Texture2D>("ObjBonusGameContent//textFalse");
            soundM = soundMusic.Play(0.1f);
            game.IsMouseVisible = true;

            Reshuffle();

            managerParticle = new ManagerParticle(game, 100, 170, 350, 5, 6, 8, 25, 0.008f, -0.02f, textureEffect);

            manager = new BonusManagerGame(textureEffect, game, colum, objType, this, coordinateStart, timeVal);

            positionScore = new Vector2(coordinateStart + (positionObj + 10) * colum / 2 - 50,
                                        game.Height - (positionObj + 10) * colum - 100);
        }

        public void UpdateCards(GameTime gameTime)
        {
            if (!isTimeLevel)
                manager.UpdateBonusGame(gameTime, objType, colum, speedObj, texture, managerParticle, soundEffectColum);

            timeLevel -= gameTime.ElapsedGameTime.TotalSeconds;
            timeLevelColor += gameTime.ElapsedGameTime.TotalSeconds;

            kb = Keyboard.GetState();

            if (kb.IsKeyDown(Keys.L) & isControlLogic)
                isControlLogic = false;

            if (timeLevel <= 0)
            {
                soundM.Stop();
                isTimeLevel = true;
            }
            else
            {
                if (isControlLogic)
                {
                    mouse = Mouse.GetState();

                    Rectangle rectMouse = new Rectangle(mouse.X, mouse.Y, 3, 3);

                    #region Color

                    for (int countList = 0; countList < objType.Count; countList++)
                    {
                        if (objType[countList].Position.Intersects(rectMouse) && mouse.LeftButton == ButtonState.Pressed &&
                            oldState.LeftButton == ButtonState.Released && !isButton)
                        {
                            objType[countList].ColorObject = Color.Red;

                            pressMouse = countList;

                            isButton = true;
                        }
                    }

                    #endregion

                    #region Logic

                    if (pressMouse > -1)
                    {
                        WriteColor();

                        for (int i = 0; i < mainCoordinate.Length; i++)
                        {
                            if (mainCoordinate[i] != -1 && isButton)
                            {
                                if (objType[mainCoordinate[i]].Position.Intersects(rectMouse))
                                {
                                    if (mouse.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
                                    {
                                        ObjBonus obj = objType[pressMouse];
                                        ObjBonus obj2 = objType[mainCoordinate[i]];

                                        Rectangle[] rectObj = new Rectangle[2];

                                        rectObj[0] = obj2.Position;
                                        rectObj[1] = obj.Position;

                                        obj.Position = rectObj[0];
                                        obj2.Position = rectObj[1];

                                        objType[mainCoordinate[i]] = obj;
                                        objType[pressMouse] = obj2;

                                        isButton = false;
                                        objType[pressMouse].ColorObject = Color.White;

                                        pressMouse = -1;
                                        cout = 0;
                                        soundEffectYes.Play(0.05f);

                                        for (int j = 0; j < mainCoordinate.Length; j++)
                                        {
                                            if (mainCoordinate[j] > -1)
                                                objType[mainCoordinate[j]].ColorObject = Color.White;
                                        }

                                        break;
                                    }
                                }
                            }
                        }
                        for (int i = 0; i < objType.Count; i++)
                        {
                            if (objType[i].Position.Intersects(rectMouse) && mouse.LeftButton == ButtonState.Pressed &&
                            oldState.LeftButton == ButtonState.Released && isButton)
                            {
                                cout++;

                                if (cout > 1)
                                {
                                    soundEffect.Play(0.2f);

                                    isButton = false;

                                    cout = 0;
                                }
                            }
                            if (!isButton)
                            {
                                objType[i].ColorObject = Color.White;
                            }
                        }
                    }

                    #endregion

                    oldState = mouse;
                }
                else
                {
                    time += gameTime.ElapsedGameTime.TotalSeconds;

                    if (time > timeSpan)
                    {
                        ObjBonus[] baseObjectX = new ObjBonus[colum];

                        int countObjType = 0;

                        for (int i = 0; i < colum; i++)
                        {
                            for (int j = 0; j < colum; j++)
                            {
                                baseObjectX[j] = objType[countObjType];
                                countObjType++;
                            }

                            if (InitializeBaseObject(baseObjectX, false) && ComparerObject(nameObjEnter, stringX + 1, false, false))
                            {
                                ObjBonus obj = objType[numberObjList];
                                ObjBonus obj2 = objType[numberObjList - colum];

                                Rectangle rect = obj.Position;
                                Rectangle rect2 = obj2.Position;

                                obj.Position = rect2;
                                obj2.Position = rect;

                                objType[numberObjList - colum] = obj;
                                objType[numberObjList] = obj2;

                                break;
                            }
                            else
                                xCoutInt++;

                            if (InitializeBaseObject(baseObjectX, false) && ComparerObject(nameObjEnter, stringX - 1, true, false))
                            {
                                ObjBonus obj = objType[numberObjList];
                                ObjBonus obj2 = objType[numberObjList + colum];

                                Rectangle rect = obj.Position;
                                Rectangle rect2 = obj2.Position;

                                obj.Position = rect2;
                                obj2.Position = rect;

                                objType[numberObjList + colum] = obj;
                                objType[numberObjList] = obj2;

                                break;
                            }
                            else
                                xCoutInt++;

                            for (int v = 0, verticalCout = stringX; v < colum; v++, verticalCout += colum)
                            {
                                baseObjectX[v] = objType[verticalCout];
                            }

                            if (InitializeBaseObject(baseObjectX, true) && ComparerObject(nameObjEnter, stringX + 1, false, true))
                            {
                                ObjBonus obj = objType[numberObjList];
                                ObjBonus obj2 = objType[numberObjList - 1];

                                Rectangle rect = obj.Position;
                                Rectangle rect2 = obj2.Position;

                                obj.Position = rect2;
                                obj2.Position = rect;

                                objType[numberObjList - 1] = obj;
                                objType[numberObjList] = obj2;

                                break;
                            }
                            else
                                xCoutInt++;

                            if (InitializeBaseObject(baseObjectX, true) && ComparerObject(nameObjEnter, stringX - 1, true, true))
                            {
                                ObjBonus obj = objType[numberObjList];
                                ObjBonus obj2 = objType[numberObjList + 1];

                                Rectangle rect = obj.Position;
                                Rectangle rect2 = obj2.Position;

                                obj.Position = rect2;
                                obj2.Position = rect;

                                objType[numberObjList + 1] = obj;
                                objType[numberObjList] = obj2;

                                break;
                            }
                            else
                                xCoutInt++;

                            stringX++;
                        }

                        if (xCoutInt >= (colum * 4))
                        {
                            xCoutInt = 0;
                            Reshuffle();
                        }
                        else
                            xCoutInt = 0;

                        xCoutInt = 0;

                        stringX = 0;

                        time = 0;
                    }
                }
            }
        }

        private void WriteColor()
        {
            for (int i = 0; i < objType.Count; i++)
            {
                if (objType[i].ColorObject == Color.Red)
                {
                    mainCoordinate = VaribleCount();

                    for (int j = 0; j < mainCoordinate.Length; j++)
                    {
                        if (mainCoordinate[j] > -1)
                        {
                            if (isColor)
                                dyColor += 5;
                            else
                                dyColor -= 5;

                            if (dyColor >= 250)
                                isColor = false;

                            else if (dyColor <= 80)
                                isColor = true;

                            color.G = 255;
                            color.B = 255;
                            color.A = (byte)dyColor;
                            objType[mainCoordinate[j]].ColorObject = color;
                        }
                    }
                }
            }
        }

        private bool ComparerObject(NameObj _name, int _stringX, bool _isUp, bool isVertical)
        {
            int xCout = (colum) * _stringX;

            if (_stringX >= colum)
                return false;
            else if (_stringX < 0)
                return false;

            int columComparerUp = _isUp ? colum : -colum;
            int x = _isUp ? 1 : -1;

            if (!isVertical)
            {
                for (int i = xCout; i < xCout + colum - 1; i++)
                {
                    if (_name == objType[i].NameType && objType[i + columComparerUp].NameType != _name)
                    {
                        numberObjList = i;
                        return true;
                    }
                }
            }
            else
            {
                xCout = _stringX;

                for (int i = _stringX; i < objType.Count; i += colum)
                {
                    if (_name == objType[i].NameType && objType[i + x].NameType != _name && isVertical)
                    {
                        numberObjList = i;
                        return true;
                    }
                }
            }

            return false;
        }

        private bool ComparerObjectVarible(NameObj _name, int _stringX, bool _isUp, bool isVertical)
        {
            int xCout = (colum) * _stringX;

            if (_stringX >= colum)
                return false;
            else if (_stringX < 0)
                return false;

            int columComparerUp = _isUp ? colum : -colum;
            int x = _isUp ? 1 : -1;

            int xVar = 0;

            if (!isVertical)
            {
                for (int i = xCout; i < xCout + colum; i++)
                {
                    if (_name == objType[i].NameType)
                        xVar++;

                    if (xVar > 1)
                        return true;
                }
            }
            else
            {
                xCout = _stringX;

                for (int i = _stringX; i < objType.Count; i += colum)
                {
                    if (_name == objType[i].NameType)
                    {
                        xVar++;

                        if (xVar > 1)
                            return true;
                    }
                }
            }

            return false;
        }

        private bool InitializeBaseObject(ObjBonus[] list, bool _isVertical)
        {
            NameObj[] nameObj = new NameObj[list.Length];

            int cout = 0;

            for (int i = 0; i < list.Length; i++)
            {
                nameObj[i] = list[i].NameType;
            }

            for (int i = 0; i < list.Length; i++)
            {
                for (int j = 0; j < list.Length; j++)
                {
                    if (nameObj[i] == list[j].NameType)
                    {
                        cout++;
                    }

                    if (cout > 1 && ComparerObject(nameObj[i], stringX + 1, false, _isVertical))
                    {
                        nameObjEnter = nameObj[i];
                        return true;
                    }
                    else if (cout > 1 && ComparerObject(nameObj[i], stringX - 1, true, _isVertical))
                    {
                        nameObjEnter = nameObj[i];
                        return true;
                    }
                }
                cout = 0;
            }
            return false;
        }

        private int[] VaribleCount()
        {
            int[] position = new int[4];

            position[0] = pressMouse <= colum ? - 1 : pressMouse - colum;
            position[1] = pressMouse == 0 ? -1 : pressMouse - 1;
            position[2] = pressMouse == objType.Count - 1 ? -1 : pressMouse + 1;
            position[3] = pressMouse > objType.Count - 1 - colum ? - 1 : pressMouse + colum;

            int[] coutColumRight = new int[colum];
            int[] coutColumLeft = new int[colum];

            for (int i = 1; i < colum + 1; i++)
            {
                coutColumRight[i - 1] = colum * i - 1;
                coutColumLeft[i - 1] = colum * i;
            }

            for (int j = 0; j < colum; j++)
            {
                if (coutColumRight[j] == pressMouse)
                    position[2] = -1;

                if (coutColumLeft[j] == pressMouse)
                    position[1] = -1;
            }

            int varibleString = -1;
            int varibleStringVertical = -1;

            for (int i = 1; i < colum + 1; i++)
            {
                if (pressMouse > -1 && pressMouse < colum * i)
                {
                    varibleString = i - 1;
                    break;
                }
            }

            int v = varibleStringVertical = pressMouse;

            for (int i = 0; i < colum; i++)
            {
                if (v >= colum)
                {
                    v -= colum;
                    if (v < colum)
                    {
                        varibleStringVertical = v;
                        break;
                    }
                }
            }

            NameObj nameObjVarible = objType[pressMouse].NameType;

            if (!ComparerObjectVarible(nameObjVarible, varibleString - 1, true, false))
                position[0] = -1;
            if (!ComparerObjectVarible(nameObjVarible, varibleString + 1, false, false))
                position[3] = -1;
            if (!ComparerObjectVarible(nameObjVarible, varibleStringVertical + 1, true, true))
                position[2] = -1;
            if (!ComparerObjectVarible(nameObjVarible, varibleStringVertical - 1, false, true))
                position[1] = -1;

            return position;
        }

        public void Reshuffle()
        {
            int x = coordinateStart;
            int y = 0;

            objType = new List<ObjBonus>();

            for (int i = 0; i < colum; i++)
            {
                for (int j = 0; j < colum; j++)
                {
                    int rNumberType = rand.Next(1, 8);

                    objType.Add(new ObjBonus(rNumberType, new Rectangle(x, y, scaleObj, scaleObj), texture, speedObj, scaleObj));

                    x += positionObj + 10;
                }
                x = coordinateStart;
                y += positionObj + 10;
            }
        }

        public void DrawParticle(SpriteBatch sBatch)
        {
            managerParticle.DrawManager(sBatch, 0);
        }

        public void DrawFon(SpriteBatch sBatch)
        {
            sBatch.Draw(textureBackground, rectBack, Color.Teal);
        }

        public void DrawBonusCards(SpriteBatch spriteBatch)
        {
            foreach (ObjBonus item in objType)
            {
                item.DrawObj(spriteBatch);
            }

            timeColor.R = (byte)timeLevelColor;
            timeColor.A = 255;

            spriteBatch.DrawString(scoreFont, "Score : " + scoreNumber.ToString(), positionScore, Color.White);
            spriteBatch.DrawString(scoreFont, "TIME  " + timeLevel.ToString("0:##"), new Vector2(game.Width / 2 - 50, game.Height / 2), timeColor);
        }
    }
}
