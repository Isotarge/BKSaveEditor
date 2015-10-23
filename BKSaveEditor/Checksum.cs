using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 
BK/BT Checksum Algorithm Reference Implementation (bryc)
--------------------------------------------------------
This version attempts to replicate the algorithm as closely as possible, following the asm line by line. 
It is pretty accurate (I'd say), but not very optimized.

If you put a breakpoint on `PC=8025C20C` in BK, it will halt immediately after the algorithm
has finished, and the checksum values are in S3 and S4.
You can compare the registers at this breakpoint with the output of this program.

BK's final checksum is S3 ^ S4. BT's final checksum is (S3 << 32) + S4.
*/

namespace BKSaveEditor {
	static class Checksum {
		public static UInt64 compute(byte[] data, bool isBT) {
			/* Initialize registers */
			UInt64 AT = 0x01;
			UInt64 V0 = 0x80283420;
			UInt64 A1 = 0x8028341C;
			UInt64 A2 = 0x8027BC68;
			UInt64 A3 = 0x80283400;
			UInt64 T0 = 0x2000FF01;
			UInt64 T1 = 0x80281700;
			UInt64 T2 = 0x0C;
			UInt64 T3 = 0x8028170C;
			UInt64 T4 = 0x802816E8;
			UInt64 T5 = 0x00;
			UInt64 T6 = 0x8F809F47;
			UInt64 T7 = 0x3108B3C1;
			UInt64 S1 = 0x00;
			UInt64 S3 = 0x00;
			UInt64 S4 = 0x00;
			UInt64 T8 = 0x802816E8;
			UInt64 T9 = 0x80277370;

			UInt64 mem_DWORD = 0x8F809F473108B3C1;

			int i;
			for (i = 0; i < data.Length; i++) {
				T8 = data[i];
				T5 = (mem_DWORD & 0xFFFFFFFF);
				T9 = S1 & 0x000F;
				T0 = T8 << (int)T9;
				T4 = (mem_DWORD >> 32);
				T7 = T0 + T5;
				T2 = T0 >> 0x1F;
				if (T7 < T5) { AT = 1; } else { AT = 0; }
				T6 = AT + T2;
				T6 = T6 + T4;
				mem_DWORD = (T6 << 32) + T7;

				A3 = mem_DWORD;
				A2 = A3 << (0x1F + 32);
				A1 = A3 << 0x1F;
				A2 = A2 >> 0x1F;
				A1 = A1 >> (0x00 + 32);
				A3 = A3 << (0x0C + 32);
				A2 = A2 | A1;
				A3 = A3 >> (0x00 + 32);
				A2 = A2 ^ A3;
				A3 = A2 >> 0x14;
				A3 = A3 & 0x0FFF;
				A3 = A3 ^ A2;
				V0 = A3 << (0x00 + 32);
				mem_DWORD = A3;
				V0 = V0 >> (0x00 + 32);

				S3 = S3 ^ V0;
				S1 = S1 + 0x0007;
			}

			for (i--; i >= 0; i--) {
				T1 = data[i];
				T3 = (mem_DWORD & 0xFFFFFFFF);
				T8 = S1 & 0x000F;
				T9 = T1 << (int)T8;
				T2 = (mem_DWORD >> 32);
				T5 = T9 + T3;
				T0 = T9 >> 0x1F;
				if (T5 < T3) { AT = 1; } else { AT = 0; }
				T4 = AT + T0;
				T4 = T4 + T2;
				mem_DWORD = (T4 << 32) + T5;

				A3 = mem_DWORD;
				A2 = A3 << (0x1F + 32);
				A1 = A3 << 0x1F;
				A2 = A2 >> 0x1F;
				A1 = A1 >> (0x00 + 32);
				A3 = A3 << (0x0C + 32);
				A2 = A2 | A1;
				A3 = A3 >> (0x00 + 32);
				A2 = A2 ^ A3;
				A3 = A2 >> 0x14;
				A3 = A3 & 0x0FFF;
				A3 = A3 ^ A2;
				V0 = A3 << (0x00 + 32);
				mem_DWORD = A3;
				V0 = V0 >> (0x00 + 32);

				S4 = S4 ^ V0;
				S1 = S1 + 0x0003;
			}

			return isBT ? ((S3 << 32) + S4) : S3 ^ S4;
        }

		public static UInt64 SwapBytes(UInt64 x) {
			// swap adjacent 32-bit blocks
			x = (x >> 32) | (x << 32);
			// swap adjacent 16-bit blocks
			x = ((x & 0xFFFF0000FFFF0000) >> 16) | ((x & 0x0000FFFF0000FFFF) << 16);
			// swap adjacent 8-bit blocks
			return ((x & 0xFF00FF00FF00FF00) >> 8) | ((x & 0x00FF00FF00FF00FF) << 8);
		}

		public static UInt32 SwapBytes(UInt32 x) {
			// swap adjacent 16-bit blocks
			x = (x >> 16) | (x << 16);
			// swap adjacent 8-bit blocks
			return ((x & 0xFF00FF00) >> 8) | ((x & 0x00FF00FF) << 8);
		}

		public static UInt16 SwapBytes(UInt16 x) {
			return (UInt16)((x & 0xFF) << 8 | (x & 0xFF00) >> 8);
		}
	}
}
