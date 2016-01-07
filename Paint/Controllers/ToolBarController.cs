using Paint.Model;

namespace Paint
{
  public class ToolBarController
  {
    private IToolBarView toolBarView;
    private PaintModel model;

    public ToolBarController(PaintModel model, IToolBarView toolBarView)
    {
      this.model = model;
      this.toolBarView = toolBarView;

      this.toolBarView.ButtonClicked += OnButtonClick;
    }

    private void OnButtonClick(string toolName)
    {
      Tool chosenTool = null;
      if (toolName == "arrowBtn")
      {
        //chosenTool = new PointerTool(toolArgs);
      }
      if (toolName == "lineBtn")
      {
        //chosenTool = new LineTool(toolArgs);
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

      model.tool = chosenTool;
    }
  }
}
