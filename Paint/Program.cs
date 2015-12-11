
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;

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

      Application.Run(mainView);
    }
  }
}