using Data.Interfaces;
using Data.Model;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Data.Logic;

public class OrderProcessor : IOrderProcessor
{
    private EmailSettings emailSettings;
    public OrderProcessor()
    {
        emailSettings = new EmailSettings();
    }

    public void ProcessOrder(Cart cart, ShippingDetails shippingDetails)
    {
        using (var smtpClient = new SmtpClient())
        {
            smtpClient.EnableSsl = emailSettings.UseSsl;
            smtpClient.Host = emailSettings.ServerName;
            smtpClient.Port = emailSettings.ServerPort;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(emailSettings.Login, emailSettings.Password);

            if (emailSettings.WriteAsFile)
            {
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                smtpClient.EnableSsl = false;
            }

            StringBuilder body = new StringBuilder()
            .AppendLine("Новый заказ обработан")
            .AppendLine("---")
            .AppendLine("Товары:");

            foreach (var line in cart.Lines)
            {
                var subtotal = line.Food.Price * line.Quantity;
                body.AppendFormat("{0} x {1} (итого: {2:0.00})", line.Quantity, line.Food.Name, subtotal);
            }

            body.AppendLine()
                .AppendFormat("Общая стоимость: {0:0.00}", cart.ComputeTotalValue())
                .AppendLine()
                .AppendLine("---")
                .AppendLine("Доставка:")
                .AppendLine(shippingDetails.FirstName + " " + shippingDetails.SecondName)
                .AppendLine(shippingDetails.Country)
                .AppendLine(shippingDetails.City)
                .AppendLine(shippingDetails.Address)
                .AppendLine("---")
                .AppendLine("Спасибо за Ваш заказ, в ближайшее время с Вами свяжется наш менеджер.");

            MailMessage mailMessage = new MailMessage(
                emailSettings.Login,
                shippingDetails.Email,
                "Новый заказ отправлен!",
                body.ToString()
                );

            if (emailSettings.WriteAsFile)
            {
                mailMessage.BodyEncoding = Encoding.UTF8;
            }

            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (Exception)
            {
            }
        }
    }
}