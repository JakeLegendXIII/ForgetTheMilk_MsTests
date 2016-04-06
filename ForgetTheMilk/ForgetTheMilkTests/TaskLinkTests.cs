using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForgetTheMilk.Controllers;

namespace ForgetTheMilkTests
{
    [TestClass]
    public class TaskLinkTests
    {
        class IgnoreLinkValidator : ILinkValidator
        {
            public void Validate(string link)
            {
            }
        }

        [TestMethod]
        public void CreateTask_DescriptionWithALink_SetLink()
        {
            //arrange
            var input = "test http://www.google.com";

            //act
            var task = new Task(input, default(DateTime), new IgnoreLinkValidator());

            //assert
            Assert.AreEqual("http://www.google.com", task.Link);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void Validate_InvalidURL_ThrowsException()
        {
            //arrange
            var input = "http://swag.www.yolo.swag.com";
            var validate = new LinkValidator();

            //act
            var task = new Task(input, default(DateTime), new LinkValidator());

            //assert, handled by ExpectedException
        }
    }
}
