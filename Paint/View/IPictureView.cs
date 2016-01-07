using System.Windows.Forms;

namespace Paint.View
{
  public interface IPictureView
  {
    void ShowImage(ImageFile imageFile);
    PictureBox PictureBox { get; }
  }
}
