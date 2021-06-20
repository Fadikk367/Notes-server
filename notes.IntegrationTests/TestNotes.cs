using NUnit.Framework;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System;
using notes.Models.DTO;
using notes.Models;
using notes.IntegrationTests.Models;
using Newtonsoft.Json;

namespace notes.IntegrationTests
{
    class TestNotes
    {
        private readonly AppFactory<Startup> _factory = new();
        private HttpClient _client;
        private SubjectResponse _subject;

        [SetUp]
        public async Task InitilizeProperties()
        {
            _client = _factory.CreateClient();
            SubjectDTO subjectDTO = new()
            {
                Description = "testDescription",
                Name = "Test Name"
            };

            using var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("/Subject", UriKind.Relative),

                Content = new StringContent(
                    JsonConvert.SerializeObject(subjectDTO),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json)
            };

            var subjectResponse = await _client.SendAsync(request);

            var subjectString = await subjectResponse.Content.ReadAsStringAsync();
            _subject = JsonConvert.DeserializeObject<SubjectResponse>(subjectString);
        }

        [Test]
        public async Task ShouldCreateNote()
        {
            // GIVEN
            NoteDTO noteDTO = new()
            {
                Title = "test note ttile",
                Content = "test content",
                SubjectId = _subject.Id
            };


            // WHEN
            using var noteRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("/Note", UriKind.Relative),

                Content = new StringContent(
                    JsonConvert.SerializeObject(noteDTO),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json)
            };

            var noteResponse = await _client.SendAsync(noteRequest);

            // THEN
            Assert.IsTrue(noteResponse.IsSuccessStatusCode);

            var noteString = await noteResponse.Content.ReadAsStringAsync();
            var createdNote = JsonConvert.DeserializeObject<NoteResponse>(noteString);

            Assert.AreEqual(noteDTO.Title, createdNote.Title);
            Assert.AreEqual(noteDTO.Content, createdNote.Content);
            Assert.AreEqual(noteDTO.SubjectId, createdNote.SubjectId);
            Assert.NotNull(createdNote.Id);
        }

        [Test]
        public async Task ShouldReturnBadRequestForMissinfTitleAttribute()
        {
            // GIVEN
            NoteDTO noteDTO = new()
            {
                Content = "test content",
                SubjectId = _subject.Id
            };


            // WHEN
            using var noteRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("/Note", UriKind.Relative),

                Content = new StringContent(
                    JsonConvert.SerializeObject(noteDTO),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json)
            };

            var noteResponse = await _client.SendAsync(noteRequest);

            // THEN
            Assert.AreEqual(400, (int)noteResponse.StatusCode);
        }

        [Test]
        public async Task ShouldReturnBadRequestForMissinfContentAttribute()
        {
            // GIVEN
            NoteDTO noteDTO = new()
            {
                Title = "test title",
                SubjectId = _subject.Id
            };

            // WHEN
            using var noteRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("/Note", UriKind.Relative),

                Content = new StringContent(
                    JsonConvert.SerializeObject(noteDTO),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json)
            };

            var noteResponse = await _client.SendAsync(noteRequest);

            // THEN
            Assert.AreEqual(400, (int) noteResponse.StatusCode);
        }

        [Test]
        public async Task ShouldDeleteNote()
        {
            // GIVEN
            NoteDTO noteDTO = new()
            {
                Title = "test note ttile",
                Content = "test content",
                SubjectId = _subject.Id
            };

            using var createRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("/Note", UriKind.Relative),

                Content = new StringContent(
                    JsonConvert.SerializeObject(noteDTO),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json)
            };

            var createResponse = await _client.SendAsync(createRequest);
            var createString = await createResponse.Content.ReadAsStringAsync();
            var createdNote = JsonConvert.DeserializeObject<NoteResponse>(createString);

            // WHEN
            using var deleteRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("/Note/" + createdNote.Id, UriKind.Relative),
            };

            var deleteResponse = await _client.SendAsync(deleteRequest);

            // THEN
            Assert.IsTrue(deleteResponse.IsSuccessStatusCode);
        }

        [Test]
        public async Task ShouldGetNoteById()
        {
            // GIVEN
            NoteDTO noteDTO = new()
            {
                Title = "test note ttile",
                Content = "test content",
                SubjectId = _subject.Id
            };

            using var noteRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("/Note", UriKind.Relative),

                Content = new StringContent(
                    JsonConvert.SerializeObject(noteDTO),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json)
            };

            var noteResponse = await _client.SendAsync(noteRequest);
            var noteString = await noteResponse.Content.ReadAsStringAsync();
            var createdNote = JsonConvert.DeserializeObject<NoteResponse>(noteString);

            // WHEN
            using var getRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("/Note/" + createdNote.Id, UriKind.Relative)
            };

            var getResponse = await _client.SendAsync(getRequest);

            // THEN
            Assert.IsTrue(getResponse.IsSuccessStatusCode);

            var getString = await getResponse.Content.ReadAsStringAsync();
            var fetchedNote = JsonConvert.DeserializeObject<NoteResponse>(getString);


            Assert.AreEqual(fetchedNote.Title, createdNote.Title);
            Assert.AreEqual(fetchedNote.Content, createdNote.Content);
            Assert.AreEqual(fetchedNote.SubjectId, createdNote.SubjectId);
            Assert.AreEqual(fetchedNote.Id, createdNote.Id);
        }
    }
}
