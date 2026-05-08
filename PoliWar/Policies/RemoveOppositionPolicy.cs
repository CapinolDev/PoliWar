using Poliwar;
namespace Poliwar.Policies{
public class RemoveOppositionPolicy: Policy{
				public RemoveOppositionPolicy():base("remove_opposition") {
				}

		public override void Execute(GameState state){

			int totalHate = state.publicView.publicHate;
			int totalPopBefore = state.population.totalPopulation;

			if (totalPopBefore > 0 && totalHate > 0) {
				
				double reductionRatio = (double)totalHate / totalPopBefore;
				state.population.redPopulation -= (int)Math.Round(state.population.redPopulation * reductionRatio);
				state.population.greenPopulation -= (int)Math.Round(state.population.greenPopulation * reductionRatio);
				state.population.bluePopulation -= (int)Math.Round(state.population.bluePopulation * reductionRatio);
				state.population.redPopulation = Math.Max(0, state.population.redPopulation);
				state.population.greenPopulation = Math.Max(0, state.population.greenPopulation);
				state.population.bluePopulation = Math.Max(0, state.population.bluePopulation);
				state.population.totalPopulation = state.population.redPopulation + 
												   state.population.greenPopulation + 
												   state.population.bluePopulation;
			}


			state.publicView.publicHate = state.publicView.publicNeutral;
			state.publicView.publicNeutral = 0;
							



			}
		}
}