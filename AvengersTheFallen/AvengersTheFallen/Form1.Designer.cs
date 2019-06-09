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
            this.SuspendLayout();
            // 
            // timerGenerateObstacles
            // 
            this.timerGenerateObstacles.Interval = 800;
            this.timerGenerateObstacles.Tick += new System.EventHandler(this.TimerGenerateObstacles_Tick);
            // 
            // timerMapMove
            // 
            this.timerMapMove.Interval = 50;
            this.timerMapMove.Tick += new System.EventHandler(this.TimerMapMove_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1312, 567);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Avengers: The Fallen";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);

		}

        #endregion

        private System.Windows.Forms.Timer timerGenerateObstacles;
        private System.Windows.Forms.Timer timerMapMove;
    }
}

