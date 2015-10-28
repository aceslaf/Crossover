using Aspose.Pdf;
using Aspose.Pdf.Generator;
using CrossoverTask.Data.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossoverTask.Service.Pdf
{
    class ConvictPdfGenerator
    {
        #region Templates
        //Idealy this should be handled by a rendering engine like Razor and them converted to pdf via aspose htmlToPdf
        //This is just to demonstrate the concept since the only available license is the free one
        private const string convictInfoTemplate = "Name {0} \n Age {1} \n Gender {2} \n Height {3} \n Weight {4} \n\n";
        private const string crimeTemplate = "Crime name:{0} \n Description:{1} \n Severity:{2}";
        #endregion

        /// <summary>
        /// Creates a pdf representation for a convict and saves it to the output stream
        /// </summary>
        /// <param name="convict">The input data that is to be rendered</param>
        /// <param name="output">An output stream to hold the resulting pdf.This stream's disposal should be managed externaly</param>
        public void CreatePdf(Convict convict, Stream output)
        {
            var pdf = new Aspose.Pdf.Generator.Pdf();
            RenderConvictToPdf(convict,pdf);
            pdf.Save(output);
        }
        
        private void InsertParagraph(string text, Section section)
        {
            var textElement = new Text(section, text);
            section.Paragraphs.Add(textElement);
        }

        private void RenderConvictToPdf(Convict convict, Aspose.Pdf.Generator.Pdf pdf)
        {
            var section = pdf.Sections.Add();
            RenderPersonInfoToSection(convict, section);           
            foreach (var crime in convict.Crimes)
            {
                RenderCrimeInfoToSection(crime,section);
            }
        }
        private void RenderPersonInfoToSection(Convict convict, Section section)
        {
            var sb = new StringBuilder();
            sb.Append("\n");
            sb.Append("\n");
            AppendLineBreak(sb);
            sb.Append(string.Format(convictInfoTemplate, convict.FullName,
                                                      DateTime.Now.Year - convict.DateOfBirth.Year,
                                                      Enum.GetName(typeof(Gender), convict.Gender),
                                                      convict.Height,
                                                      convict.Weight));
            AppendLineBreak(sb);
            InsertParagraph(sb.ToString(), section);
        }

        private void RenderCrimeInfoToSection(Crime crime, Section section)
        {
            var sb = new StringBuilder();
            AppendLineBreak(sb);
            sb.Append(string.Format(crimeTemplate, crime.CrimeName,
                                                crime.Description,
                                                crime.Severity));
            AppendLineBreak(sb);
            InsertParagraph(sb.ToString(), section);
        }

        private void AppendLineBreak(StringBuilder sb)
        {
            sb.Append("\n");
            sb.Append("__________________________________________________________");
            sb.Append("\n");
        }
        
    }
}
