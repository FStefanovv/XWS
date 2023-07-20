﻿namespace ReservationService.RabbitMQ
{
    public class BusConstants
    {
        public const string RabbitMqUri = "rabbitmq://localhost/";
        public const string UserName = "guest";
        public const string Password = "guest";
        public const string StartDeleteQueue = "reservations_queue";
        public const string StartDeleteAccommodation = "accommodation_queue";
        public const string CancelDeleteQueue = "hasReservation_queue";
    }
}