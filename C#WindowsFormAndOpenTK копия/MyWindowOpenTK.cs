using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormAndOpenTK
{
    public class MyWindowOpenTK : GameWindow
    {
        public MyWindowOpenTK(int _wid, int _hei, string _title) : base(_wid, _hei, GraphicsMode.Default, _title)
        { 

        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState input = Keyboard.GetState();

            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }
        }
    }
}
