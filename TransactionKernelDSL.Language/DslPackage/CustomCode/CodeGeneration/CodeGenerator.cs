using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TextTemplating.VSHost;
using System.IO;
using TransactionKernelDSL.Framework.Language.CustomCode.CodeGeneration;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Microsoft.VisualStudio.TextTemplating;
using System.Runtime.Remoting.Messaging;

namespace TransactionKernelDSL.Framework.Language
{
    [global::System.Runtime.InteropServices.Guid("6613FE18-21CC-4269-8F1C-16EC0542D0E6")]
    public class CodeGenerator : TemplatedCodeGenerator
    {
        private List<byte[]> _CSharpTemplates = null;
        private List<byte[]> _ConfigTemplates = null;
        private List<byte[]> _CustomCodeCSharpTemplates = null;
        private List<byte[]> _SqlServerScriptTemplates = null;
        private List<byte[]> _ProcessedTemplates = null;

        public CodeGenerator()
            : base()
        {
            _CSharpTemplates = new List<byte[]> 
                        {
                            CodeGenerationResource.TransactionFacadeGenerator,
                            CodeGenerationResource.TransactionEngineGenerator,
                            CodeGenerationResource.TransactionHandlerGenerator,
                            CodeGenerationResource.TransactionContextGenerator,
                            CodeGenerationResource.TransactionInputEngineUnitTestGenerator,

                            CodeGenerationResource.TransactionPropietaryParserGenerator,
                            CodeGenerationResource.TransactionPropietaryParserStructureGenerator,
                            CodeGenerationResource.TransactionPropietaryParserFieldGenerator,
                            CodeGenerationResource.TransactionPropietaryParserStreamGenerator,
                            CodeGenerationResource.TransactionPropietaryParser_UnitTestGenerator,
                        };

            _ConfigTemplates = new List<byte[]>
                        {
                            CodeGenerationResource.TransactionConfigGenerator
                        };

            _CustomCodeCSharpTemplates = new List<byte[]>
                        {
                            CodeGenerationResource.TransactionHandlerForwardingUserCustomizedCodeGenerator,
                            CodeGenerationResource.TransactionHandlerMaintenanceUserCustomizedCodeGenerator,
                            CodeGenerationResource.TransactionHandlerSQLDataSourceUserCustomizedCodeGenerator,
                            CodeGenerationResource.TransactionHandlerListeningUserCustomizedCodeGenerator,
                            CodeGenerationResource.TransactionHandlerOutputWebServiceUserCustomizedCodeGenerator,
                            CodeGenerationResource.TransactionFacadeUserCustomizedCodeGenerator,
                            CodeGenerationResource.TransactionContextUserCustomizedCodeGenerator,

                            CodeGenerationResource.TransactionInputEngineInputUnitTestGenerator,
                            CodeGenerationResource.TransactionInputEngineOutputUnitTestGenerator,
                            CodeGenerationResource.TransactionInputEngineUserCustomizedCodeGenerator,

                            CodeGenerationResource.TransactionOutputEngineUserCustomizedCodeGenerator,
                            CodeGenerationResource.TransactionInputEngineUnitTestUserCustomizedCodeGenerator,

                            CodeGenerationResource.TransactionPropietaryParser_UserCustomizedCodeGenerator,
                            CodeGenerationResource.TransactionPropietaryParserStructure_UserCustomizedCodeGenerator,
                            CodeGenerationResource.TransactionPropietaryParserField_UserCustomizedCodeGenerator,
                            CodeGenerationResource.TransactionPropietaryParserStream_UserCustomizedCodeGenerator,
                            CodeGenerationResource.TransactionPropietaryParser_InputUnitTest_UserCustomizedCodeGenerator
                        };

            _SqlServerScriptTemplates = new List<byte[]>
                        {
                            CodeGenerationResource.TransactionEngineScriptGenerator,
                            CodeGenerationResource.TransactionEngineScriptGenerator_SatelliteInstances,
                            CodeGenerationResource.TransactionFacadeScriptGenerator,
                            CodeGenerationResource.TransactionFacadeScriptGenerator_SatelliteInstances,                            
                            CodeGenerationResource.TransactionOutputWebServiceScriptGenerator,
                            CodeGenerationResource.TransactionOutputWebServiceScriptGenerator_SatelliteInstances,
                            CodeGenerationResource.TransactionHandlerSQLDataSourceScriptGenerator
                        };

            _ProcessedTemplates = new List<byte[]>();
        }

