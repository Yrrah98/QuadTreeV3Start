using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NQ2.Interfaces
{
    interface IAABB
    {

        void SetTxtr(Texture2D txtr);

        Vector2 Size { get; set; }

        Vector2 Position { get; set; }
    }
}
