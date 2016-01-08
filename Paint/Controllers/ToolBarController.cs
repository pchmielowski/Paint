using Paint.Model;

namespace Paint.Controllers
{
  public class ToolBarController
  {
    public ToolBarController(PaintModel model, IToolBarView toolBarView)
    {
      this.model = model;
      this.toolBarView = toolBarView;

      this.toolBarView.ButtonClicked += OnButtonClick;
    }

    private PaintModel model;
    private IToolBarView toolBarView;

    private void OnButtonClick(string toolName)
    {
      Tool chosenTool = null;
      if (toolName == "arrowBtn")
      {
        //chosenTool = new PointerTool(toolArgs);
      }
      if (toolName == "lineBtn")
      {
        chosenTool = new LineTool();
      }
      else if (toolName == "rectangleBtn")
      {
        ShapeCreator shapeCreator = new RectangleCreator();
        //toolArgs.pictureBox.Cursor = Cursors.Cross;
        chosenTool = new ShapeTool(shapeCreator);
      }
      else if (toolName == "pencilBtn")
      {
        //chosenTool = new PencilTool(toolArgs);
      }
      else if (toolName == "brushBtn")
      {
        //chosenTool = new BrushTool(toolArgs, BrushToolType.FreeBrush);
      }
      else if (toolName == "ellipseBtn")
      {
        ShapeCreator shapeCreator = new ElipseCreator();
        //toolArgs.pictureBox.Cursor = Cursors.Cross;
        chosenTool = new ShapeTool(shapeCreator);
      }
      else if (toolName == "textBtn")
      {
        //chosenTool = new TextTool(toolArgs);
      }
      else if (toolName == "fillBtn")
      {
        //chosenTool = new FillTool(toolArgs);
      }
      else if (toolName == "eraserBtn")
      {
        //chosenTool = new BrushTool(toolArgs, BrushToolType.Eraser);
      }

      model.DrawingTool = chosenTool;
    }
  }
}
