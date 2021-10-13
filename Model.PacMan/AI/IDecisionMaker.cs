namespace Model.PacMan
{
    using System;

    public interface IDecisionMaker
    {
        event Action<IDecisionMaker> OnSwitch;
    }
}