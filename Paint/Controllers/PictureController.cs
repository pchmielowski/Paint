using System.Windows.Forms;
using System.Drawing;
using Paint.View;
using Paint.Model;

namespace Paint
{
  public class PictureController
  {
    public PaintSettings toolSettings;
    private IPictureView pictureView;

    public PictureController(PaintModel model, IPictureView pictureView)
    {
      this.model = model;
      model.pictureView = pictureView;
      this.pictureView = pictureView;
      SubscribeToMouseEvents();
    }

    public void SubscribeToMouseEvents()
    {
      pictureView.PictureBox.MouseDown +=
        new MouseEventHandler(OnMouseDown);
      pictureView.PictureBox.MouseMove +=
        new MouseEventHandler(OnMouseMove);
      pictureView.PictureBox.MouseUp +=
        new MouseEventHandler(OnMouseUp);
    }

    public void UnsubscribeMouseEvents()
    {
      pictureView.PictureBox.MouseDown -= new MouseEventHandler(OnMouseDown);
      pictureView.PictureBox.MouseMove -= new MouseEventHandler(OnMouseMove);
      pictureView.PictureBox.MouseUp -= new MouseEventHandler(OnMouseUp);
    }

    private Point startingPoint;
    private PaintModel model;

    private void OnMouseUp(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
      {
        model.tool.StopDrawing(e);
      }
    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {
      if (model.tool != null)
      {
        model.tool.UpdateMousePosition(e);
      }
    }

    private void OnMouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
      {
        IStyle brushManager = StyleFactory.createStyle(model.settings);
        if (model.tool != null)
        {
          model.tool.StartDrawing(e, brushManager);
        }
        startingPoint = e.Location;
      }
    }
  }
}