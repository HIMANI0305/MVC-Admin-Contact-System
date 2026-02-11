namespace Repositories;

public interface IContactInterface
{
    Task<List<t_Contact>> GetAll();
    Task<List<t_Contact>> GetAllByUser(string userid);
    Task<t_Contact> GetOne(string contactid);
    Task<int> Add(t_Contact contactData);
    Task<int> Update(t_Contact contactData);
    Task<int> Delete(string contactid);
}
