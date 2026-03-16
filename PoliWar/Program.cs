using System;
using System.Runtime.InteropServices;

class Program
{
    
    [DllImport("fUtils", CallingConvention = CallingConvention.Cdecl)]
    public static extern void Fask();

    static void Main()
    {
       Fask();        
    }
}
