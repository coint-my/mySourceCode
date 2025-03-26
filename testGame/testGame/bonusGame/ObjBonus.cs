using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace testGame
{
    public enum NameObj
    {
        obj1,
        obj2,
        obj3,
        obj4,
        obj5,
        obj6,
        obj7,
        obj8
    }

    class ObjBonus
    {
        NameObj nameType;

        public NameObj NameType
        {
            get { return nameType; }
        }

        Texture2D texture;

        Rectangle rectPosition;

        Rectangle r;

        byte width = 52;
        byte height = 52;
        byte scaleObj;

        byte numberObject;

        public byte NumberObject
        {
            get { return numberObject; }
            set { numberObject = value; }
        }

        int speed;

        Color color = Color.White;

        public Color ColorObject
        {
            get { return color; }
            set { color = value; }
        }

        public int Speed
        {
            get { return speed; }
        }

        public Rectangle Position        
        {
            get { return rectPosition; }
            set { rectPosition = value; }
        }

        public ObjBonus(int _lenght, Rectangle _rectPosition, Texture2D _texture, int _speed, byte _scaleObj)
        {
            numberObject = (byte)_lenght;

            speed = _speed;

            texture = _texture;

            scaleObj = _scaleObj;

            r = new Rectangle();

            rectPosition = _rectPosition;

            nameType = NameT(_lenght);
        }

        private NameObj NameT(int point)
        {
            if (point == 1)
            {
                r = new Rectangle(width * 1, 0, scaleObj, scaleObj);
                return NameObj.obj1;
            }
            else if (point == 2)
            {
                r = new Rectangle(width * 8, height * 1, scaleObj, scaleObj);
                return NameObj.obj2;
            }
            else if (point == 3)
            {
                r = new Rectangle(width * 10, height * 1, scaleObj, scaleObj);
                return NameObj.obj3;
            }
            else if (point == 4)
            {
                r = new Rectangle(width * 9, height * 2, scaleObj, scaleObj);
                return NameObj.obj4;
            }
            else if (point == 5)
            {
                r = new Rectangle(width * 17, height * 2, scaleObj, scaleObj);
                return NameObj.obj5;
            }
            else if (point == 6)
            {
                r = new Rectangle(width * 10, height * 3, scaleObj, scaleObj);
                return NameObj.obj6;
            }
            else if (point == 7)
            {
                r = new Rectangle(width * 14, height * 3, scaleObj, scaleObj);
                return NameObj.obj7;
            }
            else if (point == 8)
            {
                r = new Rectangle(width, height * 6, scaleObj, scaleObj);
                return NameObj.obj8;
            }

            return 0;
        }

        public void DrawObj(SpriteBatch sBatch)
        {
            sBatch.Draw(texture, rectPosition, r, color);
        }

        public override string ToString()
        {
            return string.Format("{0}", nameType);
        }
    }
}
