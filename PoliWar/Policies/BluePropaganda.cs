using Poliwar;
using Poliwar.FertilitySpace;
namespace Poliwar.Policies{

public class BluePropaganda: Policy{
		
		public BluePropaganda():base("Start propaganda for blue") {
			}


		public override void Execute(GameState state){
            state.fertility.blue *= 1.2;			
			}
		}

}