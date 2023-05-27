using GameMars.Resource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMars.Sprites
{
    public class Helper: AdditionalSprite, ICloneable
    {
        public Helper(Dictionary<string, AnimationModel> animation):base(animation)
        {
            _animations = animation;
            _animation = new Animations(_animations.First().Value);
        }
        public int Health { get; set; }

        public Bullets Bullets { get; set; }

        public new float Speed;

        public Helper(Texture2D texture) : base(texture)
        {
        }

        protected void Shoot(float speed)
        {
            var bullet = Bullets.Clone() as Bullets;
            bullet.Position = this.Position;
            bullet.Layer = 0.1f;
            bullet.LifeSpan = 5f;
            bullet.Velocity = new Vector2(speed, 0f);
            bullet.Parent = this;

            Children.Add(bullet);
        }

        public virtual void OnCollide(AdditionalSprite sprite)
        {
            throw new NotImplementedException();
        }
    }
}
