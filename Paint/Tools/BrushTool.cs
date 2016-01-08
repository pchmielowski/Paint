using System.Drawing;
using System.Windows.Forms;

namespace Paint
{
  public class BrushTool : Tool
  {
    public override void StartDrawing(Point location, IStyle style)
    {
      pictureToDrawOn = Graphics.FromImage(model.imageFile.Bitmap);

      pen = preparePen();

      startLocation = location;
    }
    public override void UpdateMousePosition(Point location)
    {
      Point currentPosition = location;
      pictureToDrawOn.DrawLine(pen, startLocation, currentPosition);
      model.pictureView.PictureBox.Invalidate();
      startLocation = currentPosition;
    }
    public override void StopDrawing(Point location)
    {
      pen.Dispose();
      pictureToDrawOn.Dispose();
    }

    protected Point startLocation;
    protected Pen pen;

    private Pen preparePen()
    {
      return new Pen(Color.DarkBlue, 5.0f);

      //Pen pen;
      //LineCap lineCap = LineCap.Round;
      //if (toolType == BrushToolType.FreeBrush)
      //{
      //  //pen = new Pen(GetBrush(false));
      //}
      //else // if (toolType == BrushToolType.Eraser)
      //{
      //  pen = new Pen(args.settings.SecondaryColor);
      //  lineCap = LineCap.Square;
      //}
      //pen.StartCap = lineCap;
      //pen.EndCap = lineCap;

      //pen.Width = GetWidth();
      //return pen;
    }

    protected virtual int GetWidth()
    {
      return -1;
      //return args.settings.Width;
    }
  }
}
