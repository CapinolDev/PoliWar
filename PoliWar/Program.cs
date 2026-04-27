using System;
using System.Runtime.InteropServices;
class Program
{
    
    class GameState {
        public int currScene;
        public bool isInWar;
        public Population population;
        public double taxRate;
        public int chaos = 0;
        public int order = 100;
        public PublicReception publicView; 
        public GameState(int scene, bool inWar, Population population, double taxRate) {
            this.currScene = scene;
            this.isInWar = inWar;
            this.population = population;
            this.taxRate = taxRate;
        }
    }
	class Population {
		public int totalPopulation;
		public int greenPopulation;
		public int bluePopulation;
		public int redPopulation;
		public Population(int tot,int red, int green, int blue) {
			this.totalPopulation = tot;
			this.greenPopulation = green;
			this.bluePopulation = blue;
			this.redPopulation = red;
			}
		}
    class PublicReception {
        public int publicLike;
        public int publicNeutral;
        public int publicHate;
        public PublicReception(int like, int neutral, int hate) {
            this.publicLike = like;
            this.publicNeutral = neutral;
            this.publicHate = hate;
        }
    }
	class Policy {
		public string label;
		public Policy(string label) {
			this.label = label;
			}
		}
	class Fertility {
		public double total = 4.5;
		public double green = 1.5;
		public double red = 1.5;
		public double blue = 1.5; 
		}
    const int GAME_NONE = 100;
    const int GAME_EXIT = 101;
    const int GAME_MAP = 104;

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
		Random rnd = new Random();
		Population gamePopulation = new Population(600,200,200,200);
        PublicReception pubRecep = new PublicReception(0, 500, 0);
        GameState state = new GameState(GAME_NONE, false, gamePopulation, 5.5);
        Fertility fertility = new Fertility();
        state.publicView = pubRecep;
      
        Console.Clear();
        FenterRawMode();
        FhideCursor();
		var pickedPolicies = new Policy[3];
        char userInput = 'A';
        int currView = GAME_NONE;
        int currAction = GAME_NONE;
        int currMoney = 1000;
        int remainingTimeInOffice = 90;
        int pickedPolicy = -1;
        int idx1;
        int idx2;
        int idx3;
        int avgWage = 10;
        bool actionSwitched = false;
        bool policySelected = false;
        char[,] gameMap = new char[16, 58];
        for (int iX = 0; iX < gameMap.GetLength(0); iX++) {
            for (int iY = 0; iY < gameMap.GetLength(1); iY++) { 
                gameMap[iX, iY] = 'o';
            }
        }
        (pickedPolicies[0], idx1) = getPolicy(rnd.Next(20));
		(pickedPolicies[1], idx2) = getPolicy(rnd.Next(20));
		(pickedPolicies[2], idx3) = getPolicy(rnd.Next(20));

