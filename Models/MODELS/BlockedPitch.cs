﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Models.MODELS
{
    public partial class BlockedPitch
    {
        public string NombreCancha { get; set; }
        public int Id { get; set; }
        public bool? IsBlocked { get; set; }

        public virtual Pitch NombreCanchaNavigation { get; set; }
    }
}