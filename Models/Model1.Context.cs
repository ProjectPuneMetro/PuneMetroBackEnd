﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace V1.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PuneMetroV1Entities2 : DbContext
    {
        public PuneMetroV1Entities2()
            : base("name=PuneMetroV1Entities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BookedSeat> BookedSeats { get; set; }
        public virtual DbSet<CancellationReq> CancellationReqs { get; set; }
        public virtual DbSet<CancelledTicket> CancelledTickets { get; set; }
        public virtual DbSet<Coach> Coaches { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Seat> Seats { get; set; }
        public virtual DbSet<Train> Trains { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
