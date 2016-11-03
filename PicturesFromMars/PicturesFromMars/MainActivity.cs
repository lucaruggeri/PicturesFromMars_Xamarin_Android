using Android.App;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using System;

using System.Linq;

using PicturesFromMars.NasaMarsRoverPhotos;
using PicturesFromMars.Model;
using Newtonsoft.Json;
using Android.Graphics;
using System.Net;
using System.Collections.Generic;

namespace PicturesFromMars
{
    [Activity(Label = "PicturesFromMars", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        public MarsData.MarsData marsData { get; set; }
        private string DEBUGMyKey = "0YQjBhJhYphOEBvQj0zhVazadkWRw8tqt2ezn8m4";

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            await InitializeData();
            SetGraphics();

            await DEBUG_LoadRandomPicture();
        }

        private async Task DEBUG2_LoadRandomPicture()
        {
            await DEBUG_LoadRandomPicture();
        }

        private Bitmap GetBitmapFromUrl(string url)
        {
            Bitmap imgBitmap = null;

            using (var webClient = new WebClient())
            {
                var imgBytes = webClient.DownloadData(url);
                if (imgBytes != null && imgBytes.Length > 0)
                {
                    imgBitmap = BitmapFactory.DecodeByteArray(imgBytes, 0, imgBytes.Length);
                }
            }

            return imgBitmap;
        }

        private string GetRandomCamera(string rover)
        {
            try
            {
                List<MarsData.Camera> cameras = marsData.GetCameras(rover);

                if (cameras != null)
                {
                    if (cameras.Count() > 0)
                    {
                        Random random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
                        int randomNum = random.Next(0, cameras.Count() + 1);
                        MarsData.Camera randomCamera = cameras[randomNum];

                        if (randomCamera != null)
                        {
                            string toast = randomCamera.ShortName;
                            return randomCamera.ShortName;
                        }

                        Toast.MakeText(this, "(camera not found)", ToastLength.Long).Show();
                        return "FHAZ";
                    }
                }
                Toast.MakeText(this, "(camera not found)", ToastLength.Long).Show();
                return "FHAZ";
            }
            catch
            {
                Toast.MakeText(this, "(camera not found)", ToastLength.Long).Show();
                return "FHAZ";
            }
        }

        private string GetRandomRover()
        {
            Random random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            int randomRover = random.Next(0, 2 + 1);

            if (randomRover == 0)
            {
                return "Curiosity";
            }
            if (randomRover == 1)
            {
                return "Spirit";
            }
            if (randomRover == 2)
            {
                return "Opportunity";
            }
            return "Curiosity";
        }

        private async Task DEBUG_LoadRandomPicture()
        {
            //random sol
            Random random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            int randomSol = random.Next(1, marsData.Curiosity_MaxSol  + 1);

            //random rover
            string randomRover = GetRandomRover();

            //random camera
            string randomCamera = GetRandomCamera(randomRover);


            //DEBUG
            //try to load picture

            //get response
            string pictureResponse = await NasaMarsRoverPhotos.NasaMarsRoverPhotos.SendPictureRequest(randomSol, randomCamera, randomRover, DEBUGMyKey);

            //deserialize response
            if (pictureResponse != null)
            {
                Rootobject g = new Rootobject();
                g = JsonConvert.DeserializeObject<Rootobject>(pictureResponse.ToString());

                //use response
                if (g != null)
                {
                    if (g.photos.Count() > 0)
                    {
                        //decode image
                        var myImageBitmap = GetBitmapFromUrl(g.photos[0].img_src);

                        //load picture
                        ImageView demoImageView = FindViewById<ImageView>(Resource.Id.demoImageView);
                        demoImageView.SetImageBitmap(myImageBitmap);

                        //update date
                        //TODO calcolare data anche senza oggetto foto
                        TextView txtSolInfo = FindViewById<TextView>(Resource.Id.txtSolInfo);
                        txtSolInfo.Text = "Sol " + randomSol.ToString() + ", Earth date " + g.photos[0].earth_date;

                        SeekBar seekBar1 = FindViewById<SeekBar>(Resource.Id.seekBar1);
                        seekBar1.Progress = randomSol;
                    }
                    else
                    {
                        string toast = "No picture found." + " SOL " + randomSol.ToString();
                        Toast.MakeText(this, toast, ToastLength.Long).Show();
                    }
                }
                else
                {
                    string toast = "No picture found." + " SOL " + randomSol.ToString();
                    Toast.MakeText(this, toast, ToastLength.Long).Show();
                }
            }
        }

        private void SetGraphics()
        {
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner);
            spinner.ItemSelected += new System.EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.cameras_array, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            Spinner picturesSpinner = FindViewById<Spinner>(Resource.Id.picturesSpinner);
            picturesSpinner.ItemSelected += new System.EventHandler<AdapterView.ItemSelectedEventArgs>(picturesSpinner_ItemSelected);
            var picturesSpinnerAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.cameras_array, Android.Resource.Layout.SimpleSpinnerItem);
            picturesSpinnerAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            picturesSpinner.Adapter = picturesSpinnerAdapter;

