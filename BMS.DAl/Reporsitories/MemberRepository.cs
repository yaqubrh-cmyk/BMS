using BMS.DAL.DataContext;
using BMS.DAL.Models;
using BMS.DAL.Reporsitories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.DAL.Reporsitories
{
    public class MemberRepository : IRepository<Member>
    {
        public void Add(Member entity)
        {
            BMSDataBase.Members.Add(entity);
        }

        public void Delete(int id)
        {
            var member = GetById(id);
            if (member != null)
            {
                BMSDataBase.Members.Remove(member);
                return;
            }
            throw new Exception("Member not found");
        }

        public List<Member> GetAll()
        {
            return BMSDataBase.Members;
        }

        public Member GetById(int id)
        {
            var member = BMSDataBase.Members.FirstOrDefault(m => m.Id == id);

            if (member == null)
                throw new Exception("Member not found");

            return member;

        }

        public List<Member> Search(string keyword)
        {
            {
                return BMSDataBase.Members
                    .Where(m =>
                        m.FullName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                        m.Email.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                        m.PhoneNumber.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
        }

        public void Update(Member entity)
        {
            var existingMember = GetById(entity.Id);
            if (existingMember != null)
            {
                existingMember.FullName = entity.FullName;
                existingMember.Email = entity.Email;
                existingMember.PhoneNumber = entity.PhoneNumber;
                existingMember.IsActive = entity.IsActive;
            }

        } 
    }   
}
