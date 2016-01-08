using System.Drawing;
using System.Windows.Forms;

namespace Paint
{
  public class LineTool : Tool
  {
    private bool drawing;
    private Point beginingPosition;
    private Pen pen;

    private TextureBrush delBrush;

    public LineTool()
    {
      drawing = false;
    }

    public override void StopDrawing(MouseEventArgs e)
    {
      if (drawing)
      {
        drawing = false;

        //args.pictureBox.Invalidate();

        // free resources
        pen.Dispose();
        delBrush.Dispose();
        pictureToDrawOn.Dispose();
      }
    }

    public override void UpdateMousePosition(MouseEventArgs e)
    {
      if (drawing)
      {
        ClearTempShapes(delBrush);

        pictureToDrawOn.DrawLine(pen, beginingPosition, e.Location);
        //args.pictureBox.Invalidate();
      }
    }

    public override void StartDrawing(MouseEventArgs e, IStyle brushManager)
    {
      //drawing = true;
      //beginingPosition = e.Location;

      //g = Graphics.FromImage(args.bitmap);

      //pen = new Pen(GetBrush(false), args.settings.Width);
      //pen.DashStyle = args.settings.LineStyle;

      //delBrush = new TextureBrush(args.bitmap);
    }
  }
}
