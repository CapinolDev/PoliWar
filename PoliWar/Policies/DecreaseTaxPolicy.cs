using Poliwar;
namespace Poliwar.Policies{

public class DecreaseTaxPolicy: Policy{
		
		public DecreaseTaxPolicy():base("decrease_tax") {
			}


		public override void Execute(GameState state){
			state.taxRate -= 1;
							
			int gainLikers = (int)Math.Round(state.population.totalPopulation * 0.03);
			
			gainLikers = Math.Min(gainLikers, state.publicView.publicNeutral);
			
			state.publicView.publicNeutral -= gainLikers;
			state.publicView.publicLike += gainLikers;
			
			}
		}

}