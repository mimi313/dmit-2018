using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookSystem.ViewModels
{
	public class TrackAlbums
	{
		public string Title { get; set; }
		public string Artist { get; set; }
		public List<TrackDetails> Songs { get; set; }
	}
}
