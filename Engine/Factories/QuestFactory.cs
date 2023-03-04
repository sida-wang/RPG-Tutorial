using Engine.Models;

namespace Engine.Factories
{
    internal static class QuestFactory
    {
        private static readonly List<Quest> _quests = new();
        static QuestFactory()
        {
            List<ItemQuantity> itemsToComplete = new()
            {
                new ItemQuantity(9001, 5),
            };
            List<ItemQuantity> rewardItems = new()
            {
                new ItemQuantity(1002,1),
            };

            _quests.Add(new Quest(1, "Clear the herb garden", "Defeat the snakes in the Herbalist's garden",
                itemsToComplete, 25, 10, rewardItems));
        }

        internal static Quest? GetQuestByID(int id)
        {
            return _quests.FirstOrDefault(quest => quest.ID == id);
        }
    }
}
