using aYoTechTest.BR.Enums;
using aYoTechTest.BR.Services.Interfaces;
using aYoTechTest.BR.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;

namespace aYoTechTest.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UnitConverterController : Controller
    {
        private readonly IUnitConvertionService _ucService;

        public UnitConverterController(IUnitConvertionService ucService)
        {
            _ucService = ucService;
        }


        [HttpGet]
        [AllowAnonymous]
        public JsonResult Get()
        {
            return new JsonResult(new[] { "Welcome to Unit Conversion Api." });
        }

        [HttpGet]
        [Route("supported")]
        public async Task<IActionResult> GetSupported()
        {
            var _data = await _ucService.GetSupportedConversionList();
            return Ok(_data);
        }


        [HttpGet]
        [Route("unitlist")]
        public async Task<IActionResult> GetUnitList()
        {
            var _data = await _ucService.GetMeasuringUnitList();
            return Ok(_data);
        }


        [HttpGet]
        [Route("metricunits")]
        public async Task<IActionResult> GetMetricUnitList()
        {
            var param = new NameValueCollection();
            param.Add("unitType", MeasuringUnitType.Metric_Unit.ToString());
            var _data = await _ucService.GetMeasuringUnitList(param);
            return Ok(_data);
        }

        [HttpGet]
        [Route("imperialunits")]
        public async Task<IActionResult> GetImperialUnitList()
        {
            var param = new NameValueCollection();
            param.Add("unitType", MeasuringUnitType.Imperial_Unit.ToString());
            var _data = await _ucService.GetMeasuringUnitList(param);
            return Ok(_data);
        }




        [HttpPost]
        public async Task<IActionResult> ConvertUnit(ConvertUnitRequest data)
        {
            var _result = await _ucService.ProcessConvertion(data);

            if (_result == null)
                return BadRequest(new { message = "Could not process request! \n Please try again later!" });

            return Ok(_result);
        }

    }
}
