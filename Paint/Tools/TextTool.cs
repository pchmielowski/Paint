using System.Drawing;
using System.Windows.Forms;

namespace Paint
{
  public class TextTool : Tool
  {
    public TextTool()
    {
      //args.pictureBox.Cursor = Cursors.Cross;
    }

    public override void UpdateMousePosition(Point location)
    {
    }
    public override void StartDrawing(Point location, IStyle brushManager)
    {
    }

    public override void StopDrawing(Point location)
    {
      //TextDialog textDlg = new TextDialog();
      //if (textDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      //{
      //  Graphics g = Graphics.FromImage(args.bitmap);
      //  g.DrawString(textDlg.ReturnText, textDlg.TextFont, GetBrush(false), e.Location);
      //  args.pictureBox.Invalidate();
      //}
    }
  }
}
