using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Paint
{


  public class BrushManager // TODO: move to another file
  {
    public Pen outlinePen_ { get; }
    public Brush fillBrush_ { get; set; }

    public BrushManager(Pen pen, Brush brush)
    {
      outlinePen_ = pen;
      fillBrush_ = brush;
    }


    //private void PreparePen()
    //{
    //  switch (args.settings.DrawMode)
    //  {
    //  case DrawMode.Outline:
    //    outlinePen_ = new Pen(GetBrush(false), args.settings.Width);
    //    outlinePen_.DashStyle = args.settings.LineStyle;
    //    Brush TRANSPARENT_BRUSH = new SolidBrush(Color.FromArgb(0, 0, 0, 255));
    //    fillBrush_ = TRANSPARENT_BRUSH;
    //    break;

    //  case DrawMode.Filled:
    //    fillBrush_ = GetBrush(false);
    //    Pen TRANSPARENT_PEN = new Pen(Color.FromArgb(0, 0, 0, 255)); ;
    //    outlinePen_ = TRANSPARENT_PEN;
    //    break;

    //  case DrawMode.Mixed:
    //    fillBrush_ = GetBrush(false);
    //    outlinePen_ = new Pen(GetBrush(true), args.settings.Width);
    //    outlinePen_.DashStyle = args.settings.LineStyle;
    //    break;

    //  case DrawMode.MixedWithSolidOutline:
    //    fillBrush_ = GetBrush(false);
    //    outlinePen_ = new Pen(args.settings.SecondaryColor, args.settings.Width);
    //    outlinePen_.DashStyle = args.settings.LineStyle;
    //    break;
    //  }
    //}
  }
}