        protected override byte[] GenerateCode(string inputFileName, string inputFileContent)
        {
            // Replace the supplied file contents with the template we want to run 
            inputFileContent = ASCIIEncoding.UTF8.GetString(CodeGenerationResource.CodeGeneratorTemplate);
            // Substitute the name of the current model file into the template. 
            FileInfo fi = new FileInfo(inputFileName);

            foreach (byte[] template in _CSharpTemplates)
            {
                CallContext.LogicalSetData("nextTemplate", ASCIIEncoding.UTF8.GetString(template).Replace("Test.TrnxDsl", fi.Name));
                CallContext.LogicalSetData("modelFile", fi.Name);
                CallContext.LogicalSetData("fileExtension", ".cs");
                CallContext.LogicalSetData("overwritesFile", true);
                // Now just delegate the rest of the work to the base class 
                byte[] data;
                data = base.GenerateCode(inputFileName, inputFileContent);
                byte[] ascii = new byte[data.Length - 3];
                Array.Copy(data, 3, ascii, 0, data.Length - 3);

                _ProcessedTemplates.Add(ascii);
            }

            foreach (byte[] template in _CustomCodeCSharpTemplates)
            {
                CallContext.LogicalSetData("nextTemplate", ASCIIEncoding.UTF8.GetString(template).Replace("Test.TrnxDsl", fi.Name));
                CallContext.LogicalSetData("modelFile", fi.Name);
                CallContext.LogicalSetData("fileExtension", ".cs");
                CallContext.LogicalSetData("overwritesFile", false);
                // Now just delegate the rest of the work to the base class 
                byte[] data;
                data = base.GenerateCode(inputFileName, inputFileContent);
                byte[] ascii = new byte[data.Length - 3];
                Array.Copy(data, 3, ascii, 0, data.Length - 3);

                _ProcessedTemplates.Add(ascii);
            }

            foreach (byte[] template in _ConfigTemplates)
            {
                CallContext.LogicalSetData("nextTemplate", ASCIIEncoding.UTF8.GetString(template).Replace("Test.TrnxDsl", fi.Name));
                CallContext.LogicalSetData("modelFile", fi.Name);
                CallContext.LogicalSetData("fileExtension", ".config");
                CallContext.LogicalSetData("overwritesFile", false);
                // Now just delegate the rest of the work to the base class 
                byte[] data;
                data = base.GenerateCode(inputFileName, inputFileContent);
                byte[] ascii = new byte[data.Length - 3];
                Array.Copy(data, 3, ascii, 0, data.Length - 3);

                _ProcessedTemplates.Add(ascii);
            }

            foreach (byte[] template in _SqlServerScriptTemplates)
            {
                CallContext.LogicalSetData("nextTemplate", ASCIIEncoding.UTF8.GetString(template).Replace("Test.TrnxDsl", fi.Name));
                CallContext.LogicalSetData("modelFile", fi.Name);
                CallContext.LogicalSetData("fileExtension", ".sql");
                CallContext.LogicalSetData("overwritesFile", true);
                // Now just delegate the rest of the work to the base class 
                byte[] data;
                data = base.GenerateCode(inputFileName, inputFileContent);
                byte[] ascii = new byte[data.Length - 3];
                Array.Copy(data, 3, ascii, 0, data.Length - 3);

                _ProcessedTemplates.Add(ascii);
            }

            Debug.WriteLine("All processed OK, exiting...");
            return this.Combine(_ProcessedTemplates.ToArray());
        }

        private byte[] Combine(params byte[][] arrays)
        {
            byte[] rv = new byte[arrays.Sum(a => a.Length)];
            int offset = 0;
            foreach (byte[] array in arrays)
            {
                System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                offset += array.Length;
            }
            return rv;
        }
    }
}
