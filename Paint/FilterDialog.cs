using System.Collections.Generic;
using System.Windows.Forms;

namespace Paint
{
  class FilterDialog
  {
    public Dictionary<string, NumericUpDown> inputBoxes
    {
      get;
    }
    int numInputBoxes = 0;
    Form dialog = new Form();
    public FilterDialog(string dialogWindowName, List<string> controlsNames)
    {
      inputBoxes = new Dictionary<string, NumericUpDown>();
      foreach (string controlName in controlsNames)
      {
        AddInputBoxAndLabel(controlName);
      }

      dialog.Width = 800;
      dialog.Height = numInputBoxes * 50 + 100;
      dialog.Text = dialogWindowName;

      AddApplyButton();
      dialog.ShowDialog();
    }

    private void AddApplyButton()
    {
      Button applyButton = new Button()
      {
        Text = "Apply",
        Left = 350,
        Top = 70
      };
      applyButton.Click += (sender, e) => { dialog.Close(); };
      dialog.Controls.Add(applyButton);
    }

    private void AddInputBoxAndLabel(string name)
    {
      numInputBoxes++;
      int left = 50;
      int top = numInputBoxes * 50;
      inputBoxes.Add(name, new NumericUpDown()
      {
        Left = left + 100,
        Top = top
      });
      Label textLabel = new Label()
      {
        Left = left,
        Top = top,
        Text = name
      };
      dialog.Controls.Add(inputBoxes[name]);
      dialog.Controls.Add(textLabel);
    }
  }
}
