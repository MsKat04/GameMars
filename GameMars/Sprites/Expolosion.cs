using GameMars.Resource;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMars.Sprites
{
    public class Explosion : AdditionalSprite, ICloneable
    {
        private float _timer = 0f;

        public Explosion(Dictionary<string, AnimationModel> animations) : base(animations)
        {

        }

        public override void Update(GameTime gameTime, List<AdditionalSprite> sprites)
        {
            _animation.Update(gameTime);

            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > _animation.CurrentAnimation.FrameCount * _animation.CurrentAnimation.FrameSpeed)
                IsRemoved = true;
        }
    }
}
