using System.Drawing;
using System.Windows.Forms;

namespace Paint
{
  public class PencilTool : BrushTool
  {


    public PencilTool(ToolArgs args)
        : base(args, BrushToolType.FreeBrush)
    {
    }
    protected override int GetWidth()
    {
      return 1;
    }
  }
}
