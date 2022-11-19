using aYoTechTest.BR.Classes;
using aYoTechTest.BR.ViewModels;
using System.Collections.Specialized;

namespace aYoTechTest.BR.Services.Interfaces
{
    public interface IUnitConvertionService
    {
        Task<ServiceActionResult<decimal>> ProcessConvertion(ConvertUnitViewModel data);
        Task<IEnumerable<SupportedConversionViewModel>> GetSupportedConversionList();
        Task<IEnumerable<MeasuringUnitViewModel>> GetMeasuringUnitList(NameValueCollection nvcParam = null);
    }
}
