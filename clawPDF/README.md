# clawPDF // PDFCreator 2.3 fork

Yet another PDF Printer? Yes! This PDF Printer has the intention to be completely open source.


# Tested under

- Windows Server 2016 RDS/Terminalserver
- Windows 10 x64
- Windows 8 x32/x64
- Windows 7 x32/x64


# Changelog

## v0.8.4 (2021.09.06)

The PDF printer was modified to send documents to the Vivellio project backend to be assigned to a single patient.

- Installation process:
	- German language as default.
	- Branded installation with Vivellio logo.
	- Input of a printer driver license.
	- Adding installation custom action to send an installation notification to the Vivellio project backend.
	- Application icon.
	- Renamed clawPDF output.
	- Installation package name.
- UI changes: 
	- Main Window:
		- Removal of the "Application Settings" and "Profile Settings" buttons.
		- Replaced icon.
	- PrintJobWindow:
		- Removal of the buttons: "Setting", "Collect" and "E-Mail"
		- Renamed "Save" Button to "Send" and changed it's icon.
		- The following input elements were removed: "Profile", "Metadaten", "Thema", "Password" and "Autor".
		- The following input elements were added: "Body part selection".
- Print process:
	- The document to print is sent to the Vivellio backend for processing.
	- If the result is not a single match the document is print to a default physical printer.

## v0.8.4 (2019.06.11)

- [bugfix]  unicode filename support (thx to daooze for bugreport)

## v0.8.3 (2019.05.31)

- [bugfix]  starts under System-Account
- [cleanup] migrated code from c++ to c#
- [update]  ghostscript 9.27
- [bugfix]  author metadata

## v0.8.01 (2019.02.10)

- performance boost for RDS environments

## v0.8.0 (2019.02.10)

- initial version


# Features

- print to PDF, PDF/A, PDF/X, PNG, JPEG, TIF and text
- 24 languages
- many settings
- easy to use
- easy to deploy (MSI-Installer)
- no adware, spyware, nagware
- ...


# Requirements

- .Net Framework 4.5.2+


# Screenshot

![clawpdf1](clawPDF/docs/images/clawpdf1.png?raw=true "clawpdf1")
![clawpdf2](clawPDF/docs/images/clawpdf2.png?raw=true "clawpdf2")
![clawpdf3](clawPDF/docs/images/clawpdf3.png?raw=true "clawpdf3")


# Roadmap

- keep ghostscript up to date (continuous process)
- add more features
- remove DataStorage.dll, DynamicTranslator.dll and TrueTypeFontInfo.dll
- rewrite group policy settings
- batch printing


# Build

- Visual Studio 2019


# Third-party

## clawPDF uses the following licensed software or parts of the source code:

- main code: PDFCreator 2.3 (https://github.com/pdfforge/PDFCreator), licensed under AGPL v3 license.
- PDF library: iTextSharp 5.5.13 (https://github.com/itext/itextsharp), licensed under AGPL v3 license.
- logging: Nlog 4.5.11 (https://github.com/NLog/NLog), licensed under BSD 3-Clause.
- parts of the ghostscript control: PdfScribe 1.0.6 (https://github.com/stchan/PdfScribe), licensed under AGPL v3 license.
- redirection Port Monitor: clawmon (https://github.com/clawsoftware/clawmon), licensed under GPL v2 license.
- Postscript Printer Drivers: Microsoft Postscript Printer Driver V3 (https://docs.microsoft.com/en-us/windows-hardware/drivers/print/microsoft-postscript-printer-driver), copyright (c) Microsoft Corporation. All rights reserved.
- Postscript and PDF interpreter/renderer: Ghostscript 9.27 (https://www.ghostscript.com/download/gsdnld.html), licensed under AGPL v3 license.
- SystemWrapper 0.25.0.186 (https://github.com/jozefizso/SystemWrapper), licensed under Microsoft Public license.
- ftplib 1.0.1.1 (https://archive.codeplex.com/?p=ftplib), licensed under MIT license.
- DataStorage.dll, licensed under pdfforge Freeware License.
- DynamicTranslator.dll, licensed under pdfforge Freeware License.
- TrueTypeFontInfo.dll, licensed under pdfforge Freeware License.
- appbar_save (http://modernuiicons.com/), licensed under Attribution-NoDerivs 3.0 Unported.
- appbar_cogs (http://modernuiicons.com/), licensed under Attribution-NoDerivs 3.0 Unported.
- appbar_page_file_pdf (http://modernuiicons.com/), licensed under Attribution-NoDerivs 3.0 Unported.


# License

clawPDF is licensed under AGPL v3 license<br>
Copyright (C) 2019 // Andrew Hess // clawSoft