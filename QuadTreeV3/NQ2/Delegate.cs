using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NQ2.Interfaces;

namespace NQ2
{
    public delegate void ChangeEntityNode<T>(IEntity e, IQuadTree<T> currQuad) where T : IHaveRect;
}
