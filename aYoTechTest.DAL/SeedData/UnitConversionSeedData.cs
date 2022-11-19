using aYoTechTest.BR.Enums;
using aYoTechTest.DAL.Classes;
using aYoTechTest.Models.Entities;

namespace aYoTechTest.DAL.SeedData
{
    public class UnitConversionSeedData
    {
        private readonly static DateTime _nowTimestamp = DateTime.Now;
        private readonly AppDataContext _context;
        public UnitConversionSeedData(AppDataContext context)
        {
            _context = context;
        }

        public void InitializeDb()
        {
            _context.Database.EnsureCreated();

            if (!_context.MeasuringUnits.Any(x => x.MetricUnitDesc.Contains("Kilometers")))
            {
                var metricUnits = new List<MeasuringUnit>()
                   {

                new MeasuringUnit() {  MetricUnitDesc = "Kilometers", UnitOfMeasure = "km", UnitType = MeasuringUnitType.Metric_Unit },
                new MeasuringUnit() {  MetricUnitDesc = "Meters", UnitOfMeasure = "mile", UnitType = MeasuringUnitType.Metric_Unit },
                new MeasuringUnit() {  MetricUnitDesc = "Centimeters", UnitOfMeasure = "cm", UnitType = MeasuringUnitType.Metric_Unit },
                new MeasuringUnit() {  MetricUnitDesc = "Millimeters", UnitOfMeasure = "mm", UnitType = MeasuringUnitType.Metric_Unit },
                new MeasuringUnit() {  MetricUnitDesc = "Liters", UnitOfMeasure = "lit", UnitType = MeasuringUnitType.Metric_Unit },
                new MeasuringUnit() {  MetricUnitDesc = "Kilogram", UnitOfMeasure = "kg", UnitType = MeasuringUnitType.Metric_Unit }

                };
                _context.AddRange(metricUnits);


                var imperialUnits = new List<MeasuringUnit>()
                   {

                new MeasuringUnit() {  MetricUnitDesc = "Mile(s)", UnitOfMeasure = "ml", UnitType = MeasuringUnitType.Imperial_Unit },
                new MeasuringUnit() {  MetricUnitDesc = "Feet", UnitOfMeasure = "ft", UnitType = MeasuringUnitType.Imperial_Unit },
                new MeasuringUnit() {  MetricUnitDesc = "Inche(s)", UnitOfMeasure = "inc", UnitType = MeasuringUnitType.Imperial_Unit },
                new MeasuringUnit() {  MetricUnitDesc = "Quarts", UnitOfMeasure = "qt", UnitType = MeasuringUnitType.Imperial_Unit },
                new MeasuringUnit() {  MetricUnitDesc = "Gallons", UnitOfMeasure = "gal", UnitType = MeasuringUnitType.Imperial_Unit },
                new MeasuringUnit() {  MetricUnitDesc = "Cups", UnitOfMeasure = "cup", UnitType = MeasuringUnitType.Imperial_Unit }

                };
                _context.AddRange(imperialUnits);
                _context.SaveChanges();
            }


            if (_context.SupportedConversions.Count() == 0)
            {

                //Sample Conversion Seed
                var _km = _context.MeasuringUnits.SingleOrDefault(x => x.MetricUnitDesc == "Kilometers");
                var _mile = _context.MeasuringUnits.SingleOrDefault(x => x.MetricUnitDesc == "Mile(s)");
                var _feet = _context.MeasuringUnits.SingleOrDefault(x => x.MetricUnitDesc == "Feet");
                var _meter = _context.MeasuringUnits.SingleOrDefault(x => x.MetricUnitDesc == "Meters");
                var _cm = _context.MeasuringUnits.SingleOrDefault(x => x.MetricUnitDesc == "Centimeters");
                var _inch = _context.MeasuringUnits.SingleOrDefault(x => x.MetricUnitDesc == "Inche(s)");
                var _lit = _context.MeasuringUnits.SingleOrDefault(x => x.MetricUnitDesc == "Liters");
                var _qt = _context.MeasuringUnits.SingleOrDefault(x => x.MetricUnitDesc == "Quarts");


                var supportedConversions = new List<SupportedConversion>()
                   {
                    //Metric to Imperial
                new SupportedConversion() {  SourceUnitId = _km.MeasuringUnitId, TargetUnitId = _mile.MeasuringUnitId, SourceUnitValue = 1, ConversionType = (byte) ConversionType.Metric_To_Imperical, Multiplier = 0.62m},

                new SupportedConversion() {  SourceUnitId = _km.MeasuringUnitId, TargetUnitId = _feet.MeasuringUnitId, SourceUnitValue = 1, ConversionType = (byte) ConversionType.Metric_To_Imperical, Multiplier = 3280.8m},

                new SupportedConversion() {SourceUnitId = _meter.MeasuringUnitId, TargetUnitId = _feet.MeasuringUnitId, SourceUnitValue = 1, ConversionType = (byte) ConversionType.Metric_To_Imperical, Multiplier = 3.28m},

                new SupportedConversion() {SourceUnitId = _cm.MeasuringUnitId, TargetUnitId = _inch.MeasuringUnitId, SourceUnitValue = 1, ConversionType = (byte) ConversionType.Metric_To_Imperical, Multiplier = 0.39m},

                new SupportedConversion() {SourceUnitId = _lit.MeasuringUnitId, TargetUnitId = _qt.MeasuringUnitId, SourceUnitValue = 1, ConversionType = (byte) ConversionType.Metric_To_Imperical, Multiplier = 1.057m},



                 //Imperial to Metric
                new SupportedConversion() {SourceUnitId = _inch.MeasuringUnitId, TargetUnitId = _meter.MeasuringUnitId, SourceUnitValue = 1, ConversionType = (byte) ConversionType.Imperical_To_Metric, Multiplier = 0.0254m},

                new SupportedConversion() {SourceUnitId = _feet.MeasuringUnitId, TargetUnitId = _meter.MeasuringUnitId, SourceUnitValue = 1, ConversionType = (byte) ConversionType.Imperical_To_Metric, Multiplier = 0.30m},

                new SupportedConversion() {SourceUnitId = _inch.MeasuringUnitId, TargetUnitId = _cm.MeasuringUnitId, SourceUnitValue = 1, ConversionType = (byte) ConversionType.Imperical_To_Metric, Multiplier = 2.54m},

                };
                _context.AddRange(supportedConversions);

                _context.SaveChanges();

            }
        }
    }
}
