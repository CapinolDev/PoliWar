using Poliwar;
using Poliwar.FertilitySpace;
namespace Poliwar.Policies{

public class GenocideBlue: Policy{
		
		public GenocideBlue():base("Genocide blue") {
			}


		public override void Execute(GameState state)		{
			
			int blueCount = state.population.bluePopulation;
			int totalBefore = state.population.totalPopulation;

			if (totalBefore <= 0) return;

			double blueRatio = (double)blueCount / totalBefore;

			
			state.publicView.publicLike -= (int)Math.Round(state.publicView.publicLike * blueRatio);
			state.publicView.publicNeutral -= (int)Math.Round(state.publicView.publicNeutral * blueRatio);
			state.publicView.publicHate -= (int)Math.Round(state.publicView.publicHate * blueRatio);

			
			state.population.bluePopulation = 0;
			state.fertility.blue = 0.0;
			
			
			state.population.totalPopulation = state.population.redPopulation + state.population.greenPopulation;

			int gainHaters = (int)Math.Round(state.publicView.publicNeutral * 0.5);
			
			state.publicView.publicNeutral -= gainHaters;
			state.publicView.publicHate += gainHaters;
		}

} }