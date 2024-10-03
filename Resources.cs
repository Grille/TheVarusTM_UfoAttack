using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace TheVarusTM;

public static class Resources
{
    public static readonly Bitmap UFO;
    public static readonly Bitmap Boom;
    public static readonly Bitmap Decal;
    public static readonly Icon Icon;

    static Resources()
    {
        var asm = Assembly.GetExecutingAssembly();
        UFO = LoadBitmap(asm, "UFO");
        Boom = LoadBitmap(asm, "Boom");
        Decal = LoadBitmap(asm, "Decal");
        Icon = LoadIcon(asm, "UFO");
    }

    static Stream Open(Assembly asm, string name)
    {
        return asm.GetManifestResourceStream($"TheVarusTM.Assets.{name}");
    }

    static Bitmap LoadBitmap(Assembly asm, string name)
    {
        using var stream = Open(asm, name + ".png");
        return new Bitmap(stream);
    }

    static Icon LoadIcon(Assembly asm, string name)
    {
        using var stream = Open(asm, name + ".ico");
        return new Icon(stream);
    }
}
