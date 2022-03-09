using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPItwe.Entities;
using WebAPItwe.Models;

namespace WebAPItwe.Controllers.Admin
{
    [Route("api/v1/admin/sessions")]
    [ApiController]
    public class AdminSessionController : ControllerBase
    {
        private readonly dbEWTContext _context;

        public AdminSessionController(dbEWTContext context)
        {
            _context = context;
        }

        [HttpGet("/status")]
        public async Task<ActionResult<IEnumerable<Session>>> LoadSessionsActive(int pageIndex = 1, int pageSize = 5)
        {

            try
            {
                string dateCt = DateTime.Now.ToString("yyyy-MM-dd");
                var listSessions = await _context.Sessions
                                 .Where(c => c.Status == 3).Where(c => c.Date == dateCt)
                                 .Select(c => new SessionMeetingModel

                                 {
                                     SessionId = c.Id,
                                     SubjectName = c.SubjectName,
                                     SubjectImage = c.SubjectImage,
                                     MentorName = c.MentorName,
                                     CafeName = c.CafeName,
                                     Price = c.Price,
                                     Slot = c.Slot,
                                     Date = c.Date,
                                     Status = c.Status
                                 }).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                foreach (var session in listSessions)
                {
                    var cafeId = await _context.Sessions.Where(x => x.Id == session.SessionId).Select(x => x.CafeId).FirstOrDefaultAsync();
                    var cafe = await _context.Cafes.FindAsync(cafeId);
                    session.CafeDistric = cafe.Distric;
                    session.CafeStreet = cafe.Street;

                    var mentorId = await _context.Sessions.Where(x => x.Id == session.SessionId).Select(x => x.MentorId).FirstOrDefaultAsync();
                    var mentor = await _context.Mentors.FindAsync(mentorId);
                    session.MentorImage = mentor.Image;
                    List<string> listImage = await _context.MemberSessions.Where(x => x.SessionId == session.SessionId).Select(x => x.MemberImage).Take(5).ToListAsync();
                    session.ListMemberImage = listImage;

                }



                return Ok(listSessions);
            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, message = ex.Message });
            }
        }
        // GET: api/Sessions
        /// <summary>
        /// Get list Meeting with  pagination
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Session>>> LoadSessionsAll(int pageIndex = 1, int pageSize = 5)
        {

            try
            {
                var listSessions = await _context.Sessions
                                 .Select(c => new SessionMeetingModel

                                 {
                                     SessionId = c.Id,
                                     SubjectName = c.SubjectName,
                                     SubjectImage = c.SubjectImage,
                                     MentorName = c.MentorName,
                                     CafeName = c.CafeName,
                                     Price = c.Price,
                                     Slot = c.Slot,
                                     Date = c.Date,
                                     Status = c.Status
                                 }).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                foreach (var session in listSessions)
                {
                    var cafeId = await _context.Sessions.Where(x => x.Id == session.SessionId).Select(x => x.CafeId).FirstOrDefaultAsync();
                    var cafe = await _context.Cafes.FindAsync(cafeId);
                    session.CafeDistric = cafe.Distric;
                    session.CafeStreet = cafe.Street;

                    var mentorId = await _context.Sessions.Where(x => x.Id == session.SessionId).Select(x => x.MentorId).FirstOrDefaultAsync();
                    var mentor = await _context.Mentors.FindAsync(mentorId);
                    session.MentorImage = mentor.Image;
                    List<string> listImage = await _context.MemberSessions.Where(x => x.SessionId == session.SessionId).Select(x => x.MemberImage).Take(5).ToListAsync();
                    session.ListMemberImage = listImage;

                }



                return Ok(listSessions);
            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, message = ex.Message });
            }
        }


        [HttpGet("top_rate")]
        public async Task<object> LoadTopMentorHome(int pageIndex, int pageSize)
        {
            var listMentor = await _context.Mentors.OrderByDescending(x => x.Rate).Select(x => new MentorModel
            {
                Id = x.Id,
                Fullname = x.Fullname,
                Address = x.Address,
                Slogan = x.Slogan,
                Phone = x.Phone,
                Image = x.Image,
                Sex = x.Sex,
                Price = x.Price,
                Birthday = x.Birthday,
                Rate = x.Rate,
                Description = x.Description,
                Status = x.Status

            }).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            foreach (MentorModel mentor in listMentor)
            {
                mentor.Email = await _context.Users.Where(x => x.Id == mentor.Id).Select(x => x.Email).FirstOrDefaultAsync();
            }
            return Ok(listMentor);
        }
    
        // GET: api/Sessions/5
        /// <summary>
        /// Get a Session by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Session>> GetSession(string id)
        {
            var session = await (from c in _context.Sessions

                                 join cm in _context.Mentors on c.MentorId equals cm.Id
                                 join cf in _context.Cafes on c.CafeId equals cf.Id
                                 where c.Id == id

                                 select new
                                 {

                                     Id = c.Id,
                                     MaxPerson = c.MaxPerson,
                                     SubjectName = c.SubjectName,
                                     MentorName = c.MentorName,
                                     Price = c.Price,
                                     Street = cf.Street,
                                     Fullname = cm.Fullname,
                                     Distric = cf.Distric,
                                     Date = c.Date,
                                     Phone = cm.Phone,
                                     Status = c.Status

                                 }).ToListAsync();


            if (!session.Any())
            {
                return BadRequest(new { StatusCode = 404, message = "Not Found" });
            }

            return Ok(session);
        }

        //// PUT: api/Sessions/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutSession(string id, Session session)
        //{
        //    if (id != session.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(session).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!SessionExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Sessions
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Session>> PostSession(Session session)
        //{
        //    _context.Sessions.Add(session);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (SessionExists(session.Id))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetSession", new { id = session.Id }, session);
        //}

        //// DELETE: api/Sessions/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteSession(string id)
        //{
        //    var session = await _context.Sessions.FindAsync(id);
        //    if (session == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Sessions.Remove(session);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool SessionExists(string id)
        //{
        //    return _context.Sessions.Any(e => e.Id == id);
        //}
    }
}
