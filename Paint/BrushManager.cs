using System.Drawing;
using System.Drawing.Drawing2D;
namespace Paint
{
  public interface IStyle
  {
    Pen outlinePen_ { get; set; }
    Brush fillBrush_ { get; set; }
    void Update(Rectangle rect);
    Brush getBrushStyle();
  }

  public abstract class StyleDecorator : IStyle
  {
    public Pen outlinePen_
    {
      get { return styleToDecorate.outlinePen_; }
      set { styleToDecorate.outlinePen_ = value; }
    }
    public Brush fillBrush_
    {
      get { return styleToDecorate.fillBrush_; }
      set { styleToDecorate.fillBrush_ = value; }
    }
    protected IStyle styleToDecorate;
    public abstract void Update(Rectangle rect);
    public Brush getBrushStyle() { return null; }
  }

  public class OutlineStyleDecorator : StyleDecorator
  {
    public OutlineStyleDecorator(IStyle std) { styleToDecorate = std; UpdateOutlinePen(); }
    public override void Update(Rectangle rect) { }
    private void UpdateOutlinePen()
    {
      styleToDecorate.outlinePen_ = new Pen(Color.DarkMagenta, 4);
      styleToDecorate.outlinePen_.DashStyle = DashStyle.Solid;
    }
  }

  public class FilledStyleDecorator : StyleDecorator
  {
    public FilledStyleDecorator(IStyle std) { styleToDecorate = std; UpdateBrush(); }
    public override void Update(Rectangle rect) { }
    private void UpdateBrush()
    {
      styleToDecorate.fillBrush_ = styleToDecorate.getBrushStyle();
    }
  }

  public class MyHatchStyle : IStyle
  {
    // uses args.settings.Width, LineStyle, PrimaryColor, SecondaryColor, TextureBrushImage, GradiantStyle, HatchStyle
    public Pen outlinePen_ { get; set; }
    public Brush fillBrush_ { get; set; }
    private ToolArgs args;

    public MyHatchStyle(ToolArgs args)
    {
      this.args = args;
    }

    private void UpdateBrush()
    {
      fillBrush_ = GetBrush();
    }

    private void UpdateOutlinePen(ToolArgs args)
    {
      outlinePen_ = new Pen(GetBrush(), args.settings.Width);
      outlinePen_.DashStyle = args.settings.LineStyle;
    }

    private Brush GetBrush()
    {
      Color c1, c2;

      c1 = args.settings.PrimaryColor;
      c2 = args.settings.SecondaryColor;

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
      ((LinearGradientBrush)fillBrush_).ScaleTransform(.99f, .99f);
    }

    public Brush getBrushStyle() // TODO: private 
    {
      return new HatchBrush(HatchStyle.Cross, Color.CornflowerBlue, Color.WhiteSmoke);
    }
  }

  public class MySolidStyle : IStyle // TODO: Remove "My" from name
  {
    public Pen outlinePen_ { get; set; }
    public Brush fillBrush_ { get; set; }
    private ToolArgs args;

    public MySolidStyle(ToolArgs args)
    {
      this.args = args;
    }
  
    public void Update(Rectangle rect)
    {
 
    }

    public Brush getBrushStyle() // TODO: private 
    {
      return new SolidBrush(Color.Tomato);
    }
  }
}
