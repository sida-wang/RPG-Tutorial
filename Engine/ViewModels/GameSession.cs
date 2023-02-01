using Engine.Factories;
using Engine.Models;

namespace Engine.ViewModels
{
    public class GameSession
    {
        public Player CurrentPlayer { get; set; }
        public Location? CurrentLocation { get; set; }
        public World CurrentWorld { get; set; }

        public GameSession()
        {
            CurrentPlayer = new Player
            {
                Name = "Sida",
                Gold = 1000000,
                CharacterClass = "Ranger",
                HitPoints = 10,
                ExperiencePoints = 0,
                Level = 1,
            };

            WorldFactory factory = new();
            CurrentWorld = factory.CreateWorld();

            CurrentLocation = CurrentWorld.LocationAt(0, -1);
        }
    }
}
