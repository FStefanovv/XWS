﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flights.Model
{
    public class Flight
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string DeparturePoint { get; set; }
        public string ArrivalPoint { get; set; }
        public DateTime DepartureTime { get; set; }
        public int Duration { get; set; }
        public float TicketPrice { get; set; }
        public int NumberOfPassengers { get; set; }
        public int RemainingTickets { get; set; }


        public Flight(string departurePoint, string arrivalPoint, DateTime departureTime, 
                        int duraiton, float ticketPrice, int numOfPassengers)
        {
            DeparturePoint = departurePoint;
            ArrivalPoint = arrivalPoint;
            DepartureTime = departureTime;
            Duration = duraiton;
            TicketPrice = ticketPrice;
            NumberOfPassengers = numOfPassengers;
            RemainingTickets = NumberOfPassengers;
        }
    }
}
