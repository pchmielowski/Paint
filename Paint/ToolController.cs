using System.Windows.Forms;

namespace Paint
{
  public class ToolController
  {
    public ToolArgs toolArgs;
    private PaintForm view;
    private MouseEventManager mouseManager;
    public ToolController(PaintForm view)
    {
      this.view = view;
      view.SetToolController(this);

      mouseManager = new MouseEventManager();
    }

    private Tool curTool;
    public void ButtonClicked(string toolName)
    {
      if (toolName == "arrowBtn")
      {
        curTool = new PointerTool(toolArgs);
      }
      if (toolName == "lineBtn")
      {
        curTool = new LineTool(toolArgs);
      }
      else if (toolName == "rectangleBtn")
      {
        ShapeCreator shapeCreator = new RectangleCreator();
        //toolArgs.pictureBox.Cursor = Cursors.Cross;
        curTool = new ShapeTool(toolArgs, shapeCreator);
      }
      else if (toolName == "pencilBtn")
      {
        curTool = new PencilTool(toolArgs);
      }
      else if (toolName == "brushBtn")
      {
        curTool = new BrushTool(toolArgs, BrushToolType.FreeBrush);
      }
      else if (toolName == "ellipseBtn")
      {
        ShapeCreator shapeCreator = new ElipseCreator();
        toolArgs.pictureBox.Cursor = Cursors.Cross;
        curTool = new ShapeTool(toolArgs, shapeCreator);
      }
      else if (toolName == "textBtn")
      {
        curTool = new TextTool(toolArgs);
      }
      else if (toolName == "fillBtn")
      {
        curTool = new FillTool(toolArgs);
      }
      else if (toolName == "eraserBtn")
      {
        curTool = new BrushTool(toolArgs, BrushToolType.Eraser);
      }

      mouseManager.UpdateTool(curTool, toolArgs);
    }
  }
}
