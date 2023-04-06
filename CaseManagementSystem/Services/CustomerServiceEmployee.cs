using CaseManagementSystem.Contexts;
using CaseManagementSystem.Models;
using CaseManagementSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace CaseManagementSystem.Services;

internal class CustomerServiceEmployee
{
    private readonly static DataContext _context = new();

    public static async Task<(IEnumerable<Situations>, IEnumerable<Customers>)> GetAllAsync()
    {
        var _situationss = new List<Situations>();
        var _customers = new List<Customers>();

        foreach (var _situations in await _context.Situations.Include(x => x.Customer).ToListAsync())
        {
            var newSituation = new Situations
            {
                Id = _situations.Id,
                Description = _situations.Description,
                CreatedTime = DateTime.Now,
                Condition = _situations.Condition,
                FirstName = _situations.Customer.FirstName,
                LastName = _situations.Customer.LastName,
                Email = _situations.Customer.Email,
                PhoneNumber = _situations.Customer.PhoneNumber
            };

            _situationss.Add(newSituation);

        }

        return (_situationss, _customers);
    }

    public static async Task<Situations> GetAsync(int id)
    {
        var _situations = await _context.Situations.Include(x => x.Customer).FirstOrDefaultAsync(x => x.Id == id);
        if (_situations != null)
            return new Situations
            {
                Id = _situations.Id,
                Description = _situations.Description,
                CreatedTime = DateTime.Now,
                Condition = _situations.Condition,
                FirstName = _situations.Customer.FirstName,
                LastName = _situations.Customer.LastName,
                Email = _situations.Customer.Email,
                PhoneNumber = _situations.Customer.PhoneNumber
            };
        else
            return null!;
    }
    public static async Task UpdateAsync(Situations situations)
    {
        var _situationEntity = await _context.Situations.FirstOrDefaultAsync(x => x.Id == situations.Id);
        if (_situationEntity != null)
        {
            if (!string.IsNullOrEmpty(situations.Condition))
                _situationEntity.Condition = situations.Condition;
            _context.Update(_situationEntity);
            await _context.SaveChangesAsync();
        }

    }
    public static async Task<bool> DeleteAsync(int id)
    {
        var _situations = await _context.Situations.Include(x => x.Customer).FirstOrDefaultAsync(x => x.Id == id);
        if (_situations != null)
        {
            _context.Remove(_situations);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public static async Task AddCommentAsync(int situationId, Comments comment, Models.CustomerServiceEmployees customerServiceEmployee)
    {
        var situationEntity = await _context.Situations.FindAsync(situationId);
        if (situationEntity != null)
        {
            var customerServiceEmployeeEntity = await _context.CustomerServiceEmployees.FirstOrDefaultAsync(x => x.FirstName == customerServiceEmployee.FirstName && x.LastName == customerServiceEmployee.LastName && x.Email == customerServiceEmployee.Email && x.PhoneNumber == customerServiceEmployee.PhoneNumber);
            if (customerServiceEmployeeEntity == null)
            {
                customerServiceEmployeeEntity = new CustomerServiceEmployeeEntity
                {
                    FirstName = customerServiceEmployee.FirstName,
                    LastName = customerServiceEmployee.LastName,
                    Email = customerServiceEmployee.Email,
                    PhoneNumber = customerServiceEmployee.PhoneNumber
                };
                _context.CustomerServiceEmployees.Add(customerServiceEmployeeEntity);
                await _context.SaveChangesAsync();
            }

            var commentEntity = new CommentEntity
            {
                Text = comment.Text,
                CreatedAt = DateTime.Now,
                SituationId = situationId,
                CustomerServiceEmployeeId = customerServiceEmployeeEntity.Id
            };
            _context.Comments.Add(commentEntity);
            await _context.SaveChangesAsync();
        }
    }

    public static async Task<IEnumerable<Comments>> GetCommentsAsync(int situationId)
    {
        var _situationEntity = await _context.Situations.Include(c => c.Comments).FirstOrDefaultAsync(c => c.Id == situationId);

        if (_situationEntity != null)
        {
            return _situationEntity.Comments.Select(c => new Comments
            {
                Id = c.Id,
                Text = c.Text,
                CreatedAt = c.CreatedAt,
                SituationId = c.SituationId

            });
        }

        return Enumerable.Empty<Comments>();
    }
}
