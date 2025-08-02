using ecs;

[Component]
struct Position
{
    public float[] X { get; set; }
    public float[] Y { get; set; }
}

[Component]
struct Velocity
{
    public float[] X { get; set; }
    public float[] Y { get; set; }
}

[Component]
struct Acceleration
{
    public float[] X { get; set; }
    public float[] Y { get; set; }
}

[Component]
struct Rotation
{
    public float[] Angle { get; set; }
}

[Component]
struct Scale
{
    public float[] Value { get; set; }
}

[Component]
struct Colors
{
    public float[] R { get; set; }
    public float[] G { get; set; }
    public float[] B { get; set; }
    public float[] A { get; set; }
}

