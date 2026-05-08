using Poliwar;
namespace Poliwar.Policies{
public abstract class Policy {
		public string label;
		public Policy(string label) {
			this.label = label;
			}
			
		public abstract void Execute(GameState state);
		}
}