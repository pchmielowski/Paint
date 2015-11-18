using System.Drawing;
using System.Drawing.Drawing2D;

namespace Paint
{
  public class BrushManager
  {
    public Pen outlinePen_ { get; set; }
    public Brush fillBrush_ { get; set; }
    private ToolArgs args;

    public BrushManager(ToolArgs args) // TODO Brush -> Style
    {
      this.args = args;
      switch (args.settings.DrawMode)
      {
      case DrawMode.Outline:
        outlinePen_ = new Pen(GetBrush(false), args.settings.Width);
        outlinePen_.DashStyle = args.settings.LineStyle;
        Brush TRANSPARENT_BRUSH = new SolidBrush(Color.FromArgb(0, 0, 0, 255));
        fillBrush_ = TRANSPARENT_BRUSH;
        break;

      case DrawMode.Filled:
        fillBrush_ = GetBrush(false);
        Pen TRANSPARENT_PEN = new Pen(Color.FromArgb(0, 0, 0, 255));
        outlinePen_ = TRANSPARENT_PEN;
        break;

      case DrawMode.Mixed:
        fillBrush_ = GetBrush(false);
        outlinePen_ = new Pen(GetBrush(true), args.settings.Width);
        outlinePen_.DashStyle = args.settings.LineStyle;
        break;

      case DrawMode.MixedWithSolidOutline:
        fillBrush_ = GetBrush(false);
        outlinePen_ = new Pen(args.settings.SecondaryColor, args.settings.Width);
        outlinePen_.DashStyle = args.settings.LineStyle;
        break;
      }
    }

    private Brush GetBrush(bool inverseColors)
    {
      Color c1, c2;

      if (inverseColors)
      {
        c1 = args.settings.SecondaryColor;
        c2 = args.settings.PrimaryColor;
      }
      else
      {
        c1 = args.settings.PrimaryColor;
        c2 = args.settings.SecondaryColor;
      }

      Brush brush = null;
      switch (args.settings.BrushType)
      {
      case BrushType.SolidBrush:
        brush = new SolidBrush(c1);
        break;

      case BrushType.TextureBrush:
        brush = new TextureBrush(args.settings.TextureBrushImage);
        break;

      case BrushType.GradiantBrush:
        int w = args.settings.Width;
        Rectangle tempRect = new Rectangle(0, 0, args.bitmap.Width, args.bitmap.Height);
        brush = new LinearGradientBrush(tempRect,
            c1, c2, args.settings.GradiantStyle);
        break;

      case BrushType.HatchBrush:
        brush = new HatchBrush(args.settings.HatchStyle, c1, c2);
        break;
      }

      return brush;
    }

    public void Update(Rectangle rect)
    {
      if (!(fillBrush_ is LinearGradientBrush))
        return;

      if ((rect.Width == 0) || (rect.Height == 0))
        return;

      RectangleF oldRect = ((LinearGradientBrush)fillBrush_).Rectangle;
      float scaleH = rect.Height / oldRect.Height;
      float scaleW = rect.Width / oldRect.Width;

      // TODO: here is a bug
      ((LinearGradientBrush)fillBrush_).ScaleTransform(scaleW, scaleH);
    }
  }
}
