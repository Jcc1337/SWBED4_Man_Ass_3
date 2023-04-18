using System.Collections;

namespace SW4BED_Man_Ass_3.Models
{
	public class Guest : IEnumerable
	{
		public int GuestID { get; set; }
		public string? guestName { get; set; }
		public int age { get; set; }
		public int roomNumber { get; set; }
		public bool hasReserved { get; set; }
		public  bool hasCheckedIn { get; set; }

	}
}
