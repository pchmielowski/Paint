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
        IStyle brushManager = StyleFactory();
        tool_.StartDrawing(e, brushManager);
      }
    }

    private IStyle StyleFactory(/*toolArgs_.settings.BrushType, 
                    toolArgs_.settings.DrawMode*/) // TODO: make class
    {
      IStyle style;
      switch (toolArgs_.settings.BrushType)
      {
      case BrushType.SolidBrush:
        style = new MySolidStyle(toolArgs_);
        break;

      //case BrushType.TextureBrush:
      //  break;

      //case BrushType.GradiantBrush:
      //  break;

      case BrushType.HatchBrush:
        style = new MyHatchStyle(toolArgs_);
        break;
      default:
        style = new MySolidStyle(toolArgs_); // TODO: maybe parameters related to specified My*Style?
        break;
      }

      switch (toolArgs_.settings.DrawMode)
      {
      case DrawMode.Outline:
        return new OutlineStyleDecorator(style);
        break;

      case DrawMode.Filled:
        return new FilledStyleDecorator(style);
        break;

      case DrawMode.Mixed:
        return new OutlineStyleDecorator(new FilledStyleDecorator(style));
        break;
      default:
        MessageBox.Show("Remove MixedWithSolidLine!!!");
        return null;
        break;
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
