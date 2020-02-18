using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NQ2.Interfaces;

namespace NQ2
{
    class CustomRect : IAABB
    {
        // DECLARE int called _top to store top position of entity
        private float _top;
        // DECLARE int called _bottom
        private float _bottom;
        // DECLARE int called _left
        private float _left;
        // DECLARE int called _right
        private float _right;
        // DECLARE Vector2 called _location to store location
        private Vector2 _location;
        // DECLARE Texture2D called _texture to store reference to texture 
        private Texture2D _texture;

        /// <summary>
        /// CONSTRUCTOR for class CustomRect
        /// </summary>
        /// <param name="pPos"></param>
        /// <param name="pTex"></param>
        public CustomRect(Vector2 pPos, Texture2D pTex)
        {
            // SET corresponding variables to parameters passed into constructor
            _location = pPos;

            _texture = pTex;

            _top = pPos.Y;

            _bottom = pPos.Y += _texture.Height;

            _right = pPos.X += _texture.Width;

            _left = pPos.X;



        }

        #region PROPERTIES
        public int Left { get; }

        public int Right { get; }

        public int Bottom { get; }

        public int Top { get; }

        public Vector2 Position { get; set; }

        public Texture2D Tex { get; }

        #endregion
    }
}
