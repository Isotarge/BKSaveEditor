using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BKSaveEditor {
	public partial class frmMainForm : Form {

		public const int EEPROM_SIZE = 0x800;

		// File stuff
		public OpenFileDialog openEEPFileDialog;
		public FileStream EEPFile;
		public bool FileLoaded = false;

		// Slot stuff
		public BKSaveSlot[] Slots;
		public int SelectedSlot = 1;

		// Editor controls
		public TextBox[] GameTimeTextboxes;
		public CheckBox[] HoneycombCheckboxes;
		public CheckBox[] MovesCheckboxes;
		public TextBox[] NoteScoreTextboxes;
		public RadioButton[] RadioSlots;

		public frmMainForm() {
			InitializeComponent();

			// Setup the file open dialog
			openEEPFileDialog = new OpenFileDialog();
			openEEPFileDialog.Filter = "EEPROM files(*.eep, *.SaveRAM)|*.eep;*.SaveRAM|All files(*.*)|*.*";
			openEEPFileDialog.RestoreDirectory = true;

			GameTimeTextboxes = new TextBox[] {
				txtMMValue,
				txtTTCValue,
				txtCCValue,
				txtBGSValue,
				txtFPValue,
				txtGLValue,
				txtGVValue,
				txtCCWValue,
				txtRBBValue,
				txtMMMValue,
				txtSMValue
			};

			HoneycombCheckboxes = new CheckBox[] {
				chkHSM1,
				chkHMMM1,
				chkHMMM2,
				chkHSM2,
				chkHSM3,
				chkHSM4,
				chkHSM5,
				chkHSM6,
				chkHRBB1,
				chkHFP1,
				chkHFP2,
				chkHGV1,
				chkHGV2,
				chkHCCW1,
				chkHCCW2,
				chkHRBB2,
				chkHBGS1,
				chkHMM1,
				chkHMM2,
				chkHTTC1,
				chkHTTC2,
				chkHCC1,
				chkHCC2,
				chkHBGS2
			};

			// Add event handlers for honeycomb checkbox clicked
			foreach (CheckBox honeyCheckbox in HoneycombCheckboxes) {
				honeyCheckbox.Click += new System.EventHandler(honeycombCheckboxClicked);
			}

			MovesCheckboxes = new CheckBox[] {
				chkMovesBeakBarge,
				chkMovesBeakBomb,
				chkMovesBeakBuster,
				chkMovesUnknown4,
				chkMovesBearPunch,
				chkMovesClimbTrees,
				chkMovesEggs,
				chkMovesFeatheryFlap,
				chkMovesFlapFlip,
				chkMovesFlight,
				chkMovesHigherJump,
				chkMovesRatATatRap,
				chkMovesRoll,
				chkMovesShockSpringJump,
				chkMovesWadingBoots,
				chkMovesDive,
				chkMovesTalonTrot,
				chkMovesTurboTrainers,
				chkMovesWonderwing,
				chkMovesUnknown20,
				chkMovesUnknown21,
				chkMovesUnknown22,
				chkMovesUnknown23,
				chkMovesUnknown24,
				chkMovesUnknown25,
				chkMovesUnknown26,
				chkMovesUnknown27,
				chkMovesUnknown28,
				chkMovesUnknown29,
				chkMovesUnknown30,
				chkMovesUnknown31,
				chkMovesUnknown32
			};

			// Add event handlers for honeycomb checkbox clicked
			foreach (CheckBox movesCheckbox in MovesCheckboxes) {
				movesCheckbox.Click += new System.EventHandler(movesCheckboxClicked);
			}

			NoteScoreTextboxes = new TextBox[] {
				txtMMNoteScore,
				txtTTCNoteScore,
				txtCCNoteScore,
				txtBGSNoteScore,
				txtFPNoteScore,
				txtGVNoteScore,
				txtCCWNoteScore,
				txtRBBNoteScore,
				txtMMMNoteScore
			};

			RadioSlots = new RadioButton[] {
				radioSlot1,
				radioSlot2,
				radioSlot3
			};
		}

		public void refreshUI() {
			updateChecksumTextbox();
			updateGameTimeTextboxes();
			updateHoneycombCheckboxes();
			updateMovesCheckboxes();
			updateNoteScoreTextboxes();
			txtMumboTokensOnHand.Text = Slots[SelectedSlot].getMumboTokensOnHand().ToString();
			txtJiggiesOnHand.Text = Slots[SelectedSlot].getJiggiesOnHand().ToString();
		}

		private void refreshRadioButtons() {
			SelectedSlot = -1;
			for (int i = RadioSlots.Length - 1; i >= 0; i--) {
				RadioSlots[i].Enabled = false;
				if (Slots[i] != null) {
					RadioSlots[i].Enabled = true;
					RadioSlots[i].Checked = true;
					SelectedSlot = i;
				}
			}

			if (SelectedSlot != -1) {
				tabControl1.Enabled = true;
				txtEEPName.Text = openEEPFileDialog.FileName;
				btnMoveCopy.Enabled = true;
				btnSaveEEP.Enabled = true;
				this.AcceptButton = btnSaveEEP;
				refreshUI();
			} else {
				tabControl1.Enabled = false;
				btnSaveEEP.Enabled = false;
				FileLoaded = false;
			}
		}

		private void radioSlotChange(object sender, EventArgs e) {
			for (int i = 0; i < RadioSlots.Length; i++) {
				if (RadioSlots[i].Checked) {
					SelectedSlot = i;
				}
			}
			refreshUI();
		}

		private void txtEEPName_click(object sender, EventArgs e) {
			// Byte array to load the EEPROM data
			byte[] EEPROM = new byte[EEPROM_SIZE];

			if (openEEPFileDialog.ShowDialog() == DialogResult.OK) {
				try {
					if (FileLoaded && EEPFile != null) {
						EEPFile.Close();
						FileLoaded = false;
					}
					EEPFile = File.Open(openEEPFileDialog.FileName, FileMode.Open);
					EEPFile.Read(EEPROM, 0x00, EEPROM_SIZE);
					FileLoaded = true;
				} catch (Exception ex) {
					MessageBox.Show(ex.Message);
				}
			}

			if (FileLoaded) {
				Slots = new BKSaveSlot[3];
				for (int i = 0; i < 4; i++) {
					byte magic1 = EEPROM[BKSaveSlot.SLOT_SIZE * i];
					byte magic2 = EEPROM[BKSaveSlot.SLOT_SIZE * i + 1];
					if (magic1 > 0x00 && magic2 > 0x00) {
						Slots[magic2 - 1] = new BKSaveSlot(EEPROM, i, magic2);
					}
				}

				refreshRadioButtons();
			}
		}

		public void updateGameTimeTextboxes() {
			if (FileLoaded) {
				int total = 0;
				UInt16 currentLevel = 0;
				for (int i = 0; i < GameTimeTextboxes.Length; i++) {
					currentLevel = Slots[SelectedSlot].getGameTime(i);
					total += currentLevel;
                    GameTimeTextboxes[i].Text = currentLevel.ToString();
				}
				txtGameTimeTotal.Text = TimeSpan.FromSeconds((double)total).ToString();
			}
		}

		private void btnApplyGameTime_Click(object sender, EventArgs e) {
			validateGameTimeTextboxes();
		}

		public void validateGameTimeTextboxes() {
			if (FileLoaded) {
				try {
					UInt16[] gameTimeFromTextboxes = new UInt16[GameTimeTextboxes.Length];
					int total = 0;
					for (int i = 0; i < GameTimeTextboxes.Length; i++) {
						GameTimeTextboxes[i].Select();
						gameTimeFromTextboxes[i] = UInt16.Parse(GameTimeTextboxes[i].Text);
						total += gameTimeFromTextboxes[i];
						Slots[SelectedSlot].setGameTime(i, gameTimeFromTextboxes[i]);
					}
					txtGameTimeTotal.Text = TimeSpan.FromSeconds((double)total).ToString();
				} catch (Exception e) {
					MessageBox.Show("Game Time must be between 0 and 65535.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		public void updateNoteScoreTextboxes() {
			if (FileLoaded) {
				for (int i = 0; i < NoteScoreTextboxes.Length; i++) {
					NoteScoreTextboxes[i].Text = Slots[SelectedSlot].getNoteScore(i).ToString();
				}
			}
		}

		/*
		 * Code related to the "Moves" tab
		 */

		public void updateMovesCheckboxes() {
			UInt32 moves = Slots[SelectedSlot].getMoves();
			for (int i = 0; i < MovesCheckboxes.Length; i++) {
				MovesCheckboxes[i].Checked = (moves & (UInt32)Math.Pow(2,i)) > 0;
			}
		}

		public void applyMovesCheckboxes() {
			if (FileLoaded) {
				UInt32 tempBitfield = 0x00000000;
				for (int i = 0; i < MovesCheckboxes.Length; i++) {
					if (MovesCheckboxes[i].Checked) {
						tempBitfield += (UInt32)Math.Pow(2, i);
					}
				}
				Slots[SelectedSlot].setMoves(tempBitfield);
				refreshUI();
			}
		}

		public void movesCheckboxClicked(object sender, EventArgs e) {
			applyMovesCheckboxes();
		}

		private void btnMovesSelectNone_Click(object sender, EventArgs e) {
			for (int i = 0; i < MovesCheckboxes.Length; i++) {
				MovesCheckboxes[i].Checked = false;
			}
			applyMovesCheckboxes();
		}

		private void btnMovesSelectKnown_Click(object sender, EventArgs e) {
			for (int i = 0; i < MovesCheckboxes.Length; i++) {
				MovesCheckboxes[i].Checked = !MovesCheckboxes[i].Text.Contains("Unknown");
			}
			applyMovesCheckboxes();
		}

		private void btnMovesSelectAll_Click(object sender, EventArgs e) {
			for (int i = 0; i < MovesCheckboxes.Length; i++) {
				MovesCheckboxes[i].Checked = true;
			}
			applyMovesCheckboxes();
		}

		/*
		 * Code related to the "Honeycombs" tab
		 */

		public void updateHoneycombCheckboxes() {
			if (FileLoaded) {
				UInt32 honeycombBitfield = Slots[SelectedSlot].getHoneycombs();
				for (int i = 0; i < HoneycombCheckboxes.Length; i++) {
					HoneycombCheckboxes[i].Checked = (honeycombBitfield & (int)Math.Pow(2, i)) > 0;
				}
			}
		}

		public void applyHoneycombCheckboxes() {
			if (FileLoaded) {
				UInt32 tempBitfield = 0x00000000;
				for (int i = 0; i < HoneycombCheckboxes.Length; i++) {
					if (HoneycombCheckboxes[i].Checked) {
						tempBitfield += (UInt32)Math.Pow(2, i);
					}
				}
				Slots[SelectedSlot].setHoneycombs(tempBitfield);
				refreshUI();
			}
		}

		public void honeycombCheckboxClicked(object sender, EventArgs e) {
			applyHoneycombCheckboxes();
		}

		private void btnSelectAllHoneycombs_Click(object sender, EventArgs e) {
			for (int i = 0; i < HoneycombCheckboxes.Length; i++) {
				HoneycombCheckboxes[i].Checked = true;
			}
			applyHoneycombCheckboxes();
		}

		private void btnDeselectAllHoneycombs_Click(object sender, EventArgs e) {
			for (int i = 0; i < HoneycombCheckboxes.Length; i++) {
				HoneycombCheckboxes[i].Checked = false;
			}
			applyHoneycombCheckboxes();
		}

		public void updateChecksumTextbox() {
			if (FileLoaded) {
				txtChecksum.Text = Slots[SelectedSlot].CheckSum.ToString("X8");
				txtComputedChecksum.Text = Slots[SelectedSlot].ComputedChecksum.ToString("X8");
			}
		}

		private void btnSaveEEP_Click(object sender, EventArgs e) {
			if (FileLoaded) {
				EEPFile.Seek(0, SeekOrigin.Begin);
				for (int i = 0; i < Slots.Length; i++) {
					if (Slots[i] != null) {
						// Recalculate checksum
						Slots[i].CheckSum = Slots[i].ComputedChecksum;

						// Write slot to file
						EEPFile.Seek(BKSaveSlot.SLOT_SIZE * Slots[i].physicalIndex, SeekOrigin.Begin);
						EEPFile.Write(Slots[i].SlotData, 0, BKSaveSlot.SLOT_SIZE);
					}
                }
				EEPFile.Seek(0, SeekOrigin.Begin);
				refreshUI();
			}
		}

		public int getLowestUnusedPhysicalIndex() {
			int lowestUnused = 4;
			for (int i = 0; i < Slots.Length; i++) {
				if (Slots[i] != null) {
					lowestUnused = Math.Min(lowestUnused, Slots[i].physicalIndex);
				}
			}
			return lowestUnused;
		}

		private void btnMoveCopy_click(object sender, EventArgs e) {
			MoveCopyFileForm moverAndShaker = new MoveCopyFileForm();
			moverAndShaker.parentForm = this;
			moverAndShaker.ShowDialog();
			refreshRadioButtons();
		}
	}
}
