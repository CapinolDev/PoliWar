namespace Poliwar{
public class GameState {
        public int currScene;
        public bool isInWar;
        public Population population;
        public double taxRate;
        public int chaos = 0;
        public int order = 100;
        public PublicReception publicView; 
        public GameState(int scene, bool inWar, Population population, double taxRate) {
            this.currScene = scene;
            this.isInWar = inWar;
            this.population = population;
            this.taxRate = taxRate;
        }
    }
}