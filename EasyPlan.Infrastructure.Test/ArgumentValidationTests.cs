using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyPlan.Infrastructure;

namespace EasyPlan.Infrastructure.Test
{
    [TestClass]
    public class ArgumentValidationTests
    {
        [TestMethod]
        public void ArgumentValidation_ThrowIfNullOrWhiteSpace()
        {
            //act
            try
            {
                ArgumentValidation.ThrowIfNullOrWhiteSpace("   ");
            }
            //assert
            catch (ArgumentValidationException exception)
            {
                Assert.IsNotNull(exception, message: "exception is invalid");
                Assert.AreEqual(exception.StatusCode, 400, message: "status code is incorect");
                Assert.IsNotNull(exception.Message, message: "message is null");
            }
        }

        [TestMethod]
        public void ArgumentValidation_ThrowIfNull()
        {
            //act
            try
            {
                ArgumentValidation.ThrowIfNull(null);
            }
            //assert
            catch (ArgumentValidationException exception)
            {
                Assert.IsNotNull(exception, message: "exception is invalid");
                Assert.AreEqual(exception.StatusCode, 400, message: "status code is incorect");
                Assert.IsNotNull(exception.Message, message: "message is null");
            }
        }

        [TestMethod]
        public void ArgumentValidation_ThrowIfLongerThan()
        {
            //act
            try
            {
                ArgumentValidation.ThrowIfLongerThan("absdasd as das as sa as ass as sasd asd", 5);
            }
            //assert
            catch (ArgumentValidationException exception)
            {
                Assert.IsNotNull(exception, message: "exception is invalid");
                Assert.AreEqual(exception.StatusCode, 400, message: "status code is incorect");
                Assert.IsNotNull(exception.Message, message: "message is null");
            }
        }

        [TestMethod]
        public void ArgumentValidation_ThrowIfGreaterThan()
        {
            //act
            try
            {
                ArgumentValidation.ThrowIfGreaterThan(255, 200);
            }
            //assert
            catch (ArgumentValidationException exception)
            {
                Assert.IsNotNull(exception, message: "exception is invalid");
                Assert.AreEqual(exception.StatusCode, 400, message: "status code is incorect");
                Assert.IsNotNull(exception.Message, message: "message is null");
            }
        }

        [TestMethod]
        public void ArgumentValidation_ThrowIfLessThan()
        {
            //act
            try
            {
                ArgumentValidation.ThrowIfLessThan(15, 200);
            }
            //assert
            catch (ArgumentValidationException exception)
            {
                Assert.IsNotNull(exception, message: "exception is invalid");
                Assert.AreEqual(exception.StatusCode, 400, message: "status code is incorect");
                Assert.IsNotNull(exception.Message, message: "message is null");
            }
        }

        [TestMethod]
        public void ArgumentValidation_ThrowIfOutOfRange()
        {
            //act
            try
            {
                ArgumentValidation.ThrowIfOutOfRange(15, 200, 300);
            }
            //assert
            catch (ArgumentValidationException exception)
            {
                Assert.IsNotNull(exception, message: "exception is invalid");
                Assert.AreEqual(exception.StatusCode, 400, message: "status code is incorect");
                Assert.IsNotNull(exception.Message, message: "message is null");
            }
        }
    }
}
