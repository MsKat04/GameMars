using GameMars.AnimaPers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace GameMars.Resource
{
    public class Player: AdditionalSprite
    {
        public Input Input;
        protected Vector2 _position;
        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                if (_animation != null)
                    _animation.Position = _position;

            }
        }

        public Bullets Bullets;

        public Player(Dictionary<string, AnimationModel> animation) : base(animation)
        {
        }
        public Player(Texture2D texture) : base(texture)
        {
            Position = new Vector2(100, 100);
        }

        public override void Update(GameTime gameTime, List<AdditionalSprite> sprites)
        {
            Move(sprites);
            SetAnimations();

            _animation.Update(gameTime);
            Position += Velocity;
            Velocity = Vector2.Zero;

            base.Update(gameTime, sprites);
        }

        public void Move(List<AdditionalSprite> sprites)
        {
            _previousKey = _currentKey;
            _currentKey = Keyboard.GetState();

            if (Input == null) return;

            if (_currentKey.IsKeyDown(Input.Left))
                Velocity.X -= Speed;

            if (_currentKey.IsKeyDown(Input.Right))
                Velocity.X += Speed;

            if (_currentKey.IsKeyDown(Input.Up))
                Velocity.Y -= 5;

            if (_currentKey.IsKeyDown(Input.Down))
                Velocity.Y += Speed;

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

        protected virtual void SetAnimations()
        {
            if (Velocity.X > 0)
                _animation.Play(_animations["playerRun"]);
            else if (Velocity.X < 0)
                _animation.Play(_animations["playerRunLeft"]);
            else _animation.Stop();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_texture != null)
                spriteBatch.Draw(_texture, Position, Color.White);
            else if (_animation != null)
                _animation.Draw(spriteBatch);
            else throw new Exception();
        }
    }
}
