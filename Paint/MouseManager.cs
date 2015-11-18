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
        tool_.StartDrawing(e, new BrushManager(toolArgs_));
      }
    }

    //protected void ShowPointInStatusBar(Point pt)
    //{
    //  args.panel1.Text = pt.ToString();
    //  args.panel2.Text = "";
    //}

    //protected void ShowPointInStatusBar(Point pt1, Point pt2)
    //{
    //  args.panel1.Text = pt1.ToString();
    //  args.panel2.Text = pt2.ToString();
    //}
  }
}
