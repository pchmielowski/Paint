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
      SetButtonsState(e.Button);

      if (ButtonClicked != null)
        ButtonClicked(e.Button.Name);
    }


    private void SetButtonsState(ToolBarButton curButton)
    {
      curButton.Pushed = true;
      foreach (ToolBarButton btn in toolsBar.Buttons)
      {
        if (btn != curButton)
          btn.Pushed = false;
      }
    }
  }
}
