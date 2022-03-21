using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SANTA_Punto_de_Venta
{
    class Correos
    {
        //Clase instanciada por la clase Thread, debido a la tardanza al envíar correos, se implementaron para agilizar los procesos de venta y envío de peticiones

        string prod = "";
        string fecha = "";

        public Correos(string producto, string fecha)
        {
            this.prod = producto;
            this.fecha = fecha;
        }

        public void correoVenta()
        {
            try
            {
                string ciber = "CiberStore";
                
                //Aqui va codigo similar al de abajo para detectar la GPU, pero se quito porque ya no tenia el otro ciber :(sss

                MailMessage email = new MailMessage();

                email.To.Add(new MailAddress("krauser_105@hotmail.com"));

                // En pruebas solo enviar el correo a mi, si esta en produccion, tambien a mi papa
                //PC de pruebas con NVIDIA 1050
                ManagementObjectSearcher buscaProcesador = new ManagementObjectSearcher("SELECT Description FROM Win32_DisplayConfiguration");
                foreach (ManagementObject mo in buscaProcesador.Get())
                {
                    foreach (PropertyData propiedades in mo.Properties)
                    {
                        if (!propiedades.Value.ToString().Equals("NVIDIA GTX 1050"))
                        {
                            email.To.Add(new MailAddress("cesare_7maldini@yahoo.com"));
                        }    
                    }
                }
                
                email.From = new MailAddress("krauser105@gmail.com", "CiberStore");
                email.Subject = "Reporte de producto(s) vendido(s) sin registro de cantidad en base de datos en " + ciber;
                email.Body = "Se registró que el/los producto(s):<br><br>" + prod + "<br>se vendió/vendieron sin tener existencia en " + ciber;
                email.IsBodyHtml = true;
                email.Priority = MailPriority.Normal;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("krauser105@gmail.com", "Farakrystal.7");

                smtp.Send(email);
                email.Dispose();
            }
            catch (SmtpException) { MessageBox.Show("Ha ocurrido un error. Verifica lo siguiente:\n\n- Verifica que el correo no haya sido modificado o tenga alguna alerta de inicio de sesión y prueba de nuevo\n- Verifica que la contraseña no haya sido cambiada. Si es así, avisar a César\n", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        public int correoRequisicion()
        {
            try
            {
                string ciber = "CiberStore";
                //ManagementObjectSearcher buscaProcesador = new ManagementObjectSearcher("SELECT Description FROM Win32_DisplayConfiguration");
                //foreach (ManagementObject mo in buscaProcesador.Get())
                //{
                //    foreach (PropertyData propiedades in mo.Properties)
                //    {
                //        if (propiedades.Value.ToString().Equals("Intel(R) HD Graphics"))
                //        {
                //            ciber = "CiberStore 1";
                //        }
                //        if (propiedades.Value.ToString().Equals("Intel(R) Q45/Q43 Express Chipset"))
                //        {
                //            ciber = "CiberStore 2";
                //        }
                //    }
                //}

                MailMessage email = new MailMessage();

                email.To.Add(new MailAddress("krauser_105@hotmail.com"));
                // En pruebas solo enviar el correo a mi, si esta en produccion, tambien a mi papa
                //PC de pruebas con NVIDIA 1050
                ManagementObjectSearcher buscaProcesador = new ManagementObjectSearcher("SELECT Description FROM Win32_DisplayConfiguration");
                foreach (ManagementObject mo in buscaProcesador.Get())
                {
                    foreach (PropertyData propiedades in mo.Properties)
                    {
                        if (!propiedades.Value.ToString().Equals("NVIDIA GTX 1050"))
                        {
                            email.To.Add(new MailAddress("cesare_7maldini@yahoo.com"));
                        }
                    }
                }
                email.From = new MailAddress("krauser105@gmail.com", "CiberStore");
                email.Subject = "Requisición de Productos " + ciber;
                email.Body = prod;
                email.IsBodyHtml = true;
                email.Priority = MailPriority.Normal;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("krauser105@gmail.com", "Farakrystal.7");

                smtp.Send(email);
                email.Dispose();

                MessageBox.Show("Correo enviado satisfactoriamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return 1;
            }
            catch (SmtpException) { 
                MessageBox.Show("Ha ocurrido un error. Verifica lo siguiente:\n\n- Verifica que el correo no haya sido modificado o tenga alguna alerta de inicio de sesión y prueba de nuevo\n- Verifica que la contraseña no haya sido cambiada. Si es así, avisar a César\n", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        public void correoVentaDia()
        {
            try
            {
                //string ciber = "CiberStore";
                //ManagementObjectSearcher buscaProcesador = new ManagementObjectSearcher("SELECT Description FROM Win32_DisplayConfiguration");
                //foreach (ManagementObject mo in buscaProcesador.Get())
                //{
                //    foreach (PropertyData propiedades in mo.Properties)
                //    {
                //        if (propiedades.Value.ToString().Equals("Intel(R) HD Graphics"))
                //        {
                //            ciber = "CiberStore 1";
                //        }
                //        if (propiedades.Value.ToString().Equals("Intel(R) Q45/Q43 Express Chipset"))
                //        {
                //            ciber = "CiberStore 2";
                //        }
                //    }
                //}

                MailMessage email = new MailMessage();

                email.To.Add(new MailAddress("krauser_105@hotmail.com"));
                // En pruebas solo enviar el correo a mi, si esta en produccion, tambien a mi papa
                //PC de pruebas con NVIDIA 1050
                ManagementObjectSearcher buscaProcesador = new ManagementObjectSearcher("SELECT Description FROM Win32_DisplayConfiguration");
                foreach (ManagementObject mo in buscaProcesador.Get())
                {
                    foreach (PropertyData propiedades in mo.Properties)
                    {
                        if (!propiedades.Value.ToString().Equals("NVIDIA GTX 1050"))
                        {
                            email.To.Add(new MailAddress("cesare_7maldini@yahoo.com"));
                        }
                    }
                }
                email.From = new MailAddress("krauser105@gmail.com", "CiberStore");
                email.Subject = fecha;
                email.Body = prod;
                email.IsBodyHtml = true;
                email.Priority = MailPriority.Normal;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("krauser105@gmail.com", "Farakrystal.7");

                smtp.Send(email);
                email.Dispose();
            }
            catch (SmtpException) { MessageBox.Show("Ha ocurrido un error. Verifica lo siguiente:\n\n- Verifica que el correo no haya sido modificado o tenga alguna alrta de inicio de sesión y prueba de nuevo\n- Verifica que la contraseña no haya sido cambiada. Si es así, avisar a César\n", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
