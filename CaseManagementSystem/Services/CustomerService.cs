using CaseManagementSystem.Contexts;
using CaseManagementSystem.Models.Entities;
using CaseManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CaseManagementSystem.Services;

internal class CustomerService
{
    private readonly static DataContext _context = new();
    public static async Task SaveCustomerAsync(Situations situations, Customers customer)
    {
        var _situationEntity = new SituationEntity
        {
            Description = situations.Description,
            Timing = DateTime.Now,
            Condition = situations.Condition,
        };

        var _customerEntity = await _context.Customers.FirstOrDefaultAsync(x => x.FirstName == customer.FirstName && x.LastName == customer.LastName && x.Email == customer.Email && x.PhoneNumber == customer.PhoneNumber);
        if (_customerEntity != null)
            _situationEntity.CustomerId = _customerEntity.Id;
        else
            _situationEntity.Customer = new CustomerEntity
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber

            };

        _context.Add(_situationEntity);
        await _context.SaveChangesAsync();
    }
    public static async Task<Situations> GetCustomerAsync(string email)
    {
        var _situations = await _context.Situations.Include(x => x.Customer).FirstOrDefaultAsync(x => x.Customer.Email == email);
        if (_situations != null)
            return new Situations
            {
                Id = _situations.Id,
                Description = _situations.Description,
                Timing = DateTime.Now,
                Condition = _situations.Condition,
                FirstName = _situations.Customer.FirstName,
                LastName = _situations.Customer.LastName,
                Email = _situations.Customer.Email,
                PhoneNumber = _situations.Customer.PhoneNumber
            };
        else
            return null!;
    }

    public static async Task UpdateCustomerAsync(Situations situations, Customers customers)
    {
        var _situationEntity = await _context.Situations.Include(x => x.Customer).FirstOrDefaultAsync(x => x.Customer.Email == situations.Email);
        if (_situationEntity != null)
        {
            if (!string.IsNullOrEmpty(situations.Condition))
                _situationEntity.Condition = situations.Condition;
            if (!string.IsNullOrEmpty(situations.Description))
                _situationEntity.Description = situations.Description;
            if (situations.Timing != default(DateTime))
                _situationEntity.Timing = situations.Timing;
            if (!string.IsNullOrEmpty(situations.FirstName) || !string.IsNullOrEmpty(situations.LastName) || !string.IsNullOrEmpty(situations.Email) || !string.IsNullOrEmpty(situations.PhoneNumber))
            {
                var _customerEntity = await _context.Customers.FirstOrDefaultAsync(x => x.FirstName == customers.FirstName && x.LastName == customers.LastName && x.Email == customers.Email && x.PhoneNumber == customers.PhoneNumber);
                if (_customerEntity != null)
                    _situationEntity.CustomerId = _customerEntity.Id;
                else
                    _situationEntity.Customer = new CustomerEntity
                    {
                        FirstName = customers.FirstName,
                        LastName = customers.LastName,
                        Email = customers.Email,
                        PhoneNumber = customers.PhoneNumber

                    };
            }
            _context.Update(_situationEntity);
            await _context.SaveChangesAsync();
        }
    }

    public static async Task<bool> DeleteCustomerAsync(string email)
    {
        var _situations = await _context.Situations.Include(x => x.Customer).FirstOrDefaultAsync(x => x.Customer.Email == email);
        if (_situations != null)
        {
            _context.Remove(_situations);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public static async Task<IEnumerable<Comments>> CheckUpMySituationAsync(string email)
    {
        var _situationEntity = await _context.Situations.Include(c => c.Comments).Include(c => c.Customer).FirstOrDefaultAsync(c => c.Customer.Email == email);

        if (_situationEntity != null)
        {
            return _situationEntity.Comments.Select(c => new Comments
            {
                Id = c.Id,
                Text = c.Text,
                TimingAt = c.TimingAt,
                SituationId = c.SituationId

            });
        }

        return Enumerable.Empty<Comments>();
    }
}
