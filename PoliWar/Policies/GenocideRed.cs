using Poliwar;
using Poliwar.FertilitySpace;
namespace Poliwar.Policies{

public class GenocideRed: Policy{
		
		public GenocideRed():base("Genocide red") {
			}


		public override void Execute(GameState state)		{
			
			int redCount = state.population.redPopulation;
			int totalBefore = state.population.totalPopulation;

			if (totalBefore <= 0) return;

			double redRatio = (double)redCount / totalBefore;

			
			state.publicView.publicLike -= (int)Math.Round(state.publicView.publicLike * redRatio);
			state.publicView.publicNeutral -= (int)Math.Round(state.publicView.publicNeutral * redRatio);
			state.publicView.publicHate -= (int)Math.Round(state.publicView.publicHate * redRatio);

			
			state.population.redPopulation = 0;
			state.fertility.red = 0.0;
			
			
			state.population.totalPopulation = state.population.bluePopulation + state.population.greenPopulation;

			int gainHaters = (int)Math.Round(state.publicView.publicNeutral * 0.5);
			
			state.publicView.publicNeutral -= gainHaters;
			state.publicView.publicHate += gainHaters;
		}

} }