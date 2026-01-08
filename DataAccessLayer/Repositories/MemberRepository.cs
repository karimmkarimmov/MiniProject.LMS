using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.Models;
using DataAccessLayer.DataContext;
using DataAccessLayer.Repositories.Contracts;

namespace DataAccessLayer.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        public void Add(Member entity)
        {
            DataBase.Members.Add(entity);
        }

        public void Delete(int id)
        {
            var member = GetById(id);
            if (member != null)
            {
                DataBase.Members.Remove(member);
            }
            else
            {
                throw new Exception("Member not found");
            }
        }

        public List<Member> GetAll()
        {    
            return DataBase.Members;
        }

        public Member GetById(int id)
        {
            var member = DataBase.Members.Find(m => m.Id == id);
            if (member == null)
                throw new Exception("Member not found");
            return member;
        }

        public List<Member> Search(string keyword)
        {
            return DataBase.Members
                .Where(m => m.FullName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                            m.Email.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                            m.PhoneNumber.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public void Update(Member entity)
        {
            
        }
    }
}
