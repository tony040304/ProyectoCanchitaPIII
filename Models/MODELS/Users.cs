﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Models.MODELS
{
    public partial class Users
    {
        public Users()
        {
            Turns = new HashSet<Turns>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Userpassword { get; set; }
        public string Email { get; set; }
        public int Role { get; set; }

        public virtual ICollection<Turns> Turns { get; set; }
    }
}