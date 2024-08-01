using System.Collections.Generic;

namespace CodeReverie
{
    public interface IStatModifierProvider
    {
        public IEnumerable<float> GetAdditiveStatModifiers(StatAttribute stat);
        public IEnumerable<float> GetPercentageStatModifiers(StatAttribute stat);
    }
}