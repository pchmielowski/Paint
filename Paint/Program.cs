using System;
using System.Windows.Forms;
using Paint.Model;
using Paint.Controllers;

namespace Paint
{
  static class Program
  {
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      PaintModel model = new PaintModel();

      PaintForm mainView = new PaintForm();
      PictureController pictureController =
          new PictureController(model, mainView);

      ToolBarController toolBarController =
          new ToolBarController(model, mainView.toolBarView);

      model.ShowImageOnView();
      Application.Run(mainView);
    }
  }
}