using System.Drawing;
using System.Windows.Forms;

namespace Paint
{
  public class BrushTool : Tool
  {
    protected bool isDrawingState_;
    protected BrushToolType toolType;
    protected Graphics bmpGraphics;
    protected Point previousPosition;
    protected Pen pen;

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

      pen.Dispose();
      bmpGraphics.Dispose();
      g.Dispose();
    }

    public override void UpdateMousePosition(MouseEventArgs e)
    {
      Point currentPosition = e.Location;
      if (isDrawingState_)
      {
        g.DrawLine(pen, previousPosition, currentPosition);
        bmpGraphics.DrawLine(pen, previousPosition, currentPosition);
        previousPosition = currentPosition;
      }
    }

    public override void StartDrawing(MouseEventArgs e)
    {

      isDrawingState_ = true;
      previousPosition = e.Location;

      if (toolType == BrushToolType.FreeBrush)
        pen = new Pen(GetBrush(false), GetWidth());
      else
        pen = new Pen(args.settings.SecondaryColor, GetWidth());

      pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
      pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

      g = args.pictureBox.CreateGraphics();
      bmpGraphics = Graphics.FromImage(args.bitmap);

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
