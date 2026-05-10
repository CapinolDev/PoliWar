using Poliwar;
using Poliwar.FertilitySpace;
namespace Poliwar.Policies{

public class AntiBluePropaganda: Policy{
		
		public AntiBluePropaganda():base("Start propaganda against blue") {
			}


		public override void Execute(GameState state){
            state.fertility.blue *= 0.5;			
			}
		}

}