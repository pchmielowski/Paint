using System.Drawing;
using System.Drawing.Drawing2D;
namespace Paint
{
  public abstract class IStyle
  {
    public virtual Pen outlinePen_ { get; set; }
    public virtual Brush fillBrush_ { get; set; }
    public virtual void CreateGradient(RectangleF rect) { } // TODO: private

    public virtual void DrawOnGraphics(GraphicsPath shapeAsGraphicsPath, Graphics g) { }

    public void setPen()
    {
      outlinePen_ = new Pen(Color.BurlyWood, 2);
    }
  }

  public abstract class StyleDecorator : IStyle
  {
    protected IStyle styleToDecorate;
    public override Pen outlinePen_
    {
      get { return styleToDecorate.outlinePen_; }
      set { styleToDecorate.outlinePen_ = value; }
    }
    public override Brush fillBrush_
    {
      get { return styleToDecorate.fillBrush_; }
      set { styleToDecorate.fillBrush_ = value; }
    }
    public override void CreateGradient(RectangleF rect) { styleToDecorate.CreateGradient(rect); }
    public abstract override void DrawOnGraphics(GraphicsPath shapeAsGraphicsPath, Graphics g);
  }

  #region Decorators

  public class OutlineStyleDecorator : StyleDecorator
  {
    public OutlineStyleDecorator(IStyle styleToDecorate)
    {
      this.styleToDecorate = styleToDecorate;
      styleToDecorate.setPen();
    }
    public override void DrawOnGraphics(GraphicsPath shapeAsGraphicsPath, Graphics g)
    {
      styleToDecorate.DrawOnGraphics(shapeAsGraphicsPath, g);
      g.DrawPath(outlinePen_, shapeAsGraphicsPath);
    }
  }

  public class FilledStyleDecorator : StyleDecorator
  {
    public FilledStyleDecorator(IStyle styleToDecorate)
    {
      this.styleToDecorate = styleToDecorate;
    }
    public override void DrawOnGraphics(GraphicsPath shapeAsGraphicsPath, Graphics g)
    {
      styleToDecorate.DrawOnGraphics(shapeAsGraphicsPath, g);

      Region shapeAsRegion = new Region(shapeAsGraphicsPath);
      RectangleF rr = shapeAsRegion.GetBounds(g);
      CreateGradient(rr);
      g.FillRegion(styleToDecorate.fillBrush_, shapeAsRegion);
    }
  }

  #endregion

  #region Styles

  public class MyHatchStyle : IStyle
  {
    public MyHatchStyle(Color color1, Color color2, HatchStyle hatchStyle)
    {
      fillBrush_ = new HatchBrush(hatchStyle, color1, color2);
    }

    //private void UpdateOutlinePen(ToolArgs args)
    //{
    //  outlinePen_ = new Pen(GetBrush(), args.settings.Width);
    //  outlinePen_.DashStyle = args.settings.LineStyle;
    //}
  }

  public class MySolidStyle : IStyle // TODO: Remove "My" from name
  {
    public MySolidStyle(Color primaryColor)
    {
      fillBrush_ = new SolidBrush(primaryColor);
    }
  }

  public class MyGradientStyle : IStyle // TODO: Remove "My" from name
  {
    private Color primaryColor;
    private Color secondaryColor;
    private LinearGradientMode gradientMode;
    public MyGradientStyle(Color primaryColor, Color secondaryColor, LinearGradientMode gradientMode)
    {
      this.primaryColor = primaryColor;
      this.secondaryColor = secondaryColor;
      this.gradientMode = gradientMode;

      Rectangle initialRectangle = new Rectangle(0, 0, 1, 1);
      CreateGradient(initialRectangle);
    }

    public override void CreateGradient(RectangleF rectangle)
    {
      if ((rectangle.Width == 0) || (rectangle.Height == 0))
        return;

      fillBrush_ = new LinearGradientBrush(rectangle,
          primaryColor, secondaryColor,
          gradientMode);
    }
  }

  public class MyTextureStyle : IStyle // TODO: Remove "My" from name
  {
    public MyTextureStyle(Image textureImage)
    {
      fillBrush_ = new TextureBrush(textureImage);
    }
  }
  #endregion
}
