using System.Windows.Forms;

namespace Paint
{
  public class TextTool : Tool
  {
    public TextTool()
    {
      //args.pictureBox.Cursor = Cursors.Cross;
    }

    public override void UpdateMousePosition(MouseEventArgs e)
    {
    }
    public override void StartDrawing(MouseEventArgs e, IStyle brushManager)
    {
    }

    public override void StopDrawing(MouseEventArgs e)
    {
      //TextDialog textDlg = new TextDialog();
      //if (textDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      //{
      //  Graphics g = Graphics.FromImage(args.bitmap);
      //  g.DrawString(textDlg.ReturnText, textDlg.TextFont, GetBrush(false), e.Location);
      //  args.pictureBox.Invalidate();
      //}
    }

    public override void UnloadTool()
    {
      //args.pictureBox.Cursor = Cursors.Default;
    }
  }
}
