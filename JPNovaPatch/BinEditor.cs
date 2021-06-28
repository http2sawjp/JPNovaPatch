using System;
using System.IO;
using System.Text;
using System.Runtime.Serialization;

namespace JPNovaPatch
{
    internal class BinEditor
    {
        #region << Properties >>
        /// <summary>
        /// filename of filestream</summary>
        internal string sFileName { get { return this._fsTarget.Name; } }

        /// <summary>
        /// full_path of filestream</summary>
        internal string sFilePath { get { return Path.GetFileName(this.sFilePath); } }

        /// <summary>
        /// offset position</summary>
        internal long lTargetOffsetPos { get; }

        /// <summary>
        /// replace target</summary>
        private readonly byte[] _bPrevious;

        /// <summary>
        /// new byte</summary>
        private readonly byte[] _bNew;

        /// <summary>
        /// Stream of target_file</summary>
        private FileStream _fsTarget;

        /// <summary>
        /// Reading bytes of target_file</summary>
        private BinaryReader _bReadTarget;

        /// <summary>
        /// Writing bytes of target_file</summary>
        private BinaryWriter _bWriteTarget;
        #endregion

        internal BinEditor(long lTargetOffsetPos, byte[] bPrevious, byte[] bNew, FileStream fsTarget)
        {
            this.lTargetOffsetPos = lTargetOffsetPos;
            this._bPrevious = bPrevious;
            this._bNew = bNew;
            this._fsTarget = fsTarget;
        }

        internal void CheckOffsetPosByte()
        {
            byte buf;

            try
            {
                using (this._bReadTarget = new BinaryReader(this._fsTarget, Encoding.Default, true))
                {
                    if(!this._bReadTarget.BaseStream.CanSeek)
                    {
                        throw new ArgumentException($"Seek is unavailable in [{this._fsTarget.Name}]");
                    }

                    this._bReadTarget.BaseStream.Seek(this.lTargetOffsetPos, SeekOrigin.Begin);

                    for(int iCnt = 0; iCnt < this._bPrevious.Length; iCnt++)
                    {
                        buf = this._bReadTarget.ReadByte();

                        if (this._bPrevious[iCnt] != buf)
                        {
                            throw new UnmatchBytesException($"byte unmatch in offset pos[{this._bReadTarget.BaseStream.Position}]\nExpected value is[{_bPrevious[iCnt]}] but [{buf} is detected.]");
                        }
                    }
                }
            }
            catch(Exception)
            {
                throw;
            }

            return;
        }

        internal void ReplaceBytes(byte padding = 0x00)
        {
            try
            {
                using (this._bWriteTarget = new BinaryWriter(this._fsTarget, Encoding.Default, true))
                {
                    this._bWriteTarget.BaseStream.Seek(this.lTargetOffsetPos, SeekOrigin.Begin);

                    int maxLen = Math.Max(_bPrevious.Length, _bNew.Length);

                    for(int iCnt = 0; iCnt < maxLen; iCnt++)
                    {
                        if (iCnt < _bNew.Length)
                        {
                            this._bWriteTarget.Write(this._bNew[iCnt]);
                        }
                        else
                        {
                            this._bWriteTarget.Write(padding);
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Console.Write(e);

                throw new Exception(Consts.Strings.Cap_Ex_InWriteByte);
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
