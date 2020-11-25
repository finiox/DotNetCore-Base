// <copyright file="ExampleServiceTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Baseproject.Common.Tests.Tests
{
    using System.Threading.Tasks;
    using BaseProject.Common.Areas.Example.Services;
    using NUnit.Framework;

    [TestFixture]
    public class ExampleServiceTests
    {
        private ExampleService _exampleService;

        [SetUp]
        public void Setup()
        {
            DatabaseHandler.Seed();

            _exampleService = new ExampleService(new ExampleRepository(DatabaseHandler.Context));
        }

        [Test]
        public async Task ExampleTest_GetListPage1_IsNotNull()
        {
            var result = await _exampleService.GetList(1);

            Assert.IsNotNull(result, "ExampleService.GetList(1) returned null.");
        }

        [Test]
        public async Task ExampleTest_GetListPage1_IsNotEmpty()
        {
            var result = await _exampleService.GetList(1);

            Assert.IsNotEmpty(result, "ExampleService.GetList(1) returned an empty list.");
        }

        [Test]
        public async Task ExampleTest_GetListPage2_IsNotNull()
        {
            var result = await _exampleService.GetList(2);

            Assert.IsNotNull(result, "ExampleService.GetList(2) returned null.");
        }

        [Test]
        public async Task ExampleTest_GetListPage2_IsEmpty()
        {
            var result = await _exampleService.GetList(2);

            Assert.IsEmpty(result, "ExampleService.GetList(2) returned a filled list while it should be empty.");
        }
    }
}
