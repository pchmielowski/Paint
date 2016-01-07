
using Paint;
using System.Drawing;

class StyleFactory
{
  public static IStyle createStyle(PaintSettings settings)
  {
    IStyle style;
    switch (settings.BrushType)
    {
    case BrushType.SolidBrush:
      style = new MySolidStyle(settings.PrimaryColor);
      break;

    case BrushType.TextureBrush:
      style = new MyTextureStyle(settings.TextureBrushImage);
      break;

    case BrushType.GradientBrush:
      style = new MyGradientStyle(settings.PrimaryColor,
                                  settings.SecondaryColor,
                                  settings.GradientOrientation);
      break;

    case BrushType.HatchBrush:
      style = new MyHatchStyle(settings.PrimaryColor,
                                  settings.SecondaryColor,
                                  settings.HatchStyle);
      break;

    default:
      throw new System.Exception("Unknown style");
    }

    switch (settings.DrawMode)
    {
    case DrawMode.Outline:
      return new OutlineStyleDecorator(style);

    case DrawMode.Filled:
      return new FilledStyleDecorator(style);

    case DrawMode.Mixed:
      return new OutlineStyleDecorator(new FilledStyleDecorator(style));

    default:
      throw new System.Exception("Unknown style");
    }
  }
}