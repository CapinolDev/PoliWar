using System;
using System.Runtime.InteropServices;
using Poliwar.Policies;
using Poliwar.FertilitySpace;
namespace Poliwar{
class Program
{
    
    const int GAME_NONE = 100;
    const int GAME_EXIT = 101;
    const int GAME_END = 102;
	const int GAME_REVOLUTION = 103;


#region "Dll Imports"
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
#endregion

    static void Main()
    {
		Population gamePopulation = new Population(600,200,200,200);
        PublicReception pubRecep = new PublicReception(0, 600, 0);
		Fertility fertility = new Fertility();
        GameState state = new GameState(GAME_NONE, gamePopulation, 5.5, fertility);
        
        state.publicView = pubRecep;
      
        Console.Clear();
        FenterRawMode();
        FhideCursor();
		char userInput = 'A';
        int currView = GAME_NONE;
        int currAction = GAME_NONE;
        
        
        int pickedPolicy = -1;
        
        bool actionSwitched = false;
        bool policySelected = false;

		var policyFactory= new PolicyFactory();
		var pickedPolicies= policyFactory.GetRandom(3);

		state.chaos = (int)Math.Round(state.population.redPopulation*0.4*state.chaosMult);
		state.order = (int)Math.Round(state.population.greenPopulation*0.2*state.orderMult);

        while (currAction != GAME_EXIT) {
            if (actionSwitched) 
			{
                Console.Clear();
                actionSwitched = false;
                state.currScene = currView;
            }
			if (state.publicView.publicHate >= (int)Math.Round(state.population.totalPopulation * 0.5))
				{
					Console.Clear();
					Console.WriteLine("Revolution - you were killed by your own people");
					while (currAction != GAME_EXIT)
					{
						userInput = FfetchAction();	
						switch (userInput) {
                		case 'q': Console.Clear();
        				FshowCursor();
        				FexitRawMode(); return; }
					}
				}
			if (state.remainingTimeInOffice <= 0)
				{
					Console.Clear();
					Console.WriteLine("Game over - your time ran out");
					while (currAction != GAME_EXIT)
					{
						userInput = FfetchAction();	
						switch (userInput) {
						case 'q': Console.Clear();
        				FshowCursor();
        				FexitRawMode(); return; }
					}
				}	
			gameLoop(state, pickedPolicies, policySelected);

            userInput = FfetchAction();
            switch (userInput) {
                case 'q': currAction = GAME_EXIT; break;
                case 'a':
                    state.remainingTimeInOffice -= 1;
					actionSwitched = true;

					int popBeforeGrowth = state.population.totalPopulation;

					state.population.bluePopulation += (int)Math.Round(state.population.bluePopulation * (state.fertility.blue / 100));
					state.population.redPopulation += (int)Math.Round(state.population.redPopulation * (state.fertility.red / 100));
					state.population.greenPopulation += (int)Math.Round(state.population.greenPopulation * (state.fertility.green / 100));

					state.population.totalPopulation = state.population.greenPopulation + state.population.redPopulation + state.population.bluePopulation;
					int newBirths = state.population.totalPopulation - popBeforeGrowth;

					if (newBirths > 0) {
						
						double totalWeight = state.chaos + state.order;

						if (totalWeight > 0) {
						
							double hatePercent = (double)state.chaos / totalWeight;
							double likePercent = (double)state.order / totalWeight;

							int bornHaters = (int)Math.Round(newBirths * hatePercent);
							int bornLikers = (int)Math.Round(newBirths * likePercent);

						
							state.publicView.publicHate += bornHaters;
							state.publicView.publicLike += bornLikers;

						
							int remaining = newBirths - (bornHaters + bornLikers);
							state.publicView.publicNeutral += remaining;
						} else {
						
							state.publicView.publicNeutral += newBirths;
						}
					}
					if(policySelected){
						var policy= pickedPolicies[pickedPolicy];
						policy.Execute(state);
					}
                    
					pickedPolicies= policyFactory.GetRandom(3);
					
                    pickedPolicy = -1;
                    policySelected = false;
                    
                    state.currMoney += (int)Math.Round(state.avgWage * state.population.totalPopulation*(state.taxRate/100));
					state.chaos = (int)Math.Round(state.population.redPopulation*0.4*state.chaosMult);
					state.order = (int)Math.Round(state.population.greenPopulation*0.2*state.orderMult);
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
    
    private static void gameLoop(GameState state, 
		List<Policy> pickedPolicies, 
		bool policySelected
		){

		var policyFactory= new PolicyFactory();

		for(var i=0; i<policyFactory.Policies.Count; i++)
				{
					FmoveCursor(i*2+5,3);
					Console.Write($"{(i+1)}.{policyFactory.Policies[i].label}");
				}

        FdrawBox(1, 1, 35, 2, "Days remaining: " + state.remainingTimeInOffice);
		FdrawBox(3, 1, 35, 34, "Policies");

		FdrawBox(3, 37, 47, 20, "Public View");
		
		FmoveCursor(4, 38);
		Console.Write("People who like you: " + state.publicView.publicLike);
		FmoveCursor(5, 38);
		Console.Write("People who dont care: " + state.publicView.publicNeutral);
		FmoveCursor(6, 38);
		Console.Write("People who hate you: " + state.publicView.publicHate);
		
		FdrawBox(1, 37, 20, 2, "Money: " + state.currMoney);
		
		FdrawBox(1, 58, 26, 2, "Tax rate: " + state.taxRate+"%");
		FdrawBox(1, 85, 26, 2, "Average wage: " + state.avgWage);
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
		Console.Write("RED: " + state.fertility.red + "%");
		FmoveCursor(13,128);
		Console.Write("GREEN: " + state.fertility.green + "%");
		FmoveCursor(14,128);
		Console.Write("BLUE: " + state.fertility.blue + "%");
		
		
		FdrawBox(3,85,40,20, "Choose a policy");
		if (policySelected == false) {
			
			for(var i=0; i<pickedPolicies.Count; i++)
			{
				FmoveCursor(i*2+5,86);
				Console.Write($"{(i+1)}.{pickedPolicies[i].label}");
			}
		} else {
			for(var i=0; i<pickedPolicies.Count; i++)
			{
				FmoveCursor(i*2+5,86);
				Console.Write("Policy selected");	
			}
		}

	}
	
/*    static (Policy policy, int id) getPolicy(int index) {
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
		}*/
	
}
 
}