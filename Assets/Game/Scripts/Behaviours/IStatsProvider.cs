public interface IStatsProvider
{
	int GetStat(StatTypes name);
	void SetStat(StatTypes name, int value);
}
