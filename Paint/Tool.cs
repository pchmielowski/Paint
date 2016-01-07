﻿using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using Paint.Model;

namespace Paint
{
  public abstract class Tool
  {
    protected Graphics g;

    protected Rectangle GetRectangleFromPoints(Point p1, Point p2)
    {
      Point oPoint;
      Rectangle rect;

      if ((p2.X > p1.X) && (p2.Y > p1.Y))
      {
        rect = new Rectangle(p1, new Size(p2.X - p1.X, p2.Y - p1.Y));
      }
      else if ((p2.X < p1.X) && (p2.Y < p1.Y))
      {
        rect = new Rectangle(p2, new Size(p1.X - p2.X, p1.Y - p2.Y));
      }
      else if ((p2.X > p1.X) && (p2.Y < p1.Y))
      {
        oPoint = new Point(p1.X, p2.Y);
        rect = new Rectangle(oPoint, new Size(p2.X - p1.X, p1.Y - oPoint.Y));
      }
      else
      {
        oPoint = new Point(p2.X, p1.Y);
        rect = new Rectangle(oPoint, new Size(p1.X - p2.X, p2.Y - p1.Y));
      }
      return rect;
    }
    public PaintModel model;
    public abstract void StopDrawing(MouseEventArgs e);
    public abstract void UpdateMousePosition(MouseEventArgs e);
    public abstract void StartDrawing(MouseEventArgs e, IStyle brushManager);

    public abstract void UnloadTool();

    protected void ClearOldShape(Brush db)
    {
      Point rightDown = new Point(500, 500);
      Rectangle delRect = GetRectangleFromPoints(new Point(0, 0), rightDown);
      g.FillRectangle(db, delRect);
    }
  }
}
