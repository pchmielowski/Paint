
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Paint
{
  public class PaintSettings
  {
    public DrawMode DrawMode;

    public int Width;

    public LinearGradientMode GradientOrientation;

    public Color PrimaryColor;

    public Color SecondaryColor;

    public BrushType BrushType;

    public HatchStyle HatchStyle;

    public DashStyle LineStyle;

    public Image TextureBrushImage;
  }
}