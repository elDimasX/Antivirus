///
/// Arquivo de configuração
///

using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Windows.Forms;

namespace Nottext_Antivirus
{
    public partial class Settings : Form
    {

        // ServiceController
        ServiceController servico = new ServiceController("Nottext Antivirus");

        // Nome do serviço do driver
        string kernelDriverNome = "Nottext Antivirus Driver Kernel";

        /// <summary>
        /// Verifique se a proteção de kernel está instalado
        /// </summary>
        private bool ProtecaoKernel()
        {
            // Retorne true se o serviço estiver instalado
            return ServiceController.GetDevices().Any(serviceController => serviceController.ServiceName.Equals(kernelDriverNome));
        }

        /// <summary>
        /// Ver as configurações do serviço
        /// </summary>
        private void Servico()
        {
            try
            {
                try
                {
                    servico.Start();
                } catch (Exception) { }

                // Se a notificação estiver desabilitada
                if (!Global.NotificacaoHabilitada())
                    notify.Checked = false; // Desmarque a opção

                // Se não estiver no automatico
                if (servico.StartType != ServiceStartMode.Automatic)
                {
                    // Desmarque a checkbox
                    autoStart.Checked = false;
                }
            }
            catch (Exception)
            {
                // Restaure tudo
                autoStart.Checked = false;
                autoStart.Enabled = false;
                error.Visible = true;
            }
        }

        /// <summary>
        /// Inicia tudo
        /// </summary>
        public Settings()
        {
            InitializeComponent();

            // Configure o cursor nos controles
            Global.SetHandCursor(this);

            // Se tiver com a senha habilitada
            if (File.Exists(Global.Strings.arquivoSenha))
            {
                // Marque
                passwordProtection.Checked = true;
            }

            Servico();

            if (ProtecaoKernel() == false)
            {
                selfProtect.Checked = false;
                selfProtect.Enabled = false;
                label8.Visible = true;
            }
        }

        /// <summary>
        /// Botão de voltar
        /// </summary>
        private void back_Click(object sender, EventArgs e)
        {
            Effects.Animate(this, Effects.Effect.Slide, Effects.intervalo, 180);
        }

        /// <summary>
        /// Clique de notificação
        /// </summary>
        private void notify_Click(object sender, EventArgs e)
        {
            // Se a proteção por senha estiver ativa
            if (passwordProtection.Checked == true)
            {
                // Se a senha não estiver correta
                if (Global.Senha("Deseja mesmo desabilitar as notificações?") == false)
                {
                    notify.Checked = true;
                    return;
                }
            }

            // Ative ou desative a proteção
            Global.AtivarDesativarProtecao("DSBPT.set", notify);
        }

        /// <summary>
        /// Altera o valor de start do registro, para desabilitar ou habilitar
        /// Que o serviço seja iniciado com o computador
        /// </summary>
        private void ChangeStartService(string value)
        {
            try
            {
                // Key
                RegistryKey key;

                // Chave no registro para modificar
                key = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\Nottext Antivirus", true);

                // Sete o valor em Start
                key.SetValue("Start", value, RegistryValueKind.DWord);
            } catch (Exception ex)
            {
                // Se falhar, mostre uma mensagem
                Global.MostrarErro(ex.Message, false);

                // Altere a checkBox
                autoStart.Checked = !autoStart.Checked;
            }
        }

        /// <summary>
        /// Auto iniciar, após o click
        /// </summary>
        public void autoStart_Click(object sender, EventArgs e)
        {
            // Se for habilitar a execução automatica
            if (autoStart.Checked == true)
            {
                // Configure para automatico
                ChangeStartService("2");
            }
            else // Se for para desabilitar
            {
                // Se a senha estiver errada
                if (Global.Senha("Deseja mesmo desabilitar a inicialização automatica?") == false)
                {
                    autoStart.Checked = true;
                    return;
                }

                // Configure para manual
                ChangeStartService("3");
            }
        }

        /// <summary>
        /// Proteção por senha
        /// </summary>
        private void passwordProtection_Click(object sender, EventArgs e)
        {
            try
            {
                // Se for para desabilitar a proteção
                if (passwordProtection.Checked == false)
                {
                    // Se a senha estiver errada
                    if (Global.Senha("Deseja mesmo desabilitar a proteção por senha?") == false)
                    {
                        passwordProtection.Checked = true;
                        return;
                    }

                    // Delete o arquivo
                    File.Delete(Global.Strings.arquivoSenha);
                }

                // É pra habilitar
                else
                {
                    // Novo SetPassword
                    SetPassword setPassword = new SetPassword();

                    // Se for cancelado
                    if (setPassword.ShowDialog() != DialogResult.OK)
                    {
                        // Não marque a checkbox
                        passwordProtection.Checked = false;
                    }
                }
            } catch (Exception ex)
            {
                // Mostre o erro
                Global.MostrarErro(ex.Message, false);
            }
        }

        /// <summary>
        /// Altera a checkbox do kernel
        /// </summary>
        public void HabilitarKernel(bool habilitar)
        {
            selfProtect.Checked = habilitar;
            selfProtect.Enabled = habilitar;
            label8.Visible = !habilitar;
        }

        /// <summary>
        /// Auto-proteção
        /// </summary>
        public void selfProtect_Click(object sender, EventArgs e)
        {
            // Se for desmarcado
            if (selfProtect.Checked == false)
            {
                // Se a senha estiver errada
                if (Global.Senha("Deseja mesmo desabilitar a auto-proteção?") == false)
                {
                    selfProtect.Checked = true;
                    return;
                }

                // Desative a auto-proteção
                KernelCommunication.EnviarMensagem("", KernelCommunication.CtlCodes.DISABLE_SELF_PROTECT);
            }

            // Se não
            else
            {
                // Ative a auto-proteção
                KernelCommunication.EnviarMensagem("", KernelCommunication.CtlCodes.ENABLE_SELF_PROTECT);
            }
            
        }
    }
}