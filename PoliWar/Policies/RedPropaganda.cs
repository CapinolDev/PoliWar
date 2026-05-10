using Poliwar;
using Poliwar.FertilitySpace;
namespace Poliwar.Policies{

public class RedPropaganda: Policy{
		
		public RedPropaganda():base("Start propaganda for red") {
			}


		public override void Execute(GameState state){
            state.fertility.red *= 1.2;			
			}
		}

}