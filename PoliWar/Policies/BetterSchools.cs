using Poliwar;
using Poliwar.FertilitySpace;
namespace Poliwar.Policies{

public class BetterSchools: Policy{
		
		public BetterSchools():base("Invest 8000 into schools") {
			}


		public override void Execute(GameState state){
            if (state.currMoney >= 8000)
			{
				state.currMoney -= 8000;
				state.avgWage = (int)Math.Round(state.avgWage*1.2);
				state.chaosMult -= 0.2;


			}		
			}
		}

}