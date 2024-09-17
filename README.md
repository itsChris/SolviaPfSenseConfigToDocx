# pfSense Config to Docx Converter

This project is a C# WPF application that reads, parses, and exports pfSense configuration data from a **config.xml** file into a structured **Docx** document. It uses the **Open XML SDK** for document creation and provides a clear, human-readable export of your firewall and network configurations, including system settings, firewall rules, VPN settings, user accounts, and more.

## Features
- **Complete XML Parsing**: All elements and attributes in the **config.xml** file are parsed, ensuring that no data is lost.
- **Export to Docx**: Generate a well-formatted Word document with headings and subheadings for each section, including:
  - System Configurations
  - User and Group Configurations
  - Interfaces
  - DHCP Settings
  - Firewall Rules and NAT
  - VPN Configurations (IPsec, OpenVPN)
  - Certificates and Certificate Authorities
  - Static Routes
  - Other Configurations (Packages, SNMP, etc.)
- **WPF User Interface**: A simple, user-friendly interface to load the **config.xml** file, view parsed data, and export it to a Word document.
- **Search Functionality**: Easily search through the parsed data in the WPF interface.
- **Error Handling**: Logs and handles missing or malformed sections from the XML file.
  
