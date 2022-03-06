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
    [Route("api/v1/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly dbEWTContext _context;

        public AdminController(dbEWTContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Get list all member with pagination.
        /// </summary>
        [HttpGet("/members")]
        public async Task<ActionResult<IEnumerable<MemberAccountModel>>> GetAll(int pageIndex, int pageSize)
        {
            try
            {
                var mem = await (from c in _context.Members
                                 join ma in _context.Majors on c.MajorId equals ma.Id

                                 select new MemberAccountModel
                                 {
                                     Id = c.Id,
                                     Fullname = c.Fullname,
                                     Image = c.Image,
                                     Address = c.Address,
                                     Phone = c.Phone,
                                     Sex = c.Sex,
                                     Facebook = c.Facebook,
                                     Birthday = c.Birthday,
                                     Grade = c.Grade,
                                     MajorName = ma.Name,
                                     Status = c.Status

                                 }).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                foreach (var member in mem)
                {
                    string email = _context.Users.Where(x => x.Id == member.Id).Select(x => x.Email).FirstOrDefault();
                    member.Email = email;
                }

                return Ok(mem);
            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, message = ex.Message });
            }
        }
        /// <summary>
        /// Get a member by id
        /// </summary>
        [HttpGet("/members/{id}")]
        public async Task<ActionResult<MemberAccountModel>> GetById(string id)
        {
            var mem = await (from c in _context.Members
                             join ma in _context.Majors on c.MajorId equals ma.Id
                             where c.Id == id

                             select new MemberAccountModel
                             {
                                 Id = c.Id,
                                 Fullname = c.Fullname,
                                 Image = c.Image,
                                 Address = c.Address,
                                 Phone = c.Phone,
                                 Sex = c.Sex,
                                 Facebook = c.Facebook,
                                 Birthday = c.Birthday,
                                 Grade = c.Grade,
                                 MajorName = ma.Name,
                                 Status = c.Status

                             }).ToListAsync();
            foreach (var member in mem)
            {
                string email = _context.Users.Where(x => x.Id == member.Id).Select(x => x.Email).FirstOrDefault();
                member.Email = email;
            }


            if (!mem.Any())
            {
                return BadRequest(new { StatusCode = 404, message = "Not Found" });
            }

            return Ok(mem);
        }
        /// <summary>
        /// Search Member by name
        /// </summary>
        [HttpGet("/members/name")]
        public async Task<ActionResult<MemberAccountModel>> GetByName(string name)
        {
            try
            {
                var mem = await (from c in _context.Members
                                 join ma in _context.Majors on c.MajorId equals ma.Id   //search gan dung 
                                 where c.Fullname.Contains(name)

                                 select new MemberAccountModel
                                 {
                                     Id = c.Id,
                                     Fullname = c.Fullname,
                                     Image = c.Image,
                                     Address = c.Address,
                                     Phone = c.Phone,
                                     Sex = c.Sex,
                                     Facebook = c.Facebook,
                                     Birthday = c.Birthday,
                                     Grade = c.Grade,
                                     MajorName = ma.Name,
                                     Status = c.Status

                                 }).ToListAsync();
                foreach (var member in mem)
                {
                    string email = _context.Users.Where(x => x.Id == member.Id).Select(x => x.Email).FirstOrDefault();
                    member.Email = email;
                }
                if (!mem.Any())
                {
                    return BadRequest(new { StatusCode = 404, message = "Name is not found!" });
                }
                return Ok(mem);

            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, message = ex.Message });
            }
        }
        /// <summary>
        /// Search Member by majorName
        /// </summary>
        [HttpGet("/members/majorName")]
        public async Task<ActionResult<MemberAccountModel>> GetByMajorName(string majorName)
        {
            try
            {
                var mem = await (from c in _context.Members
                                 join ma in _context.Majors on c.MajorId equals ma.Id   //search gan dung 
                                 where ma.Name.Contains(majorName)

                                 select new MemberAccountModel
                                 {
                                     Id = c.Id,
                                     Fullname = c.Fullname,
                                     Image = c.Image,
                                     Address = c.Address,
                                     Phone = c.Phone,
                                     Sex = c.Sex,
                                     Facebook = c.Facebook,
                                     Birthday = c.Birthday,
                                     Grade = c.Grade,
                                     MajorName = ma.Name,
                                     Status = c.Status

                                 }).ToListAsync();
                foreach (var member in mem)
                {
                    string email = _context.Users.Where(x => x.Id == member.Id).Select(x => x.Email).FirstOrDefault();
                    member.Email = email;
                }
                if (!mem.Any())
                {
                    return BadRequest(new { StatusCode = 404, message = "Name is not found!" });
                }
                return Ok(mem);

            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, message = ex.Message });
            }
        }
        /// <summary>
        /// Sort memberAccount by name
        /// </summary>
        [HttpGet("sorting/fullName")]
        public async Task<ActionResult<MemberAccountModel>> SortByName(int pageIndex, int pageSize)
        {
            try
            {
                var mem = await (from c in _context.Members.OrderBy(c => c.Fullname)
                                 join ma in _context.Majors on c.MajorId equals ma.Id
                                 select new MemberAccountModel
                                 {

                                     Id = c.Id,
                                     Fullname = c.Fullname,
                                     Image = c.Image,
                                     Address = c.Address,
                                     Phone = c.Phone,
                                     Sex = c.Sex,
                                     Facebook = c.Facebook,
                                     Birthday = c.Birthday,
                                     Grade = c.Grade,
                                     MajorName = ma.Name,
                                     Status = c.Status,
                                 }).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                foreach (var member in mem)
                {
                    string email = _context.Users.Where(x => x.Id == member.Id).Select(x => x.Email).FirstOrDefault();
                    member.Email = email;
                }
                return Ok(mem);

            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, message = ex.Message });
            }
        }
    }
}