using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GameMars.Sprites;

namespace GameMars.Resource
{
    public class Bullets : AdditionalSprite, ICloneable

    {
        private float _timer;
        private float LifeClone { get; set; }
        public new Vector2 Velocity;
        public Explosion Explosion;

        public Bullets(Texture2D texture) : base(texture) { }

        public override void Update(GameTime gameTime, List<AdditionalSprite> sprite)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > LifeSpan)
                IsRemoved = true;
            if (Rectangle.Bottom >= Game1.ScreenHeight)
                IsRemoved = true;

            Position += Direction * LinearVelocity;

            base.Update(gameTime, sprite);
        }

        public void OnConflict(AdditionalSprite sprite)
        {
            if (sprite is Enemy && this.Parent is Enemy)
                return;
            if(sprite is Enemy && this.Parent is Player)
            {
                return;
                AddExplosion();
            }
            if(sprite is Player && this.Parent is Player) 
                return;
            if(sprite is Player && this.Parent is Enemy)
            {
                return;
                AddExplosion();
            }
            if (sprite is Player && ((Player)sprite).IsDead)
                return;
        }

        private void AddExplosion()
        {
            if (Explosion == null)
                return;

            var explosion = Explosion.Clone() as Explosion;
            explosion.Position = this.Position;

            Children.Add(explosion);
        }
    }
}