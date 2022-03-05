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
        ///// <summary>
        ///// Update cafe by Id
        ///// </summary>
        //[HttpPut("{id}")]
        //public async Task<IActionResult>PutMemberAccount(string id, MemberAccountModel memAccount)
        //{
        //    try
        //    {
        //        var result = _context.Members.Find(id);
        //        var majorId = _context.Majors.Where(x => x.Name == memAccount.MajorName).Select(x => x.Id).FirstOrDefault();
        //        result.Id = memAccount.Id;
        //        result.Fullname = memAccount.Fullname;
        //        result.Image = memAccount.Image;
        //        result.Address = memAccount.Address;
        //        result.Phone = memAccount.Phone;
        //        result.Sex = memAccount.Sex;
        //        result.Facebook = memAccount.Facebook;
        //        result.Birthday = memAccount.Birthday;  
        //        result.Grade = memAccount.Grade;
        //        result.MajorId = majorId.ToString();
        //        result.Status = memAccount.Status;

        //        await _context.SaveChangesAsync();
        //        return Ok(result);

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(409, new { StatusCode = 409, message = ex.Message });
        //    }
        //}
        ///// <summary>
        ///// Create cafe by Id
        ///// </summary>
        //[HttpPost]
        //public async Task<ActionResult<MemberAccountModel>> PostMemberAccount(MemberAccountModel memAccount)
        //{
        //    try
        //    {
        //        var result = new MemberAccountModel();

        //        result.Id = memAccount.Id;
        //        result.Fullname = memAccount.Fullname;
        //        result.Image = memAccount.Image;
        //        result.Address = memAccount.Address;
        //        result.Phone = memAccount.Phone;
        //        result.Sex = memAccount.Sex;
        //        result.Facebook = memAccount.Facebook;
        //        result.Birthday = memAccount.Birthday;
        //        result.Grade = memAccount.Grade;
        //        result.MajorName = memAccount.MajorName;
        //        result.Status = memAccount.Status;

        //        _context.Members.Add(result);
        //        await _context.SaveChangesAsync();

        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(409, new { StatusCode = 409, message = ex.Message });
        //    }

        //}
    }
}