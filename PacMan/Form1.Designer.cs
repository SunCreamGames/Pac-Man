namespace Pac_Man
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
            this.Timer = new System.Timers.Timer();
            this.label1 = new System.Windows.Forms.Label();
            this.pfLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize) (this.Timer)).BeginInit();
            this.SuspendLayout();
            // 
            // Timer
            // 
            this.Timer.Enabled = true;
            this.Timer.Interval = 33D;
            this.Timer.SynchronizingObject = this;
            this.Timer.Elapsed += new System.Timers.ElapsedEventHandler(this.Timer_Elapsed);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.label1.Location = new System.Drawing.Point(236, 431);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(235, 53);
            this.label1.TabIndex = 0;
            this.label1.Text = "Score: ";
            // 
            // pfLabel
            // 
            this.pfLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (204)));
            this.pfLabel.Location = new System.Drawing.Point(236, 480);
            this.pfLabel.Name = "pfLabel";
            this.pfLabel.Size = new System.Drawing.Size(123, 39);
            this.pfLabel.TabIndex = 1;
            this.pfLabel.Text = "label2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Maroon;
            this.ClientSize = new System.Drawing.Size(1100, 1100);
            this.Controls.Add(this.pfLabel);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize) (this.Timer)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label pfLabel;

        private System.Windows.Forms.Label label1;

        private System.Timers.Timer Timer;

        #endregion
    }
}