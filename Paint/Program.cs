using System;
using System.Windows.Forms;
using Paint.Model;

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
      ToolController toolController = new ToolController(mainView);

      ToolModel toolModel = new ToolModel();
      ToolBarController toolBarController = new ToolBarController(toolModel, mainView.toolBarView);

      Application.Run(mainView);
    }
  }
}