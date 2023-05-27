using GameMars.Resource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMars.Sprites
{
    public class Player : Helper
    {
        public Player(Dictionary<string, AnimationModel> animation) : base(animation)
        {
        }

        public bool IsDead
        {
            get
            {
                return Health <= 0;
            }
        }

        public Input Input;
        protected Vector2 _position;
        public new Vector2 Position
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
                Shoot(Speed * 2);
                //_shootTimer = 0f;
            }
        }

        public void AddBullet(List<AdditionalSprite> sprites)
        {
            var bullet = Bullets.Clone() as Bullets;
            bullet.Direction = Direction;
            bullet.Position = Position;
            bullet.LinearVelocity = LinearVelocity;
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

        public override void OnCollide(AdditionalSprite sprite)
        {
            if(IsDead) return;

            if (sprite is Bullets && ((Bullets)sprite).Parent is Enemy)
                Health--;
            if (sprite is Enemy)
                Health -= 3;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(IsDead) return;
            if (_texture != null)
                spriteBatch.Draw(_texture, Position, Color.White);
            else if (_animation != null)
                _animation.Draw(spriteBatch);
            else throw new Exception();
        }
    }
}
