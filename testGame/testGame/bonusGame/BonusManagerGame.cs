using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;

using Microsoft.Xna.Framework.Audio;

namespace testGame
{
    class BonusManagerGame
    {
        Random rand;

        double elapsedTime = 0;

        ObjBonusCards objCard;

        int countList = 0;

        int stringCount = 0;

        int stringVerticalCount = 0;
        int stringLench = 0;

        bool isVaribleNull = true;
        bool isVertical = false;
        bool isReshuffle = false;

        double speedValTime;

        Rectangle[] rectParticle;

        Game1 game;

        public BonusManagerGame(Texture2D textureEffect, Game1 _game, byte _colum, List<ObjBonus> li, ObjBonusCards _objCard,
            int _coord, double _speedValTime)
        {
            game = _game;

            speedValTime = _speedValTime;

            int ciout = 50 * _colum;

            int y = game.Height - ciout; 

            rectParticle = new Rectangle[_colum + _colum];

            for (int i = 0; i < _colum; i++)
            {
                rectParticle[i] = new Rectangle(_coord, y - 30, _colum * 60, 15);

                y += 60;
            }

            int x = 0;
            y = game.Height - ciout;

            for (int i = _colum; i < _colum + _colum; i++)
            {
                rectParticle[i] = new Rectangle(x + _coord + 15, y - 35, 15, _colum * 50);
                x += 60;
            }

            objCard = _objCard;

            //Reshuffle(_colum, objCard);

            rand = new Random();
        }

        private bool Reshuffle(int colum, ObjBonusCards _objCard)
        {
            ObjBonus[] baseObject = new ObjBonus[colum];

            List<ObjBonus> listElement = new List<ObjBonus>();

            bool flag = false;

            for (int boolFlag = 0; flag == false; boolFlag++)
            {
                listElement = _objCard.ListObject;

                int x = 0;
                int y = 0;

                for (int i = 0; i < colum; i++)
                {
                    NameObj[] nameObj = new NameObj[colum];

                    for (int j = i * colum, p = 0; j < colum; j++, p++)
                    {
                        baseObject[p] = listElement[j];
                        nameObj[p] = listElement[j].NameType;
                    }

                    int comparer = 0;

                    for (int baseCount = 0; baseCount < colum; baseCount++)
                    {
                        for (int n = 0; n < colum; n++)
                        {
                            if (baseObject[baseCount].NameType == nameObj[n])
                            {
                                comparer++;
                            }
                        }

                        if (comparer >= 3)
                        {
                            x++;
                            break;
                        }

                        comparer = 0;
                    }
                }

                for (int yColect = 0; yColect < colum; yColect++)
                {
                    NameObj[] nameObject = new NameObj[colum];

                    for (int b = yColect, d = 0; d < colum; b += colum, d++)
                    {
                        baseObject[d] = listElement[b];
                        nameObject[d] = listElement[b].NameType;
                    }

                    int comparer = 0;

                    for (int a = 0; a < nameObject.Length; a++)
                    {
                        for (int s = 0; s < colum; s++)
                        {
                            if (baseObject[a].NameType == nameObject[s])
                            {
                                comparer++;
                            }
                        }

                        if (comparer >= 3)
                        {
                            y++;
                            break;
                        }

                        comparer = 0;
                    }
                }

                if (x == 0 && y == 0)
                {
                    flag = true;
                    return flag;
                }
            }

            return flag;
        }

        public void UpdateBonusGame(GameTime gameTime, List<ObjBonus> list, byte _colum, byte speed, Texture2D t, ManagerParticle managerP, SoundEffect sound)
        {
            List<int> delListCount = new List<int>();

            if (stringLench == _colum)
            {
                stringVerticalCount = 0;
                stringLench = 0;
            }

            for (int i = 0; i < list.Count; i++)
            {
                Rectangle rect = list[i].Position;

                rect.Offset(0, speed);

                if ((rect.Bottom < game.Height) && !Colision(list, rect, i) && game.Height > 150)
                    list[i].Position = rect;
            }

            elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

            if (countList > (_colum * _colum - 1))
            {
                countList = 0;
                stringCount = 0;
            }

            managerP.UpdateOnly(gameTime, rectParticle[isVertical ? stringLench + _colum : stringCount]);

            if (!isReshuffle)
            {
                if (elapsedTime > speedValTime)
                {
                    if (isVertical == false)
                    {
                        initializeBonusObject(list, _colum, delListCount);

                        if (isVaribleNull)
                        {
                            for (int l = 0; l < delListCount.Count; l++)
                            {
                                RemoveListObject(list, delListCount[l], _colum, stringCount, t);

                                objCard.scoreNumber += 7 * delListCount.Count;
                                sound.Play(0.01f);
                            }

                            managerP.AliveCollection();

                            stringVerticalCount++;
                        }
                        else
                        {
                            if ((stringLench >= _colum - 1) && stringVerticalCount == 0)
                            {
                                isVertical = true;
                            }
                        }
                    }
                    else
                    {
                        VerticalRemoving(list, _colum, t, stringLench, speed, managerP, sound);

                        if (stringVerticalCount == 0 && stringLench + 2 == _colum + 1)
                        {
                            isVertical = false;
                            stringCount = -1;
                        }

                        if (stringLench + 1 >= _colum)
                            stringVerticalCount = 0;
                    }

                    stringLench++;

                    if (!isVertical)
                        stringCount++;

                    elapsedTime = 0;
                }
            }
        }

