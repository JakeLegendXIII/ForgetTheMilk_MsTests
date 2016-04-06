using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForgetTheMilk.Controllers;
using System.Linq;

namespace ForgetTheMilkTests
{
    [TestClass]
    public class CreateTaskTests
    {       
        [TestMethod]
        public void DescriptionAndNoDueDate()
        {
            //arrange
            var input = "Pickup the groceries";

            //act
            var task = new Task(input, default(DateTime));    

            //assert
            Assert.AreEqual(input, task.Description);
            Assert.AreEqual(null, task.DueDate);
        }

        [TestMethod]
        public void MayDueDateDoesWrapYear_WhenTodaysMonthEqualsMonthAndDayIsLessThanToday()
        {
            //arrange
            var input = "Pickup the groceries may 5 - as of 2015-05-31";
            var today = new DateTime(2015, 5, 31);

            //act
            var task = new Task(input, today);

            //assert
            Assert.AreEqual(2016, task.DueDate.Value.Year);
        }

        [TestMethod]
        public void MayDueDateDoesWrapYear_WhenMonthIsLessThanToday()
        {
            //arrange
            var input = "Pickup the groceries apr 5 - as of 2015-05-31";
            var today = new DateTime(2015, 5, 31);

            //act
            var task = new Task(input, today);

            //assert
            Assert.AreEqual(2016, task.DueDate.Value.Year);
        }

        [TestMethod]
        public void MayDueDateDoesNotWrapYear()
        {
            //arrange
            var input = "Pickup the groceries may 5 - as of 2015-05-04";
            var today = new DateTime(2015, 5, 4);

            //act
            var task = new Task(input, today);

            //assert
            Assert.AreEqual(new DateTime(2015, 5, 5), task.DueDate);
        }

        [TestMethod]
        public void DueDate()
        {
            //arrange
            var input = new[]
            {
                new { inputString = "Groceries jan 5", expectedMonth = 1},
                new { inputString = "Groceries feb 5", expectedMonth = 2},
                new { inputString = "Groceries mar 5", expectedMonth = 3},
                new { inputString = "Groceries apr 5", expectedMonth = 4},
                new { inputString = "Groceries may 5", expectedMonth = 5},
                new { inputString = "Groceries jun 5", expectedMonth = 6},
                new { inputString = "Groceries jul 5", expectedMonth = 7},
                new { inputString = "Groceries aug 5", expectedMonth = 8},
                new { inputString = "Groceries sep 5", expectedMonth = 9},
                new { inputString = "Groceries oct 5", expectedMonth = 10},
                new { inputString = "Groceries nov 5", expectedMonth = 11},
                new { inputString = "Groceries dec 5", expectedMonth = 12}
            };
            //act
            input.ToList().ForEach(val =>
            {
                var task = new Task(val.inputString, default(DateTime));
                //assert
                Assert.IsNotNull(task.DueDate);
                Assert.AreEqual(val.expectedMonth, task.DueDate.Value.Month);
            });                                      
        }

        [TestMethod]
        public void TwoDigitDay_ParseBothDigits()
        {
            //arrange
            var input = "Groceries apr 10";

            //act
            var task = new Task(input, default(DateTime));

            //assert
            Assert.AreEqual(10, task.DueDate.Value.Day);
        }

        [TestMethod]
        public void AddFeb29TaskInMarchOfYearBeforeLeapYear_ParseDueDate()
        {
            //arrange
            var input = "Groceries feb 29";
            var today = new DateTime(2015, 3, 1);

            //act
            var task = new Task(input, today);

            //assert
            Assert.AreEqual(new DateTime(2016, 2, 29), task.DueDate);
        }

        [TestMethod]
        public void DayIsPastLastDayOfTheMonth_DoesNotParseDueDate()
        {
            //arrange
            var input = "Groceries apr 44";

            //act
            var task = new Task(input, default(DateTime));

            //assert
            Assert.AreEqual(null, task.DueDate);
        }
    }
}
