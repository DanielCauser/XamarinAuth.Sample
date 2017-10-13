using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamainAuthentication.Models;

namespace XamainAuthentication.Services
{
	public interface IAuthService
	{
		void Authenticate(LoginTypeEnum type);
	}
}
