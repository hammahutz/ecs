using System;
using ecs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Game1 : Game
{
    SpriteFont _font;
    int frameCount = 0;
    double elapsedTime = 0;
    int fps = 0;

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
        base.Initialize();

        ComponentTypes.RegisterComponents();

        var arche = archetypeManager.Query<Position, Velocity>();

        for (int i = 0; i < Global.MaxEntities; i++)
        {
            arche.AddEntity(_entityHandler);
        }

        archetypeManager
            .Query<Position, Velocity>()
            .ForEach(
                (entity, position, velocity) =>
                {
                    position.X[entity] = Random.Shared.Next(0, 800);
                    position.Y[entity] = Random.Shared.Next(0, 600);
                    velocity.X[entity] = Random.Shared.Next(-100, 100);
                    velocity.Y[entity] = Random.Shared.Next(-100, 100);
                }
            );
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _font = Content.Load<SpriteFont>("Arial");
    }

    protected override void Update(GameTime gameTime)
    {
        if (
            GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
            || Keyboard.GetState().IsKeyDown(Keys.Escape)
        )
            Exit();

        archetypeManager
            .Query<Position, Velocity>()
            .ForEach(
                (entity, position, velocity) =>
                {
                    position.X[entity] +=
                        velocity.X[entity] * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    position.Y[entity] +=
                        velocity.Y[entity] * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            );

        double deltaSeconds = gameTime.ElapsedGameTime.TotalSeconds;
        elapsedTime += deltaSeconds;
        frameCount++;

        if (elapsedTime >= 1.0)
        {
            fps = frameCount;
            frameCount = 0;
            elapsedTime -= 1.0;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _spriteBatch.DrawString(_font, $"FPS: {fps}", new Vector2(10, 10), Color.Green);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
