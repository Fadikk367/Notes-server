using NUnit.Framework;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System;
using notes.Models.DTO;
using notes.IntegrationTests.Models;
using Newtonsoft.Json;

namespace notes.IntegrationTests
{
    class TestSubjects
    {
        private readonly AppFactory<Startup> _factory = new();
        private HttpClient _client;
        private readonly SubjectDTO subjectDTO = new()
        {
            Name = "test Subject name",
            Description = "test subject description"
        };

        [SetUp]
        public void Initialize()
        {
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task ShouldGetSingleSubject()
        {
            // GIVEN
            SubjectDTO subjectDTO = new()
            {
                Description = "testDescription",
                Name = "Test Name"
            };

            using var createRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("/Subject", UriKind.Relative),

                Content = new StringContent(
                    JsonConvert.SerializeObject(subjectDTO),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json)
            };

            var createResponse = await _client.SendAsync(createRequest);
            var contentString = await createResponse.Content.ReadAsStringAsync();
            var createdSubject = JsonConvert.DeserializeObject<SubjectResponse>(contentString);

            // WHEN
            using var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("/Subject/" + createdSubject.Id, UriKind.Relative)
            };

            var response = await _client.SendAsync(request);

            // THEN
            Assert.IsTrue(response.IsSuccessStatusCode);

            var contentStr = await response.Content.ReadAsStringAsync();
            var fetchedSubject = JsonConvert.DeserializeObject<SubjectResponse>(contentStr);

            Assert.AreEqual(createdSubject.Id, fetchedSubject.Id);
            Assert.AreEqual(createdSubject.Name, fetchedSubject.Name);
            Assert.AreEqual(createdSubject.Description, fetchedSubject.Description);
        }

        [Test]
        public async Task ShouldCreateSubject()
        {
            //GIVEN
            // WHEN
            using var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("/Subject", UriKind.Relative),
                
                Content = new StringContent(
                    JsonConvert.SerializeObject(subjectDTO),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json)
            };

            var response = await _client.SendAsync(request);

            // THEN
            Assert.IsTrue(response.IsSuccessStatusCode);

            var contentString = await response.Content.ReadAsStringAsync();
            var createdSubject = JsonConvert.DeserializeObject<SubjectResponse>(contentString);

            Assert.AreEqual(createdSubject.Name, subjectDTO.Name);
            Assert.AreEqual(createdSubject.Description, subjectDTO.Description);
            Assert.NotNull(createdSubject.Id);
        }

        [Test]
        public async Task ShouldReturnBadRequestForMissingDescriptionProperty()
        {
            // GIVEN
            SubjectDTO subjectDTO = new()
                {
                    Name = "text name"
                };

            // WHEN
            using var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("/Subject", UriKind.Relative),

                Content = new StringContent(
                    JsonConvert.SerializeObject(subjectDTO),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json)
            };

            var response = await _client.SendAsync(request);

            // THEN
            Assert.AreEqual(400, (int) response.StatusCode);
        }

        [Test]
        public async Task ShouldReturnBadRequestForMissingNameProperty()
        {
            // GIVEN
            SubjectDTO subjectDTO = new()
            {
                Description = "test subject description"
            };

            // WHEN
            using var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("/Subject", UriKind.Relative),

                Content = new StringContent(
                    JsonConvert.SerializeObject(subjectDTO),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json)
            };

            var response = await _client.SendAsync(request);


            // THEN
            Assert.AreEqual(400, (int)response.StatusCode);
        }

        [Test]
        public async Task ShouldReturnNotesFromGivenSubject()
        {
            //GIVEN
            SubjectDTO subjectDTO = new()
            {
                Description = "testDescription",
                Name = "Test Name"
            };

            using var createRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("/Subject", UriKind.Relative),

                Content = new StringContent(
                    JsonConvert.SerializeObject(subjectDTO),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json)
            };

            var createResponse = await _client.SendAsync(createRequest);

            var contentString = await createResponse.Content.ReadAsStringAsync();
            var createdSubject = JsonConvert.DeserializeObject<SubjectResponse>(contentString);

            //WHEN
            using var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("/Subject/" + createdSubject.Id, UriKind.Relative)
            };

            var response = await _client.SendAsync(request);

            //THEN
            Assert.IsTrue(response.IsSuccessStatusCode);

            var contentStr = await response.Content.ReadAsStringAsync();
            var fetchedSubject = JsonConvert.DeserializeObject<SubjectResponse>(contentStr);

            Assert.AreEqual(createdSubject.Id, fetchedSubject.Id);
            Assert.AreEqual(createdSubject.Name, fetchedSubject.Name);
            Assert.AreEqual(createdSubject.Description, fetchedSubject.Description);
        }

        [Test]
        public async Task ShouldDeleteSubject()
        {
            //GIVEN
            SubjectDTO subjectDTO = new()
            {
                Description = "testDescription",
                Name = "Test Name"
            };

            using var createRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("/Subject", UriKind.Relative),

                Content = new StringContent(
                    JsonConvert.SerializeObject(subjectDTO),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json)
            };

            var createResponse = await _client.SendAsync(createRequest);

            var contentString = await createResponse.Content.ReadAsStringAsync();
            var createdSubject = JsonConvert.DeserializeObject<SubjectResponse>(contentString);

            // WHEN
            using var deletRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("/Subject/" + createdSubject.Id, UriKind.Relative)
            };

            var deleteResponse = await _client.SendAsync(deletRequest);

            // THEN
            Assert.IsTrue(deleteResponse.IsSuccessStatusCode);
        }
    }
}
