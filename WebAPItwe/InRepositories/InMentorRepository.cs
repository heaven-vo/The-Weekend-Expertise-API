using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPItwe.Models;

namespace WebAPItwe.InRepositories
{
    public interface InMentorRepository
    {
        Task<IEnumerable<MentorModel>> GetAll();
        Task<MentorModel> GetById(string Id);
        Task<IEnumerable<MentorModel>> SortByPrice();
        Task<ActionResult<IEnumerable<MentorModel>>> FindByName(string name);
    }
}
