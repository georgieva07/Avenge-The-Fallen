namespace AvengersTheFallen
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
            this.timerGenerateObstacles = new System.Windows.Forms.Timer(this.components);
            this.timerMapMove = new System.Windows.Forms.Timer(this.components);
            this.timerEnemyShoot = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new AvengersTheFallen.DrawPanel();
            this.SuspendLayout();
            // 
            // timerGenerateObstacles
            // 
            this.timerGenerateObstacles.Tick += new System.EventHandler(this.TimerGenerateObstacles_Tick);
            // 
            // timerMapMove
            // 
            this.timerMapMove.Interval = 33;
            this.timerMapMove.Tick += new System.EventHandler(this.TimerMapMove_Tick);
            // 
            // timerEnemyShoot
            // 
            this.timerEnemyShoot.Interval = 1500;
            this.timerEnemyShoot.Tick += new System.EventHandler(this.TimerEnemyShoot_Tick);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(132, 28);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1065, 434);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel1_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(1312, 567);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Avengers: The Fallen";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);

		}

        #endregion

        private System.Windows.Forms.Timer timerGenerateObstacles;
        private System.Windows.Forms.Timer timerMapMove;
        private DrawPanel panel1;
        private System.Windows.Forms.Timer timerEnemyShoot;
    }
}
