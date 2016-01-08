
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Paint
{
  public class PointerTool : Tool
  {
    public PointerTool()
    {
      //args.pictureBox.Cursor = Cursors.Arrow;
    }

    public override void UpdateMousePosition(Point location)
    {
    }
    public override void StartDrawing(Point location, IStyle brushManager)
    {
    }
    public override void StopDrawing(Point location)
    {
    }
  }
}
