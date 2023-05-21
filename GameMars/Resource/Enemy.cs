using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameMars.Resource
{
    public class Enemy: AdditionalSprite
    {
        public bool HasDied = false;
        public Color Colors = Color.White;
        public Input Input;

        public Enemy(Texture2D texture) : base(texture){}

        public override void Update(GameTime gameTime, List<AdditionalSprite> sprites)
        {
            foreach (var sprite in sprites)
            {
                if (sprite == this)
                    continue;

                if (sprite.Rectangle.Intersects(Rectangle))
                {
                    HasDied = true;
                    if (HasDied)
                        IsRemoved = true;
                }
            }

            Position += Velocity;
            Position.X = MathHelper.Clamp(Position.X, 0, Game1.ScreenWidth - Rectangle.Width);
            Velocity = Vector2.Zero;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Colors);
        }
    }
}
