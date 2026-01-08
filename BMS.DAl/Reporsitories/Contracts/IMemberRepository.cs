using BMS.DAL.Models;

namespace BMS.DAL.Reporsitories.Contracts
{
    internal interface IMemberRepository
    {
        List<MemberRepository> GetAll();
        MemberRepository? GetMemberById(int id);
        void AddMember(MemberRepository member);
        void UpdateMember(int id, MemberRepository member);
        void DeleteMember(int id);
        List<MemberRepository> Search(string keyword);
    }
}
