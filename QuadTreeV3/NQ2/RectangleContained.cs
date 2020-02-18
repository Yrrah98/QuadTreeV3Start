using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using NQ2.Interfaces;

namespace NQ2
{
    public class RectangleContained
    {

        public static bool Contains(IAABB r1, IAABB r2)
        {
            if (r1.Left >= r2.Left && r1.Right < r2.Right && r1.Top >= r2.Top && r1.Bottom <= r2.Bottom)
                return true;
            else
                return false;
        }

    }
}
