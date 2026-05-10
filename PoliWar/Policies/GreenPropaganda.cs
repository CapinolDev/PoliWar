using Poliwar;
using Poliwar.FertilitySpace;
namespace Poliwar.Policies{

public class GreenPropaganda: Policy{
		
		public GreenPropaganda():base("Start propaganda for green") {
			}


		public override void Execute(GameState state){
            state.fertility.green *= 1.2;			
			}
		}

}