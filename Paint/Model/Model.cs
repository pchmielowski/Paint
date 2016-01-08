using Paint.View;
using System.Drawing;
using System.Windows.Forms;

namespace Paint.Model
{
  public class PaintModel
  {
    private Tool drawingTool;
    public Tool DrawingTool
    {
      set
      {
        drawingTool = value;
        drawingTool.model = this;
      }
    }
    public IPictureView pictureView;
    public ImageFile imageFile;
    private PaintSettings settings = new PaintSettings();

    public PaintModel()
    {
      imageFile = new ImageFile(new Size(500, 500), Color.White);
      settings.BrushType = BrushType.SolidBrush;
      settings.DrawMode = DrawMode.Mixed;
      settings.PrimaryColor = Color.Chartreuse;
      settings.SecondaryColor = Color.BlueViolet;
    }

    public void ShowImageOnView()
    {
      pictureView.ShowImage(imageFile);
    }

    private bool inDrawingState = false;
    public void StartDrawing(MouseEventArgs e)
    {
      IStyle brushManager = StyleFactory.createStyle(settings);
      if (drawingTool != null)
      {
        drawingTool.StartDrawing(e.Location, brushManager);
        inDrawingState = true;
      }
    }

    public void UpdateMousePosition(MouseEventArgs e)
    {
      if (!inDrawingState || drawingTool == null)
        return;

      drawingTool.UpdateMousePosition(e.Location);
    }

    public void StopDrawing(MouseEventArgs e)
    {
      if (!inDrawingState || drawingTool == null)
        return;

      drawingTool.StopDrawing(e.Location);
      inDrawingState = false;
    }
  }
}