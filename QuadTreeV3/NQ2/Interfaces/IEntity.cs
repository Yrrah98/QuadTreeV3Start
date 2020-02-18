using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace NQ2.Interfaces
{
    public interface IEntity
    {

        Texture2D Texture { get; }

        void SetTxtr(Texture2D txtr);
    }
}
