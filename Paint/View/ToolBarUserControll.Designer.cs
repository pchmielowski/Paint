namespace Paint.View
{
  partial class ToolBarUserControl
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.toolsBar = new System.Windows.Forms.ToolBar();
      this.arrowBtn = new System.Windows.Forms.ToolBarButton();
      this.lineBtn = new System.Windows.Forms.ToolBarButton();
      this.rectangleBtn = new System.Windows.Forms.ToolBarButton();
      this.ellipseBtn = new System.Windows.Forms.ToolBarButton();
      this.separator = new System.Windows.Forms.ToolBarButton();
      this.pencilBtn = new System.Windows.Forms.ToolBarButton();
      this.brushBtn = new System.Windows.Forms.ToolBarButton();
      this.fillBtn = new System.Windows.Forms.ToolBarButton();
      this.textBtn = new System.Windows.Forms.ToolBarButton();
      this.eraserBtn = new System.Windows.Forms.ToolBarButton();
      this.SuspendLayout();
      // 
      // toolsBar
      // 
      this.toolsBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.arrowBtn,
            this.lineBtn,
            this.rectangleBtn,
            this.ellipseBtn,
            this.separator,
            this.pencilBtn,
            this.brushBtn,
            this.fillBtn,
            this.textBtn,
            this.eraserBtn});
      this.toolsBar.Dock = System.Windows.Forms.DockStyle.Left;
      this.toolsBar.DropDownArrows = true;
      this.toolsBar.Location = new System.Drawing.Point(0, 0);
      this.toolsBar.MinimumSize = new System.Drawing.Size(30, 0);
      this.toolsBar.Name = "toolsBar";
      this.toolsBar.ShowToolTips = true;
      this.toolsBar.Size = new System.Drawing.Size(30, 360);
      this.toolsBar.TabIndex = 3;
      this.toolsBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolsBar_ButtonClick);
      // 
      // arrowBtn
      // 
      this.arrowBtn.ImageIndex = 8;
      this.arrowBtn.Name = "arrowBtn";
      this.arrowBtn.Pushed = true;
      // 
      // lineBtn
      // 
      this.lineBtn.ImageIndex = 0;
      this.lineBtn.Name = "lineBtn";
      // 
      // rectangleBtn
      // 
      this.rectangleBtn.ImageIndex = 1;
      this.rectangleBtn.Name = "rectangleBtn";
      // 
      // ellipseBtn
      // 
      this.ellipseBtn.ImageIndex = 4;
      this.ellipseBtn.Name = "ellipseBtn";
      // 
      // separator
      // 
      this.separator.Enabled = false;
      this.separator.Name = "separator";
      // 
      // pencilBtn
      // 
      this.pencilBtn.ImageIndex = 2;
      this.pencilBtn.Name = "pencilBtn";
      // 
      // brushBtn
      // 
      this.brushBtn.ImageIndex = 3;
      this.brushBtn.Name = "brushBtn";
      // 
      // fillBtn
      // 
      this.fillBtn.ImageIndex = 7;
      this.fillBtn.Name = "fillBtn";
      // 
      // textBtn
      // 
      this.textBtn.ImageIndex = 5;
      this.textBtn.Name = "textBtn";
      // 
      // eraserBtn
      // 
      this.eraserBtn.ImageIndex = 6;
      this.eraserBtn.Name = "eraserBtn";
      // 
      // ToolBarUserControll
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.toolsBar);
      this.Name = "ToolBarUserControll";
      this.Size = new System.Drawing.Size(24, 360);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ToolBar toolsBar;
    private System.Windows.Forms.ToolBarButton arrowBtn;
    private System.Windows.Forms.ToolBarButton lineBtn;
    private System.Windows.Forms.ToolBarButton rectangleBtn;
    private System.Windows.Forms.ToolBarButton ellipseBtn;
    private System.Windows.Forms.ToolBarButton separator;
    private System.Windows.Forms.ToolBarButton pencilBtn;
    private System.Windows.Forms.ToolBarButton brushBtn;
    private System.Windows.Forms.ToolBarButton fillBtn;
    private System.Windows.Forms.ToolBarButton textBtn;
    private System.Windows.Forms.ToolBarButton eraserBtn;
  }
}
