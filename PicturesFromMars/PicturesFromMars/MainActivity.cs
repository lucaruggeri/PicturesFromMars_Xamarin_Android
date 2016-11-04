using Android.App;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using System;

using System.Linq;

using PicturesFromMars.NasaMarsRoverPhotos;
using PicturesFromMars.Model;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PicturesFromMars
{
    [Activity(Label = "PicturesFromMars", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        MarsData marsData { get; set; }
        int selectedSol { get; set; }
        string selectedRover { get; set; }
        string selectedCamera { get; set; }
        string DEBUGMyKey = "0YQjBhJhYphOEBvQj0zhVazadkWRw8tqt2ezn8m4";
        string selectedEarthDate { get; set; }

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

        private async Task DEBUG_RefreshPicture()
        {
            //get response
            string pictureResponse = await NasaMarsRoverPhotos.NasaMarsRoverPhotos.SendPictureRequest(selectedSol, selectedCamera, selectedRover, DEBUGMyKey);

            //deserialize response
            if (pictureResponse != null)
            {
                NasaRoversAPI g = new NasaRoversAPI();
                g = JsonConvert.DeserializeObject<NasaRoversAPI>(pictureResponse.ToString());

                //use response
                if (g != null)
                {
                    if (g.photos.Count() > 0)
                    {
                        //decode image
                        var myImageBitmap = Utilities.Xamarin.Pictures.GetBitmapFromUrl(g.photos[0].img_src);

                        //load picture
                        ImageView demoImageView = FindViewById<ImageView>(Resource.Id.demoImageView);
                        demoImageView.SetImageBitmap(myImageBitmap);

                        selectedEarthDate = g.photos[0].earth_date;
                    }
                    else
                    {
                        //load default picture
                        ImageView demoImageView = FindViewById<ImageView>(Resource.Id.demoImageView);
                        demoImageView.SetImageResource(Resource.Drawable.imageNotFound);

                        string text = "No picture found." + " SOL " + selectedSol.ToString();
                        Toast toast = Toast.MakeText(this, text, ToastLength.Long);
                        toast.SetGravity(Android.Views.GravityFlags.Center, 50, 50);
                        toast.Show();

                        selectedEarthDate = "(to calculate)";
                    }
                }
                else
                {
                    //load default picture
                    ImageView demoImageView = FindViewById<ImageView>(Resource.Id.demoImageView);
                    demoImageView.SetImageResource(Resource.Drawable.imageNotFound);

                    string text = "No picture found." + " SOL " + selectedSol.ToString();
                    Toast toast = Toast.MakeText(this, text, ToastLength.Long);
                    toast.SetGravity(Android.Views.GravityFlags.Center, 50, 50);
                    toast.Show();

                    selectedEarthDate = "(to calculate)";
                }
            }
        }

        private void RefreshControls()
        {
            RadioButton radCuriosity = FindViewById<RadioButton>(Resource.Id.radCuriosity);
            RadioButton radSpirit = FindViewById<RadioButton>(Resource.Id.radSpirit);
            RadioButton radOpportunity = FindViewById<RadioButton>(Resource.Id.radOpportunity);
            if (selectedRover == "Curiosity")
            {
                radCuriosity.Checked = true;
            }
            if (selectedRover == "Spirit")
            {
                radSpirit.Checked = true;
            }
            if (selectedRover == "Opportunity")
            {
                radOpportunity.Checked = true;
            }

            //date
            TextView txtSolInfo = FindViewById<TextView>(Resource.Id.txtSolInfo);
            txtSolInfo.Text = "Sol " + selectedSol.ToString() + ", Earth date " + selectedEarthDate;

            //sol
            SeekBar seekBar1 = FindViewById<SeekBar>(Resource.Id.seekBar1);
            seekBar1.Progress = selectedSol;
        }

        private async Task DEBUG_LoadRandomPicture()
        {
            Random random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            selectedSol = random.Next(1, marsData.Curiosity_MaxSol  + 1);
            selectedRover = marsData.GetRandomRover();
            selectedCamera = marsData.GetRandomCamera(selectedRover);

            await DEBUG_RefreshPicture();
            RefreshControls();
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

            Button btnPrevSol = FindViewById<Button>(Resource.Id.btnPrevSol);
            btnPrevSol.Click += delegate { LoadPrevSol(); };

            Button btnNextSol = FindViewById<Button>(Resource.Id.btnNextSol);
            btnNextSol.Click += delegate { LoadNextSol(); };

            Button btnRandom = FindViewById<Button>(Resource.Id.btnRandom);
            btnRandom.Click += delegate { DEBUG_LoadRandomPicture(); };

            RadioButton radCuriosity = FindViewById<RadioButton>(Resource.Id.radCuriosity);
            radCuriosity.Click += delegate { SetSelectedRover(); };

            RadioButton radSpirit = FindViewById<RadioButton>(Resource.Id.radSpirit);
            radSpirit.Click += delegate { SetSelectedRover(); };

            RadioButton radOpportunity = FindViewById<RadioButton>(Resource.Id.radOpportunity);
            radOpportunity.Click += delegate { SetSelectedRover(); };
        }

        private void SetSelectedRover()
        {
            RadioButton radCuriosity = FindViewById<RadioButton>(Resource.Id.radCuriosity);
            RadioButton radSpirit = FindViewById<RadioButton>(Resource.Id.radSpirit);
            RadioButton radOpportunity = FindViewById<RadioButton>(Resource.Id.radOpportunity);

            if (radCuriosity.Checked == true)
            {
                selectedRover = "Curiosity";
            }
            if (radSpirit.Checked == true)
            {
                selectedRover = "Spirit";
            }
            if (radOpportunity.Checked == true)
            {
                selectedRover = "Opportunity";
            }

            DEBUG_RefreshPicture();
            RefreshControls();
        }

        private void LoadPrevSol()
        {
            selectedSol = selectedSol - 1;
            DEBUG_RefreshPicture();
            RefreshControls();
        }

        private void LoadNextSol()
        {
            selectedSol = selectedSol + 1;
            DEBUG_RefreshPicture();
            RefreshControls();
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
        }

        private void picturesSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
        }

        private void seekBar1_ProgressChanged(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
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

            marsData = new MarsData();
            marsData.Curiosity_MaxSol = await NasaMarsRoverPhotos.NasaMarsRoverPhotos.GetRoverMaxSol("curiosity", DEBUGMyKey);
            marsData.Spirit_MaxSol = await NasaMarsRoverPhotos.NasaMarsRoverPhotos.GetRoverMaxSol("spirit", DEBUGMyKey);
            marsData.Opportunity_MaxSol = await NasaMarsRoverPhotos.NasaMarsRoverPhotos.GetRoverMaxSol("opportunity", DEBUGMyKey);
        }
    }
}

