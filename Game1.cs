using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ecs;

public class Game1 : Game
{
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
        // TODO: Add your initialization logic here

        base.Initialize();
        ComponentTypes.RegisterComponents();
        Console.WriteLine(ComponentTypes.GetComponentBit<Position>());
        Console.WriteLine(ComponentTypes.GetComponentBit<Rotation>());

        var arche = archetypeManager.Query<Position, Velocity>();

        for (int i = 0; i < 10; i++)
        {
            arche.AddEntity(_entityHandler);
        }

        var arche2 = archetypeManager.Query<Position>();
        for (int i = 0; i < 10; i++)
        {
            arche2.AddEntity(_entityHandler);
        }
        var one = arche.AddEntity(_entityHandler);
        var two = arche.AddEntity(_entityHandler);

        for (int i = 0; i < 10; i++)
        {
            arche.AddEntity(_entityHandler);
        }

        Position onePosition = new Position();

        arche.Component2.X[one.Id] = 100f;
        arche.Component2.Y[one.Id] = 100f;
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (
            GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
            || Keyboard.GetState().IsKeyDown(Keys.Escape)
        )
            Exit();

        // TODO: Add your update logic here
        //
        archetypeManager
            .Query<Position, Velocity>()
            .ForEach(
                (i, position, velocity) =>
                {
                    position.X[i] += velocity.X[i] * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    position.Y[i] += velocity.Y[i] * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    Console.WriteLine($"Entity {i} Position: {position.X[i]}, {position.Y[i]}");
                }
            );

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
