using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCC
{
    public static class ThemeManager
    {
        public static bool DarkMode = Properties.Settings.Default.DarkModeEnabled;

        public static Color

         FontColor,
         WhiteFontColor,
         PresetFontColor,
         PresetLabelColor,
         DarkGrayLabelsFontColor,
         RedFontColor,

         AppBordersColor,
         AppBordersBarColor,

         FormBackColor,

         PanelColor,
         PanelBorderColor,

         StartButtonFillColor,
         StartButtonPressedColor,
         StartButtonCheckedStateColor,
         StartButtonHoverStateColor,

         MainButtonForeColor,
         MainButtonFillColor,
         MainButtonHoverBorderColor,
         MainButtonPressedColor,
         MainButtonHoverFillColor,

         ButtonFillColor,
         ButtonPressedColor,
         ButtonCheckedStateColor,
         ButtonHoverStateColor,

         RadioButtonUncheckedFillColor,

         SeparatorAndBorderColor,

         GridColor,
         GridForeColor,

         SearchBoxForeColor,
         SearchBoxBorderColor,
         SearchBoxFillColor,
         SearchBoxFillColor2,
         SearchBoxHoverBorderColor,
         SearchBoxPlaceholderColor,
         SearchBoxFocusedBorderColor,

         ComboBoxForeColor,
         ComboBoxFillColor,
         ComboBoxBorderColor,
         ComboBoxHoverBorderColor,
         ComboBoxFocusedBorderColor,
         ComboBoxSelectedItemColor,

         TextBoxFillColor,
         TextBoxForeColor,
         TextBoxHoverBorderColor,
         TextBoxBorderColor,
         TextBoxFocusedBorderColor,

         ToggleSwitchButtonCheckedFillColor,
         ToggleSwitchButtonUncheckedFillColor,

         FullRedButtonColor,
         FullRedButtonHoverColor,
         FullRedButtonCheckedColor,

         FullGreenButtonColor,
         FullGreenButtonHoverColor,
         FullGreenButtonCheckedColor,

         BorderRedButtonForeColor,
         BorderRedButtonFillColor,
         BorderRedButtonFillColor2,
         BorderRedButtonBorderColor,
         BorderRedButtonHoverForeColor,
         BorderRedButtonHoverFillColor,
         BorderRedButtonHoverFillColor2,
         BorderRedButtonHoverBorderColor,
         BorderRedButtonPressedColor,

         BorderDarkGrayButtonFillColor,
         BorderDarkGrayButtonForeColor,
         BorderDarkGrayButtonBorderColor,
         BorderDarkGrayButtonHoverBorderColor,
         BorderDarkGrayButtonHoverFillColor,
         BorderDarkGrayButtonHoverForeColor,
         BorderDarkGrayButtonPressedFocusedColor,

         DateTimePickerHoverBorderColor,

         GunaToolTipForeColor,
         GunaToolTipBackColor,
         GunaToolTipBorderColor,

         CloseMinimizeIconColor,
         CloseMinimizeHoverIconColor,

         ChoosePictureBorderColor;

        public static void ChangeMode()
        {
            if (DarkMode != true)
            {
                SetLightMode();
            }

            else
            {
                SetDarkMode();
            }
        }


        private static void SetDarkMode()
        {
            FontColor = Color.FromArgb(250, 250, 250);
            WhiteFontColor = Color.FromArgb(255, 255, 255);
            PresetFontColor = Color.FromArgb(200, 200, 200);
            PresetLabelColor = Color.FromArgb(255, 255, 255);
            DarkGrayLabelsFontColor = Color.FromArgb(255, 255, 255);
            RedFontColor = Color.FromArgb(255, 33, 0);

            FormBackColor = Color.FromArgb(38, 38, 38);

            AppBordersColor = Color.FromArgb(35, 35, 35);
            AppBordersBarColor = Color.FromArgb(51, 55, 63);

            PanelColor = Color.FromArgb(45, 45, 45);
            PanelBorderColor = Color.FromArgb(58, 58, 58);

            StartButtonFillColor = Color.FromArgb(35, 35, 35);
            StartButtonPressedColor = Color.FromArgb(40, 40, 40);
            StartButtonCheckedStateColor = Color.FromArgb(50, 50, 50);
            StartButtonHoverStateColor = Color.FromArgb(60, 60, 60);

            MainButtonFillColor = Color.FromArgb(38, 38, 38);
            MainButtonHoverBorderColor = Color.FromArgb(101, 105, 113);
            MainButtonPressedColor = Color.FromArgb(26, 20, 28);
            MainButtonHoverFillColor = Color.FromArgb(58, 58, 58);
            MainButtonForeColor = Color.FromArgb(101, 105, 113);

            ButtonFillColor = Color.FromArgb(45, 45, 45);
            ButtonPressedColor = Color.FromArgb(35, 35, 35);
            ButtonCheckedStateColor = Color.FromArgb(55, 55, 55);
            ButtonHoverStateColor = Color.FromArgb(60, 60, 60);

            SeparatorAndBorderColor = Color.FromArgb(80, 80, 80);
            
            GridColor = Color.FromArgb(38, 38, 38);
            GridForeColor = Color.FromArgb(220, 220, 220);

            SearchBoxFillColor = Color.FromArgb(38, 38, 38);
            SearchBoxFillColor2 = Color.FromArgb(45, 45, 45);
            SearchBoxForeColor = Color.FromArgb(245, 245, 245);
            SearchBoxHoverBorderColor = Color.FromArgb(101, 105, 113);
            SearchBoxPlaceholderColor = Color.FromArgb(210, 210, 210);
            SearchBoxBorderColor = Color.FromArgb(80, 80, 80);
            SearchBoxFocusedBorderColor = Color.FromArgb(180, 180, 180);

            ComboBoxFillColor = Color.FromArgb(38, 38, 38);
            ComboBoxForeColor = Color.FromArgb(245, 245, 245);
            ComboBoxBorderColor = Color.FromArgb(80, 80, 80);
            ComboBoxHoverBorderColor = Color.FromArgb(101, 105, 113);
            ComboBoxFocusedBorderColor = Color.FromArgb(180, 180, 180);
            ComboBoxSelectedItemColor = Color.FromArgb(68, 68, 68);

            TextBoxFillColor = Color.FromArgb(38, 38, 38);
            TextBoxForeColor = Color.FromArgb(245, 245, 245);
            TextBoxHoverBorderColor = Color.FromArgb(101, 105, 113);
            TextBoxBorderColor = Color.FromArgb(80, 80, 80);
            TextBoxFocusedBorderColor = Color.FromArgb(180, 180, 180);

            ToggleSwitchButtonCheckedFillColor = Color.FromArgb(255, 33, 0);
            ToggleSwitchButtonUncheckedFillColor = Color.FromArgb(210, 210, 210);

            FullRedButtonColor = Color.FromArgb(255, 33, 0);
            FullRedButtonHoverColor = Color.FromArgb(255, 63, 0);
            FullRedButtonCheckedColor = Color.FromArgb(255, 43, 0);

            FullGreenButtonColor = Color.FromArgb(49, 208, 77);
            FullGreenButtonHoverColor = Color.FromArgb(49, 228, 7);
            FullGreenButtonCheckedColor = Color.FromArgb(39, 208, 57);

            BorderRedButtonFillColor = Color.FromArgb(38, 38, 38);
            BorderRedButtonFillColor2 = Color.FromArgb(45, 45, 45);
            BorderRedButtonForeColor = Color.FromArgb(255, 33, 0);
            BorderRedButtonBorderColor = Color.FromArgb(255, 33, 0);
            BorderRedButtonHoverBorderColor = Color.FromArgb(255, 53, 0);
            BorderRedButtonHoverFillColor = Color.FromArgb(64, 23, 0);
            BorderRedButtonHoverFillColor2 = Color.FromArgb(104, 23, 0);
            BorderRedButtonHoverForeColor = Color.FromArgb(255, 33, 0);
            BorderRedButtonPressedColor = Color.FromArgb(200, 120, 120); 
            
            BorderDarkGrayButtonFillColor = Color.FromArgb(38, 38, 38);
            BorderDarkGrayButtonForeColor = Color.FromArgb(140, 140, 140);
            BorderDarkGrayButtonBorderColor = Color.FromArgb(130, 130, 130);
            BorderDarkGrayButtonHoverForeColor = Color.FromArgb(150, 150, 150);
            BorderDarkGrayButtonHoverBorderColor = Color.FromArgb(150, 150, 150);
            BorderDarkGrayButtonHoverFillColor = Color.FromArgb(50, 50, 50);
            BorderDarkGrayButtonPressedFocusedColor = Color.FromArgb(40, 40, 40);

            DateTimePickerHoverBorderColor = Color.FromArgb(101, 105, 113);

            ChoosePictureBorderColor = Color.FromArgb(38, 38, 38);

            GunaToolTipForeColor = Color.FromArgb(250, 250, 250);
            GunaToolTipBorderColor = Color.FromArgb(78, 78, 78);
            GunaToolTipBackColor = Color.FromArgb(65, 65, 65);

            CloseMinimizeIconColor = Color.FromArgb(255, 255, 255);
            CloseMinimizeHoverIconColor = Color.FromArgb(220, 220, 220);

            DarkMode = true;
        }

        private static void SetLightMode()
        {
            FontColor = Color.FromArgb(0, 0, 0);
            WhiteFontColor = Color.FromArgb(0, 0, 0);
            PresetFontColor = Color.FromArgb(81, 81, 81);
            PresetLabelColor = Color.FromArgb(81, 81, 81);
            DarkGrayLabelsFontColor = Color.FromArgb(81, 81, 81);
            RedFontColor = Color.FromArgb(255, 3, 0);

            FormBackColor = Color.FromArgb(255, 255, 255);

            AppBordersColor = Color.FromArgb(250, 250, 250);
            AppBordersBarColor = Color.FromArgb(210, 210, 210);

            PanelColor = Color.FromArgb(255, 255, 255);
            PanelBorderColor = Color.FromArgb(210, 210, 210);

            StartButtonFillColor = Color.FromArgb(251, 251, 251);
            StartButtonPressedColor = Color.FromArgb(210, 210, 210);
            StartButtonCheckedStateColor = Color.FromArgb(200, 200, 200);
            StartButtonHoverStateColor = Color.FromArgb(230, 230, 230);

            MainButtonFillColor = Color.FromArgb(255, 255, 255);
            MainButtonHoverBorderColor = Color.FromArgb(180, 180, 180);
            MainButtonPressedColor = Color.FromArgb(170, 170, 170);
            MainButtonHoverFillColor = Color.FromArgb(245, 245, 245);
            MainButtonForeColor = Color.FromArgb(150, 150, 150);

            ButtonFillColor = Color.FromArgb(255, 255, 255);
            ButtonPressedColor = Color.FromArgb(210, 210, 210);
            ButtonCheckedStateColor = Color.FromArgb(220, 220, 220);
            ButtonHoverStateColor = Color.FromArgb(235, 235, 235);

            SeparatorAndBorderColor = Color.FromArgb(210, 210, 210);

            GridColor = Color.FromArgb(255, 255, 255);
            GridForeColor = Color.FromArgb(50, 50, 50);

            SearchBoxFillColor = Color.FromArgb(255, 255, 255);
            SearchBoxFillColor2 = Color.FromArgb(255, 255, 255);
            SearchBoxForeColor = Color.FromArgb(0, 0, 0);
            SearchBoxHoverBorderColor = Color.FromArgb(180, 180, 180);
            SearchBoxPlaceholderColor = Color.FromArgb(150, 150, 150);
            SearchBoxBorderColor = Color.FromArgb(210, 210, 210);
            SearchBoxFocusedBorderColor = Color.FromArgb(0, 0, 0);

            ComboBoxFillColor = Color.FromArgb(255, 255, 255);
            ComboBoxForeColor = Color.FromArgb(0, 0, 0);
            ComboBoxBorderColor = Color.FromArgb(210, 210, 210);
            ComboBoxHoverBorderColor = Color.FromArgb(180, 180, 180);
            ComboBoxFocusedBorderColor = Color.FromArgb(180, 180, 180);
            ComboBoxSelectedItemColor = Color.FromArgb(230, 230, 230);

            TextBoxFillColor = Color.FromArgb(255, 255, 255);
            TextBoxForeColor = Color.FromArgb(0, 0, 0);
            TextBoxHoverBorderColor = Color.FromArgb(180, 180, 180);
            TextBoxBorderColor = Color.FromArgb(210, 210, 210);
            TextBoxFocusedBorderColor = Color.FromArgb(0, 0, 0);

            ToggleSwitchButtonCheckedFillColor = Color.FromArgb(255, 3, 0);
            ToggleSwitchButtonUncheckedFillColor = Color.FromArgb(210, 210, 210);

            FullRedButtonColor = Color.FromArgb(255, 3, 0);
            FullRedButtonHoverColor = Color.FromArgb(195, 3, 0);
            FullRedButtonCheckedColor = Color.FromArgb(175, 3, 0);

            FullGreenButtonColor = Color.FromArgb(49, 178, 77);
            FullGreenButtonHoverColor = Color.FromArgb(49, 198, 7);
            FullGreenButtonCheckedColor = Color.FromArgb(39, 178, 57);

            BorderRedButtonFillColor = Color.FromArgb(255, 255, 255);
            BorderRedButtonFillColor2 = Color.FromArgb(255, 255, 255);
            BorderRedButtonForeColor = Color.FromArgb(255, 3, 0);
            BorderRedButtonBorderColor = Color.FromArgb(255, 3, 0);
            BorderRedButtonHoverBorderColor = Color.FromArgb(255, 3, 0);
            BorderRedButtonHoverFillColor = Color.FromArgb(255, 244, 244);
            BorderRedButtonHoverFillColor2 = Color.FromArgb(255, 244, 244);
            BorderRedButtonHoverForeColor = Color.FromArgb(255, 3, 0);
            BorderRedButtonHoverForeColor = Color.FromArgb(255, 3, 0);
            BorderRedButtonPressedColor = Color.FromArgb(200, 120, 120);

            BorderDarkGrayButtonFillColor = Color.FromArgb(255, 255, 255);
            BorderDarkGrayButtonForeColor = Color.FromArgb(90, 90, 90);
            BorderDarkGrayButtonBorderColor = Color.FromArgb(90, 90, 90);
            BorderDarkGrayButtonHoverForeColor = Color.FromArgb(50, 50, 50);
            BorderDarkGrayButtonHoverBorderColor = Color.FromArgb(50, 50, 50);
            BorderDarkGrayButtonHoverFillColor = Color.FromArgb(235, 235, 235);
            BorderDarkGrayButtonPressedFocusedColor = Color.FromArgb(210, 210, 210);

            DateTimePickerHoverBorderColor = Color.FromArgb(210, 210, 210);

            ChoosePictureBorderColor = Color.FromArgb(255, 255, 255);

            GunaToolTipForeColor = Color.FromArgb(100, 100, 100);
            GunaToolTipBorderColor = Color.FromArgb(210, 210, 210);
            GunaToolTipBackColor = Color.FromArgb(255, 255, 255);

            CloseMinimizeIconColor = Color.FromArgb(150, 150, 150);
            CloseMinimizeHoverIconColor = Color.FromArgb(120, 120, 120);

            DarkMode = false;
        }
    }
}