            SeekBar seekBar1 = FindViewById<SeekBar>(Resource.Id.seekBar1);
            //TODO
            //seekBar1.ProgressChanged += new System.EventHandler<SeekBar.ProgressChangedEventArgs>(seekBar1_ProgressChanged);
            seekBar1.Max = marsData.Curiosity_MaxSol;

            Button btnPrev = FindViewById<Button>(Resource.Id.btnPrev);
            btnPrev.Click += delegate { DEBUG2_LoadRandomPicture(); };

            Button btnNext = FindViewById<Button>(Resource.Id.btnNext);
            btnNext.Click += delegate { DEBUG2_LoadRandomPicture(); };
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            //string toast = string.Format("The camera is {0}", spinner.GetItemAtPosition(e.Position));
            //Toast.MakeText(this, toast, ToastLength.Long).Show();
        }

        private void picturesSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
        }

        private void seekBar1_ProgressChanged(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            //string toast = string.Format("The camera is {0}", spinner.GetItemAtPosition(e.Position));
            //Toast.MakeText(this, toast, ToastLength.Long).Show();
        }

        private async Task InitializeData()
        {
            //TODO 
            //if (MP.Utilities.AppConfiguration.AppConfiguration.ReadKey() != string.Empty)
            //{
            //    marsData = new MarsData.MarsData();
            //    marsData.Curiosity_MaxSol = await NasaMarsRoverPhotos.NasaMarsRoverPhotos.GetRoverMaxSol("curiosity", MP.Utilities.AppConfiguration.AppConfiguration.ReadKey());
            //    marsData.Spirit_MaxSol = await NasaMarsRoverPhotos.NasaMarsRoverPhotos.GetRoverMaxSol("spirit", MP.Utilities.AppConfiguration.AppConfiguration.ReadKey());
            //    marsData.Opportunity_MaxSol = await NasaMarsRoverPhotos.NasaMarsRoverPhotos.GetRoverMaxSol("opportunity", MP.Utilities.AppConfiguration.AppConfiguration.ReadKey());
            //}

            marsData = new MarsData.MarsData();
            marsData.Curiosity_MaxSol = await NasaMarsRoverPhotos.NasaMarsRoverPhotos.GetRoverMaxSol("curiosity", DEBUGMyKey);
            marsData.Spirit_MaxSol = await NasaMarsRoverPhotos.NasaMarsRoverPhotos.GetRoverMaxSol("spirit", DEBUGMyKey);
            marsData.Opportunity_MaxSol = await NasaMarsRoverPhotos.NasaMarsRoverPhotos.GetRoverMaxSol("opportunity", DEBUGMyKey);
        }
    }
}

