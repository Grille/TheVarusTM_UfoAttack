using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TheVarusTM;

public class Saucer : Entity
{
    public Saucer(Vector2 position, Vector2 target)
    {
        Position = position;
        SetTarget(target, 1f);
        Radius = 64f;
        Alive = true;
    }
}
