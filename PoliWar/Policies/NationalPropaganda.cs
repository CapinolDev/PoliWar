using Poliwar;
using Poliwar.FertilitySpace;
namespace Poliwar.Policies{

public class NationalPropaganda: Policy{
		
		public NationalPropaganda():base("Invest 2000 into propaganda") {
			}


		public override void Execute(GameState state){
            if (state.currMoney >= 2000)
			{
				state.currMoney -= 2000;
				int gainLikers = (int)Math.Round(state.population.totalPopulation * 0.06);
			
				gainLikers = Math.Min(gainLikers, state.publicView.publicNeutral);
			
				state.publicView.publicNeutral -= gainLikers;
				state.publicView.publicLike += gainLikers;


			}		
			}
		}

}