namespace Battle.Projectile
{
    public class ProjectileEntity
    {
        private float _timeSincePrepare;

        public int AttackDamage { get; set; }
        public int AttackRange { get; private set; }
        public float PrepareTime { get; private set; }
        public int MoveSpeed { get; private set; }

        public bool IsReady => _timeSincePrepare >= PrepareTime;

        public void Load(ProjectileData data)
        {
            AttackRange = data.AttackRange;
            PrepareTime = data.PrepareTime;
            MoveSpeed = data.MoveSpeed;
        }

        public void RePrepare()
        {
            _timeSincePrepare = 0;
        }

        public void Prepare(float time)
        {
            _timeSincePrepare += time;
        }
    }
}