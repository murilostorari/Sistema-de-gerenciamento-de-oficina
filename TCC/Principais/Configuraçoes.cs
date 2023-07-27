using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCC.Principais
{
    public partial class Configuraçoes : Form
    {
        public static Configuraçoes ConfigsFrame;

        public Guna.UI2.WinForms.Guna2ToggleSwitch AnimateBotoesToggle;
        public Guna.UI2.WinForms.Guna2ToggleSwitch AnimateFramesToggle;

        bool FormLoaded;

        bool IsDarkModeEnabled = Properties.Settings.Default.DarkModeEnabled;
        bool AnimateButtonsAndPanels = Properties.Settings.Default.AnimarBotoes;

        bool ShowPopupsChanged;
        bool DoubleClickChanged;
        bool AutoCompleteCurrencyChanged;

        bool DarkModeChanged;
        bool LightModeChanged;

        bool AnimateButtonsChanged;
        bool AnimateFramesChanged;

        bool SettingChanged;

        List<Guna.UI2.WinForms.Guna2Button> GunaButtons;
        List<Guna.UI2.WinForms.Guna2ToggleSwitch> GunaToggleButtons;
        List<Guna.UI2.WinForms.Guna2Separator> GunaSeparators;

        List<Label> NormalLabels;

        public Configuraçoes()
        {
            InitializeComponent();
            AddControlsToList();
            SetColor();
            
            ConfigsFrame = this;

            AnimateBotoesToggle = AnimarBotoes;
            AnimateFramesToggle = AnimarFrames;
        }

        private void Configuraçoes_Load(object sender, EventArgs e)
        {
            if (IsDarkModeEnabled)
            {
                LightMode.Image = Properties.Resources.light_mode;
                DarkMode.Image = Properties.Resources.dark_mode___selected;

                SelectedTheme.Location = new Point(230, 140);
                SelectedTheme.BackColor = Color.FromArgb(35, 35, 35);

                Geral.Image = Properties.Resources.settings_branco;
                Aparencia.Image = Properties.Resources.aparencia_branco;

                Geral.ForeColor = Color.FromArgb(255, 63, 0);
            }

            else
            {
                LightMode.Image = Properties.Resources.light_mode___selected;
                DarkMode.Image = Properties.Resources.dark_mode;

                SelectedTheme.Location = new Point(88, 140);
                SelectedTheme.BackColor = Color.Transparent;

                Geral.Image = Properties.Resources.settings;
                Aparencia.Image = Properties.Resources.aparencia;

                Geral.ForeColor = Color.FromArgb(255, 3, 0);
            }

            if (AnimateButtonsAndPanels)
            {
                Geral.Animated = true;
                Aparencia.Animated = true;

                ShowPopups.Animated = true;
                AnimarBotoes.Animated = true;
                AnimarFrames.Animated = true;

                SalvarAlteracoes.Animated = true;
            }
            else
            {
                Geral.Animated = false;
                Aparencia.Animated = false;

                ShowPopups.Animated = false;
                AnimarBotoes.Animated = false;
                AnimarFrames.Animated = false;

                SalvarAlteracoes.Animated = false;
            }

            ShowPopups.Checked = Properties.Settings.Default.ShowPopups;
            DoubleClickInGrid.Checked = Properties.Settings.Default.DoubleClickInGridEnabled;
            AutoCompleteCurrency.Checked = Properties.Settings.Default.AutoCompleteCurrencyValues;
            AnimarBotoes.Checked = Properties.Settings.Default.AnimarBotoes;
            AnimarFrames.Checked = Properties.Settings.Default.AnimarFrames;

            Geral.Image = Properties.Resources.settings_red;

            FormLoaded = true;
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Funçoes */

        // Fundo preto transparente
        private void DarkBackground(Form form)
        {
            Form formBackground = new Form();

            formBackground.Name = "TransparentBack";
            formBackground.StartPosition = FormStartPosition.Manual;
            formBackground.FormBorderStyle = FormBorderStyle.None;
            formBackground.Opacity = .50d;
            formBackground.BackColor = Color.Black;
            formBackground.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            formBackground.AutoSize = true;
            formBackground.TopMost = false;
            formBackground.Location = this.Location;
            formBackground.ShowInTaskbar = false;
            formBackground.Show();

            form.Owner = formBackground;
            form.ShowDialog();
        }

        // Adicionar itens a lista pra poder usar o dark/light mode
        private void AddControlsToList()
        {
            GunaButtons = new List<Guna.UI2.WinForms.Guna2Button>();
            GunaToggleButtons = new List<Guna.UI2.WinForms.Guna2ToggleSwitch>();
            GunaSeparators = new List<Guna.UI2.WinForms.Guna2Separator>();

            NormalLabels = new List<Label>();

            //------------------//

            Guna.UI2.WinForms.Guna2Button[] Buttons = new Guna.UI2.WinForms.Guna2Button[2]
            {
                Geral, Aparencia
            };

            //------------------//

            // Toggle buttons
            Guna.UI2.WinForms.Guna2ToggleSwitch[] ToggleButtons = new Guna.UI2.WinForms.Guna2ToggleSwitch[5]
            {
               AnimarBotoes, AnimarFrames, ShowPopups, DoubleClickInGrid, AutoCompleteCurrency
            };

            //------------------//

            // Labels normais
            Label[] Labels = new Label[8]
            {
               label1, label2, label3, label4, label5, label6, label7, label8
            };

            //------------------//

            // Separators
            Guna.UI2.WinForms.Guna2Separator[] Separators = new Guna.UI2.WinForms.Guna2Separator[4]
            {
                Separator1, guna2Separator1, guna2Separator2, guna2Separator3
            };

            GunaButtons.AddRange(Buttons);
            GunaToggleButtons.AddRange(ToggleButtons);
            GunaSeparators.AddRange(Separators);
            NormalLabels.AddRange(Labels);
        }

        // Ativar/desativar o dark mode
        private void SetColor()
        {
            this.BackColor = ThemeManager.FormBackColor;

            FrameName.BackColor = ThemeManager.FormBackColor;
            FrameName.ForeColor = ThemeManager.WhiteFontColor;

            AparenciaLabel.BackColor = ThemeManager.FormBackColor;
            AparenciaLabel.ForeColor = ThemeManager.WhiteFontColor;

            AnimaçoesLabel.BackColor = ThemeManager.FormBackColor;
            AnimaçoesLabel.ForeColor = ThemeManager.WhiteFontColor;

            GeralLabel.BackColor = ThemeManager.FormBackColor;
            GeralLabel.ForeColor = ThemeManager.WhiteFontColor;

            //------------------//

            // Labels normais
            foreach (Label ct in NormalLabels)
            {
                ct.ForeColor = ThemeManager.FontColor;
                ct.BackColor = ThemeManager.FormBackColor;
            }

            //------------------//

            // Botoes normais
            foreach (Guna.UI2.WinForms.Guna2Button ct in GunaButtons)
            {
                ct.ForeColor = ThemeManager.WhiteFontColor;
                ct.FillColor = ThemeManager.FormBackColor;
                ct.PressedColor = ThemeManager.ButtonPressedColor;
                ct.CheckedState.FillColor = ThemeManager.ButtonCheckedStateColor;
                ct.HoverState.FillColor = ThemeManager.ButtonHoverStateColor;
            }

            //------------------//

            // Toggle buttons
            foreach (Guna.UI2.WinForms.Guna2ToggleSwitch ct in GunaToggleButtons)
            {
                ct.CheckedState.FillColor = ThemeManager.ToggleSwitchButtonCheckedFillColor;         
                ct.UncheckedState.FillColor = ThemeManager.ToggleSwitchButtonUncheckedFillColor;
            }

            //------------------//

            // Separators
            foreach (Guna.UI2.WinForms.Guna2Separator ct in GunaSeparators)
            {
                ct.FillColor = ThemeManager.SeparatorAndBorderColor;
            }
        }

        /*--------------------------------------------------------------------------------------------*/

        /* Outros */

        /* Toggle buttons/outros bglhs de configuraçao */

        // Mostrar/nao popups de confirmaçao
        private void ShowPopups_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowPopups.Checked)
                ShowPopupsChanged = true;
            else
                ShowPopupsChanged = false;

            if (FormLoaded)
                SettingChanged = true;
        }

        // Ativar/nao o clique duplo no grid pra mostrar as informaçoes
        private void DoubleClickInGrid_CheckedChanged(object sender, EventArgs e)
        {
            if (DoubleClickInGrid.Checked)
                DoubleClickChanged = true;
            else
                DoubleClickChanged = false;

            if (FormLoaded)
                SettingChanged = true;
        }

        // Colocar o mesmo valor de venda para todas as categorias do produto
        private void AutoCompleteCurrency_CheckedChanged(object sender, EventArgs e)
        {
            if (AutoCompleteCurrency.Checked)
                AutoCompleteCurrencyChanged = true;
            else
                AutoCompleteCurrencyChanged = false;

            if (FormLoaded)
                SettingChanged = true;
        }

        // Animar/nao botoes
        private void AnimarBotoes_CheckedChanged(object sender, EventArgs e)
        {
            if (AnimarBotoes.Checked)
                AnimateButtonsChanged = true;
            else
                AnimateButtonsChanged = false;

            if (FormLoaded)
                SettingChanged = true;
        }

        // Animar/nao frames
        private void AnimarFrames_CheckedChanged(object sender, EventArgs e)
        {
            if (AnimarFrames.Checked)
                AnimateFramesChanged = true;
            else
                AnimateFramesChanged = false;

            if (FormLoaded)
                SettingChanged = true;
        }

        // Ativar o modo claro
        private void LightMode_Click(object sender, EventArgs e)
        {
            LightMode.Image = Properties.Resources.light_mode___selected;
            DarkMode.Image = Properties.Resources.dark_mode;

            SelectedTheme.Location = new Point(88, 140);
            SelectedTheme.BackColor = Color.FromArgb(245, 245, 245);

            LightModeChanged = true;
            DarkModeChanged = false;
            SettingChanged = true;
        }

        // Ativar o modo escuro
        private void DarkMode_Click(object sender, EventArgs e)
        {
            LightMode.Image = Properties.Resources.light_mode;
            DarkMode.Image = Properties.Resources.dark_mode___selected;

            SelectedTheme.Location = new Point(230, 140);
            SelectedTheme.BackColor = Color.FromArgb(38, 38, 38);

            LightModeChanged = false;
            DarkModeChanged = true;
            SettingChanged = true;
        }

        /*--------------------------------------------------------------------------------------------*/

        // Botoes pra selecionar categoria

        private void Geral_Click(object sender, EventArgs e)
        {
            if (IsDarkModeEnabled)
            {
                Geral.Image = Properties.Resources.settings_red; Aparencia.Image = Properties.Resources.aparencia_branco;

                Geral.ForeColor = Color.FromArgb(255, 63, 0);
                Aparencia.ForeColor = ThemeManager.FontColor;
            }
            else
            {
                Geral.Image = Properties.Resources.settings_red; Aparencia.Image = Properties.Resources.aparencia;

                Geral.ForeColor = Color.FromArgb(255, 3, 0);
                Aparencia.ForeColor = ThemeManager.FontColor;
            }

            BarraLateral.Location = new Point(-1, Geral.Location.Y + 4);

            AparenciaPanel.Visible = false;
            GeralPanel.Visible = true;
        }

        private void Aparencia_Click(object sender, EventArgs e)
        {
            if (IsDarkModeEnabled)
            {
                Geral.Image = Properties.Resources.settings_branco; Aparencia.Image = Properties.Resources.aparencia_red;

                Aparencia.ForeColor = Color.FromArgb(255, 63, 0);
                Geral.ForeColor = ThemeManager.FontColor;
            }
            else
            {
                Geral.Image = Properties.Resources.settings; Aparencia.Image = Properties.Resources.aparencia_red;

                Aparencia.ForeColor = Color.FromArgb(255, 3, 0);
                Geral.ForeColor = ThemeManager.FontColor;
            }

            BarraLateral.Location = new Point(-1, Aparencia.Location.Y + 4);

            AparenciaPanel.Visible = true;
            GeralPanel.Visible = false;
        }

        // Salvar alteraçoes
        private void SalvarAlteracoes_Click(object sender, EventArgs e)
        {
            if (SettingChanged)
            {        
                ThreadStart ts = new ThreadStart(() => {
                    DarkBackground(new Frames.RestartApp(DarkModeChanged, LightModeChanged, AnimateButtonsChanged, AnimateFramesChanged,
                        ShowPopupsChanged, DoubleClickChanged, AutoCompleteCurrencyChanged));
                });

                Thread t = new Thread(ts);

                t.SetApartmentState(ApartmentState.STA);

                t.Start();
            }
        }
    }
}
