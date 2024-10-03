using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TheVarusTM;

public class Projectile : Entity
{
    public Projectile(Vector2 position, Vector2 target)
    {
        Position = position;
        SetTarget(target,2f);
        Radius = 4f;
        Alive = true;
    }
}
