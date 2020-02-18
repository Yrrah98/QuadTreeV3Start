using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace NQ2
{
    class SomeXEntity : Entity
    {
        private Vector2 position;

        private int speed;

        private int dist;

        private Vector2 startPos;

        /// <summary>
        /// CONSTRUCTOR: For some Entity class
        /// </summary>
        public SomeXEntity(Rectangle rect)
        {
            this.ObjRect = rect;

            dist = 50;

            position = new Vector2(this.ObjRect.X, this.ObjRect.Y);

            startPos = new Vector2(this.ObjRect.X, this.ObjRect.Y);

            speed = 1;
        }

        public override void Update(GameTime gameTime)
        {
            position.X += 0.5f * speed;

            position.Y += 0.5f * speed;

            if (position.X >= startPos.X + dist && position.Y >= startPos.Y + dist || position.X <= startPos.X - dist && position.Y <= startPos.Y - dist)
                speed *= -1;

            this.ObjRect = new Rectangle((int)position.X, (int)position.Y, 32, 32);

            base.Update(gameTime);
        }
    }
}