        while (currAction != GAME_EXIT) {
            if (actionSwitched) {
                Console.Clear();
                actionSwitched = false;
                state.currScene = currView;
            }

            switch (currView) {
                case GAME_NONE:
                    FdrawBox(1, 1, 35, 2, "Days remaining: " + remainingTimeInOffice);
                    FdrawBox(3, 1, 35, 20, "Policies");
                    FdrawBox(3, 37, 47, 20, "Public View");
                    
                    FmoveCursor(4, 38);
                    Console.Write("People who like you: " + state.publicView.publicLike);
                    FmoveCursor(5, 38);
                    Console.Write("People who dont care: " + state.publicView.publicNeutral);
                    FmoveCursor(6, 38);
                    Console.Write("People who hate you: " + state.publicView.publicHate);
                    
                    FdrawBox(1, 37, 20, 2, "Money: " + currMoney);
                    
                    FdrawBox(1, 58, 26, 2, "Tax rate: " + state.taxRate+"%");
                    FdrawBox(3, 126, 40, 20, "Population: " + state.population.totalPopulation);
                    FmoveCursor(4,128);
                    Console.Write("RED: " + state.population.redPopulation);
                    FmoveCursor(5,128);
                    Console.Write("GREEN: " + state.population.greenPopulation);
                    FmoveCursor(6,128);
                    Console.Write("BLUE: " + state.population.bluePopulation);
                    FmoveCursor(8,128);
                    Console.Write("Order: " + state.order + "%");
                    FmoveCursor(9,128);
                    Console.Write("Chaos: " + state.chaos + "%");
                    FmoveCursor(11,128);
                    Console.Write("== FERTILITY ==");
                    FmoveCursor(12,128);
                    Console.Write("RED: " + fertility.red + "%");
                    FmoveCursor(13,128);
                    Console.Write("GREEN: " + fertility.green + "%");
                    FmoveCursor(14,128);
                    Console.Write("BLUE: " + fertility.blue + "%");
                    
                    
                    FdrawBox(3,85,40,20, "Choose a policy");
                    if (policySelected == false) {
						FmoveCursor(5,86);
						Console.Write("1. " + pickedPolicies[0].label);
						FmoveCursor(7,86);
						Console.Write("2. " + pickedPolicies[1].label);
						FmoveCursor(9,86);
						Console.Write("3. " + pickedPolicies[2].label);							
					} else {
						FmoveCursor(5,86);
						Console.Write("Policy selected");	
						FmoveCursor(7,86);
						Console.Write("Policy selected");	
						FmoveCursor(9,86);
						Console.Write("Policy selected");	
					}
                    
                    break;

                case GAME_MAP:
                    FdrawBox(1, 1, 60, 60, "Map");
                    for (int iX = 0; iX < gameMap.GetLength(0); iX++) {
                        for (int iY = 0; iY < gameMap.GetLength(1); iY++) { 
                            FmoveCursor(iX + 2, iY + 2);
                            Console.Write(gameMap[iX, iY]);
                        }
                    }
                    break;
            }

            userInput = FfetchAction();
            switch (userInput) {
                case 'q': currAction = GAME_EXIT; break;
                case 'a':
                    remainingTimeInOffice -= 1;
                    actionSwitched = true;
                    
                    int activePolicyIndex = -1;
					if (pickedPolicy == 0) activePolicyIndex = idx1;
					else if (pickedPolicy == 1) activePolicyIndex = idx2;
					else if (pickedPolicy == 2) activePolicyIndex = idx3;

                    (pickedPolicies[0], idx1) = getPolicy(rnd.Next(20));
                    (pickedPolicies[1], idx2) = getPolicy(rnd.Next(20));
                    (pickedPolicies[2], idx3) = getPolicy(rnd.Next(20));
                    state.population.bluePopulation += (int)Math.Round(state.population.bluePopulation*(fertility.blue/100));
                    state.population.redPopulation += (int)Math.Round(state.population.redPopulation*(fertility.red/100));
                    state.population.greenPopulation += (int)Math.Round(state.population.greenPopulation*(fertility.green/100));
                    state.population.totalPopulation = state.population.greenPopulation + state.population.redPopulation + state.population.bluePopulation;
                    
					switch(activePolicyIndex) {
						case 0: state.taxRate -= 1; break;
						case 1: state.taxRate += 1; break;
					}
                    pickedPolicy = -1;
                    policySelected = false;
                    
                    currMoney += (int)Math.Round(avgWage * state.population.totalPopulation*(state.taxRate/100));
                    break;
                case 'm':
                    currView = (currView == GAME_MAP) ? GAME_NONE : GAME_MAP;
                    actionSwitched = true;
                    break;
                case '1':
					policySelected = true;
					actionSwitched = true;
					pickedPolicy = 0;
                    break;
                case '2':
					policySelected = true;
					actionSwitched = true;
					pickedPolicy = 1;
                    break;
                case '3':
					policySelected = true;
					actionSwitched = true;
					pickedPolicy = 2;
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
    static (Policy policy, int id) getPolicy(int index) {
		var policies = new Policy[20];
		for (int iX=0;iX<20;iX++) {
			policies[iX] = new Policy("TEST");	
			}
		policies[0] = new Policy("Decrease Tax by 1%");	
		policies[1] = new Policy("Increase Tax by 1%");	
		policies[2] = new Policy("Remove opposition");	
		policies[3] = new Policy("Lenghten position by 10 days");	
		policies[4] = new Policy("Legalize murder");	
		policies[5] = new Policy("Invest into propaganda");	
		policies[6] = new Policy("Start propaganda against RED");		
		policies[7] = new Policy("Start propaganda against BLUE");		
		policies[8] = new Policy("Start propaganda against GREEN");		
		policies[9] = new Policy("Remove democracy");		
		return (policies[index], index);
		}
	
}
 
