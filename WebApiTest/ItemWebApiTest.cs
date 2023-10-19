using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System.Net.Http;
using WebApi;
using WebApi.Models;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json.Linq;
using WebApiTest.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace WebApiTest
{
    [TestFixture]
    public class ItemWebApiTest
    {
        private WebTestHelper<Startup> Helper { get; set; }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            JsonConvert.DefaultSettings = () => new() { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            Helper = new WebTestHelper<Startup>("http://localhost/api", new WebApplicationFactory<Startup>(), delegate { }, delegate { });
        }

        [SetUp]
        public void Setup()
        {
            Helper.ResetUrl();
            Helper.UrlBuilder.AddPathSegment("item");
        }

        [Test]
        public async Task Get()
        {
            Helper.UrlBuilder.AddPathSegment("123");

            try
            {
                await Helper.SendRequest<ItemWebModel>(HttpMethod.Get, null);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                var line1 = ex.Message.Split(Environment.NewLine).First();
                Assert.AreEqual(line1, "System.InvalidOperationException: Sequence contains no matching element");
            }
        }

        [Test]
        public async Task Create()
        {
            var request = new ItemCreateWebRequestModel("designs_test2", new[] { new ItemAttributeWebModel("attributes_webtest", 12.5) });
            var response = await Helper.SendRequest<ItemWebModel>(HttpMethod.Post, request);
            Assert.IsNotEmpty(response.Id);
            Assert.AreEqual(response.DesignCode, "designs_test2");

            var attribute = response.Attributes.Single();
            Assert.AreEqual(attribute.Code, "attributes_webtest");
            Assert.AreEqual(attribute.Value.Type, JTokenType.Float);
            Assert.AreEqual(attribute.Value.Value<double>(), 12.5);
        }
    }
}