using Poliwar;
using Poliwar.FertilitySpace;
namespace Poliwar.Policies{

public class GenocideGreen: Policy{
		
		public GenocideGreen():base("Genocide green") {
			}


		public override void Execute(GameState state)		{
			
			int greenCount = state.population.greenPopulation;
			int totalBefore = state.population.totalPopulation;

			if (totalBefore <= 0) return;

			double greenRatio = (double)greenCount / totalBefore;

			
			state.publicView.publicLike -= (int)Math.Round(state.publicView.publicLike * greenRatio);
			state.publicView.publicNeutral -= (int)Math.Round(state.publicView.publicNeutral * greenRatio);
			state.publicView.publicHate -= (int)Math.Round(state.publicView.publicHate * greenRatio);

			
			state.population.greenPopulation = 0;
			state.fertility.green = 0.0;
			
			
			state.population.totalPopulation = state.population.bluePopulation + state.population.redPopulation;

			int gainHaters = (int)Math.Round(state.publicView.publicNeutral * 0.5);
			
			state.publicView.publicNeutral -= gainHaters;
			state.publicView.publicHate += gainHaters;
		}

} }