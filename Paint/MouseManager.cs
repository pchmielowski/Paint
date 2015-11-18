using System.Drawing;
using System.Windows.Forms;

namespace Paint
{
  class MouseManager // TODO: MouseEventHandlersRegistrar
  {
    private Tool tool_;
    private ToolArgs toolArgs_;

    public void UpdateTool(Tool tool, ToolArgs toolArgs)
    {
      tool_ = tool;
      toolArgs_ = toolArgs;

      toolArgs_.pictureBox.MouseDown +=
        new MouseEventHandler(OnMouseDown);
      toolArgs_.pictureBox.MouseMove +=
        new MouseEventHandler(OnMouseMove);
      toolArgs_.pictureBox.MouseUp +=
        new MouseEventHandler(OnMouseUp);
    }
    ~MouseManager()
    {
      // TODO: zastanowic sie gdzie to dac
      toolArgs_.pictureBox.MouseDown -= new MouseEventHandler(OnMouseDown);
      toolArgs_.pictureBox.MouseMove -= new MouseEventHandler(OnMouseMove);
      toolArgs_.pictureBox.MouseUp -= new MouseEventHandler(OnMouseUp);
    }

    private void OnMouseUp(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
        tool_.StopDrawing(e);
    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {
      tool_.UpdateMousePosition(e);
      // TODO: updatowanie statusbaru
    }

    private void OnMouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
      {
        Brush brush = new SolidBrush(Color.FromArgb(255, 255, 0, 0));
        Pen pen = new Pen(Color.FromArgb(255, 0, 0, 255));
        tool_.StartDrawing(e, new BrushManager(pen, brush));
      }
    }
  }
}
