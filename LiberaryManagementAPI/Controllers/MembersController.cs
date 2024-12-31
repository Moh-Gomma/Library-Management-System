using LiberaryManagementAPI.Data;
using LiberaryManagementAPI.Dtos;
using LiberaryManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiberaryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly LiberaryDbContext _context;

        public MembersController(LiberaryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberResponseDTO>>> GetMembers()
        {
            var members = await _context.members
                .Select(m => new MemberResponseDTO
                {
                    MemberId = m.MemberId,
                    FullName = $"{m.FirstName} {m.LastName}",
                    Email = m.Email,
                    PhoneNumber = m.Phone,
                    Address = m.Address,
                    JoinDate = m.JoinDate,
                    IsActive = m.IsActive,
                    ActiveLoans = _context.loans.Count(l => l.MemberId == m.MemberId && !l.IsReturned)
                })
                .ToListAsync();

            return Ok(members);
        }

        [HttpPost]
        public async Task<ActionResult<MemberResponseDTO>> CreateMember(MemberCreateDTO memberDTO)
        {

            if (await _context.members.AnyAsync(m => m.Email == memberDTO.Email))
                return BadRequest("Email already registered");

            var member = new Member
            {
                FirstName = memberDTO.FirstName,
                LastName = memberDTO.LastName,
                Email = memberDTO.Email,
                Phone = memberDTO.PhoneNumber,
                Address = memberDTO.Address,
                JoinDate = DateTime.Now,
                IsActive = true
            };

            _context.members.Add(member);
            await _context.SaveChangesAsync();

            var response = new MemberResponseDTO
            {
                MemberId = member.MemberId,
                FullName = $"{member.FirstName} {member.LastName}",
                Email = member.Email,
                PhoneNumber = member.Phone,
                Address = member.Address,
                JoinDate = member.JoinDate,
                IsActive = member.IsActive,
                ActiveLoans = 0
            };

            return CreatedAtAction(nameof(GetMember), new { id = member.MemberId }, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MemberResponseDTO>> GetMember(int id)
        {
            var member = await _context.members.FindAsync(id);

            if (member == null)
                return NotFound();

            var activeLoans = await _context.loans
                .CountAsync(l => l.MemberId == id && !l.IsReturned);

            var response = new MemberResponseDTO
            {
                MemberId = member.MemberId,
                FullName = $"{member.FirstName} {member.LastName}",
                Email = member.Email,
                PhoneNumber = member.Phone,
                Address = member.Address,
                JoinDate = member.JoinDate,
                IsActive = member.IsActive,
                ActiveLoans = activeLoans
            };

            return Ok(response);
        }
    }
}
