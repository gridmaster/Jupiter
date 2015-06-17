using System.Linq;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using Newtonsoft.Json;

namespace RESTPersonService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SymbolService : ISymbolService
    {
        [WebGet(UriTemplate = "/People", ResponseFormat = WebMessageFormat.Json)]
        public string GetPeople()
        {
            string json = JsonConvert.SerializeObject(people);

            return json;
        }

        [WebGet(UriTemplate = "/Person?id={id}", ResponseFormat = WebMessageFormat.Json)]
        public string GetPerson(int id)
        {
            Person peep = people.SingleOrDefault(p => p.Id == id);
            string json = JsonConvert.SerializeObject(peep);

            return json;
        }

        [WebInvoke(UriTemplate = "Person", Method = "POST")]
        public Person InsertPerson(Person person)
        {
            return people[2];
        }

        [WebInvoke(UriTemplate = "/Person?id={id}", Method = "PUT")]
        public Person UpdatePerson(int id, Person person)
        {
            return people[1];
        }

        [WebInvoke(UriTemplate = "/Person?id={id}", Method = "DELETE")]
        public void DeletePerson(int id)
        {
        }

        private Person[] people = new Person[]
            {
                new Person
                    {
                        Id = 1,
                        FirstName = "Biff",
                        LastName = "McGillicutty"
                    },
                new Person
                    {
                        Id = 2,
                        FirstName = "Muffy",
                        LastName = "McSplit"
                    },
                new Person
                    {
                        Id = 3,
                        FirstName = "Stan",
                        LastName = "Standard"
                    }
            };

    }
}