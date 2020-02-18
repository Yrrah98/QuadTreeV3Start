using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NQ2.Interfaces
{
    public interface IAABB
    {

        int Left { get; }

        int Right { get; }

        int Bottom { get; }

        int Top { get; }

        Vector2 Position { get; set; }

        Texture2D Tex { get; }
    }
}
