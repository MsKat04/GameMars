using GameMars.AnimaPers;
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
        public float Speed = 1f;
        protected Animations _animation;
        protected AnimationModel model;
        protected Dictionary<string, AnimationModel> _animations;

        public Vector2 Velocity;

        protected float _rotation;
        protected Texture2D _texture;

        protected KeyboardState _currentKey;
        protected KeyboardState _previousKey;

        public Vector2 Position;
        public Vector2 Origin;
        public Vector2 Direction;

        public float RotationVelocity = 3f;
        public float LinearVelocity = 4f;
        public AdditionalSprite Parent;

        public float LifeSpan = 0f;
        public bool IsRemoved = false;

        #endregion

        public Rectangle Rectangle//для столкновений
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        public AdditionalSprite(Texture2D texture)
        {
            _texture = texture;
            Origin = new Vector2(_texture.Width - 25, _texture.Height  - 20);
        }

        public AdditionalSprite(Dictionary<string, AnimationModel> animation)
        {
            _animations = animation;
            _animation = new Animations(_animations.First().Value);
        }

        public virtual void Update(GameTime gameTime, List<AdditionalSprite> sprites) { }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null ,Color.OrangeRed, _rotation, Origin, 1, SpriteEffects.None, 0);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
