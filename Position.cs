using ecs;

[Component]
struct Position : IComponent
{
    public Position() { }

    public float[] X { get; } = new float[Global.MaxEntities];
    public float[] Y { get; } = new float[Global.MaxEntities];
}

[Component]
struct Velocity : IComponent
{
    public Velocity() { }

    public float[] X { get; } = new float[Global.MaxEntities];
    public float[] Y { get; } = new float[Global.MaxEntities];
}

[Component]
struct Acceleration : IComponent
{
    public Acceleration() { }

    public float[] X { get; } = new float[Global.MaxEntities];
    public float[] Y { get; } = new float[Global.MaxEntities];
}

[Component]
struct Rotation : IComponent
{
    public Rotation() { }

    public float[] Angle { get; } = new float[Global.MaxEntities];
}

[Component]
struct Scale : IComponent
{
    public Scale() { }

    public float[] Value { get; } = new float[Global.MaxEntities];
}

[Component]
struct Colors : IComponent
{
    public Colors() { }

    public float[] R { get; } = new float[Global.MaxEntities];
    public float[] G { get; } = new float[Global.MaxEntities];
    public float[] B { get; } = new float[Global.MaxEntities];
    public float[] A { get; } = new float[Global.MaxEntities];
}
