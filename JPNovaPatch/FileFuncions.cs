using System;
using System.IO;

namespace JPNovaPatch
{
	internal class FileFuncions : IDisposable
	{
		#region << Fields >>
		private FileStream _fsExe;
		private BinEditor[] _binCheckers;
		private BinEditor[] _binReplacers;
		#endregion

		internal FileFuncions(string fileName)
		{
			if (!File.Exists(fileName)) { throw new Exception(Consts.Strings.Cap_EX.ExeNotFound); }

			if (!this._hasErrorInWrite()) { throw new IOException(Consts.Strings.Cap_EX.PermissionNothing); }

			try
			{
				_fsExe = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
			}
			catch
			{
				throw;
			}

			this._binCheckers = new[]
			{
				/* file header */
				new BinEditor(Consts.Nums.CHECK_EXESTRUCT_FIRST, Consts.Bytes.CHECK_EXESTRUCT_FIRST, this._fsExe)
				
				/* "CP" */
				, new BinEditor(Consts.Nums.CHECK_EXESTRUCT_CP, Consts.Bytes.CHECK_EXESTRUCT_CP, this._fsExe)
			};

			this._binReplacers = new[]
			{
				/* liviconv charset_designator */
				new BinEditor(Consts.Nums.OFFSET_LIBICONV, Consts.Bytes.LIBICONV_CHARSET_BEFORE
					, Consts.Bytes.LIBICONV_CHARSET_AFTER, this._fsExe)
				
				/* reading data encode from .rez */
				, new BinEditor(Consts.Nums.OFFSET_CONVERT_FROM, Consts.Bytes.TEXTENCODE_MacRoman
					, Consts.Bytes.TEXTENCODE_CP932, this._fsExe)
				
				/* convert readed data from .rez */
				, new BinEditor(Consts.Nums.OFFSET_CONVERT_TO, Consts.Bytes.TEXTENCODE_CP1252
					, Consts.Bytes.TEXTENCODE_CP932, this._fsExe)
				
				/* font_designate left bottom label */
				, new BinEditor(Consts.Nums.OFFSET_FONT_INGAME_LABEL, Consts.Bytes.FONT_Chicago
					, Consts.Bytes.FONT_Meiryo, this._fsExe)
				
				/* font_designate playing dialogs */
				, new BinEditor(Consts.Nums.OFFSET_FONT_DIALOGS, Consts.Bytes.FONT_Geneva
					, Consts.Bytes.FONT_Osaka, this._fsExe)
			};
		}

		internal bool IsBackupExists() => File.Exists(Consts.Strings.ExeName_bu);

		internal void PatchFunctionMain()
		{
			bool isCreatedBackUp = false;

			try
			{
				this._createBackUp();
				isCreatedBackUp = true;
				this._checkBinInFile();
				this._writeBinToFile();
			}
			catch
			{
				if (isCreatedBackUp)
				{
					try
					{
						this._cleanUp(Consts.Strings.ExeName, Consts.Strings.ExeName_bu, Consts.Strings.ExeName);
					}
					catch(Exception e)
					{
						Console.WriteLine($" {e.Message}");
						Console.WriteLine(Consts.Strings.Cap_EX.RecommendReinstall);
					}
				}

				throw;
			}

			return;
		}

		void _createBackUp()
		{
			try
			{
				File.Copy(Consts.Strings.ExeName, Consts.Strings.ExeName_bu, true);
			}
			catch
			{
				throw;
			}

			return;
		}

		void _checkBinInFile()
		{
			try
			{
				foreach (var nBinChecker in this._binCheckers) { nBinChecker.CheckOffsetPosByte(); }
			}
			catch
			{
				throw;
			}

			return;
		}

		void _writeBinToFile()
		{
			try
			{
				foreach (var nBinReplacer in this._binReplacers) { nBinReplacer.ReplaceBytes(); }
			}
			catch
			{
				throw;
			}

			return;
		}

		bool _hasErrorInWrite()
		{
			string guid;

			while (true)
			{
				guid = Guid.NewGuid().ToString();
				if (!File.Exists(guid)) { break; }
			}

			try
			{
				File.Create(guid).Close();
				File.Delete(guid);
			}
			catch (Exception e)
			{
				Console.WriteLine($" {e.Message}");
				return false;
			}

			return true;
		}

		 void _cleanUp(string deleteFileName, string renameFileName, string renameTo)
		{
			try
			{
				if (this._fsExe is not null) { this._fsExe.Dispose(); }
				File.Delete(deleteFileName);
				File.Move(renameFileName, renameTo);
			}
			catch
			{
				throw;
			}

			return;
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);

			if (this._fsExe is not null) { this._fsExe.Dispose(); }

			return;
		}
	}
}
