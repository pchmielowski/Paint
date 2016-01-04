namespace Paint
{
  public delegate void ToolBarButtonClicked(string toolName);

  public interface IToolBarView
  {
    event ToolBarButtonClicked ButtonClicked;
  }
}
