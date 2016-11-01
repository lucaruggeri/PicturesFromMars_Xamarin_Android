using Android.App;
using Android.Widget;
using Android.OS;

namespace PicturesFromMars
{
    [Activity(Label = "PicturesFromMars", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

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
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("The camera is {0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }

        private void picturesSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
        }
    }
}

