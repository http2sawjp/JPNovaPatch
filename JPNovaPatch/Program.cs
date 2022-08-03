using System;
using System.Text;

namespace JPNovaPatch
{
	internal class Program
	{
		internal static void Main(string[] args)
		{
			/* Designate console_output encoding */
			Console.OutputEncoding = Encoding.UTF8;

			/* welcome... */
			foreach (var nWelcome in Consts.Strings.Cap.Welcome) { Console.WriteLine(nWelcome); }

			/* are you sure? */
			foreach (var nAreYouSure in Consts.Strings.Cap.AreYouSure) { Console.WriteLine(nAreYouSure); }

			/* continue or exit with early return */
			if (_userOperate() == Consts.UserInputs.Exit) { return; }

			Console.WriteLine();

			try
			{
				using (var funcInst = new FileFuncions(Consts.Strings.ExeName))
				{
					if(funcInst.IsBackupExists())
					{
						foreach (var nOverwrite in Consts.Strings.Cap.OverwriteBu) { Console.WriteLine(nOverwrite); }
						if (_userOperate() == Consts.UserInputs.Exit) { return; }
						Console.WriteLine();
					}

					funcInst.PatchFunctionMain();
				}

				/* patched. */
				foreach (var nTmpStr in Consts.Strings.Cap.PatchCmpl) { Console.WriteLine(nTmpStr); }
			}
			catch (Exception e)
			{
				Console.WriteLine();
				Console.WriteLine($" {e.Message}");
				Console.WriteLine(Consts.Strings.Cap_EX.CantContinue);
			}

			Console.WriteLine();
			Console.WriteLine(Consts.Strings.Cap.AnyKeyExit);
			Console.ReadKey();
			return;
		}

		static Consts.UserInputs _userOperate()
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
						Console.WriteLine(Consts.Strings.Cap.InputDesignateKey);
						break;
				}
			}
		}
	}
}
