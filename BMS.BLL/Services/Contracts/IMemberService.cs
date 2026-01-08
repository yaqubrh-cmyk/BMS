using BMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.BLL.Services.Contracts
{
    public interface IMemberService
    {
        List<Member> GetAllMembers();
        Member? GetMemberById(int id);
        void AddMember(Member member);
        void UpdateMember(int id, Member member);
        void DeleteMember(int id);
        List<Member> SearchMembers(string keyword);
    }
}
