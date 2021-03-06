﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;
using Paint.View;

/*  If they're nested, the DocumentView contains a DocumentSelectionView 
    (could be a uses-a relationship, not a has-a relationship). 
    The DocumentView has GUI controls that provide a means for gestures 
    (mouse click, keyboard accelerators, voice command..?) to Execute a Command 
    (see Command pattern) that is observed by the DocumentController. 
    The DocumentSelectionView's GUI controls have Commands for the DocumentSelectionController.
    All of the views are active (visible); all of the associated controllers are ready 
    to observe Command Executions, then update the Model, thus updating the View(s).
*/

namespace Paint
{
  public partial class PaintForm : Form, IPictureView
  {
    public IToolBarView toolBarView;
    public PaintForm()
    {
      InitializeComponent();
      toolsBar.ImageList = imageList;

      toolBarView = toolBarUserControll;
    }

    #region ToolBar
    private void toolsBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
    {
      ToolBarButton curButton = e.Button;
      SetToolBarButtonsState(curButton);
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

    #region PictureView
    private Bitmap bitmap;
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
      bitmap = imageFile.Bitmap;

      SetToolBarButtonsState(arrowBtn);
    }

    private void imageBox_Paint(object sender, PaintEventArgs e)
    {
      Rectangle clipRect = e.ClipRectangle;
      Bitmap b = bitmap.Clone(clipRect, bitmap.PixelFormat);
      e.Graphics.DrawImageUnscaledAndClipped(b, clipRect);
      b.Dispose();
    }
    #endregion

    #region SettingsPanelView
    private void PaintForm_Load(object sender, EventArgs e)
    {
      FillFillStyleList();
      FillShapeStyleList();
      FillWidthList();
      FillGradientStyleList();

      brushImageBox.Image = new Bitmap(20, 20);
    }

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

    //DrawMode IPaintSettings.DrawMode
    //{
    //  get
    //  {
    //    return (DrawMode)shapeStyleCombo.SelectedIndex;
    //  }
    //}

    //int IPaintSettings.Width
    //{
    //  get
    //  {
    //    return Int32.Parse(widthCombo.Text);
    //  }
    //}

    //LinearGradientMode IPaintSettings.GradientOrientation
    //{
    //  get
    //  {
    //    return (LinearGradientMode)gradientStyleCombo.SelectedIndex;
    //  }
    //}

    //Color IPaintSettings.PrimaryColor
    //{
    //  get
    //  {
    //    return primColorBox.BackColor;
    //  }
    //}

    //Color IPaintSettings.SecondaryColor
    //{
    //  get
    //  {
    //    return secColorBox.BackColor;
    //  }
    //}

    //BrushType IPaintSettings.BrushType
    //{
    //  get
    //  {
    //    BrushType type;
    //    int selIndex = fillStyleCombo.SelectedIndex;
    //    switch (selIndex)
    //    {
    //    case 0:
    //    case 1:
    //    case 2:
    //      type = (BrushType)selIndex;
    //      break;
    //    default:
    //      type = BrushType.HatchBrush;
    //      break;
    //    }
    //    return type;
    //  }
    //}

    //HatchStyle IPaintSettings.HatchStyle
    //{
    //  get
    //  {
    //    int index = fillStyleCombo.SelectedIndex;
    //    if (index < 3)
    //      index = 0;
    //    else
    //      index -= 3;

    //    return (HatchStyle)index;
    //  }
    //}

    //DashStyle IPaintSettings.LineStyle
    //{
    //  get
    //  {
    //    return (DashStyle)lineStyleCombo.SelectedIndex;
    //  }
    //}

    //Image IPaintSettings.TextureBrushImage
    //{
    //  get
    //  {
    //    return brushImageBox.Image;
    //  }
    //}

    public PictureBox PictureBox
    {
      get
      {
        return imageBox;
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

    #region MenuView
    private MenuController menuController;
    public void SetMenuController(MenuController menuController)
    {
      this.menuController = menuController;
    }

    private void imageClearMnu_Click(object sender, EventArgs e)
    {
      //menuController.ClearImage(imageFile.Bitmap, settings.SecondaryColor, imageBox);
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
      menuController.BlurBlur();
    }

    private void Monochrome_Click(object sender, EventArgs e)
    {
      menuController.ImageMonochrome();
    }
    #endregion
  }
}