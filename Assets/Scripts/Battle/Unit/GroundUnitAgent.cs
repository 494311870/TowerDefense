namespace Battle.Unit
{
    public class GroundUnitAgent : UnitAgent
    {
        private void Start()
        {
            unitAnimator.SetBool("Grounded", true);
        }
    }
}