using GameMars.Resource;
using GameMars.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMars.Binder
{
    public class EnemyBinder
    {
        private float _timer;
        private List<Texture2D> _textures;

        public Bullets Bullets { get; set; }

        public EnemyBinder(ContentManager content)
        {
            _textures = new List<Texture2D>()
            {
                content.Load<Texture2D>("Ships/Enemy_1"),
                content.Load<Texture2D>("Ships/Enemy_2"),
            };

        }

        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        public Enemy GetEnemy()
        {
            var texture = _textures[Game1.Random.Next(0, _textures.Count)];

            return new Enemy(texture)
            {
                Colors = Color.Red,
            };
        }
    }
}
