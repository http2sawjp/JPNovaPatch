using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace JPNovaPatch
{
	public class BinEditor
	{
		#region << Fields >>
		/// <summary>
		/// offset position</summary>
		readonly long _lTargetOffsetPos;

		/// <summary>
		/// replace target</summary>
		readonly byte[] _bPrevious;

		/// <summary>
		/// new byte</summary>
		readonly byte[] _bNew;

		/// <summary>
		/// Stream of target_file</summary>
		FileStream _fsTarget;

		/// <summary>
		/// Reading bytes of target_file</summary>
		BinaryReader _bReadTarget;

		/// <summary>
		/// Writing bytes of target_file</summary>
		BinaryWriter _bWriteTarget;
		#endregion

		public BinEditor(long lTargetOffsetPos, byte[] bPrevious, FileStream fsTarget)
		{
			this._lTargetOffsetPos = lTargetOffsetPos;
			this._bPrevious = bPrevious;
			this._bNew = null;
			this._fsTarget = fsTarget;
		}

		public BinEditor(long lTargetOffsetPos, byte[] bPrevious, byte[] bNew, FileStream fsTarget)
		{
			this._lTargetOffsetPos = lTargetOffsetPos;
			this._bPrevious = bPrevious;
			this._bNew = bNew;
			this._fsTarget = fsTarget;
		}

		public void CheckOffsetPosByte()
		{
			byte currentByte;

			try
			{
				using (this._bReadTarget = new BinaryReader(this._fsTarget, Encoding.Default, true))
				{
					if (!this._bReadTarget.BaseStream.CanSeek)
					{
						throw new ArgumentException($"Seek is unavailable in [{this._fsTarget.Name}]");
					}

					this._bReadTarget.BaseStream.Seek(this._lTargetOffsetPos, SeekOrigin.Begin);

					for (int iCnt = 0; iCnt < this._bPrevious.Length; iCnt++)
					{
						currentByte = this._bReadTarget.ReadByte();

						if (this._bPrevious[iCnt] != currentByte)
						{
							throw new UnmatchBytesException(
								Consts.Strings.Cap_EX.UnmatchByte(
									this._bReadTarget.BaseStream.Position
									, _bPrevious[iCnt]
									, currentByte
								)
							);
						}
					}
				}
			}
			catch
			{
				throw;
			}

			return;
		}

		public void ReplaceBytes(byte padding = 0x00)
		{
			if (this._bNew is null) { throw new ArgumentNullException(nameof(this._bNew)); }

			try
			{
				using (this._bWriteTarget = new BinaryWriter(this._fsTarget, Encoding.Default, true))
				{
					this._bWriteTarget.BaseStream.Seek(this._lTargetOffsetPos, SeekOrigin.Begin);

					int maxLen = Math.Max(this._bPrevious.Length, this._bNew.Length);

					for (int iCnt = 0; iCnt < maxLen; iCnt++)
					{
						this._bWriteTarget.Write(iCnt < this._bNew.Length ? this._bNew[iCnt] : padding);
					}
				}
			}
			catch
			{
				throw;
			}

			return;
		}
	}

	[Serializable()]
	public class UnmatchBytesException : Exception
	{
		public UnmatchBytesException() : base() { }
		public UnmatchBytesException(string message) : base(message) { }
		public UnmatchBytesException(string message, Exception innerException) : base(message, innerException) { }
		protected UnmatchBytesException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
