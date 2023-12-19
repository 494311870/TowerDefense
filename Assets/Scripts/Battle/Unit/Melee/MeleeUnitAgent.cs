using Battle.Unit.Shared;

namespace Battle.Unit.Melee
{
    public class MeleeUnitAgent : UnitAgent
    {
        private void Start()
        {
            unitAnimator.SetBool("Grounded", true);
        }
    }
}