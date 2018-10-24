using System.Collections.Generic;
using MvvmCross.Base;
using MvvmCross.Plugin.FieldBinding;
using MvvmCross.Plugin.Validation.ForFieldBinding.Validators;
using MvvmCross.Tests;
using Xunit;

namespace MvvmCross.Plugin.Validation.Tests
{
    public class ValidatorTests : IClassFixture<MvxTestFixture>
    {
        private readonly MvxTestFixture _fixture;
        private readonly Validator _validator;

        public ValidatorTests(MvxTestFixture fixture)
        {
            _fixture = fixture;
            _validator = new Validator();
        }

        #region TestClasses

        public class TestViewModel
        {
            [Required]
            public string NotNull { get; set; }

            [Required]
            public int NotZero { get; set; }

            [Required(Groups = new[] { "Test" })]
            public string TestGroup { get; set; }

            [NCFieldRequired]
            public INC<string> TestNCRequired = new NC<string>();

            [StringLength(5)]
            public string TestStringLength { get; set; }


            [NCFieldStringLength(5)]
            public INC<string> TestNCFieldStringLength = new NC<string>();

            [ShouldBeLong]
            public string TestShouldBeLong { get; set; }

            [NCFieldShouldBeLong]
            public INC<string> TestNCFieldShouldBeLong = new NC<string>();

            [Range(2, 5)]
            public int TestRange { get; set; }

            [NCFieldRange(2, 5)]
            public INC<int> TestNCFieldRange = new NC<int>();

            [Regex(@"^1\d{10}$", "{0} is wrong")]
            public string TestRegex { get; set; }

            [NCFieldRegex(@"^1\d{10}$", "{0} is wrong")]
            public INC<string> TestNCFieldRegex = new NC<string>();

            [CollectionCount(2, 4)]
            public List<string> TestCollectionCount { get; set; }

            [NCFieldCollectionCount(2, 4)]
            public INCList<string> TestNCFieldCollectionCount = new NCList<string>();
        } 

        #endregion
        
        [Fact]
        public void Validator_scans_the_class_for_attributes()
        {
            Assert.NotNull(_validator.Initialize(new TestViewModel()));
        }

        [Fact]
        public void Validator_creates_a_validation_info_foreach_attributed_property()
        {
            Assert.NotEqual(0, _validator.Initialize(new TestViewModel()).Count);
        }

        [Fact]
        public void Validation_validates_using_the_validation_info()
        {
            Assert.NotNull(_validator.Validate(new TestViewModel()));
            Assert.Equal(2, _validator.Validate(new TestViewModel()).Count);
        }

        [Fact]
        public void Validation_should_fail_with_an_empty_object()
        {
            Assert.False(_validator.Validate(new TestViewModel()).IsValid);
        }

        [Fact]
        public void Validation_should_succeed_with_a_valid_object()
        {
            Assert.True(_validator.Validate(new TestViewModel
            {
                NotNull = "blah",
                NotZero = 1
            }).IsValid);
        }

        [Fact]
        public void Validation_should_only_validate_within_a_specified_group()
        {
            Assert.True(_validator.Validate(new TestViewModel
            {
                NotNull = "blah",
                NotZero = 1
            }).IsValid);
            Assert.False(_validator.Validate(new TestViewModel
            {
                NotNull = "blah",
                NotZero = 1
            }, "Test").IsValid);
            Assert.True(_validator.Validate(new TestViewModel
            {
                NotNull = "blah",
                NotZero = 1,
                TestGroup = "foo"
            }, "Test").IsValid);
        }

        [Fact]
        public void Validation_should_all_fail_or_true()
        {
            var model0 = new TestViewModel
            {
                NotNull = "",
                NotZero = 0,
                TestGroup = "",
                TestStringLength = "123456",
                TestShouldBeLong = "t",
                TestRange = 1,
                TestRegex = "1",
                TestCollectionCount = new List<string> {"1"},
                TestNCRequired = {Value = ""},
                TestNCFieldStringLength = {Value = "123456"},
                TestNCFieldShouldBeLong = {Value = "t"},
                TestNCFieldRange = {Value = 1},
                TestNCFieldRegex = {Value = "1"},
                TestNCFieldCollectionCount = {Value = new List<string> {"1"}},
            };

            var val0 = _validator.Validate(model0, "Test");
            Assert.False(val0.IsValid);

            var val2 = _validator.Validate(new TestViewModel
            {
                NotNull = null,
                NotZero = 0,
                TestGroup = null,
                TestStringLength = null,
                TestShouldBeLong = null,
                TestRange = 1,
                TestRegex = null,
            }, "Test");
            Assert.True(val2.Count == 6);

            var model1 = new TestViewModel
            {
                NotNull = "dsf",
                NotZero = 1,
                TestGroup = "d",
                TestStringLength = "12345",
                TestShouldBeLong = "12",
                TestRange = 3,
                TestRegex = "18700000000",
                TestNCRequired = {Value = "df"},
                TestNCFieldStringLength = {Value = "12345"},
                TestNCFieldShouldBeLong = {Value = "12"},
                TestNCFieldRange = {Value = 3},
                TestNCFieldRegex = {Value = "18700000000"},
                //TestCollectionCount = new List<string> { "1", "2" },
            };
            model0.TestNCFieldCollectionCount.Value = new List<string> { "1", "2" };
            var val1 = _validator.Validate(model1, "Test");
            Assert.True(val1.IsValid);
        }
    }
}
