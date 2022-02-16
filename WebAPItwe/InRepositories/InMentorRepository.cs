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
        Task<IEnumerable<MentorModel>> GetAll(int pageIndex, int pageSize);
        Task<MentorModel> GetById(string Id);
        Task<IEnumerable<MentorModel>> SortByPrice();
        Task<IEnumerable<MentorModel>> FindByName(string name);
        Task<IEnumerable<MentorModel>> Filter(string major);
    }
}
