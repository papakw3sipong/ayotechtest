using aYoTechTest.BR.Classes;
using aYoTechTest.BR.Enums;
using aYoTechTest.BR.Services.Interfaces;
using aYoTechTest.BR.ViewModels;
using aYoTechTest.DAL.Classes;
using aYoTechTest.Models.Entities;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Specialized;
using System.Text;

namespace aYoTechTest.Services.Classes
{
    public class UnitConversionService : IUnitConvertionService
    {

        private readonly AppDataContext _context;

        public UnitConversionService(AppDataContext context)
        {
            _context = context;
        }

        public async Task<ServiceActionResult<decimal>> ProcessConvertion(ConvertUnitViewModel data)
        {

            ServiceActionResult<decimal> _result = new ServiceActionResult<decimal>(0);

            if (!data.IsValid)
                return default(ServiceActionResult<decimal>);


            if (!(await ValidateIsSupportedConversionType(data)))
            {
                _result.Message = "Un-Supported Convertion Type! Please check and try again!";
                return _result;
            }

            return await ProcessConversion(data);

        }


        public async Task<IEnumerable<SupportedConversionViewModel>> GetSupportedConversionList()
        {
            return await Task.Run(() =>
             {
                 return _context.SupportedConversions.Include("SourceMeasuringUnit").Include("TargetMeasuringUnit").AsNoTracking().AsEnumerable().OrderBy(o => o.ConversionType).Select(s =>

                 new SupportedConversionViewModel()
                 {
                     SourceUnitMeasure = s.SourceMeasuringUnit.UnitOfMeasure,
                     SourceUnitName = s.SourceMeasuringUnit.MetricUnitDesc,
                     SupportedConversionId = s.SupportedConversionId,
                     TargetUnitMeasure = s.TargetMeasuringUnit.UnitOfMeasure,
                     TargetUnitName = s.TargetMeasuringUnit.MetricUnitDesc,
                     Description = $"{s.SourceMeasuringUnit.MetricUnitDesc} ==> {s.TargetMeasuringUnit.MetricUnitDesc}",
                     ConversionType = Enum.GetName(typeof(ConversionType), s.ConversionType).ToString()
                 }).OrderBy(o => o.SourceUnitName).ToList();
             });
        }


        public async Task<IEnumerable<MeasuringUnitViewModel>> GetMeasuringUnitList(NameValueCollection nvcParam = null)
        {

            var searchPredicate = PredicateBuilder.New<MeasuringUnit>();

            searchPredicate = searchPredicate.And(x => x.DeletedById == null && x.DeletedAt == null);

            if (nvcParam != null)
            {
                var keys = nvcParam.AllKeys;
                foreach (string key in keys)
                {
                    string condition = string.Empty;
                    condition = nvcParam[key];
                    switch (key)
                    {
                        case "unitType":
                            byte unitType;
                            byte.TryParse(condition, out unitType);
                            searchPredicate = searchPredicate.And(a => a.UnitType.Equals(unitType));
                            break;
                    }
                }
            }
            IQueryable<MeasuringUnit> queryResult = _context.MeasuringUnits.AsNoTracking().OrderBy(o => o.MetricUnitDesc).AsQueryable();

            return await Task.Run(() =>
            {
                return queryResult.AsEnumerable().Select(s =>

                 new MeasuringUnitViewModel()
                 {
                     MeasuringUnitId = s.MeasuringUnitId,
                     MetricUnitDesc = s.MetricUnitDesc,
                     UnitOfMeasure = s.UnitOfMeasure,
                     UnitType = Enum.GetName(typeof(MeasuringUnitType), s.UnitType).ToString()
                 }).ToList();
            });
        }

        private async Task<bool> ValidateIsSupportedConversionType(ConvertUnitViewModel data)
        {
            SupportedConversion _supportedConversion = await GetSupportedConversionById(data.SupportedConversionId);
            return _supportedConversion != null;
        }

        private async Task<ServiceActionResult<decimal>> ProcessConversion(ConvertUnitViewModel data)
        {
            StringBuilder _message = new StringBuilder();

            MeasuringUnit _sourceUnit = null;
            MeasuringUnit _targetUnit = null;

            var _conversionInfo = await GetSupportedConversionById(data.SupportedConversionId);

            switch (_conversionInfo.ConversionType)
            {
                case (byte)ConversionType.Metric_To_Imperical:
                    _sourceUnit = await GetMetricUnitByIdAsync(_conversionInfo.SourceUnitId);
                    _targetUnit = await GetImperialUnitByIdAsync(_conversionInfo.TargetUnitId);
                    break;
                case (byte)ConversionType.Imperical_To_Metric:
                    _sourceUnit = await GetImperialUnitByIdAsync(_conversionInfo.SourceUnitId);
                    _targetUnit = await GetMetricUnitByIdAsync(_conversionInfo.TargetUnitId);
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Un-Known Conversion Type {_conversionInfo.ConversionType.ToString()}");
            }

            _message.AppendLine($"Converting Form {_sourceUnit.UnitOfMeasure} to {_targetUnit.UnitOfMeasure}");

            decimal _convertedValue = data.UnitValue * _conversionInfo.Multiplier;

            _message.AppendLine($"{data.UnitValue} {_sourceUnit.UnitOfMeasure} to {_targetUnit.UnitOfMeasure} ==> {_convertedValue} {_targetUnit.UnitOfMeasure}");

            return new ServiceActionResult<decimal>(_convertedValue, _message.ToString(), true);
        }

        private async Task<SupportedConversion> GetSupportedConversion(int sourceUnitId, int targetUnitId)
        {
            return await _context.SupportedConversions.Where(x => x.SourceUnitId == sourceUnitId && x.TargetUnitId == targetUnitId).SingleOrDefaultAsync();
        }

        private async Task<SupportedConversion> GetSupportedConversionById(int supportedConversionId)
        {
            return await _context.SupportedConversions.Where(x => x.SupportedConversionId == supportedConversionId).SingleOrDefaultAsync();
        }

        private async Task<MeasuringUnit> GetMetricUnitByIdAsync(int metricUnitId)
        {
            return await GetMeasuringUnitAsync(metricUnitId, (byte)MeasuringUnitType.Metric_Unit);
        }

        private async Task<MeasuringUnit> GetImperialUnitByIdAsync(int imperialUnitId)
        {
            return await GetMeasuringUnitAsync(imperialUnitId, MeasuringUnitType.Imperial_Unit);
        }

        private async Task<MeasuringUnit> GetMeasuringUnitAsync(int measuringUnitId, MeasuringUnitType measuringUnitType)
        {
            return await _context.MeasuringUnits.Where(x => x.UnitType.Equals(measuringUnitType) && x.MeasuringUnitId == measuringUnitId).SingleOrDefaultAsync();
        }

    }
}
