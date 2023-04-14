﻿using ReservationService.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationService.Model
{
    public class ReservationRequest
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string UserId { get; set; }
        public string AccommodationId { get; set; }
        public string AccommodationName { get; set; }
        public int NumberOfGuests { get; set; }
        public RequestStatus Status { get; set; }
    }
}