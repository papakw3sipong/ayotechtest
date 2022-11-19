using aYoTechTest.BR.Models.Interfaces;

namespace aYoTechTest.Models.Entities
{
    public class SupportedConversion : EntityBase, IEntityBase
    {
        public int SupportedConversionId { get; set; }
        public byte ConversionType { get; set; }
        public int SourceUnitId { get; set; }
        public int TargetUnitId { get; set; }
        public decimal SourceUnitValue { get; set; }
        public decimal Multiplier { get; set; }


        public MeasuringUnit SourceMeasuringUnit { get; set; }
        public MeasuringUnit TargetMeasuringUnit { get; set; }
    }
}
