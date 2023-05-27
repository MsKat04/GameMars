using GameMars.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameMars.Resource
{
    public class AdditionalSprite : ICloneable
    {
        #region Fields
        protected Animations _animation;
        protected AnimationModel model;
        protected Dictionary<string, AnimationModel> _animations;

        public AdditionalSprite Parent;
        public List<AdditionalSprite> Children { get; set; }

        protected Texture2D _texture;

        protected KeyboardState _currentKey;
        protected KeyboardState _previousKey;

        public Vector2 Velocity;
        public Vector2 Position;
        public Vector2 Origin;
        public Vector2 Direction;

        protected float _rotation;
        protected float _layer { get; set; }

        public float RotationVelocity = 3f;
        public float LinearVelocity = 4f;
        public float LifeSpan = 0f;
        public float Speed = 1f;
        public bool IsRemoved = false;

        #endregion

        public Rectangle Rectangle//для столкновений
        {
            get
            {
                if (_texture != null)
                {
                    return new Rectangle((int)Position.X - (int)Origin.X, (int)Position.Y - (int)Origin.Y, _texture.Width, _texture.Height);
                }

                throw new Exception();
            }
        }
        public float Layer
        {
            get { return _layer; }
            set
            {
                _layer = value;

                if (model != null)
                    model.Layer = _layer;
            }
        }
        public AdditionalSprite(Texture2D texture)
        {
            _texture = texture;
            Origin = new Vector2(_texture.Width - 25, _texture.Height - 20);
        }

        public AdditionalSprite(Dictionary<string, AnimationModel> animation)
        {
            _animations = animation;
            _animation = new Animations(_animations.First().Value);
        }

        public virtual void Update(GameTime gameTime, List<AdditionalSprite> sprites) { }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Color.OrangeRed, _rotation, Origin, 1, SpriteEffects.None, 0);

        }

        public object Clone()
        {
            var sprite = this.MemberwiseClone() as AdditionalSprite;

            return sprite;
        }
    }
}
