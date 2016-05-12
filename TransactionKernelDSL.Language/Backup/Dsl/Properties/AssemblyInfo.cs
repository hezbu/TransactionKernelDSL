#region Using directives

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.ConstrainedExecution;

#endregion

//
// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
//
[assembly: AssemblyTitle(@"")]
[assembly: AssemblyDescription(@"")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(@"HZ - UNLP")]
[assembly: AssemblyProduct(@"TransactionKernelDSL.Framework.Language")]
[assembly: AssemblyCopyright("")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: System.Resources.NeutralResourcesLanguage("en")]

//
// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:

[assembly: AssemblyVersion(@"1.0.0.0")]
[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]
[assembly: ReliabilityContract(Consistency.MayCorruptProcess, Cer.None)]

//
// Make the Dsl project internally visible to the DslPackage assembly
//
[assembly: InternalsVisibleTo(@"TransactionKernelDSL.Framework.Language.DslPackage, PublicKey=0024000004800000940000000602000000240000525341310004000001000100F7B9480A7A637B7CD4D02AEF4C2ECC19A16E95CA003BE02A0784DE15015FA80D3AAA3B4AA465DC7CC371F30373BC97FC37BF65741C003418E4D4AA33E0D4A0CECB6369C2520CA3E2E825D6FC2E2A59D4C4E2E02670CD9825BC4FC1C337AB9355156F8FD97DDE19D27704424D9023742B5E5AF7954143B45367BD136CD55842B8")]