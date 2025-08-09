using System;
using ecs;
using ecs.Debug;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Game1 : Game
{
    SpriteFont _font;
    int frameCount = 0;
    double elapsedTime = 0;
    int fps = 0;

    string output = string.Empty;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    EntityHandler _entityHandler = new EntityHandler();

    ArchetypeManager archetypeManager = new ArchetypeManager();

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        ComponentTypes.RegisterComponents();

        archetypeManager.Create<Position, Velocity, Sprite>(_entityHandler, Global.MaxEntities);

        archetypeManager.QueryWith<Position, Velocity>(
            (entity, position, velocity) =>
            {
                position.X[entity] = Random.Shared.Next(0, 800);
                position.Y[entity] = Random.Shared.Next(0, 600);
                velocity.X[entity] = Random.Shared.Next(-100, 100);
                velocity.Y[entity] = Random.Shared.Next(-100, 100);
            }
        );

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _font = Content.Load<SpriteFont>("Arial");
        archetypeManager.QueryWith<Sprite>(
            (entity, sprite) =>
            {
                sprite.Texture[entity] = Content.Load<Texture2D>("pip");
            }
        );
    }

    protected override void Update(GameTime gameTime)
    {
        if (
            GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
            || Keyboard.GetState().IsKeyDown(Keys.Escape)
        )
            Exit();

        archetypeManager.QueryWith<Position, Velocity>(
            (entity, position, velocity) =>
            {
                position.X[entity] +=
                    velocity.X[entity] * (float)gameTime.ElapsedGameTime.TotalSeconds;
                position.Y[entity] +=
                    velocity.Y[entity] * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        );

        Stats.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        archetypeManager.QueryWith<Position, Sprite>(
            (entity, position, sprite) =>
            {
                _spriteBatch.Draw(
                    sprite.Texture[entity],
                    new Vector2(position.X[entity], position.Y[entity]),
                    Color.White
                );
            }
        );

        Stats.Draw(_spriteBatch, _font);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
