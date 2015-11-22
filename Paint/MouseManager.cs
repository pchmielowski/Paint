using System.Windows.Forms;

namespace Paint
{
  class StyleFactory
  {
    public static IStyle createStyle(ToolArgs toolArgs)
    {
      IStyle style;
      switch (toolArgs.settings.BrushType)
      {
      case BrushType.SolidBrush:
        style = new MySolidStyle(toolArgs.settings.PrimaryColor);
        break;

      case BrushType.TextureBrush:
        style = new MyTextureStyle(toolArgs.settings.TextureBrushImage);
        break;

      case BrushType.GradiantBrush:
        style = new MyGradientStyle(toolArgs.settings.PrimaryColor,
                                    toolArgs.settings.SecondaryColor, 
                                    toolArgs.settings.GradiantStyle);
        break;

      case BrushType.HatchBrush:
        style = new MyHatchStyle(toolArgs.settings.PrimaryColor,
                                    toolArgs.settings.SecondaryColor,
                                    toolArgs.settings.HatchStyle);
        break;

      default:
        style = new MySolidStyle(toolArgs.settings.PrimaryColor); // TODO: maybe parameters related to specified My*Style?
        break;
      }

      switch (toolArgs.settings.DrawMode)
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
  }

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
        IStyle brushManager = StyleFactory.createStyle(toolArgs_);
        tool_.StartDrawing(e, brushManager);
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