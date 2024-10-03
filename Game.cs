using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;

namespace TheVarusTM;

public class Game
{
    public Random Rnd { get; }
    public EntityBuffer<Saucer> Saucers { get; }
    public EntityBuffer<Projectile> SaucerProjectiles { get; }
    public EntityBuffer<Projectile> PlayerProjectiles { get; }

    public Queue<Vector2> DecalQueue { get; }
    public Queue<Vector2> EffectQueue { get; }

    public Bitmap DecalBuffer { get; }

    public int Width { get; }
    public int Height { get; }



    public Game(int width, int height)
    {
        Width = width;
        Height = height;

        Rnd = new Random();
        Saucers = new EntityBuffer<Saucer>();
        for (int i = 0; i < 4; i++)
        {
            Saucers.Add(new Saucer(GetRndPosition(), GetRndPosition()));
        }
        SaucerProjectiles = new EntityBuffer<Projectile>();
        PlayerProjectiles = new EntityBuffer<Projectile>();
        DecalQueue = new Queue<Vector2>();
        EffectQueue = new Queue<Vector2>();
        DecalBuffer = new Bitmap(width, height, PixelFormat.Format32bppArgb);
    }

    public Vector2 GetRndPosition()
    {
        float x = Rnd.Next(Width);
        float y = Rnd.Next(Height);
        return new Vector2(x, y);
    }

    public void Tick()
    {
        for (int i = 0; i < Saucers.Count; i++)
        {
            var entity = Saucers[i];
            entity.Bounce(Width, Height);
        }

        Saucers.UpdatePositions();
        SaucerProjectiles.UpdatePositions();
        PlayerProjectiles.UpdatePositions();

        foreach (var entity in SaucerProjectiles)
        {
            if (entity.DistanceToTarget()< 8f)
            {
                entity.Alive = false;
                EffectQueue.Enqueue(entity.Target);
                DecalQueue.Enqueue(entity.Target);
            }
        }

        var offset = new Vector2(64, 64);
        for (int i = 0; i < Saucers.Count; i++)
        {
            var entity = Saucers[i];
            bool fire = Rnd.NextDouble() > 0.98f;
            if (fire)
            {
                var target = GetRndPosition();
                var projectile = new Projectile(entity.Position, target);
                SaucerProjectiles.Add(projectile);
            }
            bool newTarget = Rnd.NextDouble() > 0.999f;
            if (newTarget)
            {
                entity.SetTarget(GetRndPosition(), 1f);
            }
        }

        Saucers.SwapBuffers();
        SaucerProjectiles.SwapBuffers();
        PlayerProjectiles.SwapBuffers();
    }

    public void Draw(Graphics g)
    {
        if (DecalQueue.Count > 0)
        {
            UpdateDecalBuffer();
        }

        g.DrawImage(DecalBuffer, Point.Empty);

        DrawProjectiles(g, SaucerProjectiles);
        DrawProjectiles(g, PlayerProjectiles);

        var ufo = Resources.UFO;
        float ufoSize = 128;
        float ufoHalfSize = ufoSize / 2f;
        for (int i = 0; i < Saucers.Count; i++)
        {
            var entity = Saucers[i];
            float x = entity.Position.X - ufoHalfSize;
            float y = entity.Position.Y - ufoHalfSize;
            float w = ufoSize;
            float h = ufoSize;
            g.DrawImage(ufo, x, y, w, h);
        }

        while (EffectQueue.Count > 0)
        {
            var pos = EffectQueue.Dequeue();

            float x = pos.X - 64;
            float y = pos.Y - 64;
            float w =128;
            float h = 128;
            g.DrawImage(Resources.Boom, x, y, w, h);
        }
    }

    public void DrawProjectiles(Graphics g, EntityBuffer<Projectile> projectiles)
    {
        for (int i = 0; i < projectiles.Count; i++)
        {
            var entity = projectiles[i];
            FillCircle(g, Brushes.Lime, entity.Position.X, entity.Position.Y, entity.Radius);
            FillCircle(g, Brushes.White, entity.Position.X, entity.Position.Y, entity.Radius * 0.5f);
        }
    }

    static void FillCircle(Graphics g, Brush brush, float posx, float posy, float radius)
    {
        var x = posx - radius;
        var y = posy - radius;
        var w = radius * 2;
        var h = radius * 2;
        g.FillEllipse(brush, x, y, w, h);
    }

    public void UpdateDecalBuffer()
    {
        using var g = Graphics.FromImage(DecalBuffer);

        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

        while (DecalQueue.Count > 0)
        {
            var pos = DecalQueue.Dequeue();

            float x = pos.X-32;
            float y = pos.Y-32;
            float w = 64;
            float h = 64;
            g.DrawImage(Resources.Decal, x, y, w, h);
        }
    }

}
