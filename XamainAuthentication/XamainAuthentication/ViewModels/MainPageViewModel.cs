using Prism.Commands;
using Prism.Mvvm;
using System;
using XamainAuthentication.Models;
using XamainAuthentication.Services;

namespace XamainAuthentication.ViewModels
{
	public class MainPageViewModel : BindableBase
	{
		private string _title;
		public string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}

		public DelegateCommand AuthGoogleCommand { get; set; }
		public DelegateCommand AuthFacebookCommand { get; set; }
		public DelegateCommand AuthTwitterCommand { get; set; }
		public DelegateCommand AuthInstagramCommand { get; set; }

		public readonly AuthService _authService;
		
		public MainPageViewModel()
		{
			_authService = new AuthService();

			AuthGoogleCommand = new DelegateCommand(AuthGoogle);
			AuthFacebookCommand = new DelegateCommand(AuthFacebook);
			AuthTwitterCommand = new DelegateCommand(AuthTwitter);
			AuthInstagramCommand = new DelegateCommand(AuthInstagram);
		}

		private void AuthGoogle()
		{
			_authService.Authenticate(LoginTypeEnum.Google);
		}

		private void AuthFacebook()
		{
			_authService.Authenticate(LoginTypeEnum.Facebook);
		}

		private void AuthTwitter()
		{
			_authService.Authenticate(LoginTypeEnum.Twitter);
		}

		private void AuthInstagram()
		{
			_authService.Authenticate(LoginTypeEnum.Instagram);
		}
	}
}
