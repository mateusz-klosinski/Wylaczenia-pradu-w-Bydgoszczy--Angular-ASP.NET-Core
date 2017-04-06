using Enea.Dtos;
using Enea.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enea.Controllers.api
{
    [Authorize]
    [Route("api/keywords")]
    public class KeyWordController : Controller
    {
        private EneaContext _context;

        public KeyWordController(EneaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var userName = User.Identity.Name;

            var user = _context.Users
                .Include(u => u.KeyWords)
                .Where(u => u.UserName == userName)
                .FirstOrDefault();

            return Ok(user.KeyWords);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] KeyWordDto dto)
        {
            if (ModelState.IsValid)
            {
                var keyword = await _context.KeyWords.Where(k => k.Id == dto.Id).FirstOrDefaultAsync();

                keyword.IsOnline = dto.IsOnline;

                await _context.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }
        
        
        [HttpPost("/api/keywords/delete")]
        public async Task<IActionResult> Delete([FromBody] KeyWordDto dto)
        {
            if (ModelState.IsValid)
            {
                var userName = User.Identity.Name;

                var user = await _context.Users
                    .Include(u => u.KeyWords)
                    .Where(u => u.UserName == userName)
                    .FirstOrDefaultAsync();

                var keyword = await _context.KeyWords
                    .Where(k => k.Id == dto.Id)
                    .FirstOrDefaultAsync();

                _context.KeyWords.Remove(keyword);
                user.KeyWords.Remove(keyword);

                await _context.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }
        
        
        [HttpPost("/api/keywords/create")]
        public async Task<IActionResult> Create([FromBody] KeyWordDto dto)
        {
            if (ModelState.IsValid)
            {
                var userName = User.Identity.Name;

                var user = await _context.Users
                    .Include(u => u.KeyWords)
                    .Where(u => u.UserName == userName)
                    .FirstOrDefaultAsync();

                var keyWord = new KeyWord()
                {
                    Word = dto.Word
                };

                _context.KeyWords.Add(keyWord);
                user.KeyWords.Add(keyWord);

                await _context.SaveChangesAsync();

                _context.Update(keyWord);

                return Created("", keyWord);
            }

            return BadRequest();
        }

    }
}
