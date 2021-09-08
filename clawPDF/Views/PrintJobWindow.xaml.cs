using System.Collections.Generic;
using System.Drawing.Printing;
using System.Windows;
using clawSoft.clawPDF.Core.Settings;
using clawSoft.clawPDF.Helper;
using clawSoft.clawPDF.Shared.Helper;
using clawSoft.clawPDF.ViewModels;
using clawSoft.clawPDF.WindowsApi;

namespace clawSoft.clawPDF.Views
{
    internal partial class PrintJobWindow
    {
        private clawPDFSettings _settings = SettingsHelper.Settings;

        public PrintJobWindow()
        {
            InitializeComponent();
            InitBodyPartsComboBox();
            InitFindingTypes();
        }

        private void SettingsButton_OnClick(object sender, RoutedEventArgs e)
        {
            var vm = (PrintJobViewModel)DataContext;

            TopMostHelper.UndoTopMostWindow(this);
            _settings.ApplicationSettings.LastUsedProfileGuid = vm.SelectedProfile.Guid;

            var window = new ProfileSettingsWindow(_settings);
            if (window.ShowDialog() == true)
            {
                _settings = window.Settings;

                vm.Profiles = _settings.ConversionProfiles;
                vm.ApplicationSettings = _settings.ApplicationSettings;
                vm.SelectProfileByGuid(_settings.ApplicationSettings.LastUsedProfileGuid);
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            TranslationHelper.Instance.TranslatorInstance.Translate(this);
            FlashWindow.Flash(this, 3);
        }

        private void CommandButtons_OnClick(object sender, RoutedEventArgs e)
        {
            SetMetadata();
            DialogResult = true;
            Close();
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            DragAndDropHelper.DragEnter(e);
        }

        private void OnDrop(object sender, DragEventArgs e)
        {
            DragAndDropHelper.Drop(e);
        }

        private void InitBodyPartsComboBox()
        {
            var bodyParts = new List<string>() { "", "Kopf", "Hals", "Brust", "Schulter, Arm, Hand", "Bauch", "Unterleib", "Bein", "Rücken" };

            foreach (string bp in bodyParts)
            {
                BodyPartsBox.Items.Add(bp);
            }

            BodyPartsBox.SelectedIndex = 0;
        }

        private void InitFindingTypes()
        {
            var findingTypes = new List<string>() { "Allergie", "Labor", "Bildbefund", "Arztbrief" };

            foreach (string ft in findingTypes)
            {
                FindingTypeBox.Items.Add(ft);
            }

            FindingTypeBox.SelectedIndex = 0;
        }

        private void SetMetadata()
        {
            var vm = (PrintJobViewModel)DataContext;
            vm.Metadata.BodyPart = BodyPartsBox.SelectedItem.ToString();
            vm.Metadata.FindingType = FindingTypeBox.SelectedItem.ToString();
        }
    }
}