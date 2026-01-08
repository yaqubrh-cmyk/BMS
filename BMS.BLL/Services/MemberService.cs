using BMS.BLL.Services.Contracts;
using BMS.DAL.Models;
using BMS.DAL.Reporsitories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.BLL.Services
{
    public class MemberService : IMemberService
    {
        private readonly IRepository<Member> _memberRepository;
        public MemberService(IRepository<Member> memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public List<Member> GetAllMembers() => _memberRepository.GetAll();

        public Member? GetMemberById(int id) => _memberRepository.GetById(id);

        public void AddMember(Member member) => _memberRepository.Add(member);

        public void UpdateMember(int id, Member member)
        {
            var existingMember = _memberRepository.GetById(id);
            if (existingMember == null)
                throw new Exception("Member not found");

            member.Id = id;
            _memberRepository.Update(member);
        }

        public void DeleteMember(int id) => _memberRepository.Delete(id);

        public List<Member> SearchMembers(string keyword) =>
            _memberRepository.Search(keyword)
                .Where(m => m.FullName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                            m.Email.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();
    }
}
