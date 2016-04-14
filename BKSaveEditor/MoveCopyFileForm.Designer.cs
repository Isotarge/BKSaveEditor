namespace BKSaveEditor {
	partial class MoveCopyFileForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.sourceGroupBox = new System.Windows.Forms.GroupBox();
			this.radioSourceSlot3 = new System.Windows.Forms.RadioButton();
			this.radioSourceSlot2 = new System.Windows.Forms.RadioButton();
			this.radioSourceSlot1 = new System.Windows.Forms.RadioButton();
			this.destGroupBox = new System.Windows.Forms.GroupBox();
			this.radioDestSlot3 = new System.Windows.Forms.RadioButton();
			this.radioDestSlot2 = new System.Windows.Forms.RadioButton();
			this.radioDestSlot1 = new System.Windows.Forms.RadioButton();
			this.btnMoveFile = new System.Windows.Forms.Button();
			this.btnCopyFile = new System.Windows.Forms.Button();
			this.sourceGroupBox.SuspendLayout();
			this.destGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// sourceGroupBox
			// 
			this.sourceGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.sourceGroupBox.Controls.Add(this.radioSourceSlot3);
			this.sourceGroupBox.Controls.Add(this.radioSourceSlot2);
			this.sourceGroupBox.Controls.Add(this.radioSourceSlot1);
			this.sourceGroupBox.Location = new System.Drawing.Point(9, 12);
			this.sourceGroupBox.Name = "sourceGroupBox";
			this.sourceGroupBox.Size = new System.Drawing.Size(156, 111);
			this.sourceGroupBox.TabIndex = 30;
			this.sourceGroupBox.TabStop = false;
			this.sourceGroupBox.Text = "Source";
			// 
			// radioSourceSlot3
			// 
			this.radioSourceSlot3.AutoSize = true;
			this.radioSourceSlot3.Enabled = false;
			this.radioSourceSlot3.Location = new System.Drawing.Point(6, 65);
			this.radioSourceSlot3.Name = "radioSourceSlot3";
			this.radioSourceSlot3.Size = new System.Drawing.Size(61, 17);
			this.radioSourceSlot3.TabIndex = 31;
			this.radioSourceSlot3.Text = "Kitchen";
			this.radioSourceSlot3.UseVisualStyleBackColor = true;
			// 
			// radioSourceSlot2
			// 
			this.radioSourceSlot2.AutoSize = true;
			this.radioSourceSlot2.Enabled = false;
			this.radioSourceSlot2.Location = new System.Drawing.Point(6, 42);
			this.radioSourceSlot2.Name = "radioSourceSlot2";
			this.radioSourceSlot2.Size = new System.Drawing.Size(70, 17);
			this.radioSourceSlot2.TabIndex = 30;
			this.radioSourceSlot2.Text = "Gameboy";
			this.radioSourceSlot2.UseVisualStyleBackColor = true;
			// 
			// radioSourceSlot1
			// 
			this.radioSourceSlot1.AutoSize = true;
			this.radioSourceSlot1.Checked = true;
			this.radioSourceSlot1.Enabled = false;
			this.radioSourceSlot1.Location = new System.Drawing.Point(6, 19);
			this.radioSourceSlot1.Name = "radioSourceSlot1";
			this.radioSourceSlot1.Size = new System.Drawing.Size(44, 17);
			this.radioSourceSlot1.TabIndex = 29;
			this.radioSourceSlot1.TabStop = true;
			this.radioSourceSlot1.Text = "Bed";
			this.radioSourceSlot1.UseVisualStyleBackColor = true;
			// 
			// destGroupBox
			// 
			this.destGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.destGroupBox.Controls.Add(this.radioDestSlot3);
			this.destGroupBox.Controls.Add(this.radioDestSlot2);
			this.destGroupBox.Controls.Add(this.radioDestSlot1);
			this.destGroupBox.Location = new System.Drawing.Point(176, 12);
			this.destGroupBox.Name = "destGroupBox";
			this.destGroupBox.Size = new System.Drawing.Size(156, 111);
			this.destGroupBox.TabIndex = 31;
			this.destGroupBox.TabStop = false;
			this.destGroupBox.Text = "Destination";
			// 
			// radioDestSlot3
			// 
			this.radioDestSlot3.AutoSize = true;
			this.radioDestSlot3.Location = new System.Drawing.Point(6, 65);
			this.radioDestSlot3.Name = "radioDestSlot3";
			this.radioDestSlot3.Size = new System.Drawing.Size(61, 17);
			this.radioDestSlot3.TabIndex = 31;
			this.radioDestSlot3.Text = "Kitchen";
			this.radioDestSlot3.UseVisualStyleBackColor = true;
			// 
			// radioDestSlot2
			// 
			this.radioDestSlot2.AutoSize = true;
			this.radioDestSlot2.Location = new System.Drawing.Point(6, 42);
			this.radioDestSlot2.Name = "radioDestSlot2";
			this.radioDestSlot2.Size = new System.Drawing.Size(70, 17);
			this.radioDestSlot2.TabIndex = 30;
			this.radioDestSlot2.Text = "Gameboy";
			this.radioDestSlot2.UseVisualStyleBackColor = true;
			// 
			// radioDestSlot1
			// 
			this.radioDestSlot1.AutoSize = true;
			this.radioDestSlot1.Checked = true;
			this.radioDestSlot1.Location = new System.Drawing.Point(6, 19);
			this.radioDestSlot1.Name = "radioDestSlot1";
			this.radioDestSlot1.Size = new System.Drawing.Size(44, 17);
			this.radioDestSlot1.TabIndex = 29;
			this.radioDestSlot1.TabStop = true;
			this.radioDestSlot1.Text = "Bed";
			this.radioDestSlot1.UseVisualStyleBackColor = true;
			// 
			// btnMoveFile
			// 
			this.btnMoveFile.Location = new System.Drawing.Point(9, 129);
			this.btnMoveFile.Name = "btnMoveFile";
			this.btnMoveFile.Size = new System.Drawing.Size(75, 23);
			this.btnMoveFile.TabIndex = 32;
			this.btnMoveFile.Text = "Move";
			this.btnMoveFile.UseVisualStyleBackColor = true;
			this.btnMoveFile.Click += new System.EventHandler(this.btnMoveFile_Click);
			// 
			// btnCopyFile
			// 
			this.btnCopyFile.Location = new System.Drawing.Point(90, 129);
			this.btnCopyFile.Name = "btnCopyFile";
			this.btnCopyFile.Size = new System.Drawing.Size(75, 23);
			this.btnCopyFile.TabIndex = 33;
			this.btnCopyFile.Text = "Copy";
			this.btnCopyFile.UseVisualStyleBackColor = true;
			this.btnCopyFile.Click += new System.EventHandler(this.btnCopyFile_Click);
			// 
			// MoveCopyFileForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(344, 161);
			this.Controls.Add(this.btnCopyFile);
			this.Controls.Add(this.btnMoveFile);
			this.Controls.Add(this.destGroupBox);
			this.Controls.Add(this.sourceGroupBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MoveCopyFileForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Move / Copy Files";
			this.Load += new System.EventHandler(this.MoveCopyFileForm_Load);
			this.sourceGroupBox.ResumeLayout(false);
			this.sourceGroupBox.PerformLayout();
			this.destGroupBox.ResumeLayout(false);
			this.destGroupBox.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox sourceGroupBox;
		private System.Windows.Forms.RadioButton radioSourceSlot3;
		private System.Windows.Forms.RadioButton radioSourceSlot2;
		private System.Windows.Forms.RadioButton radioSourceSlot1;
		private System.Windows.Forms.GroupBox destGroupBox;
		private System.Windows.Forms.RadioButton radioDestSlot3;
		private System.Windows.Forms.RadioButton radioDestSlot2;
		private System.Windows.Forms.RadioButton radioDestSlot1;
		private System.Windows.Forms.Button btnMoveFile;
		private System.Windows.Forms.Button btnCopyFile;
	}
}