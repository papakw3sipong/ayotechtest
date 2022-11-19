using aYoTechTest.BR.Enums;

namespace aYoTechTest.Models.Entities
{
    public class MeasuringUnit : EntityBase
    {
        public int MeasuringUnitId { get; set; }
        public MeasuringUnitType UnitType { get; set; }
        public string MetricUnitDesc { get; set; }
        public string UnitOfMeasure { get; set; }


        public ICollection<SupportedConversion> SourceSupportedConversion { get; set; }
        public ICollection<SupportedConversion> TargetSupportedConversion { get; set; }
    }
}
