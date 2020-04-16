using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Services
{

    public class SendContactService
    {
        public async Task SendMailAsync(Contact model)
        {
            model.Subject = "Consultation";

            await MessageService.SendEmailAsync(model);
        }
    }
}
