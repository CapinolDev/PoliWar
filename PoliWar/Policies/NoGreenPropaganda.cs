using Poliwar;
using Poliwar.FertilitySpace;
namespace Poliwar.Policies{

public class AntiGreenPropaganda: Policy{
		
		public AntiGreenPropaganda():base("Start propaganda against green") {
			}


		public override void Execute(GameState state){
            state.fertility.green *= 0.5;			
			}
		}

}