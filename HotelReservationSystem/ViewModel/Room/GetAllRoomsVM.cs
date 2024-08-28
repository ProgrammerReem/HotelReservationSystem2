﻿namespace HotelReservationSystem.ViewModel.Room
{
    public class GetAllRoomsVM
    {
        public List<Models.Room> Rooms { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string? Search { get; set; }
    }
}
