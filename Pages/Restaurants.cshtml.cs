using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class RestaurantModel : PageModel
    {
        [BindProperty]
        public int Zip { get; set; }

        [BindProperty]
        public string Sted { get; set; }


        public IActionResult OnPost()
        {
            ViewData["ZipView"] = this.Zip;

            if (Zip >= 0001 && Zip <= 1295)
            {
                this.Sted = "Oslo";
                ViewData["Sted"] = this.Sted;
            }
            if (Zip >= 1501 && Zip <= 1539)
            {
                this.Sted = "Moss";
                ViewData["Sted"] = this.Sted;
            }
            if (Zip >= 1751 && Zip <= 1788)
            {
                this.Sted = "Halden";
                ViewData["Sted"] = this.Sted;
            }
            if (Zip >= 2601 && Zip <= 2629)
            {
                this.Sted = "Lillehammer";
                ViewData["Sted"] = this.Sted;
            }
            if (Zip >= 3001 && Zip <= 3048)
            {
                this.Sted = "Drammen";
                ViewData["Sted"] = this.Sted;
            }
            if (Zip >= 3201 && Zip <= 3249)
            {
                this.Sted = "Sandefjord";
                ViewData["Sted"] = this.Sted;
            }
            if (Zip >= 3701 && Zip <= 3747)
            {
                this.Sted = "Skien";
                ViewData["Sted"] = this.Sted;
            }
            if (Zip >= 3901 && Zip <= 3949)
            {
                this.Sted = "Porsgrunn";
                ViewData["Sted"] = this.Sted;
            }
            if (Zip >= 4001 && Zip <= 4099)
            {
                this.Sted = "Stavanger";
                ViewData["Sted"] = this.Sted;
            }
            if (Zip >= 4604 && Zip <= 4698)
            {
                this.Sted = "Kristiansand";
                ViewData["Sted"] = this.Sted;
            }
            if (Zip >= 5003 && Zip <= 5899)
            {
                this.Sted = "Bergen";
                ViewData["Sted"] = this.Sted;
            }
            if (Zip >= 6001 && Zip <= 6048)
            {
                this.Sted = "Ålesund";
                ViewData["Sted"] = this.Sted;
            }
            if (Zip >= 6401 && Zip <= 6436)
            {
                this.Sted = "Molde";
                ViewData["Sted"] = this.Sted;
            }
            if (Zip >= 7400 && Zip <= 7498)
            {
                this.Sted = "Trondheim";
                ViewData["Sted"] = this.Sted;
            }
            if (Zip >= 8001 && Zip <= 8092)
            {
                this.Sted = "Bodø";
                ViewData["Sted"] = this.Sted;
            }
            if (Zip >= 9240 && Zip <= 9299)
            {
                this.Sted = "Tromsø";
                ViewData["Sted"] = this.Sted;
            }

            return Page();
        }
    }
}