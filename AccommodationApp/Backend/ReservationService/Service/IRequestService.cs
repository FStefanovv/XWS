﻿using Microsoft.Extensions.Primitives;
using ReservationService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReservationService.DTO;

namespace ReservationService.Service
{
    public interface IRequestService
    {
        void CancelReservationRequest(string requestId, StringValues userId);
        List<ReservationRequest> GetPendingRequestsByHost(StringValues userId);
        List<ReservationRequest> GetResolvedRequestsByHost(StringValues userId);
        Task<bool> CreateReservationRequestOrReservation(RequestReservationDTO dto);
        void AcceptRequest(string requestId, string accommodationId);
        List<ShowRequestDTO> GetRequestsForHost(string hostId);
        List<ShowRequestDTO> GetRequestsForUsers(string userId);
    }
}
