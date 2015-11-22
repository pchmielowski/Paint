using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Paint
{
  public class RectangleTool : Tool
  {
    public RectangleTool(ToolArgs args)
        : base(args)
    {
      inDrawingState_ = false;
    }

    protected bool inDrawingState_;
    protected Point startLocation_;
    private TextureBrush brushSavedState_;
    private IStyle style;
    public override void StartDrawing(MouseEventArgs e, IStyle style)
    {
      g = Graphics.FromImage(args.bitmap);

      this.style = style;

      inDrawingState_ = true;
      startLocation_ = e.Location;

      brushSavedState_ = new TextureBrush(args.bitmap);
    }

    public override void UpdateMousePosition(MouseEventArgs e)
    {
      if (!inDrawingState_)
        return;

      ClearOldShape(brushSavedState_);

      Rectangle rectangle = GetRectangleFromPoints(startLocation_, e.Location);
      GraphicsPath rectangleAsGraphicsPath = new GraphicsPath();
      rectangleAsGraphicsPath.AddRectangle(rectangle);

      style.DrawOnGraphics(rectangleAsGraphicsPath, g);
      args.pictureBox.Invalidate();
    }

    public override void StopDrawing(MouseEventArgs e)
    {
      if (inDrawingState_)
      {
        args.pictureBox.Invalidate();
        inDrawingState_ = false;
        brushSavedState_.Dispose();
        g.Dispose();
      }
    }

    public override void UnloadTool()
    {
      args.pictureBox.Cursor = Cursors.Arrow;
    }
  }
}
