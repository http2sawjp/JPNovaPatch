using System;
using System.Collections.Generic;
using System.Text;

namespace JPNovaPatch
{
	using MessageType = Consts.Strings.Cap.MessageType;

	public class ViewNovaPatch
	{
		enum userInputs
		{
			Continue,
			Exit,
			Other
		}

		static void wl(string msg) => Console.WriteLine(msg);
		public static void ReadKey() => Console.ReadKey();
		public static void Output(string msg = "") => wl(msg);
		public static void Output(IEnumerable<string> msgs) { foreach (var nMsg in msgs) { wl(nMsg); } }

		public ViewNovaPatch(Encoding consoleEnc)
		{
			try
			{
				Console.OutputEncoding = consoleEnc;
			}
			catch
			{
				throw;
			}
		}

		public bool isUserWantsExit() => this.userOperate() == userInputs.Exit;

		public void OutputMessage(MessageType type)
		{
			if(!Consts.Strings.Cap.Messages.ContainsKey(type))
			{
				throw new ArgumentException(Consts.Strings.Cap_EX.InvalidArgument, nameof(type));
			}

			Output(Consts.Strings.Cap.Messages[type]);
		}

		public bool OutputMessage(MessageType type, Func<bool> callBack)
		{
			this.OutputMessage(type);

			return callBack();
		}

		userInputs userOperate()
		{
			while (true)
			{
				switch (Console.ReadKey().Key)
				{
					case ConsoleKey.Y:
						wl(string.Empty);
						return userInputs.Continue;

					case ConsoleKey.N:
						return userInputs.Exit;

					default:
						this.OutputMessage(MessageType.InputNavigation);
						break;
				}
			}
		}
	}
}