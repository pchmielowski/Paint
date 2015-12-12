using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Paint
{
  public class MenuController
  {
    private PaintForm view;
    public MenuController(PaintForm view)
    {
      this.view = view;
      view.SetMenuController(this);
    }

    internal void ClearImage(Bitmap bitmap, Color secondaryColor, PictureBox imageBox)
    {
      Graphics.FromImage(bitmap).Clear(secondaryColor);
      imageBox.Invalidate();
    }

    internal void Paste()
    {
      throw new NotImplementedException();
    }

    internal void Copy()
    {
      throw new NotImplementedException();
    }

    internal void Cut()
    {
      throw new NotImplementedException();
    }

    internal void FileNew()
    {
      NewDialog newDlg = new NewDialog();
      if (newDlg.ShowDialog() == DialogResult.OK)
      {
        view.ShowImage(new ImageFile(newDlg.ImageSize, newDlg.imageBackColor));
      }
    }

    internal void FileOpen()
    {
      OpenFileDialog fileDialog = new OpenFileDialog();
      fileDialog.Filter = "Image Files .BMP .JPG .GIF .Png|*.BMP;*.JPG;*.GIF;*.PNG";
      if (fileDialog.ShowDialog() == DialogResult.OK)
      {
        ImageFile imageFile = new ImageFile(new Size(500, 500), Color.White);
        if (imageFile.Open(fileDialog.FileName))
        {
          view.ShowImage(imageFile);
        }
        else
        {
          MessageBox.Show("Error");
        }
      }
    }

    internal void FileSave()
    {
      ImageFile imageFile = new ImageFile(new Size(500, 500), Color.White);
      throw new Exception("Bug! It has to read the image (from model maybe?)");

      string imageFileName = imageFile.FileName;
      if (imageFileName != null)
      {
        if (!imageFile.Save(imageFileName))
          MessageBox.Show("Error");
        else
          view.ShowImage(imageFile); // is it necessary?
      }
      else
      {
        FileSaveAs();
      }
    }

    internal void FileSaveAs()
    {
      SaveFileDialog saveDlg = new SaveFileDialog();
      saveDlg.Filter = "Bitmap (*.BMP)|*.BMP";
      if (saveDlg.ShowDialog() == DialogResult.OK)
      {
        // TODO: reuse code from fileSaveMnu_Click
        //if (!imageFile.Save(saveDlg.FileName))
        //  MessageBox.Show("Error");
        //else
        //  ShowImage();
      }
    }

    internal void FileExit()
    {
      Application.Exit();
    }


    internal void BlurBlur(ToolArgs toolArgs)
    {
      FilterDialog fDialog =
            new FilterDialog("Gaussian Blur", new List<string>(new string[] { "Sigma", "Size" }));
      BlurImage((double)fDialog.inputBoxes["Sigma"].Value, (int)fDialog.inputBoxes["Size"].Value, toolArgs);
    }

    private void BlurImage(double sigma, int size, ToolArgs toolArgs)
    {
      AForge.Imaging.Filters.GaussianBlur filter = new AForge.Imaging.Filters.GaussianBlur(sigma, size);
      Bitmap image = toolArgs.bitmap;
      filter.ApplyInPlace(image);
      toolArgs.pictureBox.Invalidate();
    }

    internal void ImageMonochrome(ToolArgs toolArgs)
    {

      DesaturateImage(toolArgs);
      toolArgs.pictureBox.Invalidate();
    }
    private unsafe void DesaturateImage(ToolArgs toolArgs)
    {
      Rectangle bRect = new Rectangle(new System.Drawing.Point(0, 0), toolArgs.bitmap.Size);
      BitmapData bData = toolArgs.bitmap.LockBits(bRect, ImageLockMode.ReadWrite, toolArgs.bitmap.PixelFormat);

      int height = toolArgs.bitmap.Size.Height;
      int width = toolArgs.bitmap.Size.Width;
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
        }
      }
      toolArgs.bitmap.UnlockBits(bData);
    }
  }
}
