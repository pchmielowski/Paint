using System.Windows.Forms;

namespace Paint.View
{

  public partial class ToolBarUserControl : UserControl, IToolBarView
  {
    public ToolBarUserControl()
    {
      InitializeComponent();
    }

    public event ToolBarButtonClicked ButtonClicked;

    private void toolsBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
    {
      // SetToolBarButtonsState()

      ButtonClicked(e.Button.Name);
    }
  }
}
