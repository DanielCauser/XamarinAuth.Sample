using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.PM;

namespace XamainAuthentication.Droid
{
	[Activity(Label = "CustomUrlSchemeInterceptorActivity", NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
	[
			IntentFilter
			(
				actions: new[] { Intent.ActionView },
				Categories = new[]
						{
						Intent.CategoryDefault,
						Intent.CategoryBrowsable
						},
				DataSchemes = new[]
						{
						"com.xamarin.traditional.standard.samples.oauth.providers.android",
						"com.googleusercontent.apps.1093596514437-d3rpjj7clslhdg3uv365qpodsl5tq4fn",
						"fb1982655385305897",
						"xamarin-auth",
					/*
					"urn:ietf:wg:oauth:2.0:oob",
					"urn:ietf:wg:oauth:2.0:oob.auto",
					"http://localhost:PORT",
					"https://localhost:PORT",
					"http://127.0.0.1:PORT",
					"https://127.0.0.1:PORT",              
					"http://[::1]:PORT", 
					"https://[::1]:PORT", 
					*/
						},
				//DataHost = "localhost",
				DataHosts = new[]
						{
						"localhost",
						"authorize",                // Facebook in fb1889013594699403://authorize 
				},
				DataPaths = new[]
						{
						"/",                        // Facebook
						"/oauth2redirect",          // Google
				},
				AutoVerify = true
			)
		]
	public class CustomUrlSchemeInterceptorActivity : Activity
	{
		string message;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			global::Android.Net.Uri uri_android = Intent.Data;

#if DEBUG
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("ActivityCustomUrlSchemeInterceptor.OnCreate()");
            sb.Append("     uri_android = ").AppendLine(uri_android.ToString());
            System.Diagnostics.Debug.WriteLine(sb.ToString());
#endif

			// Convert iOS NSUrl to C#/netxf/BCL System.Uri - common API
			Uri uri_netfx = new Uri(uri_android.ToString());

			// load redirect_url Page
			XamainAuthentication.Services.AuthService.Authenticator.OnPageLoading(uri_netfx);

			this.Finish();

			return;
		}
	}
}
