using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPItwe.Entities;
using WebAPItwe.Models;

namespace WebAPItwe.Controllers.Cafe
{
    [Route("api/v1/cafe/sessions")]
    [ApiController]
    public class CafeSessionsController : ControllerBase
    {
        private readonly dbEWTContext _context;

        public CafeSessionsController(dbEWTContext context)
        {
            _context = context;
        }

        [HttpGet("/done")]
        public async Task<ActionResult<IEnumerable<Session>>> LoadSessionsCafe(int pageIndex = 1, int pageSize = 5)
        {

            try
            {
                string dateCt = DateTime.Now.ToString("yyyy-MM-dd");
                var listSessions = await _context.Sessions
                                 .Where(c => c.Status == 2).Where(c => c.Date == dateCt)
                                 .Select(c => new SessionMeetingModel

                                 {
                                     SessionId = c.Id,
                                     SubjectName = c.SubjectName,
                                     SubjectImage = c.SubjectImage,
                                     MentorName = c.MentorName,
                                     CafeName = c.CafeName,
                                     Slot = c.Slot,
                                     Price = c.Price,
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
        //GET: api/Sessions
        /// <summary>
        /// Get list Meeting with  pagination
        /// </summary>
        [HttpGet("listRq")]
        public async Task<ActionResult<IEnumerable<Session>>> LoadCafeSessionsAll(int pageIndex = 1, int pageSize = 5)
        {

            try
            {
                var listSessions = await _context.Sessions
                                  .Where(c => c.Status ==2)
                                 .Select(c => new SessionMeetingModel

                                 {
                                     SessionId = c.Id,
                                     SubjectName = c.SubjectName,
                                     SubjectImage = c.SubjectImage,
                                     MentorName = c.MentorName,
                                     CafeName = c.CafeName,
                                     Slot = c.Slot,
                                     Price = c.Price,
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
        [HttpGet("/history")]
        public async Task<ActionResult<IEnumerable<Session>>> LoadHistorySessionsCafe(int pageIndex = 1, int pageSize = 5)
        {

            try
            {
                var listSessions = await _context.Sessions
                                 .Where(c => c.Status == 2 || c.Status == 3)
                                 .Select(c => new SessionMeetingModel

                                 {
                                     SessionId = c.Id,
                                     SubjectName = c.SubjectName,
                                     SubjectImage = c.SubjectImage,
                                     MentorName = c.MentorName,
                                     CafeName = c.CafeName,
                                     Slot = c.Slot,
                                     Price = c.Price,
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
        
        /// <summary>
        /// Cafe Confirm or Cancel 
        /// </summary>
        [HttpPut("cafeActive/{id}")]
        public async Task<IActionResult> PutCafeSession(string id)
        {
            try
            {
                var cafeAc = _context.Sessions.Find(id);
                if (cafeAc.CafeActive == true)
                {
                    cafeAc.CafeActive = false;
                }
                else
                {
                    cafeAc.CafeActive = true;
                }
                await _context.SaveChangesAsync();
                return Ok(cafeAc);
            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, message = ex.Message });
            }
        }
    }
}
