using System;
using System.IO;
using System.Collections.Generic;

namespace JPNovaPatch
{
    internal class FileFuncions : IDisposable
    {
        #region << Properties >>

        private FileStream _fsExe;

        private BinEditor _binInst;

        private List<BinEditor> _lBinEditor;

        private List<BinEditor> _lBinChecker;
        #endregion

        internal FileFuncions(string fileName)
        {
            try
            {
                if (!File.Exists(fileName)) { throw new Exception(Consts.Strings.Cap_Ex_ExeNotFound); }

                /* = file open = */
                _fsExe = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);

                this._lBinChecker = new List<BinEditor>();
                this._lBinEditor = new List<BinEditor>();

                /* ===== checker ===== */
                /* file header */
                this._lBinChecker.Add(this._binInst = new BinEditor(
                    Consts.Nums.CHECK_EXESTRUCT_FIRST, Consts.Bytes.CHECK_EXESTRUCT_FIRST, new byte[0], this._fsExe));

                /* "CP" */
                this._lBinChecker.Add(this._binInst = new BinEditor(
                    Consts.Nums.CHECK_EXESTRUCT_CP, Consts.Bytes.CHECK_EXESTRUCT_CP, new byte[0], this._fsExe));

                /* ===== replacer ===== */
                /* liviconv charset_designator */
                this._lBinEditor.Add(this._binInst = new BinEditor(
                    Consts.Nums.OFFSET_LIBICONV, Consts.Bytes.LIBICONV_CHARSET_BEFORE, Consts.Bytes.LIBICONV_CHARSET_AFTER, this._fsExe));

                /* reading data encode from .rez */
                this._lBinEditor.Add(this._binInst = new BinEditor(
                    Consts.Nums.OFFSET_CONVERT_FROM, Consts.Bytes.TEXTENCODE_MacRoman, Consts.Bytes.TEXTENCODE_CP932, this._fsExe));

                /* convert readed data from .rez */
                this._lBinEditor.Add(this._binInst = new BinEditor(
                    Consts.Nums.OFFSET_CONVERT_TO, Consts.Bytes.TEXTENCODE_CP1252, Consts.Bytes.TEXTENCODE_CP932, this._fsExe));

                /* font_designate left bottom label */
                this._lBinEditor.Add(this._binInst = new BinEditor(
                    Consts.Nums.OFFSET_FONT_INGAME_LABEL, Consts.Bytes.FONT_Chicago, Consts.Bytes.FONT_Meiryo, this._fsExe));

                /* font_designate playing dialogs */
                this._lBinEditor.Add(this._binInst = new BinEditor(
                    Consts.Nums.OFFSET_FONT_DIALOGS, Consts.Bytes.FONT_Geneva, Consts.Bytes.FONT_Osaka, this._fsExe));
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal void CreateBackUp()
        {
            try
            {
                File.Copy(Consts.Strings.ExeName, Consts.Strings.ExeName_bu, true);
            }
            catch(Exception)
            {
                throw;
            }

            return;
        }

        internal void CheckBinInFile()
        {
            try
            {
                foreach (var nTmpBinInst in this._lBinChecker) { nTmpBinInst.CheckOffsetPosByte(); }
            }
            catch(Exception)
            {
                throw;
            }

            return;
        }

        internal void WriteBinToFile()
        {
            try
            {
                foreach (var nTmpBinInst in this._lBinEditor) { nTmpBinInst.ReplaceBytes(); }
            }
            catch(Exception)
            {
                throw;
            }

            return;
        }

        public void Dispose()
        {
            if (this._fsExe != null) { this._fsExe.Dispose(); }

            return;
        }
    }
}
