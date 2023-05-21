using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace GameMars.Resource
{
    public class AdditionalSprite : ICloneable
    {
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

        public AdditionalSprite(Texture2D texture)
        {
            _texture = texture;
            Origin = new Vector2(_texture.Height - 90, _texture.Width-80);
        }

        public Rectangle Rectangle//для столкновений
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        public virtual void Update(GameTime gameTime, List<AdditionalSprite> sprites)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null ,Color.White, _rotation, Origin, 1, SpriteEffects.None, 0);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
