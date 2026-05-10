using Poliwar;
namespace Poliwar.Policies{
public class IncreaseTaxPolicy: Policy{

		public IncreaseTaxPolicy():base("increase tax by 1%") {
				}


		public override void Execute(GameState state){

		state.taxRate += 1;
		
		
		int gainHaters = (int)Math.Round(state.population.totalPopulation * 0.03);
		
		gainHaters = Math.Min(gainHaters, state.publicView.publicNeutral);
		
		state.publicView.publicNeutral -= gainHaters;
		state.publicView.publicHate += gainHaters;


			}
		}
}