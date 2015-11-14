
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Paint
{
  public class PencilTool : Tool
  {
    private bool drawing;
    private Graphics bmpGraphics;
    private Point prevPoint;
    private Pen pen;

    public PencilTool(ToolArgs args)
        : base(args)
    {
      drawing = false;
      args.pictureBox.Cursor = Cursors.Cross;
    }

    public override void StopDrawing(MouseEventArgs e)
    {
      drawing = false;
    }

    public override void UpdateMousePosition(MouseEventArgs e)
    {
      Point curPoint = e.Location;
      if (drawing)
      {
        g.DrawLine(pen, prevPoint, curPoint);
        bmpGraphics.DrawLine(pen, prevPoint, curPoint);
        prevPoint = curPoint;
      }
      ShowPointInStatusBar(curPoint);
    }

    public override void StartDrawing(MouseEventArgs e)
    {

      drawing = true;
      prevPoint = e.Location;
      pen = new Pen(args.settings.PrimaryColor, 1);
      g = args.pictureBox.CreateGraphics();
      bmpGraphics = Graphics.FromImage(args.bitmap);

    }

    public override void UnloadTool()
    {
      args.pictureBox.Cursor = Cursors.Arrow;
    }
  }
}
