namespace LevelEditor
{
    partial class LevelForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LevelForm));
            this.openGLControl1 = new SharpGL.OpenGLControl();
            this.brushSize = new System.Windows.Forms.TrackBar();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.forest = new System.Windows.Forms.Button();
            this.concreteWall = new System.Windows.Forms.Button();
            this.brickWall = new System.Windows.Forms.Button();
            this.water = new System.Windows.Forms.Button();
            this.ice = new System.Windows.Forms.Button();
            this.Base = new System.Windows.Forms.Button();
            this.eraser = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pointEnterPlayer = new System.Windows.Forms.Button();
            this.pointEnterEnemy = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.clear = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.brushSize)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openGLControl1
            // 
            this.openGLControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.openGLControl1.DrawFPS = false;
            this.openGLControl1.FrameRate = 28;
            this.openGLControl1.Location = new System.Drawing.Point(0, 29);
            this.openGLControl1.Name = "openGLControl1";
            this.openGLControl1.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl1.RenderContextType = SharpGL.RenderContextType.DIBSection;
            this.openGLControl1.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl1.Size = new System.Drawing.Size(512, 512);
            this.openGLControl1.TabIndex = 0;
            this.openGLControl1.OpenGLInitialized += new System.EventHandler(this.openGLControl1_OpenGLInitialized);
            this.openGLControl1.OpenGLDraw += new SharpGL.RenderEventHandler(this.openGLControl1_OpenGLDraw);
            this.openGLControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.openGLControl1_MouseDown);
            this.openGLControl1.MouseEnter += new System.EventHandler(this.openGLControl1_MouseEnter);
            this.openGLControl1.MouseLeave += new System.EventHandler(this.openGLControl1_MouseLeave);
            this.openGLControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.openGLControl1_MouseMove);
            this.openGLControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.openGLControl1_MouseUp);
            // 
            // brushSize
            // 
            this.brushSize.Location = new System.Drawing.Point(565, 326);
            this.brushSize.Maximum = 3;
            this.brushSize.Minimum = 1;
            this.brushSize.Name = "brushSize";
            this.brushSize.Size = new System.Drawing.Size(104, 45);
            this.brushSize.TabIndex = 3;
            this.brushSize.Value = 1;
            this.brushSize.Scroll += new System.EventHandler(this.brushSize_Scroll);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.SystemColors.Control;
            this.checkBox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.checkBox1.Location = new System.Drawing.Point(574, 252);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(55, 17);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "сетка";
            this.checkBox1.UseVisualStyleBackColor = false;
            // 
            // forest
            // 
            this.forest.Location = new System.Drawing.Point(609, 47);
            this.forest.Name = "forest";
            this.forest.Size = new System.Drawing.Size(27, 27);
            this.forest.TabIndex = 5;
            this.forest.UseVisualStyleBackColor = true;
            this.forest.Click += new System.EventHandler(this.forest_Click);
            // 
            // concreteWall
            // 
            this.concreteWall.Location = new System.Drawing.Point(574, 47);
            this.concreteWall.Name = "concreteWall";
            this.concreteWall.Size = new System.Drawing.Size(29, 27);
            this.concreteWall.TabIndex = 2;
            this.concreteWall.UseVisualStyleBackColor = true;
            this.concreteWall.Click += new System.EventHandler(this.concreteWall_Click);
            // 
            // brickWall
            // 
            this.brickWall.Location = new System.Drawing.Point(539, 47);
            this.brickWall.Name = "brickWall";
            this.brickWall.Size = new System.Drawing.Size(29, 27);
            this.brickWall.TabIndex = 1;
            this.brickWall.UseVisualStyleBackColor = true;
            this.brickWall.Click += new System.EventHandler(this.brickWall_Click);
            // 
            // water
            // 
            this.water.Location = new System.Drawing.Point(642, 47);
            this.water.Name = "water";
            this.water.Size = new System.Drawing.Size(27, 27);
            this.water.TabIndex = 6;
            this.water.UseVisualStyleBackColor = true;
            this.water.Click += new System.EventHandler(this.water_Click);
            // 
            // ice
            // 
            this.ice.Location = new System.Drawing.Point(675, 47);
            this.ice.Name = "ice";
            this.ice.Size = new System.Drawing.Size(27, 27);
            this.ice.TabIndex = 7;
            this.ice.UseVisualStyleBackColor = true;
            this.ice.Click += new System.EventHandler(this.ice_Click);
            // 
            // Base
            // 
            this.Base.Location = new System.Drawing.Point(572, 109);
            this.Base.Name = "Base";
            this.Base.Size = new System.Drawing.Size(46, 42);
            this.Base.TabIndex = 8;
            this.Base.UseVisualStyleBackColor = true;
            this.Base.Click += new System.EventHandler(this.Base_Click);
            // 
            // eraser
            // 
            this.eraser.BackgroundImage = global::LevelEditor.Properties.Resources.eraser;
            this.eraser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.eraser.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.eraser.Location = new System.Drawing.Point(572, 181);
            this.eraser.Name = "eraser";
            this.eraser.Size = new System.Drawing.Size(46, 41);
            this.eraser.TabIndex = 9;
            this.eraser.UseVisualStyleBackColor = true;
            this.eraser.Click += new System.EventHandler(this.eraser_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(724, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.openToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.saveToolStripMenuItem.Text = "Соранить";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.openToolStripMenuItem.Text = "Открыть";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // pointEnterPlayer
            // 
            this.pointEnterPlayer.Location = new System.Drawing.Point(642, 109);
            this.pointEnterPlayer.Name = "pointEnterPlayer";
            this.pointEnterPlayer.Size = new System.Drawing.Size(46, 42);
            this.pointEnterPlayer.TabIndex = 11;
            this.pointEnterPlayer.UseVisualStyleBackColor = true;
            this.pointEnterPlayer.Click += new System.EventHandler(this.pointEnterPlayer_Click);
            // 
            // pointEnterEnemy
            // 
            this.pointEnterEnemy.Location = new System.Drawing.Point(642, 180);
            this.pointEnterEnemy.Name = "pointEnterEnemy";
            this.pointEnterEnemy.Size = new System.Drawing.Size(46, 42);
            this.pointEnterEnemy.TabIndex = 12;
            this.pointEnterEnemy.UseVisualStyleBackColor = true;
            this.pointEnterEnemy.Click += new System.EventHandler(this.pointEnterEnemy_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(571, 298);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "размер кисти";
            // 
            // clear
            // 
            this.clear.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.clear.Location = new System.Drawing.Point(590, 377);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(79, 30);
            this.clear.TabIndex = 14;
            this.clear.Text = "очистить";
            this.clear.UseVisualStyleBackColor = true;
            this.clear.Click += new System.EventHandler(this.clear_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(565, 432);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "label2";
            // 
            // LevelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(724, 547);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.clear);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pointEnterEnemy);
            this.Controls.Add(this.pointEnterPlayer);
            this.Controls.Add(this.eraser);
            this.Controls.Add(this.Base);
            this.Controls.Add(this.ice);
            this.Controls.Add(this.water);
            this.Controls.Add(this.forest);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.brushSize);
            this.Controls.Add(this.concreteWall);
            this.Controls.Add(this.brickWall);
            this.Controls.Add(this.openGLControl1);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "LevelForm";
            this.Text = "Редактор уровней";
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.brushSize)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SharpGL.OpenGLControl openGLControl1;
        private System.Windows.Forms.Button brickWall;
        private System.Windows.Forms.Button concreteWall;
        private System.Windows.Forms.TrackBar brushSize;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button forest;
        private System.Windows.Forms.Button water;
        private System.Windows.Forms.Button ice;
        private System.Windows.Forms.Button Base;
        private System.Windows.Forms.Button eraser;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.Button pointEnterPlayer;
        private System.Windows.Forms.Button pointEnterEnemy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button clear;
        private System.Windows.Forms.Label label2;
    }
}

