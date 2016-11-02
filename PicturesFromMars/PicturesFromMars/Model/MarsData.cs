using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicturesFromMars.MarsData
{
    public class MarsData
    {

        public MarsData()
        {
            LoadCameras();
        }

        private List<Camera> cameraList = new List<Camera>();

        private int curiosityMaxSol;
        public int Curiosity_MaxSol
        {
            get
            {
                if (this.curiosityMaxSol <= 0)
                {
                    return 1;
                }
                else
                {
                    return curiosityMaxSol;
                }
            }
            set
            {
                this.curiosityMaxSol = value;
            }
        }

        private int spiritMaxSol;
        public int Spirit_MaxSol
        {
            get
            {
                if (this.spiritMaxSol <= 0)
                {
                    return 1;
                }
                else
                {
                    return spiritMaxSol;
                }
            }
            set
            {
                this.spiritMaxSol = value;
            }
        }

        private int opportunityMaxSol;
        public int Opportunity_MaxSol
        {
            get
            {
                if (this.opportunityMaxSol <= 0)
                {
                    return 1;
                }
                else
                {
                    return opportunityMaxSol;
                }
            }
            set
            {
                this.opportunityMaxSol = value;
            }
        }

        public List<Camera> GetCameras(string rover)
        {
            if (rover == null)
            {
                return cameraList;
            }
            {
                return cameraList.Where(x => x.Rovers.Any(y => y.Name == rover)).ToList();
            }
        }

        public void LoadCameras()
        {
            Camera tempCamera;

            tempCamera = new Camera();
            tempCamera.Name = "Front Hazard Avoidance Camera";
            tempCamera.ShortName = "FHAZ";
            tempCamera.Rovers = new List<Rover>();
            tempCamera.Rovers.Add(new Rover { Name = "Curiosity" });
            tempCamera.Rovers.Add(new Rover { Name = "Opportunity" });
            tempCamera.Rovers.Add(new Rover { Name = "Spirit" });
            tempCamera.Description = "Hazcams (short for hazard avoidance cameras) are photographic cameras mounted on the front and rear of NASA's Spirit, Opportunity and Curiosity rover missions to Mars and on the lower front portion of Chinese Yutu rover mission to the Moon.";
            cameraList.Add(tempCamera);

            tempCamera = new Camera();
            tempCamera.Name = "Rear Hazard Avoidance Camera";
            tempCamera.ShortName = "RHAZ";
            tempCamera.Rovers = new List<Rover>();
            tempCamera.Rovers.Add(new Rover { Name = "Curiosity" });
            tempCamera.Rovers.Add(new Rover { Name = "Opportunity" });
            tempCamera.Rovers.Add(new Rover { Name = "Spirit" });
            tempCamera.Description = "Hazcams (short for hazard avoidance cameras) are photographic cameras mounted on the front and rear of NASA's Spirit, Opportunity and Curiosity rover missions to Mars and on the lower front portion of Chinese Yutu rover mission to the Moon.";
            cameraList.Add(tempCamera);

            tempCamera = new Camera();
            tempCamera.Name = "Mast Camera";
            tempCamera.ShortName = "MAST";
            tempCamera.Rovers = new List<Rover>();
            tempCamera.Rovers.Add(new Rover { Name = "Curiosity" });
            tempCamera.Description = "The MastCam system provides multiple spectra and true-color imaging with two cameras. The cameras can take true-color images at 1600×1200 pixels and up to 10 frames per second hardware-compressed video at 720p (1280×720).";
            cameraList.Add(tempCamera);

            tempCamera = new Camera();
            tempCamera.Name = "Chemistry and Camera Complex";
            tempCamera.ShortName = "CHEMCAM";
            tempCamera.Rovers = new List<Rover>();
            tempCamera.Rovers.Add(new Rover { Name = "Curiosity" });
            tempCamera.Description = "ChemCam is a suite of remote sensing instruments, and as the name implies, ChemCam is actually two different instruments combined as one: a laser-induced breakdown spectroscopy (LIBS) and a Remote Micro Imager (RMI) telescope.";
            cameraList.Add(tempCamera);

            tempCamera = new Camera();
            tempCamera.Name = "Mars Hand Lens Imager";
            tempCamera.ShortName = "MAHLI";
            tempCamera.Rovers = new List<Rover>();
            tempCamera.Rovers.Add(new Rover { Name = "Curiosity" });
            tempCamera.Description = "MAHLI is a camera on the rover's robotic arm, and acquires microscopic images of rock and soil. MAHLI can take true-color images at 1600×1200 pixels with a resolution as high as 14.5 micrometers per pixel.";
            cameraList.Add(tempCamera);

            tempCamera = new Camera();
            tempCamera.Name = "Mars Descent Imager";
            tempCamera.ShortName = "MARDI";
            tempCamera.Rovers = new List<Rover>();
            tempCamera.Rovers.Add(new Rover { Name = "Curiosity" });
            tempCamera.Description = "During the descent to the Martian surface, MARDI took color images at 1600×1200 pixels with a 1.3-millisecond exposure time starting at distances of about 3.7 km (2.3 mi) to near 5 m (16 ft) from the ground, at a rate of four frames per second for about two minutes.";
            cameraList.Add(tempCamera);

            tempCamera = new Camera();
            tempCamera.Name = "Navigation Camera";
            tempCamera.ShortName = "NAVCAM";
            tempCamera.Rovers = new List<Rover>();
            tempCamera.Rovers.Add(new Rover { Name = "Curiosity" });
            tempCamera.Rovers.Add(new Rover { Name = "Opportunity" });
            tempCamera.Rovers.Add(new Rover { Name = "Spirit" });
            tempCamera.Description = "The rover has two pairs of black and white navigation cameras mounted on the mast to support ground navigation.[78][79] The cameras have a 45° angle of view and use visible light to capture stereoscopic 3-D imagery.";
            cameraList.Add(tempCamera);

            tempCamera = new Camera();
            tempCamera.Name = "Panoramic Camera";
            tempCamera.ShortName = "PANCAM";
            tempCamera.Rovers = new List<Rover>();
            tempCamera.Rovers.Add(new Rover { Name = "Opportunity" });
            tempCamera.Rovers.Add(new Rover { Name = "Spirit" });
            tempCamera.Description = "Pancam is a high-resolution color stereo pair of CCD cameras used to image the surface and sky of Mars. The cameras are located on a camera bar that sits on top of the mast of the rover.";
            cameraList.Add(tempCamera);

            tempCamera = new Camera();
            tempCamera.Name = "Miniature Thermal Emission Spectrometer (Mini-TES)";
            tempCamera.ShortName = "MINITES ";
            tempCamera.Rovers = new List<Rover>();
            tempCamera.Rovers.Add(new Rover { Name = "Opportunity" });
            tempCamera.Rovers.Add(new Rover { Name = "Spirit" });
            tempCamera.Description = "The Miniature Thermal Emission Spectrometer (Mini-TES) is an infrared spectrometer used for detecting the composition of a material (typically rocks) from a distance.";
            cameraList.Add(tempCamera);
        }
    }

    public class Camera
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public List<Rover> Rovers { get; set; }
        public string Description { get; set; }
    }

    public class Rover
    {
        public string Name { get; set; }
    }

}
