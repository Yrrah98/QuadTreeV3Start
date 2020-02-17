using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace NQ2
{
    class SomeXEntity : EntitY
    {
        private Vector2 position;

        private int speed;

        /// <summary>
        /// CONSTRUCTOR: For some Entity class
        /// </summary>
        public SomeXEntity()
        {
            position = new Vector2(this.ObjRect.X, this.ObjRect.Y);

            speed = 1;
        }

        public override void Update(GameTime gameTime)
        {
            position.X += speed;

            position.Y += speed;

            //this.ObjRect = new Rectangle((int)position.X, (int)position.Y, 32, 32);

            base.Update(gameTime);
        }
    }
}