        private void VerticalRemoving(List<ObjBonus> list, byte colum, Texture2D rexture, int stringLench, byte speed, ManagerParticle mP, SoundEffect s)
        {
            int[] baseCountObject = new int[colum];

            for (int i = 0; i < baseCountObject.Length; i++)
            {
                baseCountObject[i] = -1;
            }

            List<ObjBonus> baseObject = new List<ObjBonus>();

            int thisColum = 0;

            NameObj[] nameObjectBase = new NameObj[colum];

            for (int i = 0; i < colum; i++)
            {
                thisColum += i == 0 ? 0 : colum;

                baseObject.Add(list[stringLench + thisColum]);

                nameObjectBase[i] = baseObject[i].NameType;
            }

            List<int> delObject = DelObjectCount(baseObject, nameObjectBase, list);

            ObjBonus[] newBaseObject = new ObjBonus[baseObject.Count];

            if (delObject != null)
            {
                mP.AliveCollection();

                stringVerticalCount++;

                for (int i = 0; i < delObject.Count; i++)
                {
                    Rectangle r = baseObject[delObject[i]].Position;
                    r.Offset(0, -game.Height + 100);
                    newBaseObject[i] = new ObjBonus(rand.Next(1, colum + 1), r, rexture, speed, (byte)r.Width);

                    s.Play(0.01f);
                    objCard.scoreNumber += 6 * delObject.Count;
                }

                for (int i = delObject.Count - 1; i > -1; i--)
                {
                    baseObject.RemoveAt(delObject[i]);
                }

                int y = 0;

                for (int i = delObject.Count; i < colum; i++)
                {
                    newBaseObject[i] = baseObject[y];
                    y++;
                }

                for (int i = stringLench, j = 0; i < list.Count; i += colum, j++)
                {
                    list[i] = newBaseObject[j];
                }
            }
        }

        private static List<int> DelObjectCount(List<ObjBonus> baseObject, NameObj[] nameObjectBase, List<ObjBonus> list)
        {
            int coutObject = 0;

            int comparer = 0;

            List<int> l = new List<int>();

            for (int i = 0; i < nameObjectBase.Length; i++)
            {
                for (int j = 0; j < baseObject.Count; j++)
                {
                    if ((nameObjectBase[i] == baseObject[j].NameType))
                    {
                        l.Add(j);
                        comparer++;
                        //coutObject++;
                    }
                    
                    coutObject++;
                }
                if (comparer >= 3)
                {
                    return l;
                }

                comparer = 0;
                l = new List<int>();
            }
            return null;
        }

        private void RemoveListObject(List<ObjBonus> list, int delListCount, int colum, int stringCount, Texture2D t)
        {
            int huhu = delListCount;

            if (huhu >= colum)
            {
                huhu -= colum * stringCount;
            }

            ObjBonus[] positionObject = new ObjBonus[colum];

            for (int i = huhu, j = 0; i < list.Count; i += colum, j++)
            {
                positionObject[j] = list[i];
            }

            ObjBonus[] newPositionObject = new ObjBonus[colum];

            for (int g = 1, h = 0; g < stringCount + 1; g++, h++)
            {
                newPositionObject[g] = positionObject[h];
            }

            int r = rand.Next(1, 8);

            Rectangle rect = list[delListCount].Position;

            rect.Offset(0, -game.Height + 100);

            newPositionObject[0] = (new ObjBonus(r, rect, t, positionObject[0].Speed, (byte)rect.Width));

            for (int f = stringCount + 1; f < colum; f++)
            {
                newPositionObject[f] = positionObject[f];
            }

            for (int i = huhu, j = 0; i < list.Count; i += colum, j++)
            {
                list[i] = newPositionObject[j];
            }
        }

        private void initializeBonusObject(List<ObjBonus> list, byte _colum, List<int> delListCount)
        {
            ObjBonus[] objBonus = new ObjBonus[_colum];

            for (int x = 0; x < _colum; x++)
            {
                objBonus[x] = list[countList];

                countList++;
            }

            int[] q = DelBonusObject(objBonus, countList < _colum ? 0 : countList - _colum);

            if (q != null)
            {
                isVaribleNull = true;

                for (int a = 0; a < q.Length; a++)
                {
                    if (q[a] > -1)
                        delListCount.Add(q[a]);
                }
            }
            else
                isVaribleNull = false;
        }

        private int[] DelBonusObject(ObjBonus[] obj, int countList)
        {
            byte num = 0;

            NameObj[] nameObj = new NameObj[obj.Length];

            for (int m = 0; m < obj.Length; m++)
            {
                nameObj[m] = obj[m].NameType;    
            }

            NameObj[] baseNameObj = new NameObj[obj.Length];

            for (int s = 0; s < obj.Length; s++)
            {
                baseNameObj[s] = obj[s].NameType;
            }

            for (int b = 0; b < obj.Length; b++)
            {
                int[] numberDel = new int[obj.Length];

                for (int v = 0; v < obj.Length; v++)
                {
                    numberDel[v] = -1;
                }

                for (int n = 0; n < obj.Length; n++)
                {
                    if (baseNameObj[b] == nameObj[n])
                    {
                        num++;
                        numberDel[n] = n + countList;
                    }
                }

                if (num >= 3)
                {
                    return numberDel;
                }
                else
                    num = 0;
            }

            return null;
        }

        private bool Colision(List<ObjBonus> list, Rectangle rect, int colum)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] != list[colum])
                {
                    if (list[i].Position.Intersects(rect))
                        return true;
                }
            }

            return false;
        }
    }
}
