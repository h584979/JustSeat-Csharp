using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace WebApp.Pages
{

    public class ContactFormModel
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(255)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(1000)]
        public string Message { get; set; }
    }

    public class ContactModel : PageModel
    {
        [BindProperty]
        public ContactFormModel Contact { get; set; }

        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ContactModel> _logger;

        public ContactModel(IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            ILogger<ContactModel> logger)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        private bool RecaptchaPassed(string recaptchaResponse)
        {
            _logger.LogDebug("Contact.RecaptchaPassed entered");

            var secret =
                _configuration.GetSection("RecaptchaKey").Value;

            var endPoint =
                _configuration.GetSection("RecaptchaEndPoint").Value;

            var googleCheckUrl =
                $"{endPoint}?secret={secret}&response={recaptchaResponse}";

            _logger.LogDebug("Checking reCaptcha");
            var httpClient = _httpClientFactory.CreateClient();

            var response = httpClient.GetAsync(googleCheckUrl).Result;

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogDebug($"reCaptcha bad response {response.StatusCode}");
                return false;
            }

            dynamic jsonData =
                JObject.Parse(response.Content.ReadAsStringAsync().Result);

            _logger.LogDebug("reCaptcha returned successfully");

            return (jsonData.success == "true");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogDebug("Contact.OnPostSync entered");

            if (!ModelState.IsValid)
            {
                _logger.LogDebug("Model state not valid");
                return Page();
            }

            var gRecaptchaResponse = Request.Form["g-recaptcha-response"];

            if (string.IsNullOrEmpty(gRecaptchaResponse)
                || !RecaptchaPassed(gRecaptchaResponse))
            {
                _logger.LogDebug("Recaptcha empty or failed");
                ModelState.AddModelError(string.Empty, "You failed the CAPTCHA");
                return Page();
            }

            // Mail header
            var from = new EmailAddress(
                Contact.Email, Contact.Name);
            var to = new EmailAddress(
                _configuration.GetSection("ContactUsMailbox").Value);
            const string subject = "Website Contact Us Message";

            // Get SendGrid client ready
            var apiKey = _configuration.GetSection("SENDGRID_API_KEY").Value;

            var client = new SendGridClient(apiKey);

            var msg = MailHelper.CreateSingleEmail(from, to, subject,
                Contact.Message, WebUtility.HtmlEncode(Contact.Message));

            _logger.LogDebug("Sending email via SendGrid");
            var response = await client.SendEmailAsync(msg);

            if (response.StatusCode != HttpStatusCode.Accepted)
            {
                _logger.LogDebug($"Sendgrid problem {response.StatusCode}");
                throw new ExternalException("Error sending message");
            }

            // On success just go to index page
            // (could refactor later to go to a thank you page instead)
            _logger.LogDebug("Email sent via SendGrid");
            return RedirectToPage("/Success");
        }
    }
}