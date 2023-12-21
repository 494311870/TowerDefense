using System.Collections.Generic;
using Battle.Shared;
using Battle.Unit.Shared;

namespace Battle.Faction
{
    public class FactionContext
    {
        /// <summary>
        ///     阵营的全部单位
        /// </summary>
        private List<UnitContext> Units { get; } = new();

        /// <summary>
        ///     当前的目标
        /// </summary>
        public ITarget Target { get; set; }

        /// <summary>
        ///     阵营的层级
        /// </summary>
        public int Layer { get; set; }

        public void AddUnit(UnitContext unit)
        {
            Units.Add(unit);
        }

        public void RemoveUnit(UnitContext unit)
        {
            Units.Remove(unit);
        }

        public UnitContext CreateUnit()
        {
            return new UnitContext
            {
                FactionLayer = Layer
            };
        }
    }
}