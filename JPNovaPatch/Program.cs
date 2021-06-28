using System;
using System.Text;
using System.IO;

namespace JPNovaPatch
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            FileFuncions funcInst;

            /* Designate console_output encoding */
            Console.OutputEncoding = Encoding.UTF8;

            /* welcome... */
            foreach (var nTmpStr in Consts.Strings.Cap_Welcome) { Console.WriteLine(nTmpStr); }

            /* are you sure? */
            foreach (var nTmpStr in Consts.Strings.Cap_AreYouSure) { Console.WriteLine(nTmpStr); }

            /* continue or exit with early return */
            if (_userOperate() == Consts.UserInputs.Exit) { return; }

            Console.WriteLine();

            try
            {
                using (funcInst = new FileFuncions(Consts.Strings.ExeName))
                {
                    funcInst.CreateBackUp();
                    funcInst.CheckBinInFile();
                    funcInst.WriteBinToFile();
                }

                /* patched. */
                foreach (var nTmpStr in Consts.Strings.Cap_PatchCmpl) { Console.WriteLine(nTmpStr); }
            }
            catch (Exception e)
            {
                _cleanUp(Consts.Strings.ExeName, Consts.Strings.ExeName_bu, Consts.Strings.ExeName);
                Console.WriteLine();
                Console.WriteLine($" {e.Message}");
                Console.WriteLine(Consts.Strings.Cap_Ex_CantContinue);
            }

            Console.WriteLine();
            Console.WriteLine(Consts.Strings.Cap_AnyKeyExit);
            Console.ReadKey();
            return;
        }

        private static Consts.UserInputs _userOperate()
        {
            while (true)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Y:
                        return Consts.UserInputs.Continue;

                    case ConsoleKey.N:
                        return Consts.UserInputs.Exit;

                    default:
                        Console.WriteLine(Consts.Strings.Cap_InputDesignateKey);
                        break;
                }
            }
        }

        private static void _cleanUp(string deleteFileName, string renameFileName, string renameTo)
        {
            if(File.Exists(renameFileName) && File.Exists(deleteFileName))
            {
                File.Delete(deleteFileName);
                File.Move(renameFileName, renameTo);
            }

            return;
        }
    }
}
