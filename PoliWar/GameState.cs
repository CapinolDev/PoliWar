using Poliwar.FertilitySpace;
namespace Poliwar{
public class GameState {
        public int currScene;
        public int currMoney = 1000;
        public Population population;
        public double taxRate;
        public int avgWage = 10;
        public int chaos = 10;
        public int order = 90;
        public double chaosMult = 1.0; 
        public double orderMult = 1.0;
        public int remainingTimeInOffice = 20;
        public PublicReception publicView; 

        public Fertility fertility;
        public GameState(int scene, Population population, double taxRate, Fertility fertility) {
            this.currScene = scene;
            
            this.population = population;
            this.taxRate = taxRate;
            this.fertility = fertility;
        }
    }
}