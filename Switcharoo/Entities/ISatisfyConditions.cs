namespace Switcharoo.Entities
{
    public interface ISatisfyConditions
    {
        bool IsSatisfied(ICondition condition);
    }
}