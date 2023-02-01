using Engine.Models;

namespace Engine.ViewModels
{
    public class GameSession
    {
        public Player CurrentPlayer { get; set; }
        public Location CurrentLocation { get; set; }

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

            CurrentLocation = new Location
            {
                Name = "Home",
                XCoordinate = 0,
                YCoordinate = -1,
                Description = "This is your house",
                ImageName = "/Engine;component/Images/Locations/Home.png",
            };
        }
    }
}
