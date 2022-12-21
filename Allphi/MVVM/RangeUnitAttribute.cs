using System.ComponentModel.DataAnnotations;

namespace MVVM
{
    public class RangeUnitAttribute : RangeAttribute
    {
        public RangeUnitAttribute(int minimum, int maximum, string units) : base(minimum, maximum)
        {
            Units = units;
        }

        public RangeUnitAttribute(double minimum, double maximum, string units) : base(minimum, maximum)
        {
            Units = units;
        }

        public string Units { get; }
    }
}