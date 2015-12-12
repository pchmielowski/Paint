using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Collections.Generic;


using ImageDesaturator;

namespace Paint
{
  public partial class PaintForm : Form, IPaintSettings
  {
    private IPaintSettings settings;

    public PaintForm()
    {
      InitializeComponent();
      toolsBar.ImageList = imageList;
      settings = (IPaintSettings)this;
    }

    private ImageFile imageFile;
    private ToolArgs toolArgs;
    private Tool curTool;
    private void PaintForm_Load(object sender, EventArgs e)
    {
      FillFillStyleList();
      FillShapeStyleList();
      FillWidthList();
      FillGradientStyleList();

      // default texture brush image
      brushImageBox.Image = new Bitmap(20, 20);

      // default image
      ShowImage(new ImageFile(new Size(500, 500), Color.White));
    }

    public void ShowImage(ImageFile imageFile)
    {
      string fileName = imageFile.FileName;
      if (fileName == null)
        fileName = "Untitled";
      else
        fileName = new FileInfo(fileName).Name;
      Text = string.Format("Paint - [{0}]", fileName);

      imageBox.ClientSize = imageFile.Bitmap.Size;
      imageBox.Invalidate();
      toolArgs = new ToolArgs(imageFile.Bitmap, imageBox, pointPanel1, pointPanel2, settings);

      if (curTool != null)
        curTool.UnloadTool();
      curTool = new PointerTool(toolArgs);
      SetToolBarButtonsState(arrowBtn);
    }

    #region ToolBar
    private ToolController toolController;
    public void SetToolController(ToolController toolController)
    {
      this.toolController = toolController;
    }

    private void toolsBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
    {
      curTool.UnloadTool();
      ToolBarButton curButton = e.Button;
      SetToolBarButtonsState(curButton);

      toolController.toolArgs = toolArgs;
      if (toolController != null)
        toolController.ButtonClicked(curButton.Name);
      else
        throw new Exception("toolController == null");
    }

    private void SetToolBarButtonsState(ToolBarButton curButton)
    {
      curButton.Pushed = true;
      foreach (ToolBarButton btn in toolsBar.Buttons)
      {
        if (btn != curButton)
          btn.Pushed = false;
      }
    }
    #endregion

    #region imageBox
    private void imageBox_Paint(object sender, PaintEventArgs e)
    {
      //e.Graphics.DrawImageUnscaledAndClipped(imageFile.Bitmap, new Rectangle(new Point(0,0),imageFile.Bitmap.Size));
      Rectangle clipRect = e.ClipRectangle;
      Bitmap b = toolArgs.bitmap.Clone(clipRect, toolArgs.bitmap.PixelFormat);
      e.Graphics.DrawImageUnscaledAndClipped(b, clipRect);
      b.Dispose();
    }
    #endregion

    #region settingsPanel
    private void FillGradientStyleList()
    {
      for (int i = 0; i < 4; i++)
      {
        LinearGradientMode gm = (LinearGradientMode)i;
        gradientStyleCombo.Items.Add(gm);
      }
      gradientStyleCombo.SelectedIndex = 0;

      for (int i = 0; i < 4; i++)
      {
        DashStyle ds = (DashStyle)i;
        lineStyleCombo.Items.Add(ds.ToString());
      }
      lineStyleCombo.SelectedIndex = 0;
    }

    private void FillWidthList()
    {
      // fill Width list
      for (int i = 1; i <= 10; i++)
        widthCombo.Items.Add(i);
      for (int i = 15; i <= 60; i += 5)
        widthCombo.Items.Add(i);
      widthCombo.SelectedIndex = 0;
    }

    private void FillShapeStyleList()
    {
      // fill shape style list
      for (int i = 0; i < 4; i++)
      {
        DrawMode ss = (DrawMode)i;
        shapeStyleCombo.Items.Add(ss);
      }
      shapeStyleCombo.SelectedIndex = 0;
    }

    private void FillFillStyleList()
    {
      // fill (fill style) list
      for (int i = 0; i < 3; i++)
      {
        BrushType bt = (BrushType)i;
        fillStyleCombo.Items.Add(bt);
      }
      const int NUM_HATCH_STYLES = 53;
      for (int i = 0; i < NUM_HATCH_STYLES; i++)
      {
        HatchStyle hs = (HatchStyle)i;
        fillStyleCombo.Items.Add(hs);
      }
      fillStyleCombo.SelectedIndex = 0;
    }

