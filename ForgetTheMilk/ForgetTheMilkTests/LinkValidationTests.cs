using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForgetTheMilk.Controllers;
using System.Net;

namespace ForgetTheMilkTests
{
    [TestClass]
    public class LinkValidationTests
    {
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void Validate_InvalidURL_ThrowsException()
        {
            //arrange
            var invalidLink = "http://swag.www.yolo.swag.com";
            var validate = new LinkValidator();

            //act
            validate.Validate(invalidLink);

            //assert, handled by ExpectedException
        }

        [TestMethod]
        public void Validate_ValidUrl_DoesNotThrowException()
        {
            //arrange
            var validLink = "http://www.google.com";
            var validate = new LinkValidator();
            Exception expectedException = null;
            //act
            try
            {
                validate.Validate(validLink);
            }
            catch (Exception ex)
            {
                expectedException = ex;
            }
            //assert
            Assert.IsNull(expectedException);
        }
    }
}
