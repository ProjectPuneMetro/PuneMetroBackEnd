//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class CancelledTicket
    {
        public int cancellation_id { get; set; }
        public int cancellationReq_id { get; set; }
        public int booking_id { get; set; }
    
        public virtual BookedSeat BookedSeat { get; set; }
        public virtual CancellationReq CancellationReq { get; set; }
    }
}
