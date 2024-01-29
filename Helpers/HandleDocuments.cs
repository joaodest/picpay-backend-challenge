using PicpayChallenge.Exceptions;
using System.Text.RegularExpressions;

namespace PicpayChallenge.Helpers
{
    public class HandleDocuments
    {
        private string _doc = "O documento informado não é válido";

        public HandleDocuments(string doc)
        {
            _doc = FormatDocument(doc);
            if (!IsCpf(_doc) && !IsCnpj(_doc))
                throw new InvalidDocumentException($"Insira um documento válido");
           
        }
        private static string FormatDocument(string doc)
        {
           return Regex.Replace(doc, @"[\.\-\/]", "");
        }
        private static bool IsCpf(string doc)
        {
            return Regex.IsMatch(doc, @"^\d{11}$");
        }
        private static bool IsCnpj(string doc)
        {
            return Regex.IsMatch(doc, @"^\d{14}$");
        }

        public string GetDocument() => _doc;
        
    }
}
