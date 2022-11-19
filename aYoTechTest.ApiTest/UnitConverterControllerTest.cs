﻿using aYoTechTest.BR.Classes;
using aYoTechTest.BR.ViewModels;

namespace aYoTechTest.ApiTest
{
    public class UnitConverterControllerTest : aYoTechTestBase
    {

        [Fact]
        public async Task A_WhenCalled_Convert_Centimeters_to_Inches_Test()
        {
            //Act           

            ConvertUnitRequest _convertData = new ConvertUnitRequest()
            {
                SupportedConversionId = 5,
                UnitValue = 20
            };

            var _convertResult = await _conversionController.ConvertUnit(_convertData);
            Assert.NotNull(_convertResult);

            var _actionResult = Assert.IsType<ServiceActionResult<ConvertUnitResponse>>(_convertResult);
            Assert.True(_actionResult.Status.Equals(true));

            Assert.NotNull(_actionResult.Data);
            var _convertedData = Assert.IsType<ConvertUnitResponse>(_actionResult.Data);

            Assert.True(_convertedData.ConvertedValue.Equals(7.80m));

        }

    }
}
