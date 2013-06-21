namespace Switcharoo.Entities
{
    public interface ICondition
    {
        string Key { get; }
        bool IsMatch(string condition);
    }
}