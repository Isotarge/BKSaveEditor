using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKSaveEditor {
	public class BKSaveSlot {

		public const int SLOT_SIZE = 0x78;

		public const int HONEYCOMB_BITFIELD_OFFSET = 0x0E;
		public const int NOTE_SCORE_BASE_OFFSET = 0x22;
		public const int GAME_TIME_BASE_OFFSET = 0x2A;

		public const int MUMBO_TOKENS_ON_HAND = 0x64;
		public const int JIGGIES_ON_HAND = 0x69;
		public const int MOVES_BITFIELD = 0x6A;

		public const int CHECKSUM_OFFSET = 0x74;

		public const UInt64 fullNotes = 0x7FFFFFFFFFFFFFFF;
		public static UInt64[] noteScoreBitmasks = new UInt64[] {
			0x7F00000000000000, // MM
			0x00FE000000000000, // TTC
			0x0001FC0000000000, // CC
			0x000003F800000000, // BGS
			0x00000007F0000000, // FP
			0x000000000FE00000, // GV
			0x00000000001FC000, // CCW
			0x0000000000003F80, // RBB
			0x000000000000007F  // MMM
		};

		public byte[] SlotData;
		public int physicalIndex;

		public BKSaveSlot(byte[] EEPROM, int _physicalIndex, int index) {
			SlotData = new Byte[SLOT_SIZE];
			physicalIndex = _physicalIndex;
			Buffer.BlockCopy(EEPROM, SLOT_SIZE * physicalIndex, SlotData, 0x00, SLOT_SIZE);
		}

		public BKSaveSlot(BKSaveSlot other) {
			this.physicalIndex = other.physicalIndex;
			this.SlotData = other.SlotData;
		}

		#region Low Level methods

		public UInt16 getUInt16(int index) {
			UInt16 raw_value = System.BitConverter.ToUInt16(SlotData, index);
			if (System.BitConverter.IsLittleEndian) {
				raw_value = Checksum.SwapBytes(raw_value);
			}
			return raw_value;
		}

		public UInt32 getUInt32(int index) {
			UInt32 raw_value = System.BitConverter.ToUInt32(SlotData, index);
			if (System.BitConverter.IsLittleEndian) {
				raw_value = Checksum.SwapBytes(raw_value);
			}
			return raw_value;
		}

		public UInt64 getUInt64(int index) {
			UInt64 raw_value = System.BitConverter.ToUInt64(SlotData, index);
			if (System.BitConverter.IsLittleEndian) {
				raw_value = Checksum.SwapBytes(raw_value);
			}
			return raw_value;
		}

		public void setData(int index, UInt16 value) {
			byte[] bytes = System.BitConverter.GetBytes(value);
			if (System.BitConverter.IsLittleEndian) {
				Array.Reverse(bytes);
			}
			Array.Copy(bytes, 0, SlotData, index, bytes.Length);
		}

		public void setData(int index, UInt32 value) {
			byte[] bytes = System.BitConverter.GetBytes(value);
			if (System.BitConverter.IsLittleEndian) {
				Array.Reverse(bytes);
			}
			Array.Copy(bytes, 0, SlotData, index, bytes.Length);
		}

		public void setData(int index, UInt64 value) {
			byte[] bytes = System.BitConverter.GetBytes(value);
			if (System.BitConverter.IsLittleEndian) {
				Array.Reverse(bytes);
			}
			Array.Copy(bytes, 0, SlotData, index, bytes.Length);
		}

		#endregion

		public UInt32 ComputedChecksum {
			get {
				return (UInt32)Checksum.compute(SlotData.Take(SLOT_SIZE - 4).ToArray(), false);
			}
		}

		public UInt32 CheckSum {
			get {
				return getUInt32(CHECKSUM_OFFSET);
			} set {
				setData(CHECKSUM_OFFSET, ComputedChecksum);
			}
		}

		public int getNoteScore(int level) {
			if (level < noteScoreBitmasks.Length) {
				UInt64 noteScores = getUInt64(NOTE_SCORE_BASE_OFFSET);
				UInt64 noteScoreMasked = noteScores & noteScoreBitmasks[level];
				int shiftAmount = (9 - (level + 1)) * 7;
				UInt64 noteScoreShifted = noteScoreMasked >> shiftAmount;
				return (int)noteScoreShifted;
			}
			return 0;
		}

		public UInt16 getGameTime(int level) {
			return getUInt16(GAME_TIME_BASE_OFFSET + level * 2);
		}

		public void setGameTime(int level, UInt16 value) {
			setData(GAME_TIME_BASE_OFFSET + level * 2, value);
		}

		public UInt32 getHoneycombs() {
			return getUInt32(HONEYCOMB_BITFIELD_OFFSET);
		}

		public void setHoneycombs(UInt32 bitfield) {
			setData(HONEYCOMB_BITFIELD_OFFSET, bitfield);
		}

		public byte getMumboTokensOnHand() {
			return SlotData[MUMBO_TOKENS_ON_HAND];
		}

		public byte getJiggiesOnHand() {
			return SlotData[JIGGIES_ON_HAND];
		}

		public UInt32 getMoves() {
			return getUInt32(MOVES_BITFIELD);
		}

		public void setMoves(UInt32 bitfield) {
			setData(MOVES_BITFIELD, bitfield);
		}

		// Sets the index of the file on the file select screen
		public void setFileSelectIndex(byte index) {
			SlotData[0] = 0x11; // Mark the file as active
			SlotData[1] = index; // Set which slot it will appear in
		}

		// Gets the index of the file on the file select screen
		public byte getFileSelectIndex() {
			return SlotData[0];
		}
	}
}
