using aYoTechTest.BR.Enums;


namespace aYoTechTest.BR.ViewModels
{
    public class ConvertUnitViewModel
    {        
        public int SupportedConversionId { get; set; }       
        public decimal UnitValue { get; set; }

        public bool IsValid
        {
            get
            {

                return SupportedConversionId > 0 &&  UnitValue > 0.0m;
            }
        }
    }
}
