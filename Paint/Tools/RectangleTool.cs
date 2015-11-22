using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Paint
{
  public class ShapeTool : Tool
  {
    ShapeCreator shapeCreator;
    public ShapeTool(ToolArgs args, ShapeCreator shapeCreator)
        : base(args)
    {
      inDrawingState_ = false;
      this.shapeCreator = shapeCreator;
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
      GraphicsPath shape = shapeCreator.CreateShape(rectangle);

      style.DrawOnGraphics(shape, g);
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

  public abstract class ShapeCreator
  {
    public abstract GraphicsPath CreateShape(Rectangle boundaries);
  }

  public class RectangleCreator : ShapeCreator
  {
    public override GraphicsPath CreateShape(Rectangle boundaries)
    {
      GraphicsPath rectangleAsGraphicsPath = new GraphicsPath();
      rectangleAsGraphicsPath.AddRectangle(boundaries);
      return rectangleAsGraphicsPath;
    }
  }
  public class ElipseCreator : ShapeCreator
  {
    public override GraphicsPath CreateShape(Rectangle boundaries)
    {
      GraphicsPath rectangleAsGraphicsPath = new GraphicsPath();
      rectangleAsGraphicsPath.AddEllipse(boundaries);
      return rectangleAsGraphicsPath;
    }
  }
}
