using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForgetTheMilk.Controllers;
using NUnit.Framework;

namespace ConsoleVerification
{
    [TestFixture]
    public class TaskTests
    {
        [Test]
        public void TestDescriptionAndNoDueDate()
        {
            var input = "Pickup the groceries";
            Console.WriteLine("Scenario: " + input);

            var task = new Task(input, default(DateTime));

            var descriptionShouldBe = input;
            DateTime? dueDateShouldBe = null;
            var success = descriptionShouldBe == task.Description &&
                dueDateShouldBe == task.DueDate;
            var failureMessage = "Description: " + task.Description + " should be " + descriptionShouldBe
                + Environment.NewLine
                + "Due Date: " + task.DueDate + " should be " + dueDateShouldBe;
            Program.PrintOutcome(success, failureMessage);
        }
    }
}
