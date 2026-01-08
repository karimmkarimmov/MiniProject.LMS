using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.Models;

namespace DataAccessLayer.DataContext
{
    public class DataBase
    {
        public static List<Book> Books { get; set; } = [];
        public static List<Member> Members { get; set; } = [];
        public static List<Category> Categories { get; set; } = [];
    }
}
