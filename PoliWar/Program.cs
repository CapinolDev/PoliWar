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
    

    static void Main()
    {
		Console.WriteLine(FfileUtil(FILE_SAVE));
		Console.WriteLine(FfileUtil(FILE_LOAD));
		Console.WriteLine(FfileUtil(0));
		FerrExit(10);
    }
}
