using System;
using System.Runtime.InteropServices;

class Program
{
	const int GAME_NONE = 100;
	const int GAME_EXIT = 101;
	const int GAME_DOWN = 102;
	const int GAME_UP = 103;
	const int GAME_MAP = 104;
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
    [DllImport("fUtils", CallingConvention = CallingConvention.Cdecl)]
    public static extern void FmoveCursor(int x, int y);
    
    static void Main()
    {
		Console.Clear();
		FenterRawMode();
		FhideCursor();
		char userInput;
		userInput='A';
		int currView;
		currView = GAME_NONE;
		int currAction;
		int iX; int iY;
		int currMoney = 1000;
		int population = 500;
		int publicLike = 0;
		int publicNeutral = 500;
		int publicHate = 0;
		char[,] gameMap = new char[16,58];
		int gameMapXSize = gameMap.GetLength(0);
		int gameMapYSize = gameMap.GetLength(1);
		for (iX=0;iX<gameMapXSize;iX++) {
			for (iY=0;iY<gameMapYSize;iY++) { 
				gameMap[iX,iY] = 'o';
				}
			}
		
		int remainingTimeInOffice;
		bool actionSwitched;
		actionSwitched = false;
		remainingTimeInOffice=90;
		currAction = GAME_NONE;
		
		
		while (currAction!=GAME_EXIT) {
			if (actionSwitched) {
				Console.Clear();
				actionSwitched = false;
				}
			switch (currView) {
				case GAME_NONE:
					FdrawBox(1, 1, 35, 2, "Days remaining in office: " + remainingTimeInOffice.ToString());
					FdrawBox(3, 1, 35, 20, "Policies");
					FdrawBox(3, 37, 47, 20, "Public View");
					FmoveCursor(4,38);
					Console.Write("People who like you: " + publicLike.ToString());
					FmoveCursor(5,38);
					Console.Write("People who dont care: " + publicNeutral.ToString());
					FmoveCursor(6,38);
					Console.Write("People who hate you: " + publicHate.ToString());
					FdrawBox(1, 37, 20, 2, "Money: "+ currMoney.ToString());
					FdrawBox(1, 58, 26, 2, "Population: "+ population.ToString());
					break;
				case GAME_MAP:
					FdrawBox(1,1,60,60, "Map");
					for (iX=0;iX<gameMapXSize;iX++) {
						for (iY=0;iY<gameMapYSize;iY++) { 
							FmoveCursor(iX+2,iY+2);
							Console.Write(gameMap[iX,iY]);
							}
						}
					break;
				}
				
			userInput = FfetchAction();
			switch (userInput) {
				case 'q':
				currAction = GAME_EXIT;
				break;
				case 'a':
				remainingTimeInOffice -= 1;
				actionSwitched = true;
				break;
				case 'm':
				if (currView == GAME_MAP) {
						currView = GAME_NONE;
					} else {
						currView = GAME_MAP;
					}
				actionSwitched = true;
				break;
				default:
				currAction = GAME_NONE;
				actionSwitched = true;
				break;
				}
			}	
		Console.Clear();
		FshowCursor();
		FexitRawMode();
    }
}
