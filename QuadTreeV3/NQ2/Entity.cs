using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NQ2.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace NQ2
{
    abstract class EntitY : IEntity, IHaveRect, IUpdateableComponent
    {
        // DECLARE: Texture2D called _txtr
        private Texture2D _txtr;
        // DECLARE Rectangle called _objRect
        private Rectangle _objRect;

        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public EntitY()
        {

        }

        /// <summary>
        /// METHOD: Update called on every updateable object once every frame
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// METHOD: SetTxtr a method which is used to set the texture of an entity
        /// </summary>
        /// <param name="txtr">the texture for the entity</param>
        public virtual void SetTxtr(Texture2D txtr)
        {
            _txtr = txtr;
        }

        #region PROPERTIES
        // PROPERTY to provide access to the entities texture
        public Texture2D Texture
        {
            get { return _txtr; }
        }
        // PROPERTY to provide access to the entities rectangle 
        public Rectangle ObjRect
        {
            get { return _objRect; }
            set { _objRect = value; }
        }

        #endregion
    }
}
