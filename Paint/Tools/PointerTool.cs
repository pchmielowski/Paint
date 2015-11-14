
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Paint
{
  public class PointerTool : Tool
  {
    public PointerTool(ToolArgs args)
      : base(args) {
      args.pictureBox.Cursor = Cursors.Arrow;
    }

    public override void UpdateMousePosition(MouseEventArgs e)
    {
      // show cursor location in status bar
      ShowPointInStatusBar(e.Location);
    }
    public override void StartDrawing(MouseEventArgs e)
    {
      // show cursor location in status bar
      ShowPointInStatusBar(e.Location);
    }
    public override void StopDrawing(MouseEventArgs e)
    {
      // show cursor location in status bar
      ShowPointInStatusBar(e.Location);
    }

    public override void UnloadTool() {
    }
  }
}
