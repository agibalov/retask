using System.Linq;
using Service.DAL;
using Service.Exceptions;

namespace Service.TransactionScripts.BL
{
    public class AuthenticationService
    {
        private readonly int _sessionTtlSeconds;
        private readonly ITimeProvider _timeProvider;

        public AuthenticationService(
            int sessionTtlSeconds,
            ITimeProvider timeProvider)
        {
            _sessionTtlSeconds = sessionTtlSeconds;
            _timeProvider = timeProvider;
        }

        public User GetUserBySessionToken(TodoContext context, string sessionToken)
        {
            var currentTime = _timeProvider.GetCurrentTime();
            var goodLastActivityTime = currentTime.AddSeconds(-_sessionTtlSeconds);
            var sessionsToDelete = context
                .Sessions
                .Where(s => s.LastAccessTime < goodLastActivityTime)
                .ToList();

            if (sessionsToDelete.Count > 0)
            {
                foreach (var sessionToDelete in sessionsToDelete)
                {
                    context.Sessions.Remove(sessionToDelete);
                }

                context.SaveChanges();
            }

            var session = context.Sessions
                .Include("User")
                .SingleOrDefault(s => s.Token == sessionToken);
            if (session == null)
            {
                throw new TodoException(ServiceError.SessionExpired);
            }

            session.LastAccessTime = currentTime;
            var user = session.User;
            user.LastActivityAt = _timeProvider.GetCurrentTime();
            context.SaveChanges();

            return session.User;
        }

        public void MakeSureSessionIsValid(TodoContext context, string sessionToken)
        {
            GetUserBySessionToken(context, sessionToken);
        }
    }
}