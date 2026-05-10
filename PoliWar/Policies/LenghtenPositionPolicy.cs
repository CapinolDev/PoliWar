using Poliwar;
namespace Poliwar.Policies{

public class LenghtenPositionPolicy: Policy{
		
		public LenghtenPositionPolicy():base("increase position by 10 days") {
			}


		public override void Execute(GameState state){
							
			int gainHaters = (int)Math.Round(state.population.totalPopulation * 0.03);
		
            gainHaters = Math.Min(gainHaters, state.publicView.publicNeutral);
            
            state.publicView.publicNeutral -= gainHaters;
            state.publicView.publicHate += gainHaters;
			state.remainingTimeInOffice += 10;
			}
		}

}