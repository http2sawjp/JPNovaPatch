using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace JPNovaPatch
{
	public static class Consts
	{
		public static class Strings
		{
			public static readonly string ExeName = "EV Nova.exe";
			public static readonly string ExeName_bu = $"{ExeName}.org";

			public static class Cap
			{
				public enum MessageType
				{
					Welcome
					, AreYouSure
					, OverWrite
					, Patched
					, AnyKeyToExit
					, InputNavigation
					, InputCorrection
				}

				static readonly string DownloadURL = "download.escape-velocity.games/EV%20Nova.zip";
				public static ReadOnlyDictionary<MessageType, string[]> Messages { get; }

				static Cap()
				{
					Messages = new( new Dictionary<MessageType, string[]>
					{
						{ MessageType.Welcome, new[] {
							" EV Nova.exe 日本語対応化パッチャーへようこそ."
							, $" {ExeName}を日本語表示が可能になるよう改造します."
							, $" {DownloadURL}よりダウンロードされた{ExeName}へのパッチを想定しており、"
							, " それ以外のファイルへのパッチ動作は未検証です."
							, " EV Nova Windows Resolution Patcherを先に適用しておくことをお勧めします." }
						}
						, { MessageType.AreYouSure, new[] {
							" パッチ作業を継続してよろしいですか?" }
						}
						, { MessageType.OverWrite, new[] {
							$" すでにバックアップされたファイル[{ExeName_bu}]が存在するようです"
							, " バックアップを上書きして作業を継続してよろしいですか？" }
						}
						, { MessageType.Patched, new[] {
							$" オリジナルファイルのバックアップ作成...[{ExeName_bu}]"
							, " パッチ作業が終了しました." }
						}
						, { MessageType.AnyKeyToExit, new[] {
							string.Empty
							, " 何かキーを押すと終了します..." }
						}
						, {
							MessageType.InputNavigation, new[] {
							" 'y'キー押下で継続 / 'n'キー押下で終了" }
						}
						, {
							MessageType.InputCorrection, new[] {
							" 'y'キー 若しくは 'n'キー を押下してください." }
						}
					});
				}
			}

			public static class Cap_EX
			{
				public static readonly string ExeNotFound = $" {ExeName}が見つかりません.";
				public static readonly string ExeCantOpen = $" {ExeName}を開けませんでした.";
				public static readonly string IgnoreValueInDesignateOffset = $" {ExeName}の構造に不整合が見つかりました.";
				public static readonly string InWriteByte = $" {ExeName}の編集中にエラーが発生しました.";
				public static readonly string CantContinue = $" {ExeName}にパッチを適用できませんでした.";
				public static readonly string PermissionNothing = $" ディレクトリ内のファイルを編集できません.";
				public static readonly string RecommendReinstall = $" パッチ作業中の致命的なエラーにより、ゲームの再インストールをお勧めします.";

				public static string UnmatchByte(long currentPos, string expect, string current)
					=> $"Unexpected byte detected in offset pos[{currentPos}]\n Expected value: [{expect}], Detected value: [{current}].";

				public static string ExceptionString(Exception e)
					=> $"\n {e.Message}\n {CantContinue}";

				public static readonly string InvalidArgument = "Invalid argument type.";
			}
		}

		public static class Nums
		{
			public static readonly long CHECK_EXESTRUCT_FIRST = 0x00;
			public static readonly long CHECK_EXESTRUCT_CP = 0x16c340;

			/// <summary>
			/// libiconv</summary>
			public static readonly long OFFSET_LIBICONV = 0xbb8f9;

			public static readonly long OFFSET_CONVERT_FROM = 0x16c334;
			public static readonly long OFFSET_CONVERT_TO = 0x16c340;

			/// <summary>
			/// IMPORTANT!!!!!!!</summary>
			public static readonly long OFFSET_FONT_DIALOGS = 0x16be51;
			public static readonly long OFFSET_FONT_INGAME_LABEL = 0x16c315;
		}

		public static class Bytes
		{
			public static readonly byte[] CHECK_EXESTRUCT_FIRST = { 0x4d, 0x5A, 0x90 };
			public static readonly byte[] CHECK_EXESTRUCT_CP = { 0x43, 0x50, };

			public static readonly byte[] LIBICONV_CHARSET_BEFORE = { 0x00 };
			public static readonly byte[] LIBICONV_CHARSET_AFTER = { 0x80 };

			public static readonly byte[] TEXTENCODE_MacRoman = { 0x4d, 0x61, 0x63, 0x52, 0x6f, 0x6d, 0x61, 0x6e };
			public static readonly byte[] TEXTENCODE_CP1252 = { 0x43, 0x50, 0x31, 0x32, 0x35, 0x32 };
			public static readonly byte[] TEXTENCODE_CP932 = { 0x43, 0x50, 0x39, 0x33, 0x32 };

			public static readonly byte[] FONT_Chicago = { 0x43, 0x68, 0x69, 0x63, 0x61, 0x67, 0x6f };
			public static readonly byte[] FONT_Arial = { 0x41, 0x72, 0x69, 0x61, 0x6c };
			public static readonly byte[] FONT_Geneva = { 0x47, 0x65, 0x6e, 0x65, 0x76, 0x61 };
			public static readonly byte[] FONT_Meiryo = { 0x4d, 0x65, 0x69, 0x72, 0x79, 0x6f };
			public static readonly byte[] FONT_Osaka = { 0x4f, 0x73, 0x61, 0x6b, 0x61 };
		}
	}
}
