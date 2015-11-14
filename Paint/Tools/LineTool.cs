
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Paint
{
  public class LineTool : Tool
  {
    private bool drawing;
    private Point sPoint;
    private TextureBrush delBrush;
    private Pen pen;
    private Rectangle delRect;

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
        // delete old line !!
        int w = args.settings.Width;
        delRect.Inflate(w, w);
        g.FillRectangle(delBrush, delRect);

        //draw the new line
        g.DrawLine(pen, sPoint, e.Location);
        args.pictureBox.Invalidate();

        delRect = GetRectangleFromPoints(sPoint, e.Location);
      }
    }

    public override void StartDrawing(MouseEventArgs e)
    {

        drawing = true;
        sPoint = e.Location;

        g = Graphics.FromImage(args.bitmap);

        pen = new Pen(GetBrush(false), args.settings.Width);
        pen.DashStyle = args.settings.LineStyle;

        // delete brush
        delBrush = new TextureBrush(args.bitmap);
      
    }

    public override void UnloadTool()
    {
      args.pictureBox.Cursor = Cursors.Default;
      }
  }
}
