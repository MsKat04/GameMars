using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameMars.Resource
{
    public class Bullets: AdditionalSprite
    {
        private float _timer;
        public Bullets(Texture2D texture2D) : base(texture2D)
        {
        }

        public override void Update(GameTime gameTime, List<AdditionalSprite> sprite)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > LifeSpan)
                IsRemoved = true;

            Position += Direction * LinearVelocity;
            base.Update(gameTime, sprite);
        }
    }
}
