using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TheVarusTM;

public partial class MainForm : Form
{
    public Game? Game;

    public MainForm()
    {
        InitializeComponent();

        Icon = Resources.Icon;
    }

    protected override void OnResizeEnd(EventArgs e)
    {
        base.OnResizeEnd(e);

        Game = new Game(Width, Height);
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        Game = new Game(Width, Height);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        var g = e.Graphics;

        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;
        g.SmoothingMode = SmoothingMode.None;
        g.PixelOffsetMode = PixelOffsetMode.Half;
        g.InterpolationMode = InterpolationMode.NearestNeighbor;

        if (Game != null)
        {
            Game.Draw(g);
        }

        g.DrawString("The Varus™ UFO Attack", Font, Brushes.Lime, Point.Empty);
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        if (Game != null)
        {
            Game.Tick();
            if (Game.Saucers.Count < 1)
            {
                Close();
            }
        }
        Invalidate();
    }
}
