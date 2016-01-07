using Paint.View;
using System.Drawing;

namespace Paint.Model
{
  public class PaintModel
  {
    private Tool _tool;
    public Tool tool
    {
      get { return _tool; }
      set
      {
        _tool = value;
        _tool.model = this;
      }
    }
    public IPictureView pictureView;
    public ImageFile imageFile;
    public PaintSettings settings = new PaintSettings();

    public PaintModel()
    {
      imageFile = new ImageFile(new Size(500, 500), Color.White);
      settings.BrushType = BrushType.SolidBrush;
      settings.DrawMode = DrawMode.Mixed;
      settings.PrimaryColor = Color.BlueViolet;
      settings.SecondaryColor = Color.Chartreuse;
    }

    public void ShowImageOnView()
    {
      pictureView.ShowImage(imageFile);
    }
  }
}
