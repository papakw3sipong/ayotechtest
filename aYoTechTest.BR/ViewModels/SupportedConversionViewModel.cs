using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aYoTechTest.BR.ViewModels
{
    public class SupportedConversionViewModel
    {
        public int SupportedConversionId { get; set; }      
        public string SourceUnitName { get; set; }
        public string SourceUnitMeasure { get; set; }       
        public string TargetUnitName { get; set; }
        public string TargetUnitMeasure { get; set; }
        public string ConversionType { get; set; }
        public string Description { get; set; }
    }
}
