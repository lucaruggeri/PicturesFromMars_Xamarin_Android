using Android.App;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;

namespace PicturesFromMars
{
    [Activity(Label = "PicturesFromMars", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        public Data.Data data { get; set; }

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            await InitializeData();
            SetGraphics();
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
            seekBar1.Max = data.Curiosity_MaxSol;
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
            //    data = new Data.Data();
            //    data.Curiosity_MaxSol = await NasaMarsRoverPhotos.NasaMarsRoverPhotos.GetRoverMaxSol("curiosity", MP.Utilities.AppConfiguration.AppConfiguration.ReadKey());
            //    data.Spirit_MaxSol = await NasaMarsRoverPhotos.NasaMarsRoverPhotos.GetRoverMaxSol("spirit", MP.Utilities.AppConfiguration.AppConfiguration.ReadKey());
            //    data.Opportunity_MaxSol = await NasaMarsRoverPhotos.NasaMarsRoverPhotos.GetRoverMaxSol("opportunity", MP.Utilities.AppConfiguration.AppConfiguration.ReadKey());
            //}

            data = new Data.Data();
            data.Curiosity_MaxSol = await NasaMarsRoverPhotos.NasaMarsRoverPhotos.GetRoverMaxSol("curiosity", "0YQjBhJhYphOEBvQj0zhVazadkWRw8tqt2ezn8m4");
            data.Spirit_MaxSol = await NasaMarsRoverPhotos.NasaMarsRoverPhotos.GetRoverMaxSol("spirit", "0YQjBhJhYphOEBvQj0zhVazadkWRw8tqt2ezn8m4");
            data.Opportunity_MaxSol = await NasaMarsRoverPhotos.NasaMarsRoverPhotos.GetRoverMaxSol("opportunity", "0YQjBhJhYphOEBvQj0zhVazadkWRw8tqt2ezn8m4");
        }
    }
}

