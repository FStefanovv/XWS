﻿using Accommodation.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Accommodation.Adapters
{
    public static class CreateAccommodationAdapter
    {
        public static Model.Accommodation CreateAccommodaitonDtoToObject(CreateAccommodationDTO dto, string hostId)
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("sr-Cyrl-Rs");
            var accommodation = new Model.Accommodation()
            {
                Name = dto.Name,
                Address = dto.Address,
                Offers = dto.Offers,
                MaxGuests = dto.MaxGuests,
                MinGuests = dto.MinGuests,
                HostId = hostId,
                AutoApprove = dto.AutoApprove,
                StartSeasonDate = DateTime.ParseExact(dto.StartSeason, "yyyy-MM-dd", culture),
                EndSeasonDate = DateTime.ParseExact(dto.EndSeason, "yyyy-MM-dd", culture),
                AccomodationPrice = new Model.Price()
                {
                    FinalPrice = dto.Price,
                    PricePerAccomodation = dto.PricePerAccomodation,
                    PricePerGuest = dto.PricePerGuest,
                    HolidayCost=dto.HolidayCost,
                    WeekendCost=dto.WeekendCost,
                    SummerCost=dto.SummerCost,
                }
            };

            return accommodation;
        }

        public static AccommodationDTO ObjectToAccommodationDTO(Model.Accommodation accommodation)
        {
            var accommodationDTO = new AccommodationDTO()
            {
                Name = accommodation.Name,
                StartSeason = accommodation.StartSeasonDate.ToString("yyyy-MM-dd"),
                EndSeason = accommodation.EndSeasonDate.ToString("yyyy-MM-dd"),
                Price = accommodation.AccomodationPrice.FinalPrice,
                PricePerGuest = accommodation.AccomodationPrice.PricePerGuest,
                PricePerAccomodation = accommodation.AccomodationPrice.PricePerAccomodation,
                HolidayCost = accommodation.AccomodationPrice.HolidayCost,
                WeekendCost = accommodation.AccomodationPrice.WeekendCost,
                SummerCost = accommodation.AccomodationPrice.SummerCost

            };

            return accommodationDTO;
        }
    }
}
