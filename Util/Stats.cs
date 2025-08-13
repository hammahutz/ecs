using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ecs.Debug;

public static class Stats
{
    // public static int EntityCount { get; private set; } = 0;
    // public static int ComponentCount { get; private set; } = 0;
    /// public static void Reset()
    // {
    //     EntityCount = 0;
    //     ComponentCount = 0;
    //     SystemCount = 0;
    // }
    //
    // public static void IncrementEntityCount() => EntityCount++;
    // public static void IncrementComponentCount() => ComponentCount++;
    // public static void IncrementSystemCount() => SystemCount++;
    // public static int SystemCount { get; private set; } = 0;
    //
    public const int ROLLING_SIZE = 60;
    private static Queue<float> _rollingFPS = new Queue<float>();

    public static float FPS { get; set; } = 0.0f;
    public static float MinFPS { get; set; } = 0.0f;
    public static float MaxFPS { get; set; } = 0.0f;
    public static float AverageFPS { get; set; } = 0.0f;
    public static bool IsRunningSlow { get; set; } = false;
    public static int NbUpdateCalled { get; set; } = 0;
    public static int NbDrawCalled { get; set; } = 0;

    private static string _output = string.Empty;

    public static void Update(GameTime gameTime)
    {
        NbUpdateCalled++;
        FPS = 1.0f / (float)gameTime.ElapsedGameTime.TotalSeconds;
        _rollingFPS.Enqueue(FPS);

        if (_rollingFPS.Count > ROLLING_SIZE)
        {
            _rollingFPS.Dequeue();
            var sum = 0.0f;
            MaxFPS = float.MinValue;
            MinFPS = float.MaxValue;

            foreach (var fps in _rollingFPS)
            {
                sum += fps;
                if (fps > MaxFPS)
                    MaxFPS = fps;
                if (fps < MinFPS)
                    MinFPS = fps;
            }
            AverageFPS = sum / _rollingFPS.Count;
        }
        else
        {
            AverageFPS = FPS;
            MinFPS = FPS;
            MaxFPS = FPS;
        }

        _output =
            $"FPS: {FPS:F2}\n"
            + $"Average FPS: {AverageFPS:F2}\n"
            + $"Is Running Slow: {IsRunningSlow}\n"
            + $"Update Called: {NbUpdateCalled}\n"
            + $"Draw Called: {NbDrawCalled}\n"
            + $"Diff: {NbUpdateCalled - NbDrawCalled}\n"
            + $"Entities: {Global.MaxEntities.ToString("N0", new System.Globalization.CultureInfo("sv-SE")).Replace('\u00A0', ' ')}";
    }

    public static void Draw(SpriteBatch spriteBatch, SpriteFont font)
    {
        NbDrawCalled++;

        spriteBatch.DrawString(font, _output, new Vector2(10, 10), Color.Green);
    }
}
