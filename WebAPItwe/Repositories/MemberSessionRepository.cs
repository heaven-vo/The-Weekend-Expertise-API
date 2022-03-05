﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPItwe.Entities;
using WebAPItwe.InRepositories;
using WebAPItwe.Models;

namespace WebAPItwe.Repositories
{
    public class MemberSessionRepository : InMemberSessionRepository
    {
        private readonly dbEWTContext context;

        public MemberSessionRepository(dbEWTContext context)
        {
            this.context = context;
        }
        public async Task<object> LoadHistory(string memberId, int pageIndex, int pageSize)
        {
            var history = await (from s in context.Sessions 
                                 join m in context.MemberSessions on s.Id equals m.SessionId 
                                 where m.MemberId == memberId && s.Status == "Done" 
                                 select new HistoryModel {
                                        SessionId = s.Id,
                                        Slot = s.Slot,
                                        Date = s.Date
                                 }).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return null;
        }
    }
}