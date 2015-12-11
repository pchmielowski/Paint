using System.Windows.Forms;
using System.Drawing;

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
                                    toolArgs.settings.GradientOrientation);
        break;

      case BrushType.HatchBrush:
        style = new MyHatchStyle(toolArgs.settings.PrimaryColor,
                                    toolArgs.settings.SecondaryColor,
                                    toolArgs.settings.HatchStyle);
        break;

      default:
        throw new System.Exception("Unknown style");
      }

      switch (toolArgs.settings.DrawMode)
      {
      case DrawMode.Outline:
        return new OutlineStyleDecorator(style);

      case DrawMode.Filled:
        return new FilledStyleDecorator(style);

      case DrawMode.Mixed:
        return new OutlineStyleDecorator(new FilledStyleDecorator(style));

      default:
        throw new System.Exception("Unknown style");
      }
    }
  }

  public class MouseEventManager
  {
    private Tool tool_;
    private ToolArgs toolSettings;

    public void UpdateTool(Tool tool, ToolArgs toolArgs)
    {
      tool_ = tool;
      toolSettings = toolArgs;

      toolSettings.pictureBox.MouseDown +=
        new MouseEventHandler(OnMouseDown);
      toolSettings.pictureBox.MouseMove +=
        new MouseEventHandler(OnMouseMove);
      toolSettings.pictureBox.MouseUp +=
        new MouseEventHandler(OnMouseUp);
    }
    ~MouseEventManager()
    {
      // TODO: zastanowic sie gdzie to dac
      toolSettings.pictureBox.MouseDown -= new MouseEventHandler(OnMouseDown);
      toolSettings.pictureBox.MouseMove -= new MouseEventHandler(OnMouseMove);
      toolSettings.pictureBox.MouseUp -= new MouseEventHandler(OnMouseUp);
    }

    private bool twoPointsInStatusBar = false;
    private Point startingPoint;
    private void OnMouseUp(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
      {
        tool_.StopDrawing(e);
        twoPointsInStatusBar = false;
      }
    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {
      tool_.UpdateMousePosition(e);
      if (twoPointsInStatusBar)
      {
        ShowPointInStatusBar(startingPoint, e.Location);
      }
      else
      {
        ShowPointInStatusBar(e.Location);
      }
    }

    private void OnMouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
      {
        IStyle brushManager = StyleFactory.createStyle(toolSettings);
        tool_.StartDrawing(e, brushManager);
        twoPointsInStatusBar = true;
        startingPoint = e.Location;
      }
    }

    protected void ShowPointInStatusBar(Point point)
    {
      toolSettings.statusBarLeft.Text = point.ToString();
      toolSettings.statusBarRight.Text = "";
    }

    protected void ShowPointInStatusBar(Point startPoint, Point endPoint)
    {
      toolSettings.statusBarLeft.Text = startPoint.ToString();
      toolSettings.statusBarRight.Text = endPoint.ToString();
    }
  }
}