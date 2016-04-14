using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BKSaveEditor {
	public partial class MoveCopyFileForm : Form {
		public frmMainForm parentForm;
		RadioButton[] sourceRadioButtons;
		RadioButton[] destRadioButtons;

		public MoveCopyFileForm() {
			InitializeComponent();
		}

		private void MoveCopyFileForm_Load(object sender, EventArgs e) {
			sourceRadioButtons = new RadioButton[] { radioSourceSlot1, radioSourceSlot2, radioSourceSlot3};
			destRadioButtons = new RadioButton[] { radioDestSlot1, radioDestSlot2, radioDestSlot3 };
			if (parentForm.FileLoaded) {
				for (int i = 0; i < parentForm.Slots.Length; i++) {
					sourceRadioButtons[i].Enabled = parentForm.Slots[i] != null;
				}
			}
		}

		private int getSourceIndex() {
			for (int i = 0; i < sourceRadioButtons.Length; i++) {
				if (sourceRadioButtons[i].Checked) {
					return i;
				}
			}
			return -1;
		}

		private int getDestIndex() {
			for (int i = 0; i < destRadioButtons.Length; i++) {
				if (destRadioButtons[i].Checked) {
					return i;
				}
			}
			return -1;
		}

		private void btnMoveFile_Click(object sender, EventArgs e) {
			int source = getSourceIndex();
			int dest = getDestIndex();
			if (source != dest && source != -1 && dest != -1) {
				bool allowAction = true;
				if (parentForm.Slots[dest] != null) {
					DialogResult result = MessageBox.Show("This will overwrite the file in the destination slot. Are you sure you want to continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
					allowAction = result == DialogResult.Yes;
				}
				if (allowAction) {
					parentForm.Slots[dest] = parentForm.Slots[source];
					parentForm.Slots[source] = null;
					parentForm.Slots[dest].setFileSelectIndex((byte)(dest + 1));
					MessageBox.Show("Done!");
					this.Close();
				}
			}
		}

		private void btnCopyFile_Click(object sender, EventArgs e) {
			int source = getSourceIndex();
			int dest = getDestIndex();
			if (source != dest && source != -1 && dest != -1) {
				bool allowAction = true;
				if (parentForm.Slots[dest] != null) {
					DialogResult result = MessageBox.Show("This will overwrite the file in the destination slot. Are you sure you want to continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
					allowAction = result == DialogResult.Yes;
				}
				if (allowAction) {
					parentForm.Slots[dest] = parentForm.Slots[source];
					parentForm.Slots[dest].setFileSelectIndex((byte)(dest + 1));
					parentForm.Slots[dest].physicalIndex = parentForm.getLowestUnusedPhysicalIndex();
					MessageBox.Show("Done!");
					this.Close();
				}
			}
		}
	}
}
