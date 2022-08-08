using System;
using System.Text;

namespace JPNovaPatch
{
	using MessageType = Consts.Strings.Cap.MessageType;

	internal class Program
	{
		internal static void Main()
		{
			ViewNovaPatch viewInst = null;
			FileFuncions funcInst = null;
			Func<bool> userOperate;

			try
			{
				viewInst = new(Encoding.UTF8);
				userOperate = () => viewInst.isUserWantsExit();

				viewInst.OutputMessage(MessageType.Welcome);
				if ( viewInst.OutputMessage(MessageType.AreYouSure, userOperate))
				{
					return;
				}

				using (funcInst = new FileFuncions(Consts.Strings.ExeName))
				{
					if(funcInst.IsBackupExists())
					{
						if(viewInst.OutputMessage(MessageType.OverWrite, userOperate))
						{
							return;
						}
					}

					funcInst.PatchFunctionMain();
				}

				viewInst.OutputMessage(MessageType.Patched);
			}
			catch (Exception e)
			{
				ViewNovaPatch.Output($"{Consts.Strings.Cap_EX.ExceptionString(e)}");
			}

			if (viewInst is not null) { viewInst.OutputMessage(MessageType.AnyKeyToExit); }
			ViewNovaPatch.ReadKey();
			return;
		}
	}
}
