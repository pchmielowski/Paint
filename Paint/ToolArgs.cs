
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Paint
{
    public class ToolArgs
    {
        public Bitmap bitmap;
        public PictureBox pictureBox;
        public StatusBarPanel statusBarLeft;
        public StatusBarPanel statusBarRight;
        public IPaintSettings settings;

        public ToolArgs(Bitmap bmp, PictureBox picBox, StatusBarPanel p1, StatusBarPanel p2, IPaintSettings settings)
        {
            this.bitmap = bmp;
            this.pictureBox = picBox;
            this.statusBarLeft = p1;
            this.statusBarRight = p2;
            this.settings = settings;
        }
    }
}
