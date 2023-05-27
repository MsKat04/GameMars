using GameMars.Resource;
using GameMars.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace GameMars
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private List<AdditionalSprite> _sprites;
        private Camera _camera;
        private Texture2D _playerTexture;
        private Texture2D _backgroundTexture;
        private Vector2 _playerPosition;
        public static Random Random;


        public static int ScreenHeight;
        public static int ScreenWidth;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            ScreenHeight = _graphics.PreferredBackBufferHeight;
            ScreenWidth = _graphics.PreferredBackBufferWidth;
        }

        protected override void Initialize()
        {
            Random = new Random();
            ScreenHeight = _graphics.PreferredBackBufferHeight;
            ScreenWidth = _graphics.PreferredBackBufferWidth;

            //_graphics.IsFullScreen= true;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            var texture = Content.Load<Texture2D>("demon");
            var animation = new Dictionary<string, AnimationModel>()
            {
                { "playerRun", new AnimationModel(Content.Load<Texture2D>("Player/playerRun"),8) },
                { "playerRunLeft", new AnimationModel(Content.Load<Texture2D>("Player/playerRunLeft"),8) },
            };

            _camera = new Camera();
            _backgroundTexture = Content.Load<Texture2D>("flag");
            _playerTexture = Content.Load<Texture2D>("demon");

            _sprites = new List<AdditionalSprite>()
            {
                new Player(animation)
                {
                    Position = new Vector2(100, 100),
                    Input= new Input()
                    {
                        Up = Keys.W,
                        Down = Keys.S,
                        Left = Keys.A,
                        Right = Keys.D,
                        Bullet = Keys.E,
                    },
                    Bullets = new Bullets(Content.Load<Texture2D>("star")),
                },
                new Enemy(texture)
                {
                    Position = new Vector2((ScreenWidth/2) - (texture.Width/2), ScreenHeight - texture.Height),
                    Colors = Color.Black,
                },//???
            };
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (var sprite in _sprites.ToArray())
                sprite.Update(gameTime, _sprites);

            Addi();
            base.Update(gameTime);
        }

        private void Addi()
        {
            for (int i = 0; i < _sprites.Count; i++)
            {
                var sprite = _sprites[i];
                if (sprite.IsRemoved)
                {
                    _sprites.RemoveAt(i);
                    i--;
                }
                if (sprite is Enemy)//??
                    sprite.IsRemoved = false;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.MistyRose);
            _spriteBatch.Begin();
            foreach (var sprite in _sprites)
                sprite.Draw(_spriteBatch);
            _spriteBatch.End();
            _spriteBatch.Begin();

            _spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.White);

            _spriteBatch.End();

            _spriteBatch.Begin(transformMatrix: _camera.Transform);

            _spriteBatch.Draw(_playerTexture, _playerPosition, Color.Green);
            _spriteBatch.Draw(_playerTexture, new Vector2(0, 0), Color.Red);

            _spriteBatch.End();
            base.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}