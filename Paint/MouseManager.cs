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
        //brushManager = newSolidBrushManager();
        break;

      case BrushType.TextureBrush:
        //brushManager = newTextureBrushManager();
        break;

      case BrushType.GradiantBrush:
        //brushManager = newGradiantBrushManager();
        break;

      case BrushType.HatchBrush:
        //brushManager = newHatchBrushManager();
        break;
      }

      switch (toolArgs_.settings.DrawMode)
      {
      case DrawMode.Outline:
        style = new Style(toolArgs_);
        return new OutlineStyleDecorator(style);
        //TODO: return new OutlineBrushManagerDecorator(brushManager);
        break;

      case DrawMode.Filled:
        //TODO: return new FilledBrushManagerDecorator(brushManager);
        break;

      case DrawMode.Mixed:
        //TODO: return new OutlineBrushManagerDecorator(
        //                          new FilledBrushManagerDecorator(
        //                              brushManager));
        break;

      case DrawMode.MixedWithSolidOutline: // TODO: wyebac ta opcje
        break;
      }
      return new Style(toolArgs_);
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
