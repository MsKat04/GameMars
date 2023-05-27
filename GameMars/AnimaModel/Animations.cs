using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace GameMars
{
    public class Animations
    {
        private AnimationModel _animation;

        private float _timer;
        public float Layer { get; set; }

        public Vector2 Origin { get; set; }
        public Vector2 Position { get; set; }

        public float Rotation { get; set; }
        public float Scale { get; set; }

        public Animations(AnimationModel animation)
        {
            _animation = animation;
            Scale = 1f;
        }

        public AnimationModel CurrentAnimation
        {
            get
            {
                return _animation;
            }
        }

        public void Play(AnimationModel animation)
        {
            if (_animation == animation)
                return;

            _animation = animation;
            _animation.CurrentFrame = 0;
            _timer = 0;
        }

        public void Stop()
        {
            _timer = 0f;
            _animation.CurrentFrame = 0;
        }

        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > _animation.FrameSpeed)
            {
                _timer = 0f;
                _animation.CurrentFrame++;

                if (_animation.CurrentFrame >= _animation.FrameCount)
                    _animation.CurrentFrame = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
         _animation.Texture,
         Position,
         new Rectangle(
           _animation.CurrentFrame * _animation.FrameWidth,
           0,
           _animation.FrameWidth,
           _animation.FrameHeight
           ),
         Color.White,
         Rotation,
         Origin,
         Scale,
         SpriteEffects.None,
         Layer
         );
        }

        public object Clone()
        {
            var animationManager = this.MemberwiseClone() as Animations;

            animationManager._animation = animationManager._animation.Clone() as AnimationModel;

            return animationManager;
        }
    }
}
