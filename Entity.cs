using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TheVarusTM;

public abstract class Entity
{
    public Vector2 Position;
    public Vector2 Velocity;
    public Vector2 Target;
    public bool Alive;
    public float Radius;

    public void SetTarget(Vector2 target, float speed)
    {
        Target = target;
        var direction = Vector2.Normalize(Target - Position);
        Velocity = direction * speed;
    }

    public bool IsCollision(Entity entity)
    {
        var distance = Vector2.Distance(Velocity, entity.Position);
        return distance < (Radius + entity.Radius);
    }

    public void Bounce(float width, float height)
    {
        if (Position.X < Radius)
        {
            Velocity.X = -Velocity.X;
            Position.X = Radius;
        }
        if (Position.Y < Radius)
        {
            Velocity.Y = -Velocity.Y;
            Position.Y = Radius;
        }
        if (Position.X > width - Radius)
        {
            Velocity.X = -Velocity.X;
            Position.X = width - Radius;
        }
        if (Position.Y > height - Radius)
        {
            Velocity.Y = -Velocity.Y;
            Position.Y = height - Radius;
        }
    }

    public float DistanceToTarget()
    {
        return Vector2.Distance(Position, Target);
    }
}
