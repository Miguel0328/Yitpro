using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Models;
using Repository.Interfaces;
using Resources.Constants;
using Resources.Extension;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Activity : IActivity
    {
        private readonly DataContext _context;

        public Activity(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ActivityModel>> Get(long userId)
        {
            var user = await _context.User.FindAsync(userId);
            var activities = user.GetActivities(_context);

            return await activities.ToListAsync();
        }

        public async Task<ActivityModel> GetDetail(long id)
        {
            return await _context.Activity.FindAsync(id);
        }

        public 

        public async Task<long> Post(ActivityModel _activity)
        {
            _context.Activity.Add(_activity);
            await _context.SaveChangesAsync();
            await PostComment(_activity, Log.Activity.Creation, true);

            return _activity.Id;
        }

        private async Task PostComment(ActivityModel _activity, string comment, bool log)
        {
            _context.ActivityComment.Add(new ActivityCommentModel
            {
                ActivityId = _activity.Id,
                UserId = _activity.UpdatedById,
                Comment = comment,
                Log = log,
                Date = DateTime.Now
            });
            await _context.SaveChangesAsync();

            return;
        }
    }
}
