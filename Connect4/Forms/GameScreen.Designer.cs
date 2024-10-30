using System;

namespace Connect4
{
    partial class GameScreen
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
            this.lblPlayerColour = new System.Windows.Forms.Label();
            this.picRed = new System.Windows.Forms.PictureBox();
            this.picYellow = new System.Windows.Forms.PictureBox();
            this.btnRestart = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.gridTemplate = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picYellow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPlayerColour
            // 
            this.lblPlayerColour.AutoSize = true;
            this.lblPlayerColour.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayerColour.Location = new System.Drawing.Point(436, 54);
            this.lblPlayerColour.Name = "lblPlayerColour";
            this.lblPlayerColour.Size = new System.Drawing.Size(79, 25);
            this.lblPlayerColour.TabIndex = 49;
            this.lblPlayerColour.Text = "Player:";
            // 
            // picRed
            // 
            this.picRed.BackColor = System.Drawing.Color.Red;
            this.picRed.Location = new System.Drawing.Point(436, 91);
            this.picRed.Name = "picRed";
            this.picRed.Size = new System.Drawing.Size(79, 60);
            this.picRed.TabIndex = 50;
            this.picRed.TabStop = false;
            // 
            // picYellow
            // 
            this.picYellow.BackColor = System.Drawing.Color.Yellow;
            this.picYellow.Location = new System.Drawing.Point(436, 91);
            this.picYellow.Name = "picYellow";
            this.picYellow.Size = new System.Drawing.Size(79, 60);
            this.picYellow.TabIndex = 51;
            this.picYellow.TabStop = false;
            this.picYellow.Visible = false;
            // 
            // btnRestart
            // 
            this.btnRestart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestart.Location = new System.Drawing.Point(436, 262);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(172, 76);
            this.btnRestart.TabIndex = 52;
            this.btnRestart.TabStop = false;
            this.btnRestart.Text = "Restart Game";
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.BtnRestart_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(543, 12);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(93, 45);
            this.btnExit.TabIndex = 53;
            this.btnExit.TabStop = false;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Visible = false;
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // gridTemplate
            // 
            this.gridTemplate.BackColor = System.Drawing.Color.White;
            this.gridTemplate.Location = new System.Drawing.Point(58, 54);
            this.gridTemplate.Name = "gridTemplate";
            this.gridTemplate.Size = new System.Drawing.Size(42, 42);
            this.gridTemplate.TabIndex = 54;
            this.gridTemplate.TabStop = false;
            this.gridTemplate.Tag = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(65, 357);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(272, 20);
            this.label1.TabIndex = 55;
            this.label1.Text = "A        B        C       D        E       F        G";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(34, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 240);
            this.label2.TabIndex = 56;
            this.label2.Text = "\r\n6\r\n\r\n5\r\n\r\n4\r\n\r\n3\r\n\r\n2\r\n\r\n1";
            this.label2.Visible = false;
            // 
            // GameScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ClientSize = new System.Drawing.Size(648, 537);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gridTemplate);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.picYellow);
            this.Controls.Add(this.picRed);
            this.Controls.Add(this.lblPlayerColour);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameScreen";
            this.Text = "Connect 4";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picYellow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTemplate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion
        private System.Windows.Forms.Label lblPlayerColour;
        private System.Windows.Forms.PictureBox picRed;
        private System.Windows.Forms.PictureBox picYellow;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.PictureBox gridTemplate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

