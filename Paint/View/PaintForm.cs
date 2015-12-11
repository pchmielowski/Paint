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
    private ImageFile imageFile;
    private ToolArgs toolArgs;
    private Tool curTool;
    private IPaintSettings settings;

    private MouseEventManager mouseManager;

    public PaintForm()
    {
      InitializeComponent();
      toolsBar.ImageList = imageList;
      settings = (IPaintSettings)this;

      mouseManager = new MouseEventManager();
    }

    private void toolsBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
    {
      curTool.UnloadTool();
      ToolBarButton curButton = e.Button;

      SetToolBarButtonsState(curButton);

      ToolController toolController = new ToolController(toolArgs, mouseManager);
      toolController.ButtonClicked(curButton.Name);

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

    private void imageBox_Paint(object sender, PaintEventArgs e)
    {
      //e.Graphics.DrawImageUnscaledAndClipped(imageFile.Bitmap, new Rectangle(new Point(0,0),imageFile.Bitmap.Size));
      Rectangle clipRect = e.ClipRectangle;
      Bitmap b = toolArgs.bitmap.Clone(clipRect, toolArgs.bitmap.PixelFormat);
      e.Graphics.DrawImageUnscaledAndClipped(b, clipRect);
      b.Dispose();
    }

    private void PaintForm_Load(object sender, EventArgs e)
    {
      FillFillStyleList();

      FillShapeStyleList();

      FillWidthList();

      FillGradientStyleList();

      // default texture brush image
      brushImageBox.Image = new Bitmap(20, 20);

      // default image
      imageFile = new ImageFile(new Size(500, 500), Color.White);
      ShowImage();
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

    private void inverseLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Color temp = primColorBox.BackColor;
      primColorBox.BackColor = secColorBox.BackColor;
      secColorBox.BackColor = temp;
    }

    private void imageClearMnu_Click(object sender, EventArgs e)
    {
      Graphics.FromImage(imageFile.Bitmap).Clear(settings.SecondaryColor);
      imageBox.Invalidate();
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

    private void editCutMnu_Click(object sender, EventArgs e)
    {
      curTool.UnloadTool();
      //curTool = new ClipboardTool(toolArgs, ClipboardAction.Cut);
      SetToolBarButtonsState(arrowBtn);
    }

    private void editCopyMnu_Click(object sender, EventArgs e)
    {
      curTool.UnloadTool();
      //curTool = new ClipboardTool(toolArgs, ClipboardAction.Copy);
      SetToolBarButtonsState(arrowBtn);
    }

    private void editPasteMnu_Click(object sender, EventArgs e)
    {
      curTool.UnloadTool();
      //curTool = new ClipboardTool(toolArgs, ClipboardAction.Paste);
      SetToolBarButtonsState(arrowBtn);
    }

    private void fileNewMnu_Click(object sender, EventArgs e)
    {
      NewDialog newDlg = new NewDialog();
      if (newDlg.ShowDialog() == DialogResult.OK)
      {
        imageFile = new ImageFile(newDlg.ImageSize, newDlg.imageBackColor);
        ShowImage();
      }
    }

    private void fileOpenMnu_Click(object sender, EventArgs e)
    {
      OpenFileDialog fileDialog = new OpenFileDialog();
      fileDialog.Filter = "Image Files .BMP .JPG .GIF .Png|*.BMP;*.JPG;*.GIF;*.PNG";
      if (fileDialog.ShowDialog() == DialogResult.OK)
      {
        if (imageFile.Open(fileDialog.FileName))
        {
          ShowImage();
        }
        else
        {
          MessageBox.Show("Error");
        }
      }
    }

    private void fileSaveMnu_Click(object sender, EventArgs e)
    {
      if (imageFile.FileName != null)
      {
        if (!imageFile.Save(imageFile.FileName))
          MessageBox.Show("Error");
        else
          ShowImage();
      }
      else
      {
        fileSaveAsMnu_Click(sender, e);
      }
    }

    private void fileSaveAsMnu_Click(object sender, EventArgs e)
    {
      SaveFileDialog saveDlg = new SaveFileDialog();
      saveDlg.Filter = "Bitmap (*.BMP)|*.BMP";
      if (saveDlg.ShowDialog() == DialogResult.OK)
      {
        if (!imageFile.Save(saveDlg.FileName))
          MessageBox.Show("Error");
        else
          ShowImage();
      }
    }

    private void fileExitMnu_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }

    private void ShowImage()
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

    private void Blur_Blur_Click(object sender, EventArgs e)
    {
      FilterDialog fDialog = new FilterDialog("Gaussian Blur", new List<string>(new string[] { "Sigma", "Size" }));
      BlurImage((double)fDialog.inputBoxes["Sigma"].Value, (int)fDialog.inputBoxes["Size"].Value);
    }

    private void BlurImage(double sigma, int size)
    {
      AForge.Imaging.Filters.GaussianBlur filter = new AForge.Imaging.Filters.GaussianBlur(sigma, size);
      Bitmap image = toolArgs.bitmap;
      filter.ApplyInPlace(image);
      toolArgs.pictureBox.Invalidate();
    }

    private void Monochrome_Click(object sender, EventArgs e)
    {

      DesaturateImage(toolArgs.bitmap);
      Class1 c = new Class1();
      c.doDesaturate(toolArgs.bitmap);
      toolArgs.pictureBox.Invalidate();
    }

    private unsafe void DesaturateImage(Bitmap bitmap)
    {
      Rectangle bRect = new Rectangle(new System.Drawing.Point(0, 0), bitmap.Size);
      BitmapData bData = toolArgs.bitmap.LockBits(bRect, ImageLockMode.ReadWrite, bitmap.PixelFormat);

      int height = bitmap.Size.Height;
      int width = bitmap.Size.Width;
      int pixelSize = bData.Stride / bData.Width;

      for (int x = 0; x < 0; x++)
      {
        for (int y = 0; y < 0; y++)
        {
          byte* pixelBaseAddress = (byte*)bData.Scan0 + (y * bData.Stride) + (x * pixelSize);
          byte value = 0;
          const int NUM_CHANNELS = 3;
          for (int channelIdx = 0; channelIdx < NUM_CHANNELS; ++channelIdx)
          {
            value += (byte)(*pixelBaseAddress++ / NUM_CHANNELS);
          }
          pixelBaseAddress = (byte*)bData.Scan0 + (y * bData.Stride) + (x * pixelSize);

          Class1 c = new Class1();
          c.desaturate(NUM_CHANNELS, pixelBaseAddress, value);
        }
      }
      bitmap.UnlockBits(bData);

    }
  }

}