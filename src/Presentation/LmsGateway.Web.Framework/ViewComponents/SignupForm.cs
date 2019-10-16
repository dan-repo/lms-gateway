using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LmsGateway.Core.Models;

namespace LmsGateway.Web.Framework.ViewComponents
{
    public class SignupForm : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            UserModel user = new UserModel();
            return await Task.FromResult(View("/Views/Home/Components/_SignupForm.cshtml", user));

            //return await Task.FromResult(View("/Views/Home/Components/HomePage/_Gallery.cshtml", user));
        }


    }
}
