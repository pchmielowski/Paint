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

    public void StartDrawing(MouseEventArgs e)
    {
      IStyle brushManager = StyleFactory.createStyle(settings);
      if (drawingTool != null)
      {
        drawingTool.StartDrawing(e, brushManager);
      }
    }

    public void UpdateMousePosition(MouseEventArgs e)
    {
      if (drawingTool != null)
      {
        drawingTool.UpdateMousePosition(e);
      }
    }

    public void StopDrawing(MouseEventArgs e)
    {
      if (drawingTool != null)
      {
        drawingTool.StopDrawing(e);
      }
    }
  }
}