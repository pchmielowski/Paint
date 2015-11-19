﻿using System.Drawing;
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
    private IStyle brushManager_;
    public override void StartDrawing(MouseEventArgs e, IStyle brushManager)
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
        return;

      ClearOldShape(brushSavedState_);
      Rectangle rectangle = GetRectangleFromPoints(startLocation_, e.Location);
      brushManager_.Update(rectangle);

      if (brushManager_.fillBrush_ != null)
        g.FillRectangle(brushManager_.fillBrush_, rectangle);
      if (brushManager_.outlinePen_ != null)
        g.DrawRectangle(brushManager_.outlinePen_, rectangle);
      args.pictureBox.Invalidate();
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
