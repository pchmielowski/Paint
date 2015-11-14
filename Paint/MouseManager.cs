using System.Windows.Forms;

namespace Paint
{
  class MouseManager
  {
    private Tool tool_;
    public MouseManager(Tool tool, ToolArgs toolArgs)
    {
      tool_ = tool;

      toolArgs.pictureBox.MouseDown +=
        new MouseEventHandler(tool_.OnMouseDown);
      toolArgs.pictureBox.MouseMove +=
        new MouseEventHandler(tool_.OnMouseMove);
      toolArgs.pictureBox.MouseUp +=
        new MouseEventHandler(tool_.OnMouseUp);

    }

    private void OnMouseUp(object sender, MouseEventArgs e)
    {

    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {

    }

    private void OnMouseDown(object sender, MouseEventArgs e)
    {

    }
  }
}
