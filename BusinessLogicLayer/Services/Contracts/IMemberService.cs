using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Services.Contracts
{
    public interface IMemberService
    {
        void Add(Member member);
        Member Get(int id);
        List<Member> GetAll();
        void Update(Member member);
        void Delete(int id);
        List<Member> Search(string keyword);
    }
}
