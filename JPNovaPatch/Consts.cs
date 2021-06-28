namespace JPNovaPatch
{
    internal static class Consts
    {
        internal enum UserInputs
        {
            Continue,
            Exit,
            Other
        }

        internal static class Strings
        {
            internal static readonly string ExeName = "EV Nova.exe";
            internal static readonly string ExeName_bu = $"{ExeName}.org";

            internal static readonly string Cap_DownloadURL = "download.escape-velocity.games/EV%20Nova.zip";
            
            internal static readonly string[] Cap_Welcome = { " EV Nova.exe 日本語対応化パッチャーへようこそ."
                                                            , $" {ExeName}を日本語表示が可能になるよう改造します."
                                                            , $" {Cap_DownloadURL}よりダウンロードされた{ExeName}へのパッチを想定しており、"
                                                            , " それ以外のファイルへのパッチ動作は未検証です."
                                                            , " EV Nova Windows Resolution Patcherを先に適用しておくことをお勧めします."
                                                            , string.Empty };

            internal static readonly string[] Cap_AreYouSure = {  " パッチ作業を継続してよろしいですか?"
                                                                , " 'y'キー押下で継続 / 'n'キー押下で終了"
                                                                , string.Empty };

            internal static readonly string Cap_InputDesignateKey = " 'y'キー 若しくは 'n'キー を押下してください.";

            internal static readonly string Cap_Ex_ExeNotFound = $" {ExeName}が見つかりません.";
            internal static readonly string Cap_Ex_ExeCantOpen = $" {ExeName}を開けませんでした.";
            internal static readonly string Cap_Ex_IgnoreValueInDesignateOffset = $" {ExeName}の構造に不整合が見つかりました.";
            internal static readonly string Cap_Ex_InWriteByte = $" {ExeName}の編集中にエラーが発生しました.";
            internal static readonly string Cap_Ex_CantContinue = $" {ExeName}にパッチを適用できませんでした.";

            internal static readonly string[] Cap_PatchCmpl = { $" オリジナルファイルのバックアップ作成...[{ExeName_bu}]"
                                                              , " パッチ作業が終了しました." };

            internal static readonly string Cap_AnyKeyExit = " 何かキーを押すと終了します...";
        }

        internal static class Nums
        {
            internal static readonly long CHECK_EXESTRUCT_FIRST = 0x00;
            internal static readonly long CHECK_EXESTRUCT_CP = 0x16c340;

            /// <summary>
            /// libiconv</summary>
            internal static readonly long OFFSET_LIBICONV = 0xbb8f9;

            internal static readonly long OFFSET_CONVERT_FROM = 0x16c334;
            internal static readonly long OFFSET_CONVERT_TO = 0x16c340;

            /// <summary>
            /// IMPORTANT!!!!!!!</summary>
            internal static readonly long OFFSET_FONT_DIALOGS = 0x16be51;
            internal static readonly long OFFSET_FONT_INGAME_LABEL = 0x16c315;
        }

        internal static class Bytes
        {
            internal static readonly byte[] CHECK_EXESTRUCT_FIRST = { 0x4d, 0x5A, 0x90 };
            internal static readonly byte[] CHECK_EXESTRUCT_CP = { 0x43, 0x50, };

            internal static readonly byte[] LIBICONV_CHARSET_BEFORE = { 0x00 };
            internal static readonly byte[] LIBICONV_CHARSET_AFTER = { 0x80 };

            internal static readonly byte[] TEXTENCODE_MacRoman = { 0x4d, 0x61, 0x63, 0x52, 0x6f, 0x6d, 0x61, 0x6e };
            internal static readonly byte[] TEXTENCODE_CP1252 = { 0x43, 0x50, 0x31, 0x32, 0x35, 0x32};
            internal static readonly byte[] TEXTENCODE_CP932 = { 0x43, 0x50, 0x39, 0x33, 0x32 };

            internal static readonly byte[] FONT_Chicago = { 0x43, 0x68, 0x69, 0x63, 0x61, 0x67, 0x6f };
            internal static readonly byte[] FONT_Arial = { 0x41, 0x72, 0x69, 0x61, 0x6c };
            internal static readonly byte[] FONT_Geneva = { 0x47, 0x65, 0x6e, 0x65, 0x76, 0x61 };
            internal static readonly byte[] FONT_Meiryo = { 0x4d, 0x65, 0x69, 0x72, 0x79, 0x6f };
            internal static readonly byte[] FONT_Osaka = { 0x4f, 0x73, 0x61, 0x6b, 0x61};
        }
    }
}
