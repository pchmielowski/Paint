using System.Diagnostics;
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
    //public abstract void Update(Rectangle rect);
    public Brush getBrushStyle() { return null; }
    public void Update(Rectangle rect) { styleToDecorate.Update(rect); }
  }

  public class OutlineStyleDecorator : StyleDecorator
  {
    public OutlineStyleDecorator(IStyle std) { styleToDecorate = std; UpdateOutlinePen(); }
    private void UpdateOutlinePen()
    {
      styleToDecorate.outlinePen_ = new Pen(Color.DarkMagenta, 4);
      styleToDecorate.outlinePen_.DashStyle = DashStyle.Solid;
    }
  }

  public class FilledStyleDecorator : StyleDecorator
  {
    public FilledStyleDecorator(IStyle std) { styleToDecorate = std; UpdateBrush(); }
    //public override void Update(Rectangle rect) { styleToDecorate.Update(rect); }
    private void UpdateBrush()
    {
      styleToDecorate.fillBrush_ = styleToDecorate.getBrushStyle();
    }
  }

  public class MyHatchStyle : IStyle
  {
    public Pen outlinePen_ { get; set; }
    public Brush fillBrush_ { get; set; }
    private HatchStyle hatchStyle;
    private Color color1;
    private Color color2;

    public MyHatchStyle(Color c1, Color c2, HatchStyle h)
    {
      this.hatchStyle = h;
      this.color1 = c1;
      this.color2 = c2;
    }

    //private void UpdateOutlinePen(ToolArgs args)
    //{
    //  outlinePen_ = new Pen(GetBrush(), args.settings.Width);
    //  outlinePen_.DashStyle = args.settings.LineStyle;
    //}

    public void Update(Rectangle rect) { }

    public Brush getBrushStyle() // TODO: private 
    {
      return new HatchBrush(hatchStyle, color1, color2);
    }
  }

  public class MySolidStyle : IStyle // TODO: Remove "My" from name
  {
    public Pen outlinePen_ { get; set; }
    public Brush fillBrush_ { get; set; }
    private Color color;

    public MySolidStyle(Color color)
    {
      this.color = color;
    }

    public void Update(Rectangle rect)
    {

    }

    public Brush getBrushStyle() // TODO: private 
    {
      return new SolidBrush(color);
    }
  }

  public class MyGradientStyle : IStyle // TODO: Remove "My" from name
  {
    public Pen outlinePen_ { get; set; }
    public Brush fillBrush_ { get; set; }

    private Color c1;
    private Color c2;
    private LinearGradientMode mode;
    public MyGradientStyle(Color color1, Color color2, LinearGradientMode gradientMode)
    {
      c1 = color1;
      c2 = color2;
      mode = gradientMode;
    }

    public void Update(Rectangle rect)
    {
      if ((rect.Width == 0) || (rect.Height == 0))
        return;

      if (!(fillBrush_ is LinearGradientBrush))
        return;

      fillBrush_ = new LinearGradientBrush(rect,
          c1, c2,
          mode);
    }

    public Brush getBrushStyle() // TODO: private 
    {
      Rectangle tempRect = new Rectangle(0, 0, 600, 800);
      return new LinearGradientBrush(tempRect,
          c1, c2,
          mode);
    }
  }

  public class MyTextureStyle : IStyle // TODO: Remove "My" from name
  {
    public Pen outlinePen_ { get; set; }
    public Brush fillBrush_ { get; set; }

    private Image textureBrushStyle;

    public MyTextureStyle(Image textureBrushStyle)
    {
      this.textureBrushStyle = textureBrushStyle;
    }

    public void Update(Rectangle rect) {}

    public Brush getBrushStyle() // TODO: private 
    {
      return new TextureBrush(textureBrushStyle);
    }
  }
}
