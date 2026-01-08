using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using DataAccessLayer.DataContext;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace BusinessLogicLayer.Services
{
    public class MemberService
    {
        private readonly MemberRepository _repo = new MemberRepository();

        public void Add(Member m)
        {
            Validate(m);

            var existing = DataBase.Members.Find(mem => mem.Id == m.Id);
            if (existing != null)
                throw new Exception("Member Id already exists.");

            _repo.Add(m);
        }

        public List<Member> GetAll() => _repo.GetAll();

        public Member GetById(int id) => _repo.GetById(id);

        public void Update(Member m)
        {
            Validate(m);

            var existing = DataBase.Members.Find(mem => mem.Id == m.Id);
            if (existing == null)
                throw new Exception("Member not found.");

            _repo.Update(m);
        }

        public void Delete(int id) => _repo.Delete(id);

        public List<Member> Search(string keyword) => _repo.Search(keyword);

        private static void Validate(Member m)
        {
            if (m == null) throw new Exception("Member is null.");
            if (m.Id <= 0) throw new Exception("Member Id must be > 0.");
            if (string.IsNullOrWhiteSpace(m.FullName)) throw new Exception("FullName is required.");
            if (string.IsNullOrWhiteSpace(m.Email)) throw new Exception("Email is required.");

            var ok = Regex.IsMatch(m.Email.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!ok) throw new Exception("Email format is invalid.");

            if (m.MembershipDate == default)
                m.MembershipDate = DateTime.Today;
        }
    }
}