namespace GameLibrary;

public class QuestLog
{
    public List<string> Quests { get; private set; }

    public QuestLog(string initialQuest)
    {
        Quests = new(){ initialQuest };
    }

    public bool AddQuest(string quest)
    {
        Quests.Insert(0, quest);
        return true;
    }
}