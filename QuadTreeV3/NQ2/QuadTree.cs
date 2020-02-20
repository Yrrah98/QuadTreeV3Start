using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NQ2.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NQ2
{
    class QuadTree<T> : IUpdateableComponent, IQuadTree<T> where T : IHaveRect
    {
        // DECLARE IList<IEntity> called _nodeEntities
        private IList<IEntity> _nodeEntities;
        // DECLARE IList<IEntity> called _toMove
        private IEntity _toMove;
        // DECLARE Texture2D called _rectTex, to store texture for this node
        private Texture2D _rectTex;
        // DECLARE a const int called MAX_ENTITIES to hold a value which represents the maximum number of entities
        private const int MAX_ENTITIES = 8;
        // DECLARE a const in called MAX_LEVELS to hold a value which represents the maximum number of levels the quad can split into
        private const int MAX_LEVELS = 5;
        // DECLARE a Rectangle called _rootRect which will store the rectangle of each quad
        private Rectangle _rootRect;
        // DECLARE int called _level which will be used to hold the current level 
        private int _level;
        // DECLARE IList<IQuadTree<T>> called _quads
        private IList<IQuadTree<T>> _quads;

        // DECLARE 4 IQuadTree<T>, _northWest, _northEast, _southWest, _southEast
        private IQuadTree<T> _northWest;

        private IQuadTree<T> _northEast;

        private IQuadTree<T> _southWest;

        private IQuadTree<T> _SouthEast;

        private ChangeEntityNode<T> _changeEntityNode;


        /// <summary>
        /// CONSTRUCTOR for class QuadTree
        /// </summary>
        /// <param name="rect">the rectangle for this node</param>
        /// <param name="level">the level that this node is at</param>
        /// <param name="rectTex">the texture of this node </param>
        public QuadTree(Rectangle rect, int level, Texture2D rectTex)
        {
            // SET member variables to value of passed in parameters
            this._rootRect = rect;
            // _level = level
            _level = level;
            // _rectTex = rectTex
            _rectTex = rectTex;

        }

        public QuadTree(Rectangle rect, int level, Texture2D rectTex, ChangeEntityNode<T> changeEntityNode)
        {
            this._rootRect = rect;

            _level = level;

            _rectTex = rectTex;

            _changeEntityNode = changeEntityNode;
        }

        /// <summary>
        /// METHOD: Update, called once per frame, provides behaviour 
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            // IF list of entities is not null
            if (_nodeEntities != null)
            {
                // FOREACH IUpdateable in that list 
                foreach (IUpdateableComponent u in _nodeEntities)
                    // CALL update method, passing in gameTime param
                    u.Update(gameTime);

                // IF the maximum count of entities is exceeded, divide the node into 4
               

                for (int i = 0; i < _nodeEntities.Count - 1; i++)
                {
                    if (this.RootRect.Contains(((IHaveRect)_nodeEntities[i]).ObjRect) == false)
                    {

                        _toMove = _nodeEntities[i];

                        _changeEntityNode?.Invoke(_toMove, this);

                        _toMove = null;
                    }

                    
                }
                    
            }

            

            // IF _northWest, or any of the quad nodes are not null
            if (_northWest != null)
            {
                // THEN CAST to IUpdateableComponent and call Update method
                ((IUpdateableComponent)_northWest).Update(gameTime);
                ((IUpdateableComponent)_northEast).Update(gameTime);
                ((IUpdateableComponent)_southWest).Update(gameTime);
                ((IUpdateableComponent)_SouthEast).Update(gameTime);
            }
        }

        /// <summary>
        /// METHOD: Add, a method used to add an entity to the quad tree, entitiy is passed in as a parameter
        /// </summary>
        /// <param name="e">passed in entity to be stored</param>
        public void Add(IEntity e)
        {
            // IF the list of entities is null or has a count of zero
            if (_nodeEntities == null)
                // THEN reset/make a new list of entities 
                _nodeEntities = new List<IEntity>();

            // ADD the entity passed in to the entity list
            _nodeEntities.Add(e);

            if (_nodeEntities.Count >= MAX_ENTITIES)
            {
                // IF this level is less than or equal to the max level 
                if (this._level <= MAX_LEVELS)
                    // THEN CALL Divide method
                    Divide();
            }
        }

        public void ChangeEntityNode<C>(IEntity e, IQuadTree<T> currQuad) where C : IHaveRect
        {


                

            if (_northWest.RootRect.Contains(((IHaveRect)e).ObjRect))
            {
                // THEN add to the quad node 
                _northWest.Add(e);
                currQuad._Entities.Remove(e);

            }
            else if (_northEast.RootRect.Contains(((IHaveRect)e).ObjRect))
            {

                _northEast.Add(e);
                currQuad._Entities.Remove(e);
            }
            else if (_southWest.RootRect.Contains(((IHaveRect)e).ObjRect))
            {

                _southWest.Add(e);
                currQuad._Entities.Remove(e);

            }
            else if (_SouthEast.RootRect.Contains(((IHaveRect)e).ObjRect))
            {

                _SouthEast.Add(e);
                currQuad._Entities.Remove(e);
            }
            else
            {
                _nodeEntities.Add(e);
                currQuad._Entities.Remove(e);
            }

            currQuad._Entities.Remove(e);

        }

        /// <summary>
        /// METHOD: Divide, method which is used to create the divisions in the quad
        /// </summary>
        public void Divide()
        {
            // CREATE local variables used to store half width, height and the top and left of this quad
            int w = this.RootRect.Width / 2;
            int h = this.RootRect.Height / 2;
            int x = this.RootRect.Left;
            int y = this.RootRect.Top;


            // INCREMENT the level by 1 
            int nLevel = _level++;

            // IF the list of nodes is null
            if (_quads == null && this._level <= MAX_LEVELS)
            {
                // THEN instantiate a new list of IQuadTree<T>
                _quads = new List<IQuadTree<T>>();

                // INSTANTIATE the four nodes to new QuadTrees<T>, passing in a new Rectangle taking the half height and width as well as the position as parameters
                // then send in the current level and the texture of the quad
                _northWest = new QuadTree<T>(new Rectangle(x,y,w,h), nLevel, this.RectText, this.ChangeEntityNode<T>);
                // THEN add this node to the list of nodes, repeat x4
                _quads.Add(_northWest);
                _northEast = new QuadTree<T>(new Rectangle(x + w, y, w, h), nLevel, this.RectText, this.ChangeEntityNode<T>);
                _quads.Add(_northEast);
                _southWest = new QuadTree<T>(new Rectangle(x, y + h, w, h), nLevel, this.RectText, this.ChangeEntityNode<T>);
                _quads.Add(_southWest);
                _SouthEast = new QuadTree<T>(new Rectangle(x + w, y + h, w, h), nLevel, this.RectText, this.ChangeEntityNode<T>);
                _quads.Add(_SouthEast);
            }

            // FORLOOP through the entities based on the count 
            for (int i = 0; i < _nodeEntities.Count - 1; i++)
            {

                ///
                ///
                /// Need to run check to see if entity rectangle is completely contained within a node before adding it to it 
                ///
                /// Check if (entity)left is greater than (node)left, (e)right is less than (n)right 
                /// (e)top is grater than (n)top and (e)bottom is less than (n)bottom 
                ///
                /// If any part of this statement comes back negative, entity will remain in parent node
                ///

                // IF 
                if (_northWest.RootRect.Contains(((IHaveRect)_nodeEntities[i]).ObjRect))
                {
                    // THEN add to the quad node 
                    _northWest.Add(_nodeEntities[i]);
                    // THEN remove from the list of entities
                    _nodeEntities.RemoveAt(i);
                }
                else if (_northEast.RootRect.Contains(((IHaveRect)_nodeEntities[i]).ObjRect))
                {
                    _northEast.Add(_nodeEntities[i]);
                    _nodeEntities.RemoveAt(i);
                }
                else if (_southWest.RootRect.Contains(((IHaveRect)_nodeEntities[i]).ObjRect))
                {
                    
                    _southWest.Add(_nodeEntities[i]);
                    _nodeEntities.RemoveAt(i);
                }
                else if (_SouthEast.RootRect.Contains(((IHaveRect)_nodeEntities[i]).ObjRect))
                {
                    
                    _SouthEast.Add(_nodeEntities[i]);
                    _nodeEntities.RemoveAt(i);
                }
                
            }

        }

        /// <summary>
        /// METHOD: Clear the list of entities, will also check child nodes and clear them if necessary
        /// </summary>
        public void Clear()
        {
            _nodeEntities = null;
        }

        /// <summary>
        /// METHOD: To be decided if necessary in this quad 
        /// </summary>
        /// <returns></returns>
        public int getIndex()
        {
            int index = 0;

            return index;
        }

        /// <summary>
        /// METHOD: Draw, to be used to call the draw method of entities, or get the necessary data to draw and returns this 
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <returns></returns>
        public SpriteBatch Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(this.RectText, RootRect, Color.AntiqueWhite);
            if (_nodeEntities != null)
                foreach (IEntity e in _nodeEntities)
                    spriteBatch.Draw(e.Texture, ((IHaveRect)e).ObjRect, Color.AntiqueWhite);
            if(_quads != null)
                foreach (IQuadTree<T> q in _quads)
                    q.Draw(spriteBatch);

            


            return spriteBatch;
        }


        #region PROPERTIES

        public Rectangle RootRect
        {
            get { return _rootRect; }
        }

        public IList<IEntity> _Entities
        {
            get
            {
                IList<IEntity> all = new List<IEntity>();
                if(_nodeEntities != null)
                    ((List<IEntity>)all).AddRange(_nodeEntities);

                if (_northWest != null)
                    ((List<IEntity>)all).AddRange(_northWest._Entities);
                if (_northEast != null)
                    ((List<IEntity>)all).AddRange(_northEast._Entities);
                if (_southWest != null)
                    ((List<IEntity>)all).AddRange(_southWest._Entities);
                if (_SouthEast != null)
                    ((List<IEntity>)all).AddRange(_SouthEast._Entities);

                return all;
            }
            set { _nodeEntities = value; }
        }

        public Texture2D RectText
        {
            get { return _rectTex; }
        }

        #endregion
    }
}
