using AutoMoq;
using MVVMCross.Plugins.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MVVMCross.Plugins.Validation.UnitTests
{
    [TestClass]
    public class TestBase<T> where T : class
    {
        public T CUT { get; private set; }

        [TestInitialize]
        public void TestInitialize()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            CreateContainer();
            CreateClass();
        }

        protected virtual void CreateClass()
        {
            CUT = Containter.Create<T>();
        }

        protected virtual void CreateContainer()
        {
            Containter = new AutoMoqer();
        }

        protected AutoMoqer Containter { get; private set; }

        protected Mock<TMock> GetMock<TMock>() where TMock : class
        {
            return Containter.GetMock<TMock>();
        }

        protected TInstance RegisterInstance<TInstance>(TInstance instance) where TInstance : class
        {
            Containter.SetInstance(instance);
            return instance;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Cleanup();
        }

        protected virtual void Cleanup()
        {
        }
    }

    [TestClass]
    public class ValidatorTests : TestBase<Validator>
    {
        [TestMethod]
        public void Validator_scans_the_class_for_attributes()
        {
            Assert.IsNotNull(CUT.Initialize(new TestViewModel()));
        }

        [TestMethod]
        public void Validator_creates_a_validation_info_foreach_attributed_property()
        {
            Assert.AreNotEqual(0, CUT.Initialize(new TestViewModel()).Count);
        }

        [TestMethod]
        public void Validation_validates_using_the_validation_info()
        {
            Assert.IsNotNull(CUT.Validate(new TestViewModel()));
            Assert.AreEqual(2, CUT.Validate(new TestViewModel()).Count);
        }

        [TestMethod]
        public void Validation_should_fail_with_an_empty_object()
        {
            Assert.IsFalse(CUT.Validate(new TestViewModel()).IsValid);
        }

        [TestMethod]
        public void Validation_should_succeed_with_a_valid_object()
        {
            Assert.IsTrue(CUT.Validate(new TestViewModel
            {
                NotNull = "blah",
                NotZero = 1
            }).IsValid);
        }

        [TestMethod]
        public void Validation_should_only_validate_within_a_specified_group()
        {
            Assert.IsTrue(CUT.Validate(new TestViewModel
            {
                NotNull = "blah",
                NotZero = 1
            }).IsValid);
            Assert.IsFalse(CUT.Validate(new TestViewModel
            {
                NotNull = "blah",
                NotZero = 1
            }, "Test").IsValid);
            Assert.IsTrue(CUT.Validate(new TestViewModel
            {
                NotNull = "blah",
                NotZero = 1,
                TestGroup = "foo"
            }, "Test").IsValid);
        }
    }

    [TestClass]
    public class RequiredAttributeTests
    {
        [TestMethod]
        public void Creates_correct_lambda_for_validation_type_negative_validation()
        {
            var attr = new RequiredAttribute();
            var validation = attr.CreateValidation(typeof(int));
            Assert.IsNotNull(validation.Validate("Foo", 0, new TestViewModel()));

            attr = new RequiredAttribute();
            validation = attr.CreateValidation(typeof(int?));
            Assert.IsNotNull(validation.Validate("Foo", null, new TestViewModel()));
        }

        [TestMethod]
        public void Creates_correct_lambda_for_validation_type_positive_validation()
        {
            var attr = new RequiredAttribute();
            var validation = attr.CreateValidation(typeof(decimal));
            Assert.IsNull(validation.Validate("Foo", 1m, new TestViewModel()));
        }
    }

    public class TestViewModel
    {
        [Required]
        public string NotNull { get; set; }

        [Required]
        public int NotZero { get; set; }

        [Required(Groups = new[] { "Test" })]
        public string TestGroup { get; set; }
    }
}