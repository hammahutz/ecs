using ecs;

struct PositionX
{
    float X;
}

[Component]
struct Position
{
    public Position() { }

    public float[] X { get; } = new float[Global.MaxEntities];
    public float[] Y { get; } = new float[Global.MaxEntities];
}

[Component]
struct Velocity
{
    public Velocity() { }

    public float[] X { get; } = new float[Global.MaxEntities];
    public float[] Y { get; } = new float[Global.MaxEntities];
}

[Component]
struct Acceleration
{
    public Acceleration() { }

    public float[] X { get; } = new float[Global.MaxEntities];
    public float[] Y { get; } = new float[Global.MaxEntities];
}

[Component]
struct Rotation
{
    public Rotation() { }

    public float[] Angle { get; } = new float[Global.MaxEntities];
}

[Component]
struct Scale
{
    public Scale() { }

    public float[] Value { get; } = new float[Global.MaxEntities];
}

[Component]
struct Colors
{
    public Colors() { }

    public float[] R { get; } = new float[Global.MaxEntities];
    public float[] G { get; } = new float[Global.MaxEntities];
    public float[] B { get; } = new float[Global.MaxEntities];
    public float[] A { get; } = new float[Global.MaxEntities];
}
