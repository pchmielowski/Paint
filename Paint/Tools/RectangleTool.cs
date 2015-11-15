using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Paint
{
  public class RectangleTool : Tool
  {
    private RectangleDrawer rectangleDrawer_;
    public RectangleTool(ToolArgs args)
        : base(args)
    {
      inDrawingState_ = false;
      args.pictureBox.Cursor = Cursors.Cross;
    }

    protected bool inDrawingState_;
    protected Point startLocation_;
    private TextureBrush brushSavedState_;
    public override void StartDrawing(MouseEventArgs e)
    {
      PreparePen();

      g = Graphics.FromImage(args.bitmap);
      rectangleDrawer_ = new RectangleDrawer(g, args.settings, outlinePen_, fillBrush_);
      inDrawingState_ = true;
      startLocation_ = e.Location;

      brushSavedState_ = new TextureBrush(args.bitmap);
    }

    protected Pen outlinePen_; //
    protected Brush fillBrush_; //
    private void PreparePen()
    {
      switch (args.settings.DrawMode)
      {
      case DrawMode.Outline:
        outlinePen_ = new Pen(GetBrush(false), args.settings.Width);
        outlinePen_.DashStyle = args.settings.LineStyle;
        break;

      case DrawMode.Filled:
        fillBrush_ = GetBrush(false);
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

    public override void UpdateMousePosition(MouseEventArgs e)
    {
      if (!inDrawingState_)
      {
        return;
      }

      ClearOldShape(brushSavedState_);

      Rectangle rect = GetRectangleFromPoints(startLocation_, e.Location);
      rectangleDrawer_.DrawRectangle(rect, args.settings.DrawMode);

      args.pictureBox.Invalidate();
    }

    public override void StopDrawing(MouseEventArgs e)
    {
      if (inDrawingState_)
      {
        args.pictureBox.Invalidate();
        inDrawingState_ = false;
        if (fillBrush_ != null)
          fillBrush_.Dispose();
        if (outlinePen_ != null)
          outlinePen_.Dispose();
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
