using System.Windows.Forms;
using Paint.View;
using Paint.Model;

namespace Paint.Controllers
{
  public class PictureController
  {
    public PictureController(PaintModel model, IPictureView pictureView)
    {
      this.model = model;
      this.model.pictureView = pictureView;
      this.pictureView = pictureView;

      SubscribeToMouseEvents();
    }

    private void SubscribeToMouseEvents()
    {
      pictureView.PictureBox.MouseDown +=
        new MouseEventHandler(OnMouseDown);
      pictureView.PictureBox.MouseMove +=
        new MouseEventHandler(OnMouseMove);
      pictureView.PictureBox.MouseUp +=
        new MouseEventHandler(OnMouseUp);
    }

    private PaintModel model;
    private IPictureView pictureView;

    private void OnMouseUp(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
      {
        model.StopDrawing(e);
      }
    }
    private void OnMouseMove(object sender, MouseEventArgs e)
    {
      model.UpdateMousePosition(e);
    }
    private void OnMouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
      {
        model.StartDrawing(e);
      }
    }
  }
}