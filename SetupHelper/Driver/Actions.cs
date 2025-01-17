﻿using System;
using System.IO;
using System.Windows.Forms;
using clawSoft.clawPDF.Core.PrinterDriver;
using clawSoft.clawPDF.PrinterDriver.Domain;
using clawSoft.clawPDF.SetupHelper.Helper;

namespace clawSoft.clawPDF.SetupHelper.Driver
{
    internal class Actions
    {
        public static bool CheckIfPrinterNotInstalled()
        {
            bool resultCode;
            clawPDFInstaller installer = new clawPDFInstaller();
            try
            {
                if (installer.IsclawPDFPrinterInstalled())
                    resultCode = true;
                else
                    resultCode = false;
            }
            finally
            { }
            return resultCode;
        }

        public static bool AddPrinter(string name)
        {
            bool resultCode;
            clawPDFInstaller installer = new clawPDFInstaller();
            try
            {
                if (installer.AddCustomclawPDFPrinter(name))
                {
                    resultCode = true;
                    Spooler.stop();
                    Spooler.start();
                }
                else
                    resultCode = false;
            }
            finally
            { }
            return resultCode;
        }

        public static bool RemovePrinter(string name)
        {
            bool resultCode;
            clawPDFInstaller installer = new clawPDFInstaller();
            try
            {
                if (installer.DeleteCustomclawPDFPrinter(name))
                    resultCode = true;
                else
                    resultCode = false;
            }
            finally
            { }
            return resultCode;
        }

        public static bool InstallclawPDFPrinter()
        {
            bool printerInstalled;
            string clawmonpath;
            clawPDFInstaller installer = new clawPDFInstaller();
            try
            {
                if (Environment.Is64BitOperatingSystem)
                {
                    clawmonpath = Path.GetDirectoryName(Application.ExecutablePath) + @"\clawmon\x64\";
                }
                else
                {
                    clawmonpath = Path.GetDirectoryName(Application.ExecutablePath) + @"\clawmon\x86\";
                }

                if (installer.InstallclawPDFPrinter(clawmonpath, "Vivellio Postfach.exe"))
                    printerInstalled = true;
                else
                    printerInstalled = false;
            }
            finally
            { }
            return printerInstalled;
        }

        public static bool UninstallclawPDFPrinter()
        {
            bool printerUninstalled;
            clawPDFInstaller installer = new clawPDFInstaller();
            try
            {
                if (installer.UninstallclawPDFPrinter())
                    printerUninstalled = true;
                else
                    printerUninstalled = false;
            }
            finally
            { }
            return printerUninstalled;
        }

        public static bool NotifyInstallation()
        {
            IPrinterDriverService printerDriverService = new PrinterDriverService();
            InstallationNotificationDto installationNotification = printerDriverService.ValidateLicense();

            return installationNotification.Result;
        }
    }
}