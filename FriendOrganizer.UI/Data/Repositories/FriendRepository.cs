using FriendOrganizer.DataAccess;
using FriendOrganizer.Model;
using System.Data.Entity;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.Data.Repositories
{
    public class FriendRepository : IFriendRepository
    {
        private FriendOrganizerDbContext _context;

        public FriendRepository(FriendOrganizerDbContext context)
        {
            _context = context;
        }

        public async Task<Friend> GetByIdAsync(int friendId)
        {
            return await _context.Friends
                .Include(f => f.PhoneNumbers)
                .SingleAsync(f => f.Id == friendId);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public void Add(Friend friend)
        {
            _context.Friends.Add(friend);
        }

        public void Remove(Friend model)
        {
            _context.Friends.Remove(model);
        }

        public void RemovePhoneNumber(FriendPhoneNumber model)
        {
            _context.FriendPhoneNumbers.Remove(model);
        }
    }
}
