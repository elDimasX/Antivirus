///
/// Arquivo que contém a função de instalar drivers de kernel
/// Ele pede confirmação, e depois, cria o arquivo e reinicia o computador
/// 

using Guna.UI2.WinForms;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nottext_Antivirus
{
    class inf
    {
        /// <summary>
        /// Reiniciar o computador para ativar o modo teste
        /// </summary>
        public async Task Reiniciar(
            // Checkbox, para alterar o valor caso ele cancele a operação
            Guna2CustomCheckBox checkbox
        )
        {
            try
            {
                await Task.Delay(1);

                // Form de alerta
                Alert form = new Alert(
                    // Texto
                    "Você deve reiniciar o computador primeiro, ativaremos o modo de teste para que a instalação dos drivers seja concluída.\r\nDeseja reiniciar agora?",

                    // Ativar o botão de cancelar
                    true
                );

                // Se o usuário selecionar OK
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // Inicie o modo de teste
                    await Global.IniciarProcesso("bcdedit.exe", "-set testsigning on");

                    // Cria o arquivo para confirmar que o computador foi reiniciado
                    File.Create("C:\\Windows\\RESTARTREQUIRED.set");

                    // Reinicie o computador
                    await Global.IniciarProcesso("shutdown.exe", "-f -r -t 0");
                }
                else
                {
                    // Desmarque a proteção de kernel, pois o usuário cancelou a operação
                    checkbox.Checked = false;
                }
            }
            catch (Exception ex)
            {
                // Mostre o erro
                Global.MostrarErro(ex.Message, false);
            }
        }
    }
}