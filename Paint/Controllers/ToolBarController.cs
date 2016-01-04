using Paint.Model;

namespace Paint
{
  public class ToolBarController
  {
    private ToolModel toolModel;
    private IToolBarView toolBarView;

    public ToolBarController(ToolModel toolModel, IToolBarView toolBarView)
    {
      this.toolModel = toolModel;
      this.toolBarView = toolBarView;
      toolBarView.ButtonClicked += OnButtonClick;
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
        //chosenTool = new ShapeTool(toolArgs, shapeCreator);
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
        //chosenTool = new ShapeTool(toolArgs, shapeCreator);
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

      toolModel.UpdateTool(chosenTool);
    }
  }
}
