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
      args.pictureBox.Cursor = Cursors.Cross;
    }

    protected bool inDrawingState_;
    protected Point startLocation_;
    private TextureBrush brushSavedState_;
    private BrushManager brushManager_;
    public override void StartDrawing(MouseEventArgs e, BrushManager brushManager)
    {
      g = Graphics.FromImage(args.bitmap);

      brushManager_ = brushManager;

      inDrawingState_ = true;
      startLocation_ = e.Location;

      brushSavedState_ = new TextureBrush(args.bitmap);
    }


    public override void UpdateMousePosition(MouseEventArgs e)
    {
      if (!inDrawingState_)
      {
        return;
      }

      ClearOldShape(brushSavedState_);

      Rectangle rect = GetRectangleFromPoints(startLocation_, e.Location);
      DrawRectangle(rect, args.settings.DrawMode);
      args.pictureBox.Invalidate();
    }
    public void DrawRectangle(Rectangle rect,
      DrawMode drawMode)
    {
      if (brushManager_.fillBrush_ is LinearGradientBrush)
      {
        brushManager_.fillBrush_ = UpdateGradientBrush(rect, brushManager_.fillBrush_);
      }

      g.FillRectangle(brushManager_.fillBrush_, rect);
      g.DrawRectangle(brushManager_.outlinePen_, rect);
    }

    private Brush UpdateGradientBrush(Rectangle rect, Brush fillBrush)
    {
      if ((rect.Width == 0) || (rect.Height == 0))
        return fillBrush;

      fillBrush = new LinearGradientBrush(rect,
            args.settings.PrimaryColor,
            args.settings.SecondaryColor,
            args.settings.GradiantStyle);

      return fillBrush;
    }
    public override void StopDrawing(MouseEventArgs e)
    {
      if (inDrawingState_)
      {
        args.pictureBox.Invalidate();
        inDrawingState_ = false;
        if (brushManager_.fillBrush_ != null)
          brushManager_.fillBrush_.Dispose();
        if (brushManager_.outlinePen_ != null)
          brushManager_.outlinePen_.Dispose();
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
