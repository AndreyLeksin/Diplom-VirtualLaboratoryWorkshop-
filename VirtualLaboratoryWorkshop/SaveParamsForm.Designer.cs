
namespace VirtualLaboratoryWorkshop
{
    partial class SaveParamsForm
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
            this.TitleParamtextBox = new System.Windows.Forms.TextBox();
            this.SaveParamBtn = new System.Windows.Forms.Button();
            this.Cancelbtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TitleParamtextBox
            // 
            this.TitleParamtextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TitleParamtextBox.Location = new System.Drawing.Point(105, 100);
            this.TitleParamtextBox.Multiline = true;
            this.TitleParamtextBox.Name = "TitleParamtextBox";
            this.TitleParamtextBox.Size = new System.Drawing.Size(575, 56);
            this.TitleParamtextBox.TabIndex = 0;
            this.TitleParamtextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // SaveParamBtn
            // 
            this.SaveParamBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.SaveParamBtn.BackColor = System.Drawing.Color.Lime;
            this.SaveParamBtn.FlatAppearance.BorderSize = 0;
            this.SaveParamBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveParamBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SaveParamBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SaveParamBtn.Location = new System.Drawing.Point(85, 210);
            this.SaveParamBtn.Name = "SaveParamBtn";
            this.SaveParamBtn.Size = new System.Drawing.Size(300, 60);
            this.SaveParamBtn.TabIndex = 5;
            this.SaveParamBtn.Text = "Сохранить";
            this.SaveParamBtn.UseVisualStyleBackColor = false;
            this.SaveParamBtn.Click += new System.EventHandler(this.SaveParamBtn_Click);
            // 
            // Cancelbtn
            // 
            this.Cancelbtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Cancelbtn.BackColor = System.Drawing.Color.Red;
            this.Cancelbtn.FlatAppearance.BorderSize = 0;
            this.Cancelbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Cancelbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Cancelbtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Cancelbtn.Location = new System.Drawing.Point(401, 210);
            this.Cancelbtn.Name = "Cancelbtn";
            this.Cancelbtn.Size = new System.Drawing.Size(300, 60);
            this.Cancelbtn.TabIndex = 6;
            this.Cancelbtn.Text = "Отмена";
            this.Cancelbtn.UseVisualStyleBackColor = false;
            this.Cancelbtn.Click += new System.EventHandler(this.Cancelbtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(99, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(449, 36);
            this.label1.TabIndex = 7;
            this.label1.Text = "Название набора параметров";
            // 
            // SaveParamsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.ClientSize = new System.Drawing.Size(781, 337);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Cancelbtn);
            this.Controls.Add(this.SaveParamBtn);
            this.Controls.Add(this.TitleParamtextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SaveParamsForm";
            this.Text = "Сохранение набора параметров";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TitleParamtextBox;
        private System.Windows.Forms.Button SaveParamBtn;
        private System.Windows.Forms.Button Cancelbtn;
        private System.Windows.Forms.Label label1;
    }
}