namespace aYoTechTest.BR.ViewModels
{
    public class ConvertUnitResponse
    {
        public string Description { get; set; }
        public string SourceUnitName { get; set; }
        public string SourceUnitMeasure { get; set; }
        public decimal SourceValue { get; set; }
        public string ConvertedUnitMeasure { get; set; }
        public string ConvertedUnitName { get; set; }
        public decimal ConvertedValue { get; set; }
    }
}
