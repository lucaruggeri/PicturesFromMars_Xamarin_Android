using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicturesFromMars.Model
{
    public class NasaRoversAPI
    {
        public PhotoAPI[] photos { get; set; }
    }

    public class PhotoAPI
    {
        public int id { get; set; }
        public int sol { get; set; }
        public CameraAPI camera { get; set; }
        public string img_src { get; set; }
        public string earth_date { get; set; }
        public RoverAPI rover { get; set; }
    }

    public class CameraAPI
    {
        public int id { get; set; }
        public string name { get; set; }
        public int rover_id { get; set; }
        public string full_name { get; set; }
    }

    public class RoverAPI
    {
        public int id { get; set; }
        public string name { get; set; }
        public string landing_date { get; set; }
        public int max_sol { get; set; }
        public string max_date { get; set; }
        public int total_photos { get; set; }
        public Camera1API[] cameras { get; set; }
    }

    public class Camera1API
    {
        public string name { get; set; }
        public string full_name { get; set; }
    }

}
