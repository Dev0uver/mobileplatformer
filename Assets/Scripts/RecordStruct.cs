[System.Serializable]
public struct RecordStruct {
    public int id;
    public int score;
    public int userId;
    public int level;
    public string time;
    public string nickname;

    public string toString() {
        return $"Nickname = {nickname}, Time = {time}, Score = {score}";
    }
}