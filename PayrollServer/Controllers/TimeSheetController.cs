﻿using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollServer.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSheetController : ControllerBase
    {
        private RepositoryContext _repository;
        public TimeSheetController(RepositoryContext repository)
        {
            _repository = repository;
        }

        [HttpGet]
        //public IEnumerable<IGrouping<int,TimeSheet>> GetAllTimePeriods()
        public IEnumerable<TimeSheet> GetAllTimePeriods()
        {
            //var result = _repository.TimeSheets.AsEnumerable().GroupBy(r => r.SheetId).ToList();
            var result = _repository.TimeSheets.Where(r => r.DateDeleted == null).ToList();

            return result;
        }

        [HttpPost]
        public void Insert(List<TimeSheet> timeSheets)
        {
            var last = _repository.TimeSheets.Where(r => r.DateDeleted == null)
                                            .OrderByDescending(r => r.SheetId).FirstOrDefault();
            int lastId = 1;
            if (last != null)
            {
                lastId = last.SheetId;
            }

            lastId++;
            foreach (var item in timeSheets)
            {
                item.Id = Guid.NewGuid();
                item.SheetId = lastId;
                item.DateCreated = DateTime.Now;
            }

            _repository.TimeSheets.AddRange(timeSheets);
            _repository.SaveChanges();
        }


        public class InsertParams
        {
            public string Name { get; set; }
            public int SheetId { get; set; }
            public List<TimeSheet> timeSheets { get; set; }
        }
        [HttpPut]
        public void Update([FromBody]InsertParams InsertParams)
        {
            var timeSheets = InsertParams.timeSheets;
            var SheetId = InsertParams.SheetId;
            var Name = InsertParams.Name;

            var currentTime = DateTime.Now;
            var data = _repository.TimeSheets.Where(r => r.SheetId == SheetId);
            if (data != null)
            {
                foreach (var item in data.ToList())
                {
                    item.DateDeleted = currentTime;
                }
            }


            foreach (var item in timeSheets)
            {
                item.Name = Name;
                item.SheetId = SheetId;
                item.DateCreated = DateTime.Now;
                item.Id = Guid.NewGuid();
            }

            _repository.TimeSheets.AddRange(timeSheets);


            _repository.SaveChanges();
        }
    }
}
