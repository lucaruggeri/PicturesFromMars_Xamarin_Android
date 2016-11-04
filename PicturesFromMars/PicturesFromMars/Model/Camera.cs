using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicturesFromMars.Model
{
    public class Camera
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public List<Rover> Rovers { get; set; }
        public string Description { get; set; }
    }

}
