using System;
using System.Diagnostics;
using System.Json;
using System.Linq;
using XamainAuthentication.Models;
using XamainAuthentication.Services;
using Xamarin.Auth;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(AuthService))]
namespace XamainAuthentication.Services
{
	public class AuthService //: IAuthService
	{
		public static OAuth2Authenticator Authenticator { get; private set; }
		
		public void Authenticate(LoginTypeEnum type)
		{
			var account = AccountStore.Create().FindAccountsForService(Constants.AppName).FirstOrDefault();
			if (account != null)
				return;

			switch (type)
			{
				case LoginTypeEnum.Facebook:
					FacebookAuth();
					break;
				case LoginTypeEnum.Twitter:
					TwitterAuth();
					break;
				case LoginTypeEnum.Instagram:
					InstagramAuth();
					break;
				case LoginTypeEnum.Google:
					GoogleAuth();
					break;
				default:
					break;
			}

			Authenticator.Completed += Authenticator_Completed;
			Authenticator.Error += Authenticator_Error;

			var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
			presenter.Login(Authenticator);
		}
		private void Authenticator_Completed(object sender, AuthenticatorCompletedEventArgs e)
		{
			if (e.IsAuthenticated)
			{
				var request = new OAuth2Request("GET", new Uri("https://graph.facebook.com/me?fields=name,picture,email"), null, e.Account);
				request.GetResponseAsync().ContinueWith(t =>
				{
					if (t.IsFaulted)
						Debug.WriteLine("Something went wrong!");
					else
					{
						string json = JsonValue.Parse(t.Result.GetResponseText());
						AccountStore.Create().Save(e.Account, Constants.AppName);
					}
				});
			}
		}

		private void Authenticator_Error(object sender, AuthenticatorErrorEventArgs e)
		{
			Debug.WriteLine("Error");
		}

		private void FacebookAuth()
		{
			Authenticator = new OAuth2Authenticator(clientId: "1982655385305897",
																scope: "",
																authorizeUrl: new Uri("https://www.facebook.com/v2.10/dialog/oauth"),
																redirectUrl: new Uri("fb1982655385305897://authorize"),
																isUsingNativeUI: true);
		}

		private void GoogleAuth()
		{
			throw new NotImplementedException();
		}

		private void TwitterAuth()
		{
			throw new NotImplementedException();
		}

		private void InstagramAuth()
		{
			throw new NotImplementedException();
		}
		
	}
}
