namespace RestPOC.Service
{
    using System;
    using System.Collections.Generic;

    using RestPOC.Domain;

    public class PeopleService : IPeopleService
    {
        private readonly IEntityRepository<Person> repository;
        public PeopleService(IEntityRepository<Person> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            this.repository = repository;
        }

        public OperationResult AddNewUser(Person person)
        {
            try
            {
                repository.Add(person);
                repository.Save();
                return new OperationResult<Person>(true);
            }
            catch (Exception exception)
            {
                var result = new OperationResult<Person>(false) { Message = exception.Message };
                result.Exceptions.Add(exception);
                return result;
            }
        }

        public OperationResult UpdateUser(Person person)
        {
            try
            {
                repository.Update(person);
                repository.Save();
                return new OperationResult<Person>(true);
            }
            catch (Exception exception)
            {
                var result = new OperationResult<Person>(false) { Message = exception.Message };
                result.Exceptions.Add(exception);
                return result;
            }
        }

        public OperationResult<Person> GetById(int personId)
        {
            try
            {
                var person = repository.GetSingle(personId);
                return new OperationResult<Person>(true, person);
            }
            catch (Exception exception)
            {
                var result = new OperationResult<Person>(false) { Message = exception.Message };
                result.Exceptions.Add(exception);
                return result;
            }
        }

        public OperationResult<PaginatedList<Person>> GetAll(int pageIndex, int pageSize) {
            try {
                var people = repository.Paginate<Person>(pageIndex, pageSize);
                return new OperationResult<PaginatedList<Person>>(true, people);
            }
            catch (Exception exception)
            {
                var result = new OperationResult<PaginatedList<Person>>(false) { Message = exception.Message };
                result.Exceptions.Add(exception);
                return result;
            }
        }
    }
}