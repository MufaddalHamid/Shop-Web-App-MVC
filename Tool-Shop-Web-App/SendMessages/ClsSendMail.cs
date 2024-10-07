using System;
using System.Data;
using System.Net.Mail;
using System.Configuration;
using AutomatedMessagingServices.Classes;
using System.Threading.Tasks;
using System.Collections.Generic;

public class ClsSendMail
{
    private static readonly string SmtpServer = ConfigurationManager.AppSettings["SmtpServer"];
    private static readonly int SmtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
    private static readonly string SmtpUsername = ConfigurationManager.AppSettings["SmtpUsername"];
    private static readonly string SmtpPassword = ConfigurationManager.AppSettings["SmtpPassword"];

    public async Task<string> SendEmailAsync(string fromMailId, string sendTo, string cc, string subject, string message)
    {
        try
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(fromMailId);
            mail.To.Add(sendTo);

            if (!string.IsNullOrEmpty(cc))
            {
                mail.CC.Add(cc);
            }

            mail.Subject = subject;
            mail.Body = message;
            mail.IsBodyHtml = true;

            using (SmtpClient smtpClient = new SmtpClient(SmtpServer, SmtpPort))
            {
                smtpClient.Credentials = new System.Net.NetworkCredential(SmtpUsername, SmtpPassword);
                smtpClient.EnableSsl = true;
                await smtpClient.SendMailAsync(mail);
            }

            return "Success";
        }
        catch (Exception ex)
        {
            //Write Error Log
            return "Failed: " + ex.Message;
        }
    }

    public async Task<DataTable> SendEmailsAsync(DataTable dataTable, string fromMailId)
    {
        // Add a new column to the DataTable to store the status
        if (!dataTable.Columns.Contains("Status"))
        {
            dataTable.Columns.Add("Status", typeof(string));
        }

        var tasks = new List<Task>();

        foreach (DataRow row in dataTable.Rows)
        {
            string sendTo = row["SendTo"].ToString();
            string cc = row["CC"].ToString();
            string message = row["Message"].ToString();
            string subject = row["Subject"].ToString();

            var task = SendEmailAsync(fromMailId, sendTo, cc, subject, message)
                .ContinueWith(t => row["Status"] = t.Result);

            tasks.Add(task);
        }

        await Task.WhenAll(tasks);

        return dataTable;
    }
}
