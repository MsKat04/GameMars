using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMars.Resource
{
    public class Player: AdditionalSprite
    {
        public float Speed = 3;
        public Input Input;
        public new Vector2 Position;

        public Bullets Bullets;

        public Player(Texture2D texture):base (texture)
        {
            Position = new Vector2(100, 100);
        }

        public override void Update(GameTime gameTime, List<AdditionalSprite> sprites)
        {
            Move(sprites);
            base.Update(gameTime, sprites);
        }

        public void Move(List<AdditionalSprite> sprites)
        {
            _previousKey = _currentKey;
            _currentKey = Keyboard.GetState();

            if (Input == null) return;

            if (_currentKey.IsKeyDown(Input.Left))
            {
                Position.X -= Speed;
            }
            if (_currentKey.IsKeyDown(Input.Right))
            {
                Position.X += Speed;
            }
            if (_currentKey.IsKeyDown(Input.Up))
            {
                Position.Y -= 5;
            }
            if (_currentKey.IsKeyDown(Input.Down))
            {
                Position.Y += Speed;
            }
            if (_currentKey.IsKeyDown(Input.Bullet) && _previousKey.IsKeyUp(Input.Bullet))
            {
                Direction = new Vector2((float)Math.Cos(_rotation), (float)Math.Sin(_rotation));
                AddBullet(sprites);
            }
        }

        public void AddBullet(List<AdditionalSprite> sprites)
        {
            var bullet = Bullets.Clone() as Bullets;
            bullet.Direction = this.Direction;
            bullet.Position = this.Position;
            bullet.LinearVelocity = this.LinearVelocity;
            bullet.LifeSpan = 2f;
            bullet.Parent = this;

            sprites.Add(bullet);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }
    }
}
