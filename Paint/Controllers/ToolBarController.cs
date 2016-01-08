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
        //toolArgs.pictureBox.Cursor = Cursors.Cross;
        chosenTool = new ShapeTool(new LineCreator());
      }
      else if (toolName == "rectangleBtn")
      {
        //toolArgs.pictureBox.Cursor = Cursors.Cross;
        chosenTool = new ShapeTool(new RectangleCreator());
      }
      else if (toolName == "pencilBtn")
      {
        //chosenTool = new PencilTool(toolArgs);
      }
      else if (toolName == "brushBtn")
      {
        chosenTool = new BrushTool();
      }
      else if (toolName == "ellipseBtn")
      {
        //toolArgs.pictureBox.Cursor = Cursors.Cross;
        chosenTool = new ShapeTool(new ElipseCreator());
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
