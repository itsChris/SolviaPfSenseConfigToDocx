using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SolviaPfSenseConfigToDocx.Helpers
{
    public static class CertificateHelper
    {
        public static string DecodeCertificate(byte[] certificateBytes)
        {
            try
            {
                // Load the certificate
                X509Certificate2 certificate = new X509Certificate2(certificateBytes);

                // StringBuilder to hold the decoded information
                StringBuilder decodedInfo = new StringBuilder();

                // Add relevant certificate information
                decodedInfo.AppendLine("Subject: " + certificate.Subject);
                decodedInfo.AppendLine("Issuer: " + certificate.Issuer);
                decodedInfo.AppendLine("Valid From: " + certificate.NotBefore);
                decodedInfo.AppendLine("Valid Until: " + certificate.NotAfter);
                decodedInfo.AppendLine("Serial Number: " + certificate.SerialNumber);
                decodedInfo.AppendLine("Thumbprint: " + certificate.Thumbprint);
                decodedInfo.AppendLine("Signature Algorithm: " + certificate.SignatureAlgorithm.FriendlyName);

                // Append extensions
                foreach (X509Extension extension in certificate.Extensions)
                {
                    decodedInfo.AppendLine($"Extension: {extension.Oid.FriendlyName} ({extension.Oid.Value})");
                    decodedInfo.AppendLine("Critical: " + extension.Critical);
                }

                return decodedInfo.ToString();
            }
            catch (Exception ex)
            {
                // Log and return error message
                Console.WriteLine($"Error decoding certificate: {ex.Message}");
                return "Error decoding certificate.";
            }
        }

        public static string DecodeCertificate(string filePath)
        {
            try
            {
                // Load the certificate from file
                byte[] certificateBytes = System.IO.File.ReadAllBytes(filePath);
                return DecodeCertificate(certificateBytes);
            }
            catch (Exception ex)
            {
                // Log and return error message
                Console.WriteLine($"Error reading certificate file: {ex.Message}");
                return "Error reading certificate file.";
            }
        }

        public static string DecodeBase64ToCertificateString(string base64EncodedString)
        {
            try
            {
                // Step 1: Decode the base64 encoded string to a byte array
                byte[] certificateBytes = Convert.FromBase64String(base64EncodedString);

                // Step 2: Load the decoded byte array into an X509Certificate2 object
                X509Certificate2 certificate = new X509Certificate2(certificateBytes);

                // Step 3: StringBuilder to hold the decoded certificate information
                StringBuilder decodedInfo = new StringBuilder();

                // Add relevant certificate information
                decodedInfo.AppendLine("Subject: " + certificate.Subject);
                decodedInfo.AppendLine("Issuer: " + certificate.Issuer);
                decodedInfo.AppendLine("Valid From: " + certificate.NotBefore);
                decodedInfo.AppendLine("Valid Until: " + certificate.NotAfter);
                decodedInfo.AppendLine("Serial Number: " + certificate.SerialNumber);
                decodedInfo.AppendLine("Thumbprint: " + certificate.Thumbprint);
                decodedInfo.AppendLine("Signature Algorithm: " + certificate.SignatureAlgorithm.FriendlyName);

                // Append extensions if any
                foreach (X509Extension extension in certificate.Extensions)
                {
                    decodedInfo.AppendLine($"Extension: {extension.Oid.FriendlyName} ({extension.Oid.Value})");
                    decodedInfo.AppendLine("Critical: " + extension.Critical);
                }

                return decodedInfo.ToString();
            }
            catch (FormatException ex)
            {
                // Handle Base64 format issues
                Console.WriteLine($"Error decoding base64 string: {ex.Message}");
                return "Error decoding base64 string.";
            }
            catch (Exception ex)
            {
                // Handle other general errors
                Console.WriteLine($"Error decoding certificate: {ex.Message}");
                return "Error decoding certificate.";
            }
        }

        public static X509Certificate2 DecodeBase64ToCertificate(string base64EncodedString)
        {
            try
            {
                // Step 1: Decode the base64 encoded string to a byte array
                byte[] certificateBytes = Convert.FromBase64String(base64EncodedString);

                // Step 2: Load the decoded byte array into an X509Certificate2 object
                X509Certificate2 certificate = new X509Certificate2(certificateBytes);

                return certificate;
            }
            catch { 
            return null;
            }
        }
    }
}
