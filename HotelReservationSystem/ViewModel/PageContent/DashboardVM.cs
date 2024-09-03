using HotelReservationSystem.Controllers;

namespace HotelReservationSystem.ViewModel.PageContent
{
    public class DashboardVM
    {
        public List<RoomCreationData> roomCreations { get; set; }
        public List<HotelUserData> hotelUser { get; set; }

        public List<ReportDate> reportDateMonth { get; set; }
        public List<ReportDate> reportDateYear { get; set; }
    }
    public class HotelUserData
    {
        public string HotelName { get; set; }
        public DateTime Date { get; set; }
        public int UserCount { get; set; }
    }
}
