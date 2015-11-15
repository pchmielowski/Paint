using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace Paint
{
  class RectangleDrawer
  {
    protected Pen outlinePen_;
    protected Brush fillBrush_;
    private IPaintSettings settings_; // to delete in future
    private Graphics g_;
    public RectangleDrawer(Graphics g, IPaintSettings settings,
      Pen outlinePen, Brush fillBrush)
    {
      g_ = g;
      settings_ = settings;
      outlinePen_ = outlinePen;
      fillBrush_ = fillBrush;
    }

    public void DrawRectangle(Rectangle rect,
      DrawMode drawMode)
    {
      if (fillBrush_ is LinearGradientBrush)
      {
        fillBrush_ = UpdateGradientBrush(rect, fillBrush_);
      }

      switch (drawMode)
      {
      case DrawMode.Outline:
        g_.DrawRectangle(outlinePen_, rect);
        break;

      case DrawMode.Filled:
        g_.FillRectangle(fillBrush_, rect);
        break;

      case DrawMode.Mixed:
        g_.FillRectangle(fillBrush_, rect);
        g_.DrawRectangle(outlinePen_, rect);
        break;

      case DrawMode.MixedWithSolidOutline:
        g_.FillRectangle(fillBrush_, rect);
        g_.DrawRectangle(outlinePen_, rect);
        break;
      }
    }

    private Brush UpdateGradientBrush(Rectangle rect, Brush fillBrush)
    {
      if ((rect.Width == 0) || (rect.Height == 0))
        return fillBrush;

      fillBrush = new LinearGradientBrush(rect,
            settings_.PrimaryColor,
            settings_.SecondaryColor,
            settings_.GradiantStyle);
      return fillBrush;
    }


  }
}
