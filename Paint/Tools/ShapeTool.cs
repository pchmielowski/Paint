using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Paint
{
  public class ShapeTool : Tool
  {
    public ShapeTool(ShapeCreator shapeCreator)
    {
      this.shapeCreator = shapeCreator;
    }

    public override void StartDrawing(Point location, IStyle style)
    {
      pictureBeforeDrawing = new TextureBrush(model.imageFile.Bitmap);
      pictureToDrawOn = Graphics.FromImage(model.imageFile.Bitmap);

      this.style = style;

      startLocation = location;
    }
    public override void UpdateMousePosition(Point location)
    {

      ClearTempShapes(pictureBeforeDrawing);

      GraphicsPath shape = shapeCreator.CreateShape(startLocation, location);

      style.DrawShapeOnGraphics(shape, pictureToDrawOn);
      model.pictureView.PictureBox.Invalidate();
    }
    public override void StopDrawing(Point location)
    {
      pictureBeforeDrawing.Dispose();
      pictureToDrawOn.Dispose();
    }

    ShapeCreator shapeCreator;
    private Point startLocation;
    private TextureBrush pictureBeforeDrawing;
    private IStyle style;
  }

  public abstract class ShapeCreator
  {
    public abstract GraphicsPath CreateShape(Point p1, Point p2);
    protected Rectangle GetRectangleFromPoints(Point p1, Point p2)
    {
      Point oPoint;
      Rectangle rect;

      if ((p2.X > p1.X) && (p2.Y > p1.Y))
      {
        rect = new Rectangle(p1, new Size(p2.X - p1.X, p2.Y - p1.Y));
      }
      else if ((p2.X < p1.X) && (p2.Y < p1.Y))
      {
        rect = new Rectangle(p2, new Size(p1.X - p2.X, p1.Y - p2.Y));
      }
      else if ((p2.X > p1.X) && (p2.Y < p1.Y))
      {
        oPoint = new Point(p1.X, p2.Y);
        rect = new Rectangle(oPoint, new Size(p2.X - p1.X, p1.Y - oPoint.Y));
      }
      else
      {
        oPoint = new Point(p2.X, p1.Y);
        rect = new Rectangle(oPoint, new Size(p1.X - p2.X, p2.Y - p1.Y));
      }
      return rect;
    }
  }

  public class RectangleCreator : ShapeCreator
  {
    public override GraphicsPath CreateShape(Point p1, Point p2)
    {
      GraphicsPath rectangleAsGraphicsPath = new GraphicsPath();
      rectangleAsGraphicsPath.AddRectangle(GetRectangleFromPoints(p1, p2));
      return rectangleAsGraphicsPath;
    }
  }
  public class ElipseCreator : ShapeCreator
  {
    public override GraphicsPath CreateShape(Point p1, Point p2)
    {
      GraphicsPath rectangleAsGraphicsPath = new GraphicsPath();
      rectangleAsGraphicsPath.AddEllipse(GetRectangleFromPoints(p1, p2));
      return rectangleAsGraphicsPath;
    }
  }
  public class LineCreator : ShapeCreator
  {
    public override GraphicsPath CreateShape(Point p1, Point p2)
    {
      GraphicsPath rectangleAsGraphicsPath = new GraphicsPath();
      rectangleAsGraphicsPath.AddLine(p1, p2);
      return rectangleAsGraphicsPath;
    }
  }
}
