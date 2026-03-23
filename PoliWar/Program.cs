using System;
using System.Runtime.InteropServices;

class Program
{
	const int FILE_SAVE = 1;
	const int FILE_LOAD = 2;
    [DllImport("fUtils", CallingConvention = CallingConvention.Cdecl)]
    public static extern void FerrExit(int code);
    [DllImport("fUtils", CallingConvention = CallingConvention.Cdecl)]
    public static extern int FfileUtil(int opcode);
    [DllImport("fUtils", CallingConvention = CallingConvention.Cdecl)]
    public static extern void FenterRawMode();
	[DllImport("fUtils", CallingConvention = CallingConvention.Cdecl)]
    public static extern void FexitRawMode();
    [DllImport("fUtils", CallingConvention = CallingConvention.Cdecl)]
    public static extern void FhideCursor();
    [DllImport("fUtils", CallingConvention = CallingConvention.Cdecl)]
    public static extern void FshowCursor();
    [DllImport("fUtils", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
	public static extern void FdrawBox(int x, int y, int width, int height, string title);
    [DllImport("fUtils", CallingConvention = CallingConvention.Cdecl)]
    public static extern char FfetchAction();
    static void Main()
    {
		Console.Clear();
		FenterRawMode();
		FhideCursor();
		char userInput;
		userInput='A';
		FdrawBox(1, 1, 40, 16, "Ahoj");
		while (userInput!='e') {
			userInput = FfetchAction();
			}
		Console.Clear();
		FshowCursor();
		FexitRawMode();
    }
}
