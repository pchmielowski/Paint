using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Paint
{
  public class BrushTool : Tool
  {
    protected bool isDrawingState_;
    protected BrushToolType toolType;
    protected Point previousPosition;
    protected Pen penCfg_;

    public BrushTool(ToolArgs args, BrushToolType type)
      : base(args)
    {
      toolType = type;
      isDrawingState_ = false;

      args.pictureBox.Cursor = Cursors.Cross;

    }

    public override void StopDrawing(MouseEventArgs e)
    {
      isDrawingState_ = false;
      args.pictureBox.Invalidate();

      penCfg_.Dispose();
      g.Dispose();
    }

    public override void UpdateMousePosition(MouseEventArgs e)
    {
      Point currentPosition = e.Location;
      if (isDrawingState_)
      {
        g.DrawLine(penCfg_, previousPosition, currentPosition);
        args.pictureBox.Invalidate();
        previousPosition = currentPosition;
      }
    }

    public override void StartDrawing(MouseEventArgs e, BrushManager brushManager)
    {

      isDrawingState_ = true;
      previousPosition = e.Location;

      penCfg_ = preparePen();

      g = Graphics.FromImage(args.bitmap);
    }

    private Pen preparePen()
    {
      Pen pen;
      LineCap lineCap = LineCap.Round;
      if (toolType == BrushToolType.FreeBrush)
      {
        pen = new Pen(GetBrush(false));
      }
      else // if (toolType == BrushToolType.Eraser)
      {
        pen = new Pen(args.settings.SecondaryColor);
        lineCap = LineCap.Square;
      }
      pen.StartCap = lineCap;
      pen.EndCap = lineCap;

      pen.Width = GetWidth();
      return pen;
    }

    protected virtual int GetWidth()
    {
      return args.settings.Width;
    }

    public override void UnloadTool()
    {
      args.pictureBox.Cursor = Cursors.Default;
    }
  }
}
