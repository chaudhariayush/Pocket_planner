using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ayushChaudhary.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        //public string Gender { get; set; }

        public string Currency { get; set; }

        public Guid Id { get; set; }
        //public string Name { get; set; }


    }

}