    DrawMode IPaintSettings.DrawMode
    {
      get
      {
        return (DrawMode)shapeStyleCombo.SelectedIndex;
      }
    }

    int IPaintSettings.Width
    {
      get
      {
        return Int32.Parse(widthCombo.Text);
      }
    }

    LinearGradientMode IPaintSettings.GradientOrientation
    {
      get
      {
        return (LinearGradientMode)gradientStyleCombo.SelectedIndex;
      }
    }

    Color IPaintSettings.PrimaryColor
    {
      get
      {
        return primColorBox.BackColor;
      }
    }

    Color IPaintSettings.SecondaryColor
    {
      get
      {
        return secColorBox.BackColor;
      }
    }

    BrushType IPaintSettings.BrushType
    {
      get
      {
        BrushType type;
        int selIndex = fillStyleCombo.SelectedIndex;
        switch (selIndex)
        {
        case 0:
        case 1:
        case 2:
          type = (BrushType)selIndex;
          break;
        default:
          type = BrushType.HatchBrush;
          break;
        }
        return type;
      }
    }

    HatchStyle IPaintSettings.HatchStyle
    {
      get
      {
        int index = fillStyleCombo.SelectedIndex;
        if (index < 3)
          index = 0;
        else
          index -= 3;

        return (HatchStyle)index;
      }
    }

    DashStyle IPaintSettings.LineStyle
    {
      get
      {
        return (DashStyle)lineStyleCombo.SelectedIndex;
      }
    }

    Image IPaintSettings.TextureBrushImage
    {
      get
      {
        return brushImageBox.Image;
      }
    }
    private void inverseLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Color temp = primColorBox.BackColor;
      primColorBox.BackColor = secColorBox.BackColor;
      secColorBox.BackColor = temp;
    }

    private void ColorBox_Click(object sender, EventArgs e)
    {
      PictureBox picBox = (PictureBox)sender;
      ColorDialog colorDlg = new ColorDialog();
      colorDlg.FullOpen = true;

      colorDlg.Color = picBox.BackColor;
      if (colorDlg.ShowDialog() == DialogResult.OK)
      {
        picBox.BackColor = colorDlg.Color;
      }
    }

    private void brushImageBox_Click(object sender, EventArgs e)
    {
      MessageBox.Show(imgContainer.DisplayRectangle.ToString());
      OpenFileDialog openDlg = new OpenFileDialog();
      openDlg.Filter = "Image Files .BMP .JPG .GIF .Png|*.BMP;*.JPG;*.GIF;*.PNG";
      if (openDlg.ShowDialog() == DialogResult.OK)
      {
        brushImageBox.Image = Image.FromFile(openDlg.FileName);
      }
    }
    #endregion

    #region menu
    private MenuController menuController;
    public void SetMenuController(MenuController menuController)
    {
      this.menuController = menuController;
    }

    private void imageClearMnu_Click(object sender, EventArgs e)
    {
      menuController.ClearImage(imageFile.Bitmap, settings.SecondaryColor, imageBox);
    }

    private void editCutMnu_Click(object sender, EventArgs e)
    {
      menuController.Cut();
    }

    private void editCopyMnu_Click(object sender, EventArgs e)
    {
      menuController.Copy();
    }

    private void editPasteMnu_Click(object sender, EventArgs e)
    {
      menuController.Paste();
    }

    private void fileNewMnu_Click(object sender, EventArgs e)
    {
      menuController.FileNew();
    }

    private void fileOpenMnu_Click(object sender, EventArgs e)
    {
      menuController.FileOpen();
    }

    private void fileSaveMnu_Click(object sender, EventArgs e)
    {
      menuController.FileSave();
    }

    private void fileSaveAsMnu_Click(object sender, EventArgs e)
    {
      menuController.FileSaveAs();
    }

    private void fileExitMnu_Click(object sender, EventArgs e)
    {
      menuController.FileExit();
    }

    private void Blur_Blur_Click(object sender, EventArgs e)
    {
      menuController.BlurBlur(toolArgs);
    }

    private void Monochrome_Click(object sender, EventArgs e)
    {
      menuController.ImageMonochrome(toolArgs);
    }
    #endregion
  }
}