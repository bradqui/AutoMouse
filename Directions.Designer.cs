namespace AutoMouse
{
    partial class Directions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Directions));
            this.richTxtDirections = new System.Windows.Forms.RichTextBox();
            this.bntClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTxtDirections
            // 
            this.richTxtDirections.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTxtDirections.Location = new System.Drawing.Point(12, 12);
            this.richTxtDirections.Name = "richTxtDirections";
            this.richTxtDirections.Size = new System.Drawing.Size(557, 393);
            this.richTxtDirections.TabIndex = 0;
            this.richTxtDirections.Text = resources.GetString("richTxtDirections.Text");
            // 
            // bntClose
            // 
            this.bntClose.BackColor = System.Drawing.SystemColors.Control;
            this.bntClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bntClose.Location = new System.Drawing.Point(446, 411);
            this.bntClose.Name = "bntClose";
            this.bntClose.Size = new System.Drawing.Size(123, 30);
            this.bntClose.TabIndex = 1;
            this.bntClose.Text = "&Close";
            this.bntClose.UseVisualStyleBackColor = false;
            this.bntClose.Click += new System.EventHandler(this.bntClose_Click);
            // 
            // Directions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(581, 453);
            this.Controls.Add(this.bntClose);
            this.Controls.Add(this.richTxtDirections);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Directions";
            this.Text = "Directions";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTxtDirections;
        private System.Windows.Forms.Button bntClose;
    }
}