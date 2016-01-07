using System;
using System.Windows.Forms;
using Paint.Model;
using Paint.View;

namespace Paint
{
  static class Program
  {
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      PaintForm mainView = new PaintForm();

      PaintModel model = new PaintModel();
      PictureController pictureController =
          new PictureController(model, (IPictureView)mainView);

      ToolBarController toolBarController =
          new ToolBarController(model, mainView.toolBarView);

      model.ShowImageOnView();
      Application.Run(mainView);
    }
  }
}