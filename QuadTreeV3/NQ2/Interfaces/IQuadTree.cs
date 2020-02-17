using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NQ2.Interfaces
{
    interface IQuadTree<T> where T : IHaveRect
    {


        void Add(IEntity e);

        void Divide();

        int getIndex();

        void Clear();

        SpriteBatch Draw(SpriteBatch spriteBatch);

        Rectangle RootRect { get; }

        IList<IEntity> _Entities { get; }

        Texture2D RectText { get; }



    }
}
