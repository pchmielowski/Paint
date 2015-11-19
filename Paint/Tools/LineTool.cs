
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Paint
{
  public class LineTool : Tool
  {
    private bool drawing;
    private Point beginingPosition;
    private Pen pen;

    private TextureBrush delBrush;

    public LineTool(ToolArgs args)
      : base(args)
    {
      drawing = false;
      args.pictureBox.Cursor = Cursors.Cross;
    }

    public override void StopDrawing(MouseEventArgs e)
    {
      if (drawing)
      {
        drawing = false;

        args.pictureBox.Invalidate();

        // free resources
        pen.Dispose();
        delBrush.Dispose();
        g.Dispose();
      }
    }

    public override void UpdateMousePosition(MouseEventArgs e)
    {
      if (drawing)
      {
        ClearOldShape(delBrush);

        g.DrawLine(pen, beginingPosition, e.Location);
        args.pictureBox.Invalidate();
      }
    }

    public override void StartDrawing(MouseEventArgs e, Style brushManager)
    {
      drawing = true;
      beginingPosition = e.Location;

      g = Graphics.FromImage(args.bitmap);

      pen = new Pen(GetBrush(false), args.settings.Width);
      pen.DashStyle = args.settings.LineStyle;

      delBrush = new TextureBrush(args.bitmap);
    }

    public override void UnloadTool()
    {
      args.pictureBox.Cursor = Cursors.Default;
    }
  }
}
