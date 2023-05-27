using GameMars.Resource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameMars.Sprites
{
    public class Enemy : Helper
    {
        public bool HasDied = false;
        public Color Colors = Color.White;
        private float _timer;

        public float ShootingTimer = 1.75f;

        public Enemy(Texture2D texture)
          : base(texture)
        {
            Speed = 2f;
        }
        //override
        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer >= ShootingTimer)
            {
                Shoot(-5f);
                _timer = 0;
            }

            Position += new Vector2(-Speed, 0);
            if (Position.X < -_texture.Width)
                IsRemoved = true;
        }
        public override void OnCollide(AdditionalSprite sprite)
        {
            if (sprite is Player player && !player.IsDead)
                IsRemoved = true;

            if (sprite is Bullets bullets && bullets.Parent is Player)
            {
                Health--;

                if (Health <= 0)
                    IsRemoved = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Colors);
        }
    }
}
