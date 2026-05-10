using Poliwar;
namespace Poliwar.Policies{
    public class PolicyFactory{
		
		private List<Policy> _policies;
		public PolicyFactory(){
			_policies= new List<Policy>(){
				new DecreaseTaxPolicy(),
				new IncreaseTaxPolicy(),
				new RemoveOppositionPolicy(),
				new LenghtenPositionPolicy(),
				new AntiRedPropaganda(),
				new AntiGreenPropaganda(),
				new AntiBluePropaganda(),
				new BetterSchools(),
				new GenocideRed(),
				new GenocideGreen(),
				new GenocideBlue(),
				new NationalPropaganda(),
				new RedPropaganda(),
				new GreenPropaganda(),
				new BluePropaganda()
				};
			}
			
		public List<Policy> Policies{
			get
			{
				return _policies;
			}
		}
		
		public List<Policy> GetRandom(int count){
			Random rnd = new Random();

			var result= new List<Policy>();
					
			for(var i=0;i<count; i++){
				var randPolicy=rnd.Next(_policies.Count);
				result.Add(_policies[randPolicy]);
			}
			return result;
			
	   }
			
	}
    
}