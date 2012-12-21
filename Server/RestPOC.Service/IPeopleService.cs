namespace RestPOC.Service
{
    using RestPOC.Domain;

    public interface IPeopleService {

        // User Role
        OperationResult AddNewUser(Person person);
        // User Role
        OperationResult UpdateUser(Person person);
        // User Role
        OperationResult<Person> GetById(int personId);

        // SuperUser Role
        OperationResult<PaginatedList<Person>> GetAll(int pageIndex, int pageSize);
    }
}
