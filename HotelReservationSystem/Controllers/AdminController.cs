using HotelReservationSystem.Models;
using HotelReservationSystem.Models.Data;
using HotelReservationSystem.ViewModel.PageContent;
using HotelReservationSystem.ViewModel.testimonials;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _environment;

        public AdminController(ModelContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult GetAllReservation()
        {
            var model = _context.reservations.Include(x => x.user).Include(x => x.room).ThenInclude(x => x.hotel).ToList();
            return View(model);
        }
        public IActionResult Accept(int id)//reservation id
        {
            var OldReservation = _context.reservations.FirstOrDefault(x => x.Id == id);
            //user in room Resident
            var resident = new Resident()
            {
                CheckIn = OldReservation.CheckIn,
                CheckOut = OldReservation.CheckOut,
                RoomId = OldReservation.RoomId,
                UserID = OldReservation.UserID
            };
            //remove old reservation
            //add resident
            //save
            _context.Remove(OldReservation);
            _context.Add(resident);
            _context.SaveChanges();

            return RedirectToAction("GetAllReservation");
        }
        public IActionResult Refuse(int id)
        {
            //get reser 
            var reservation = _context.reservations.FirstOrDefault(x => x.Id == id);

            //user Transaction
            var userTRansaction = _context.userTransactions.FirstOrDefault(x => x.UserID == reservation.UserID && x.RoomId == reservation.RoomId);
            //total price
            var user = _context.users.FirstOrDefault(x => x.Id == reservation.UserID);
            user.Balance += userTRansaction.Price;

            //remove reser
            //update user
            //save changes
            _context.Remove(userTRansaction);
            _context.Remove(reservation);
            _context.Update(user);
            _context.SaveChanges();
            return RedirectToAction("GetAllReservation");
        }

        public IActionResult GetAllResidents()
        {
            var Residents = _context.residents.Include(x => x.user).Include(x => x.room).ThenInclude(x => x.hotel).ToList();
            return View(Residents);
        }


        public IActionResult AddTestimonial(AddTestimonialVM testimonialsVM)
        {
            //text , roomId , User?
            var user = GetUser();
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var test = new Testimonial()
            {
                CreatedAt = DateTime.Now,
                roomId = testimonialsVM.RoomId,
                text = testimonialsVM.Text,
                userId = user.Id
            };
            _context.Add(test);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult GetAllTestimonial()
        {
            var tests = _context.testimonials.Include(x => x.user).Include(x => x.room).ThenInclude(x => x.hotel).Where(x => x.Status == false).ToList();
            return View(tests);
        }
        public IActionResult AcceptTest(int id)
        {
            //residents?
            var testie = _context.testimonials.FirstOrDefault(x => x.Id == id);
            testie.Status = true;
            _context.Update(testie);
            _context.SaveChanges();
            return RedirectToAction("GetAllTestimonial");
        }

        public IActionResult RefuseTest(int id)
        {
            var testie = _context.testimonials.FirstOrDefault(x => x.Id == id);
            _context.Remove(testie);
            _context.SaveChanges();
            return RedirectToAction("GetAllTestimonial");
        }

        public IActionResult ContactUs(ContactUsVM contactUs)
        {
            var model = new ContactUs()
            {
                CreatedAt = DateTime.Now,
                Email = contactUs.Email,
                Name = contactUs.Name,
                PhoneNumber = contactUs.phone,
                Text = contactUs.text
            };
            //Save in DB
            _context.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult GetContactUs()
        {
            var CU = _context.contactUs.ToList();
            return View(CU);
        }
        [HttpGet]
        public IActionResult PageContent()
        {
            var model = _context.pageContent.FirstOrDefault();

            return View(model);
        }
        [HttpPost]
        public IActionResult PageContent(PageContent pageContent)
        {
            var dbContentPage = _context.pageContent.FirstOrDefault();
            dbContentPage.Text = pageContent.Text;
            dbContentPage.Title = pageContent.Title;
            dbContentPage.AboutUsTitle = pageContent.AboutUsTitle;
            dbContentPage.AboutUs = pageContent.AboutUs;
            //image
            if (pageContent.AboutUsImageFile != null)
            {
                dbContentPage.AboutUsImagePath = SaveImage(pageContent.AboutUsImageFile);
            }
            _context.Update(dbContentPage);
            _context.SaveChanges();
            return View(dbContentPage);
        }

        public IActionResult GetUsers()
        {
            var users = _context.users.Where(x => x.Role == "user").ToList();
            return View(users);

        }

        public IActionResult index(DateTime month)
        {

            #region Charts
            //room - date
            var roomCounts = _context.room
                           .GroupBy(r => r.CreatedAt.Date)
                           .Select(g => new RoomCreationData
                           {
                               Date = g.Key,
                               Count = g.Count()
                           })
                           .ToList();

            // hotel - users
            var hotelUserCounts = _context.room
            .SelectMany(r => r.Residents, (r, u) => new { r.hotel, r.CreatedAt, u.Id })
            .GroupBy(x => new { x.hotel.Id, x.hotel.Name, x.CreatedAt.Date })
            .Select(g => new HotelUserData
            {
                HotelName = g.Key.Name,
                Date = g.Key.Date,
                UserCount = g.Count()
            })
            .ToList();
            #endregion

            #region Tables
            var data = _context.residents.Include(x => x.room)
                .Where(x => x.CheckIn.Month == month.Month &&  x.CheckIn.Year == month.Year)
                .GroupBy(b => new { b.room.hotel.Id, b.room.hotel.Name })
                .AsEnumerable() // Switch to client-side evaluation from this point onward
               .Select(g => new ReportDate
               {
                   HotelName = g.Key.Name,
                   BenefitCount = g.Sum(b => b.room.PriceByNight * (b.CheckOut - b.CheckIn).Days)
               })
                .ToList();

            //var data = _context.residents.Include(x => x.room)
            //    .Where(b => b.CheckIn.Month == month.Month && b.CheckIn.Year == month.Year)
            //    .GroupBy(b => new { b.room.hotel.Id, b.room.hotel.Name })
            //    .AsEnumerable() // Switch to client-side evaluation from this point onward
            //   .Select(g => new ReportDate
            //   {
            //       HotelName = g.Key.Name,
            //       BenefitCount = g.Sum(b => b.room.PriceByNight * (b.CheckOut - b.CheckIn).Days)
            //   })
            //    .ToList();

            //var data2 = _context.residents.Include(x => x.room)
            //   .Where(b => b.CheckIn.Year == year.Year && b.CheckIn.Year == year.Year)
            //   .GroupBy(b => new { b.room.hotel.Id, b.room.hotel.Name })
            //   .AsEnumerable() // Switch to client-side evaluation from this point onward
            //  .Select(g => new ReportDate
            //  {
            //      HotelName = g.Key.Name,
            //      BenefitCount = g.Sum(b => b.room.PriceByNight * (b.CheckOut - b.CheckIn).Days)
            //  })
            //   .ToList();


            #endregion


            var model = new DashboardVM()
            {
                roomCreations = roomCounts,
                hotelUser = hotelUserCounts,
                reportDateMonth = data,
            };

            return View(model);
        }






        private User GetUser()
        {
            var userId = HttpContext.Session.GetString("userId");
            if (userId == null)
            {
                return null;
            }
            var user = _context.users.FirstOrDefault(x => x.Id == int.Parse(userId));
            if (user == null)
            {
                return null;
            }
            return user;
        }

        private string SaveImage(IFormFile file)
        {
            if (file == null)
            {
                return string.Empty;
            }
            string RootPath = _environment.WebRootPath;//== ~
            if (file != null)
            {
                string filename = Guid.NewGuid().ToString();
                var Upload = Path.Combine(RootPath, @"Images");
                var ext = Path.GetExtension(file.FileName);

                using (var filestream = new FileStream(Path.Combine(Upload, filename + ext), FileMode.Create))
                {
                    file.CopyTo(filestream);
                }
                return @"Images\" + filename + ext;
            }
            return "";
        }

    }



    public class RoomCreationData
    {
        public DateTime Date { get; set; }
        public int Count { get; set; }
    }
    public class BenefitData
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }

    public class ReportDate
    {
        public string HotelName { get; set; }
        public int BenefitCount { get; set; }
    }
}
