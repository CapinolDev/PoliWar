using Poliwar;
using Poliwar.FertilitySpace;
namespace Poliwar.Policies{

public class AntiRedPropaganda: Policy{
		
		public AntiRedPropaganda():base("Start propaganda against red") {
			}


		public override void Execute(GameState state){
            state.fertility.red *= 0.5;			
			}
		}

}