﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Models.MODELS
{
    public partial class Turns
    {
        public int IdTurns { get; set; }
        public int IdUsers { get; set; }
        public int IdPitch { get; set; }
        public DateTime Dia { get; set; }

        public virtual Pitch IdPitchNavigation { get; set; }
        public virtual Users IdUsersNavigation { get; set; }
    }
}