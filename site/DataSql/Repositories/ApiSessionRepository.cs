using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataInterfaces.Models;
using DataInterfaces.Repositories;
using System.Data.Entity;
using Data.Services;

namespace Data.Repositories
{
    public class ApiSessionRepository : IApiSessionRepository
    {
        public Session GetSession(int userId)
        {
            using (var db = new statusContainer())
            {
                var session = new api_session { session_id = Guid.NewGuid(), user_id = userId, date_created = DateTime.Now, expires = DateTime.Now.AddMinutes(30)};
                db.api_session.AddObject(session);
                db.SaveChanges();
                return new Session
                {
                    SessionId = session.session_id.ToString(),
                    Expires = session.expires
                };
            }
        }

        public Session IsSessionValid(string sessionId)
        {
            using (var db = new statusContainer())
            {
                var guid = new Guid(sessionId);
                var session = db.api_session.SingleOrDefault(a => a.session_id == guid);
                if (session != null && session.expires >= DateTime.Now)
                    return new Session
                    {
                        SessionId = session.session_id.ToString(),
                        Expires = session.expires
                    };

                return null;
            }
        }

        public void RefreshExpiry(string sessionId)
        {
            using (var db = new statusContainer())
            {
                var guid = new Guid(sessionId);
                var session = db.api_session.SingleOrDefault(a => a.session_id == guid);
                session.expires = DateTime.Now.AddMinutes(30);
                db.SaveChanges();
            }
        }

        public void EndSession(string sessionId)
        {
            using (var db = new statusContainer())
            {
                var guid = new Guid(sessionId);
                var session = db.api_session.Single(s => s.session_id == guid);
                db.api_session.DeleteObject(session);
                db.SaveChanges();
            }
        }

        public int GetUserFromSession(string sessionId)
        {
            using (var db = new statusContainer())
            {
                var guid = new Guid(sessionId);
                return db.api_session.SingleOrDefault(a => a.session_id == guid).user_id;
            }
        }
    }
}
