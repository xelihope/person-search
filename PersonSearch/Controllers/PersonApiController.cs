using PersonSearch.Models;
using PersonSearch.Services;
using PersonSearch.Helpers;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PersonSearch.Controllers
{
    public class PersonApiController : ApiController
    {
        private readonly PersonCrud _personCrud;
        private readonly HobbyCrud _hobbyCrud;
        private readonly PersonValidator _personValidator;
        private readonly ImageUploader _imageUploader;

        // DI ideally uses interfaces for the minimum sake of super easy unit test mocking
        // Pretend these are interfaces *waves magic hands*
        public PersonApiController(PersonCrud personCrud, HobbyCrud hobbyCrud, 
            PersonValidator personValidator, ImageUploader imageUploader)
        {
            _personCrud = personCrud;
            _hobbyCrud = hobbyCrud;
            _personValidator = personValidator;
            _imageUploader = imageUploader;
        }

        [HttpPost]
        [Route("api/people")]
        public async Task<IHttpActionResult> Get([FromBody]PeopleFilter filter)
        {
            if (!ModelState.IsValid) {
                BadRequest();
            }

            var people = await _personCrud.Get(filter.PageSize, filter.Page, filter.NameFilter);
            var mappedPeople = people
                // Auto-Mapper could be used to handle mappings like this, but auto-mappings
                // can also get gruesome the larger the application gets, tempting you to put
                // more and more non-mapping logic in the mappers as view models require more
                // and more properties from different DB objects and calculated values from different services
                .Select(p => new PersonView {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Age = p.BirthDate.ToYearDifference(DateTime.Now),
                    PictureFile = p.PictureFile,
                    Address = p.Address == null ? null : new Address {
                        Street1 = p.Address.Street1,
                        Street2 = p.Address.Street2,
                        City = p.Address.City,
                        State = p.Address.State,
                        PrettyState = p.Address.State.ToDisplay(),
                        ZipCode = p.Address.ZipCode
                    },
                    Hobbies = p.Hobbies.Select(h => h.HobbyType.ToDisplay()).ToList()
                });

            return Ok(mappedPeople);
        }

        [HttpPost]
        [Route("api/people/count")]
        public async Task<IHttpActionResult> GetCount([FromBody]string nameFilter)
        {
            var count = await _personCrud.GetCount(nameFilter);

            return Ok(count);
        }

        [HttpGet]
        [Route("api/person/hobbies")]
        public async Task<IHttpActionResult> GetHobbies()
        {
            var hobbies = await _hobbyCrud.GetAll();
            var hobbyInfo = hobbies.Select(h => new Hobby {
                Id = h.HobbyId,
                HobbyType = h.HobbyType.ToDisplay()
            });

            return Ok(hobbyInfo);
        }

        [HttpPut]
        [Route("api/person")]
        public async Task<IHttpActionResult> Create(PersonEdit newPerson)
        {
            if (!ModelState.IsValid) {
                BadRequest();
            }

            var errors = _personValidator.GetErrors(newPerson);
            if (errors.Any()) {
                throw new ValidationException(string.Join(" ", errors));
            }

            var newPersonId = await _personCrud.Create(newPerson);

            return Ok(newPersonId);
        }

        [HttpPost]
        [Route("api/person/picture/{id}")]
        public async Task<IHttpActionResult> UpdatePicture(int id)
        {
            if (!Request.Content.IsMimeMultipartContent()) {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);
            var stream = await provider.Contents[0].ReadAsStreamAsync();

            string fileName = "";
            try {
                fileName = _imageUploader.Upload(stream);
            } catch (ArgumentException) {
                throw new ValidationException(Resources.Person.PictureError);
            }
            await _personCrud.UpdatePicture(id, fileName);

            return Ok();
        }
    }
}