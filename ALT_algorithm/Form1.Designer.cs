using System.Windows.Forms;

namespace ALT_algorithm
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.ControlPanel = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.animateButton = new System.Windows.Forms.Button();
            this.searchButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.toBox = new System.Windows.Forms.ComboBox();
            this.fromBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.examinedNodes = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.MemoryTitle = new System.Windows.Forms.Label();
            this.MemoryUsage = new System.Windows.Forms.Label();
            this.timeValue = new System.Windows.Forms.Label();
            this.timeTitle = new System.Windows.Forms.Label();
            this.graphPanel = new System.Windows.Forms.Panel();
            this.AnimateTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.ControlPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.graphPanel);
            this.splitContainer1.Size = new System.Drawing.Size(1004, 600);
            this.splitContainer1.SplitterDistance = 400;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.ControlPanel);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.panel2);
            this.splitContainer2.Size = new System.Drawing.Size(400, 600);
            this.splitContainer2.SplitterDistance = 380;
            this.splitContainer2.TabIndex = 0;
            // 
            // ControlPanel
            // 
            this.ControlPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ControlPanel.Controls.Add(this.button1);
            this.ControlPanel.Controls.Add(this.label4);
            this.ControlPanel.Controls.Add(this.checkedListBox1);
            this.ControlPanel.Controls.Add(this.animateButton);
            this.ControlPanel.Controls.Add(this.searchButton);
            this.ControlPanel.Controls.Add(this.label1);
            this.ControlPanel.Controls.Add(this.toBox);
            this.ControlPanel.Controls.Add(this.fromBox);
            this.ControlPanel.Controls.Add(this.label2);
            this.ControlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ControlPanel.Location = new System.Drawing.Point(0, 0);
            this.ControlPanel.Name = "ControlPanel";
            this.ControlPanel.Size = new System.Drawing.Size(400, 380);
            this.ControlPanel.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.Location = new System.Drawing.Point(240, 312);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 30);
            this.button1.TabIndex = 11;
            this.button1.Text = "Refresh";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label4.Location = new System.Drawing.Point(104, 259);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(153, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Landmark algorithm:";
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.BackColor = System.Drawing.SystemColors.Control;
            this.checkedListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "Planar with border points",
            "Planar with inside points",
            "Random",
            "Shortest points from a random point"});
            this.checkedListBox1.SetItemChecked(0, true);
            this.checkedListBox1.Location = new System.Drawing.Point(11, 301);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(223, 60);
            this.checkedListBox1.TabIndex = 6;
            this.checkedListBox1.ThreeDCheckBoxes = true;
            this.checkedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            // 
            // animateButton
            // 
            this.animateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.animateButton.Location = new System.Drawing.Point(203, 175);
            this.animateButton.Name = "animateButton";
            this.animateButton.Size = new System.Drawing.Size(129, 28);
            this.animateButton.TabIndex = 5;
            this.animateButton.Text = "Animate";
            this.animateButton.UseVisualStyleBackColor = true;
            this.animateButton.Click += new System.EventHandler(this.animateButton_Click);
            // 
            // searchButton
            // 
            this.searchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.searchButton.Location = new System.Drawing.Point(48, 175);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(135, 28);
            this.searchButton.TabIndex = 4;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(44, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "From:";
            // 
            // toBox
            // 
            this.toBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.toBox.FormattingEnabled = true;
            this.toBox.ItemHeight = 20;
            this.toBox.Location = new System.Drawing.Point(48, 125);
            this.toBox.Name = "toBox";
            this.toBox.Size = new System.Drawing.Size(284, 28);
            this.toBox.TabIndex = 3;
            // 
            // fromBox
            // 
            this.fromBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.fromBox.FormattingEnabled = true;
            this.fromBox.ItemHeight = 20;
            this.fromBox.Location = new System.Drawing.Point(48, 43);
            this.fromBox.Name = "fromBox";
            this.fromBox.Size = new System.Drawing.Size(284, 28);
            this.fromBox.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(44, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "To:";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.examinedNodes);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.MemoryTitle);
            this.panel2.Controls.Add(this.MemoryUsage);
            this.panel2.Controls.Add(this.timeValue);
            this.panel2.Controls.Add(this.timeTitle);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(400, 216);
            this.panel2.TabIndex = 0;
            // 
            // examinedNodes
            // 
            this.examinedNodes.AutoSize = true;
            this.examinedNodes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.examinedNodes.Location = new System.Drawing.Point(34, 174);
            this.examinedNodes.Name = "examinedNodes";
            this.examinedNodes.Size = new System.Drawing.Size(18, 20);
            this.examinedNodes.TabIndex = 5;
            this.examinedNodes.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(7, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(250, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Legutóbb vizsgált csúcsok száma:";
            // 
            // MemoryTitle
            // 
            this.MemoryTitle.AutoSize = true;
            this.MemoryTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MemoryTitle.Location = new System.Drawing.Point(3, 9);
            this.MemoryTitle.Name = "MemoryTitle";
            this.MemoryTitle.Size = new System.Drawing.Size(207, 20);
            this.MemoryTitle.TabIndex = 3;
            this.MemoryTitle.Text = "Aktuális memória használat:";
            // 
            // MemoryUsage
            // 
            this.MemoryUsage.AutoSize = true;
            this.MemoryUsage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.MemoryUsage.Location = new System.Drawing.Point(33, 41);
            this.MemoryUsage.Name = "MemoryUsage";
            this.MemoryUsage.Size = new System.Drawing.Size(41, 20);
            this.MemoryUsage.TabIndex = 2;
            this.MemoryUsage.Text = "0 kB";
            // 
            // timeValue
            // 
            this.timeValue.AutoSize = true;
            this.timeValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.timeValue.Location = new System.Drawing.Point(33, 109);
            this.timeValue.Name = "timeValue";
            this.timeValue.Size = new System.Drawing.Size(43, 20);
            this.timeValue.TabIndex = 1;
            this.timeValue.Text = "0 ms";
            // 
            // timeTitle
            // 
            this.timeTitle.AutoSize = true;
            this.timeTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline);
            this.timeTitle.Location = new System.Drawing.Point(3, 73);
            this.timeTitle.Name = "timeTitle";
            this.timeTitle.Size = new System.Drawing.Size(152, 20);
            this.timeTitle.TabIndex = 0;
            this.timeTitle.Text = "Legutóbbi futási idő:";
            // 
            // graphPanel
            // 
            this.graphPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.graphPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphPanel.Location = new System.Drawing.Point(0, 0);
            this.graphPanel.Name = "graphPanel";
            this.graphPanel.Size = new System.Drawing.Size(600, 600);
            this.graphPanel.TabIndex = 0;
            this.graphPanel.Click += new System.EventHandler(this.graphPanel_Click);
            this.graphPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.graphPanel_Paint);
            this.graphPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouse_Down);
            this.graphPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mouse_Move);
            this.graphPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouse_Up);
            this.graphPanel.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.mouse_Wheel);
            // 
            // AnimateTimer
            // 
            this.AnimateTimer.Tick += new System.EventHandler(this.AnimateTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 600);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "ALT algorithm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ControlPanel.ResumeLayout(false);
            this.ControlPanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        private void onPaint()
        {
            
        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.ComboBox toBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox fromBox;
        private System.Windows.Forms.Panel graphPanel;
        private Panel ControlPanel;
        private SplitContainer splitContainer2;
        private Panel panel2;
        private Button animateButton;
        private Label MemoryTitle;
        private Label MemoryUsage;
        private Label timeValue;
        private Label timeTitle;
        private Label label3;
        private Label examinedNodes;
        private Timer AnimateTimer;
        private Label label4;
        private CheckedListBox checkedListBox1;
        private Button button1;

    }
}

