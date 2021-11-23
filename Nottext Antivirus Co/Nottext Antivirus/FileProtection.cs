///
/// Arquivo de proteções
///

using System;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Windows.Forms;

namespace Nottext_Antivirus
{
    public partial class FileProtection : Form
    {
        // Nome do drivers
        string kernelDriverNome = "Nottext Antivirus Driver Kernel";

        // Todos os arquivos que são incompátiveis com o Nottext Antivirus
        string[] programasIcompativeis =
        {
            "C:\\Windows\\System32\\Drivers\\FDCDNT.SYS"
        };

        /// <summary>
        /// Verifica o serviço de kernel
        /// </summary>
        private void VerificarServico()
        {
            // Se a proteção de kernel estiver instalada
            if (kernelProtection.Checked == true)
            {
                try
                {
                    // Novo controle de serviço
                    ServiceController kernel = new ServiceController(kernelDriverNome);

                    // Se o serviço não estiver sendo executado
                    if (kernel.Status != ServiceControllerStatus.Running)
                    {
                        try
                        {
                            // Inicie o serviço
                            kernel.Start();
                        } catch (Exception) { }
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        /// <summary>
        /// Verifique se a proteção de kernel está instalado
        /// </summary>
        private bool ProtecaoKernel()
        {
            // Retorne true se o serviço estiver instalado
            return ServiceController.GetDevices().Any(serviceController => serviceController.ServiceName.Equals(kernelDriverNome));
        }

        /// <summary>
        /// Carrega tudo
        /// </summary>
        public FileProtection()
        {
            InitializeComponent();

            // Configure o cursor nos controles
            Global.SetHandCursor(this);

            // Verifique as proteções
            Global.VerificarProtecao(Global.Strings.protecaoTempoReal, realTimeProtection);
            Global.VerificarProtecao(Global.Strings.leituraProfunda, advancedScan);

            // Caso a proteção esteja ativada
            if (File.Exists(Global.Strings.antiRansomware))
            {
                // Habilite o checkbox
                antiRansomware.Checked = true;
            }

            // Se a proteção de kernel estiver instalada
            kernelProtection.Checked = ProtecaoKernel();
            
            // Se a proteção de kernel não estiver instalada
            if (kernelProtection.Checked == false)
            {
                // Altere os botões e label
                antiRansomware.Checked = false;
                antiRansomware.Enabled = false;
                label6.Visible = true;

                opcoes.Enabled = false;
            }

            if (File.Exists(Global.Strings.bloquearAmeaca))
            {
                opcoes.Text = "Bloquear ameaça";
            }

            // Verifique o serviço de kernel
            VerificarServico();
        }

        /// <summary>
        /// Quando clicar em voltar
        /// </summary>
        private void back_Click(object sender, EventArgs e)
        {
            Effects.Animate(this, Effects.Effect.Slide, Effects.intervalo, 180);
        }

        /// <summary>
        /// Proteção de kernel, após clicar no botão
        /// </summary>
        private async void protecaoKernel_Click(object sender, EventArgs e)
        {
            try
            {
                // Se a checkbox for marcada
                if (kernelProtection.Checked == true)
                {
                    // Procure todos os programas incompativeis
                    foreach (string programa in programasIcompativeis)
                    {
                        // Se o arquivo existir
                        if (File.Exists(programa))
                        {
                            // Mostre o erro
                            Global.MostrarErro("Um arquivo incompatível com o Nottext Antivirus foi encontrado, remova-o antes de instalar o driver de kernel pra não causar um congelamento no sistema.\r\nArquivo: " + programa, false);

                            // Desmarque o arquivo
                            kernelProtection.Checked = false;

                            // Pare
                            return;
                        }
                    }

                    // Confirma a instação dos drivers de kernel
                    inf _inf = new inf();

                    // Se ele deseja iniciar o modo de teste para instalar os drivers
                    await _inf.Reiniciar(kernelProtection);
                }

                // Se o checkbox for desmarcado, ele deseja remover a proteção
                else
                {
                    // Se a senha estiver errada
                    if (Global.Senha("Deseja mesmo desabilitar a proteção de kernel?") == false)
                    {
                        // Altere a checkbox
                        kernelProtection.Checked = true;
                        return;
                    }

                    // Desative os botões
                    kernelProtection.Enabled = false;

                    // Serviço de kernel
                    ServiceController kernel = new ServiceController(kernelDriverNome);

                    try
                    {
                        // Pare o driver
                        kernel.Stop();
                    }
                    catch (Exception) { }


                    // Delete o serviço
                    await Global.IniciarProcesso("sc.exe", "delete " + '"' + kernelDriverNome + '"');

                    // Desabilite o modo de teste
                    await Global.IniciarProcesso("bcdedit.exe", "/set testsigning off");

                    // Mostre a mensagem
                    //restart.Visible = true;
                    kernelProtection.Enabled = true;

                    // Desabilite os botões
                    antiRansomware.Checked = false;
                    antiRansomware.Enabled = false;
                    label6.Visible = true;
                }
            } catch (Exception ex)
            {
                // Mostre o erro
                Global.MostrarErro(ex.Message, false);
                kernelProtection.Checked = !kernelProtection.Checked;
            }
        }

        /// <summary>
        /// Proteção em tempo real, após o click
        /// </summary>
        private void protecaoTempoReal_Click(object sender, EventArgs e)
        {
            // Só continue caso a proteção esteja sendo desativada
            if (realTimeProtection.Checked == false)
            {
                // Se a senha estiver errada
                if (Global.Senha("Deseja mesmo desabilitar a proteção em tempo real?") == false)
                {
                    realTimeProtection.Checked = true;
                    return;
                }
            }

            // Ative ou desative a proteção
            Global.AtivarDesativarProtecao("realTime.set", realTimeProtection);
        }

        /// <summary>
        /// Proteção anti-ransomware, após o click
        /// </summary>
        private async void antiRansomware_Click(object sender, EventArgs e)
        {
            try
            {
                // Se o anti-ransomware estiver marcado
                if (antiRansomware.Checked == false)
                {
                    // Se a senha estiver errada
                    if (Global.Senha("Deseja mesmo desabilitar a proteção anti-ransomware?") == false)
                    {
                        antiRansomware.Checked = true;
                        return;
                    }

                    // Delete o arquivo para desabilitar a proteção
                    File.Delete(Global.Strings.antiRansomware);
                }

                // Se marcar a proteção de anti-ransomware
                else
                {
                    // Se o arquivo de exeções de ransomware não existir
                    if (!File.Exists(Global.Strings.excecoesAntiRanso))
                    {
                        // Novo ExecptionsRANSO
                        ExecptionsRANSO execptions = new ExecptionsRANSO();

                        // Restaure aos padrões
                        await execptions.RestaurarTudo();
                    }

                    // Crie o arquivo para habilitar a proteção
                    File.WriteAllText(Global.Strings.antiRansomware, "");
                }
            } catch (Exception ex)
            {
                // Mostre o erro
                Global.MostrarErro(ex.Message, false);

                // Altere o checkbox
                antiRansomware.Checked = !antiRansomware.Checked;
            }
        }

        /// <summary>
        /// Botão de exeções, após o click
        /// </summary>
        private async void exceptionsRanso_Click(object sender, EventArgs e)
        {
            try
            {
                // Mostre os programar permitidos no anti-ransomware
                ExecptionsRANSO execptions = new ExecptionsRANSO();

                // Se não existir de exeções
                if (!File.Exists(Global.Strings.excecoesAntiRanso))
                {
                    exceptionsRanso.Enabled = false;

                    // Restaure aos padrões
                    await execptions.RestaurarTudo();
                    exceptionsRanso.Enabled = true;
                }

                // Mostre o dialogo
                execptions.ShowDialog();
            } catch (Exception) { }
        }

        /// <summary>
        /// Botão de pastas protegidas
        /// </summary>
        private void folders_Click(object sender, EventArgs e)
        {
            // Mostre as páginas protegidas
            Folders folder = new Folders();
            folder.ShowDialog();
        }

        /// <summary>
        /// Checkbox da leitura profunda
        /// </summary>
        private void advancedScan_Click(object sender, EventArgs e)
        {
            if (advancedScan.Checked == false)
            {
                // Se a senha estiver errada
                if (Global.Senha("Deseja mesmo desabilitar a proteção leitura profunda?") == false)
                {
                    advancedScan.Checked = true;
                    return;
                }
            }

            // Ative ou desative a proteção
            Global.AtivarDesativarProtecao("advancedScan.set", advancedScan);
        }

        /// <summary>
        /// Focus
        /// </summary>
        private void FileProtection_MouseEnter(object sender, EventArgs e)
        {
            //Focus();
        }

        /// <summary>
        /// Alterar as opções
        /// </summary>
        private void opcoes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (opcoes.Text == "Automaticamente")
                {
                    File.Delete(Global.Strings.bloquearAmeaca);
                } else
                {
                    File.Create(Global.Strings.bloquearAmeaca).Close();
                }
            } catch (Exception) { }
        }

        /// <summary>
        /// Efeito de sombra
        /// </summary>
        private void ativarEfeito1_MouseHover(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button button = (Guna.UI2.WinForms.Guna2Button)sender;
            Global.Styles.GunaButtonEfeitoAtivar(button);
        }

        /// <summary>
        /// Efeito de sombra
        /// </summary>
        private void desativarEfeito1_MouseLeave(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button button = (Guna.UI2.WinForms.Guna2Button)sender;
            Global.Styles.GunaButtonEfeitoDesativar(button);
        }

    }
}