<?xml version="1.0" encoding="utf-8"?>
<Dsl xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="93da2ac6-c95d-4f6b-ba78-30531b1e1250" Description="Transaction Kernel DSL" Name="TrnxDsl" DisplayName="Transaction Kernel DSL" HelpKeyword="1" Namespace="TransactionKernelDSL.Framework.Language" ProductName="TramsactionKernelDSL.Framework.Language" CompanyName="HZ - UNLP" PackageGuid="5baeeeac-b2c5-4218-965b-88e4d10181bb" PackageNamespace="TransactionKernelDSL.Framework.Language" xmlns="http://schemas.microsoft.com/VisualStudio/2005/DslTools/DslDefinitionModel">
  <Classes>
    <DomainClass Id="2b1c79a1-95d7-4cc7-b958-107b1d7029a7" Description="The root in which all other elements are embedded. Appears as a diagram." Name="TransactionModel" DisplayName="Transaction Model" Namespace="TransactionKernelDSL.Framework.Language">
      <Properties>
        <DomainProperty Id="43282d0a-1a82-477d-aa3e-9e1d4de95fc1" Description="Solution's name" Name="Name" DisplayName="Name" Category="Setup">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="c982fae7-3fd2-4a1e-bdbb-9ddc3064ef22" Description="Value to uniquely identify the solution within others" Name="InstanceId" DisplayName="Instance Id" DefaultValue="0" Category="Setup">
          <Type>
            <ExternalTypeMoniker Name="/System/Int16" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="ece9338b-6cf2-408c-b753-d49a4da16408" Description="Base namespace that will be used in every generated code file" Name="Namespace" DisplayName="Namespace" Category="Setup">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="5801b1e8-f032-407a-b536-9d4c9fa6b4d4" Description="GetValue() must be implemented somehow in order to catch initial parameters for the solution to work." Name="GetValueImplementationType" DisplayName="Get Value Implementation Type" DefaultValue="NotTyped" Category="Data Source Setup">
          <Type>
            <DomainEnumerationMoniker Name="PersistableSourceType" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="b88fc4bb-f518-4178-974b-022ddde56f47" Description="SequenceFactory() must be implemented somehow in order to generate trace number for entrying transactions." Name="SequenceFactoryImplementationType" DisplayName="Sequence Factory Implementation Type" DefaultValue="NotTyped" Category="Data Source Setup">
          <Type>
            <DomainEnumerationMoniker Name="PersistableSourceType" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="30980775-0bc6-40c4-b53e-0fa23592c393" Description="Description for TransactionKernelDSL.Framework.Language.TransactionModel.Database Server Instance" Name="DatabaseServerInstance" DisplayName="Database Server Instance" Category="Database Setup">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="9f6c2dbe-90d9-486e-8ba2-1d354ab2ef4e" Description="Description for TransactionKernelDSL.Framework.Language.TransactionModel.Database Initial Catalog" Name="DatabaseInitialCatalog" DisplayName="Database Initial Catalog" Category="Database Setup">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="6d6efdee-1e43-4f64-a4de-0a104009e47d" Description="Description for TransactionKernelDSL.Framework.Language.TransactionModel.Database User" Name="DatabaseUser" DisplayName="Database User" Category="Database Setup">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="29820332-cb8c-405d-9b16-18d0a6e53bf1" Description="Description for TransactionKernelDSL.Framework.Language.TransactionModel.Database Password" Name="DatabasePassword" DisplayName="Database Password" Category="Database Setup">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="2e207304-3670-4c6b-9dc2-8f607ba3da5e" Description="It is a pipe-separated array of instance numbers, which will be used to " Name="SatelliteInstances" DisplayName="Satellite Instances" Category="Misc">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="894452f0-ef45-4563-9374-5d51eb41a777" Description="Version associated with the script that will be generated automatically by the transformation tool" Name="ScriptVersion" DisplayName="Script Version" DefaultValue="1">
          <Type>
            <ExternalTypeMoniker Name="/System/Int16" />
          </Type>
        </DomainProperty>
      </Properties>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="TransactionLayer" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>TransactionModelHasTransactionLayers.TransactionLayers</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="TransactionEnvironmentLayer" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>TransactionModelHasTransactionEnvironmentLayer.TransactionEnvironmentLayer</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="1cdb00e4-ec80-43a4-8912-07403efeeaf5" Description="Elements embedded in the model. Appear as boxes on the diagram." Name="TransactionHandler" DisplayName="Transaction Handler" Namespace="TransactionKernelDSL.Framework.Language">
      <Properties>
        <DomainProperty Id="bbcbe939-4a13-46fe-a37e-1db127d24fcf" Description="Handler's Name" Name="Name" DisplayName="Name" DefaultValue="TransactionName" Category="Setup" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="30725a1a-c7dd-467b-a94e-c88aa629b4d3" Description="Transaction Id that will be the main identifier of the handler." Name="TransactionId" DisplayName="Transaction Id" Category="Setup">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="1a449a15-8e57-4417-859c-1ccf23ab2370" Description="Logger's name used within this transaction handler" Name="LoggerName" DisplayName="Logger Name" DefaultValue="MainLogger" Category="Log Setup">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="e9b57f91-7da8-49b1-b917-7f43efffb581" Description="Description for TransactionKernelDSL.Framework.Language.TransactionLayer" Name="TransactionLayer" DisplayName="Transaction Layer" Namespace="TransactionKernelDSL.Framework.Language">
      <Properties>
        <DomainProperty Id="1890c134-1751-495f-8f89-f10b20e54ed9" Description="Layer's name. It is only figurative." Name="Name" DisplayName="Name" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="2b765f2a-b6c7-42f4-adea-fd0353a788a0" Description="Is a value that means the order of a layer. Lower levels mean layers involved catching a requirement, and higher levels mean layers involved in forwarding/resolving the transaction" Name="Level" DisplayName="Level">
          <Type>
            <ExternalTypeMoniker Name="/System/Int16" />
          </Type>
        </DomainProperty>
      </Properties>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="TransactionHandler" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>TransactionLayerHasTransactionHandlers.TransactionHandlers</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="InputTransactionEngine" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>TransactionLayerHasInputTransactionEngines.InputTransactionEngines</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="OutputTransactionEngine" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>TransactionLayerHasOutputTransactionEngines.OutputTransactionEngines</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="TransactionSQLServerDataSource" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>TransactionLayerHasTransactionSQLServerDataSources.TransactionSQLServerDataSources</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="OutputTransactionWebService" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>TransactionLayerHasOutputTransactionWebServices.OutputTransactionWebServices</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="TimeTrigger" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>TransactionLayerHasTimeTriggers.TimeTriggers</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="TransactionDataSourceSupport" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>TransactionLayerHasTransactionDataSourceSupports.TransactionDataSourceSupports</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="2d46088e-0b4e-43d2-8cac-17dfdd93f58b" Description="Description for TransactionKernelDSL.Framework.Language.InputTransactionEngine" Name="InputTransactionEngine" DisplayName="Input Transaction Engine" Namespace="TransactionKernelDSL.Framework.Language">
      <BaseClass>
        <DomainClassMoniker Name="TransactionEngine" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="0798c381-96e7-420f-afa2-14287ba897ea" Description="Generates unit testing supporting code for every transaction generated from it" Name="HasUnitTests" DisplayName="Has Unit Tests">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="5b76ddce-3f9a-4ec2-b345-380d75c48cce" Description="Input Transaction Engine's Type. The type will be associated with an abstract class from TransactionKernelDSL.Framework" Name="Type" DisplayName="Type" DefaultValue="InputEngine">
          <Type>
            <DomainEnumerationMoniker Name="InputEngineType" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="08e1f725-9205-47dc-8f27-ca8edeefaba1" Description="If true, call SequenceFactory into constructor and saves the new trace number in a Context Value" Name="GeneratesSequenceNumber" DisplayName="Generates Sequence Number" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="a7a60d2a-fabc-4674-915e-4b5973256959" Description="Description for TransactionKernelDSL.Framework.Language.OutputTransactionEngine" Name="OutputTransactionEngine" DisplayName="Output Transaction Engine" Namespace="TransactionKernelDSL.Framework.Language">
      <BaseClass>
        <DomainClassMoniker Name="TransactionEngine" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="8820ceaf-1ad0-4865-b8e7-7222cda2217b" Description="Output Transaction Engine's Type. The type is associated with an abstract class from TransactionKernelDSL.Framework with built-in functionalities" Name="Type" DisplayName="Type" DefaultValue="OutputEngine">
          <Type>
            <DomainEnumerationMoniker Name="OutputEngineType" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="02a2fbab-6580-4776-83a8-4a4972016491" Description="Name of the logger used within the engine. Can be the same as MainLogger, but if intended, it can separetely log in another file." Name="EngineLoggerName" DisplayName="Engine Logger Name" DefaultValue="MainLogger" Category="Associated Logger">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="8c02853b-6e81-4143-bb83-7d3fa3f82135" Description="Description for TransactionKernelDSL.Framework.Language.TransactionEngine" Name="TransactionEngine" DisplayName="Transaction Engine" Namespace="TransactionKernelDSL.Framework.Language">
      <Properties>
        <DomainProperty Id="1f4815c4-8e5b-4bd0-b561-e27bf2dbaeef" Description="Description for TransactionKernelDSL.Framework.Language.TransactionEngine.Name" Name="Name" DisplayName="Name" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="abcd17b8-c520-4eab-bc9e-3b785969abe4" Description="If true, it starts within Facade's Start() method." Name="AutoStarts" DisplayName="Auto Starts" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="7e7c94ef-7626-4989-b890-ed7cbf6b8fe2" Description="Type of the parser that will be used" Name="Parser" DisplayName="Parser" DefaultValue="NotTyped" Category="Associated Parser">
          <Type>
            <DomainEnumerationMoniker Name="ParserType" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="44220b01-927e-4a7a-a2fa-cac24ce364ee" Description="Parser Section's name, that will be linked with a section in app.config with the same name" Name="ParserSectionName" DisplayName="Parser Section Name" DefaultValue="" Category="Associated Parser">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="5fee9d95-044d-4e6d-ab3e-160fe56fc1fd" Description="MainLogger's name. By default is &quot;MainLogger&quot;" Name="LoggerName" DisplayName="Logger Name" DefaultValue="MainLogger" Category="Associated Logger">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="f2cfed51-2909-45e8-b5db-8c1ebee4be20" Description="Logger's name used within the parser" Name="ParserLoggerName" DisplayName="Parser Logger Name" DefaultValue="MainLogger" Category="Associated Parser">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="99be29ec-84a3-41e1-bd12-def751d6218d" Description="Description for TransactionKernelDSL.Framework.Language.TransactionSQLServerDataSource" Name="TransactionSQLServerDataSource" DisplayName="Transaction SQLServer Data Source" Namespace="TransactionKernelDSL.Framework.Language">
      <BaseClass>
        <DomainClassMoniker Name="TransactionDataSource" />
      </BaseClass>
      <Properties>
        <DomainProperty Id="8c1c1cc5-5a06-4330-ac33-638f9d99d90a" Description="Description for TransactionKernelDSL.Framework.Language.TransactionSQLServerDataSource.Instance Name" Name="InstanceName" DisplayName="Instance Name" Category="SQLServer Data Source Setup">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="dde512aa-73d3-4b63-a07a-371e40188fca" Description="Description for TransactionKernelDSL.Framework.Language.TransactionSQLServerDataSource.Database Name" Name="DatabaseName" DisplayName="Database Name" Category="SQLServer Data Source Setup">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="42d77b18-62a0-4bf6-8b58-a9e4a107aeea" Description="Description for TransactionKernelDSL.Framework.Language.TransactionSQLServerDataSource.Connection String" Name="ConnectionString" DisplayName="Connection String" Category="SQLServer Data Source Setup">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="89681efa-3012-42c3-90f1-8f1658acf606" Description="Description for TransactionKernelDSL.Framework.Language.TransactionSQLServerDataSource.Stored Procedure" Name="StoredProcedure" DisplayName="Stored Procedure" Category="Data Setup">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="7d2af7fe-b89f-489c-8bc7-f171e076dcd4" Description="Description for TransactionKernelDSL.Framework.Language.TransactionSQLServerDataSource.Executed On Stage" Name="ExecutedOnStage" DisplayName="Executed On Stage" DefaultValue="PreProcessStage" Category="Data Setup">
          <Type>
            <DomainEnumerationMoniker Name="PersistableStage" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="148da126-f44a-4214-8710-7aae812bef32" Description="Description for TransactionKernelDSL.Framework.Language.TransactionDataSource" Name="TransactionDataSource" DisplayName="Transaction Data Source" Namespace="TransactionKernelDSL.Framework.Language">
      <Properties>
        <DomainProperty Id="c7ffb06f-9807-4fbd-9e51-6c6fe96c009f" Description="Description for TransactionKernelDSL.Framework.Language.TransactionDataSource.Name" Name="Name" DisplayName="Name" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="b38e96dc-9237-4794-a16b-01499a2d5504" Description="Description for TransactionKernelDSL.Framework.Language.TransactionDataSource.Step Order" Name="StepOrder" DisplayName="Step Order" DefaultValue="1">
          <Type>
            <ExternalTypeMoniker Name="/System/Int16" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="db7b73cc-f2a8-4df1-be9c-7ae1220d270d" Description="Description for TransactionKernelDSL.Framework.Language.TransactionWebService" Name="TransactionWebService" DisplayName="Transaction Web Service" Namespace="TransactionKernelDSL.Framework.Language">
      <Properties>
        <DomainProperty Id="2f9fff5b-9434-49bb-a2ea-c6a17ea86cbb" Description="Web Service's Name" Name="Name" DisplayName="Name" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="3e058ce2-bf79-4439-b636-ec1f2c3346df" Description="Name of the proxy class generated from the wsdl. The file with the class must be included in project" Name="WebServiceClassName" DisplayName="Web Service Class Name">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="e6bce5d9-5263-4f84-a31b-6c38d92e30fe" Description="Url of the referenced Web Service" Name="Url" DisplayName="Url">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="8bfc91f1-0ac7-4ccb-a8e8-5f8b0fbee4df" Description="Type of architecture used to implement this outpue WS" Name="ArchitecturalPattern" DisplayName="Architectural Pattern" DefaultValue="MultipleInstances" Category="Design">
          <Type>
            <DomainEnumerationMoniker Name="WebServiceArchitecturalPattern" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="a21e5234-0f88-4728-a883-038589f4aac3" Description="Technology used by remote WS implementation" Name="Technology" DisplayName="Technology" DefaultValue="SoapHttpClientProtocol" Category="Design">
          <Type>
            <DomainEnumerationMoniker Name="WebServiceTechnology" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="407dac2b-f175-4575-a1c3-641bdec4cf8b" Description="Description for TransactionKernelDSL.Framework.Language.OutputTransactionWebService" Name="OutputTransactionWebService" DisplayName="Output Transaction Web Service" Namespace="TransactionKernelDSL.Framework.Language">
      <BaseClass>
        <DomainClassMoniker Name="TransactionWebService" />
      </BaseClass>
    </DomainClass>
    <DomainClass Id="673b58bf-5573-4d87-9546-63d5b3508429" Description="Description for TransactionKernelDSL.Framework.Language.TimeTrigger" Name="TimeTrigger" DisplayName="Time Trigger" Namespace="TransactionKernelDSL.Framework.Language">
      <Properties>
        <DomainProperty Id="33e24177-1a05-4ccd-a307-d8cde49434da" Description="Trigger's Name" Name="Name" DisplayName="Name" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="fc4b337e-2ba0-40d4-b106-ca7f45780ba1" Description="Description for TransactionKernelDSL.Framework.Language.TransactionDataSourceSupport" Name="TransactionDataSourceSupport" DisplayName="Transaction Data Source Support" Namespace="TransactionKernelDSL.Framework.Language">
      <Properties>
        <DomainProperty Id="7235ea2b-af1b-4abf-a045-99c1f2138809" Description="Description for TransactionKernelDSL.Framework.Language.TransactionDataSourceSupport.Name" Name="Name" DisplayName="Name" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="02637180-8dcd-492c-8bf6-b84e4a46d839" Description="Description for TransactionKernelDSL.Framework.Language.TransactionDataSourceSupport.Support Type" Name="SupportType" DisplayName="Support Type" DefaultValue="NotTyped">
          <Type>
            <DomainEnumerationMoniker Name="DataSourceSupportType" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
    <DomainClass Id="ca2b8d4e-d613-40cc-877a-a7b9aa62ab97" Description="Description for TransactionKernelDSL.Framework.Language.TransactionEnvironmentLayer" Name="TransactionEnvironmentLayer" DisplayName="Transaction Environment Layer" Namespace="TransactionKernelDSL.Framework.Language">
      <Properties>
        <DomainProperty Id="794a615e-9c16-419d-a3d0-ba4430ee7815" Description="Description for TransactionKernelDSL.Framework.Language.TransactionEnvironmentLayer.Name" Name="Name" DisplayName="Name" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
      <ElementMergeDirectives>
        <ElementMergeDirective>
          <Index>
            <DomainClassMoniker Name="TransactionEnvironmentSQLServerVariable" />
          </Index>
          <LinkCreationPaths>
            <DomainPath>TransactionEnvironmentLayerHasTransactionEnvironmentSQLServerVariables.TransactionEnvironmentSQLServerVariables</DomainPath>
          </LinkCreationPaths>
        </ElementMergeDirective>
      </ElementMergeDirectives>
    </DomainClass>
    <DomainClass Id="9ed1d0fa-25c2-4ea5-8df7-b31d23263bb2" Description="Description for TransactionKernelDSL.Framework.Language.TransactionEnvironmentSQLServerVariable" Name="TransactionEnvironmentSQLServerVariable" DisplayName="Transaction Environment SQLServer Variable" Namespace="TransactionKernelDSL.Framework.Language">
      <Properties>
        <DomainProperty Id="3a1e9238-c60c-4558-a433-57eb14be41a9" Description="Description for TransactionKernelDSL.Framework.Language.TransactionEnvironmentSQLServerVariable.Name" Name="Name" DisplayName="Name" IsElementName="true">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="aaba969c-bc50-46b3-98c2-ae49e45a0b54" Description="Description for TransactionKernelDSL.Framework.Language.TransactionEnvironmentSQLServerVariable.Default Value" Name="DefaultValue" DisplayName="Default Value">
          <Type>
            <ExternalTypeMoniker Name="/System/String" />
          </Type>
        </DomainProperty>
      </Properties>
    </DomainClass>
  </Classes>
  <Relationships>
    <DomainRelationship Id="2a5a3857-51b3-4cd6-ab52-203102839ce3" Description="Links one handler to another in a routing or forwarding way." Name="TransactionHandlerReferencesForwarderTransactionHandlers" DisplayName="Transaction Handler References Forwarder Transaction Handlers" Namespace="TransactionKernelDSL.Framework.Language">
      <Properties>
        <DomainProperty Id="788c4c10-e4e4-4f7f-9e9c-987f799e6df2" Description="If this link is in Routing Mode, it means source handler might eventually forward to target handler, through a routing algorithm. If it isn't a Routing Link, it means source and target handlers are always linked, and links ar ordered by Sequence Order" Name="IsRoutingLink" DisplayName="Is Routing Link" DefaultValue="true" Category="Working Mode">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
        <DomainProperty Id="d8f0cf4a-4b03-47bd-aa07-08219057270b" Description="Number which means the order in that the forwarding handler linked here will be instantiated, within a group of others routing handlers" Name="SequenceOrder" DisplayName="Sequence Order" Category="Misc">
          <Type>
            <ExternalTypeMoniker Name="/System/Int16" />
          </Type>
        </DomainProperty>
      </Properties>
      <Source>
        <DomainRole Id="4a7421f1-c31b-4039-bc5e-71d9b0d6885f" Description="Description for TransactionKernelDSL.Framework.Language.ExampleRelationship.Target" Name="SourceForwardingTransactionHandler" DisplayName="Source Forwarding Transaction Handler" PropertyName="ForwarderTransactionHandlers" Category="Links" PropertyDisplayName="Forwarder Transaction Handlers">
          <RolePlayer>
            <DomainClassMoniker Name="TransactionHandler" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="df4f91de-36bf-4da7-8f03-4ebbd74d4426" Description="Description for TransactionKernelDSL.Framework.Language.ExampleRelationship.Source" Name="TargetForwardingTransactionHandler" DisplayName="Target Forwarding Transaction Handler" PropertyName="BackwardTransactionHandlers" PropertyDisplayName="Backward Transaction Handlers">
          <RolePlayer>
            <DomainClassMoniker Name="TransactionHandler" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="01623d21-e7e5-4815-89a7-ecd8b9ec9cfc" Description="Description for TransactionKernelDSL.Framework.Language.TransactionModelHasTransactionLayers" Name="TransactionModelHasTransactionLayers" DisplayName="Transaction Model Has Transaction Layers" Namespace="TransactionKernelDSL.Framework.Language" IsEmbedding="true">
      <Source>
        <DomainRole Id="317ce9b0-af3f-4386-84ea-ad9949a4e400" Description="Description for TransactionKernelDSL.Framework.Language.TransactionModelHasTransactionLayers.TransactionModel" Name="TransactionModel" DisplayName="Transaction Model" PropertyName="TransactionLayers" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Transaction Layers">
          <RolePlayer>
            <DomainClassMoniker Name="TransactionModel" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="05730fa1-9470-4c53-bb18-95031dd81169" Description="Description for TransactionKernelDSL.Framework.Language.TransactionModelHasTransactionLayers.TransactionLayer" Name="TransactionLayer" DisplayName="Transaction Layer" PropertyName="TransactionModel" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Transaction Model">
          <RolePlayer>
            <DomainClassMoniker Name="TransactionLayer" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="0b224db5-ade6-44ad-a893-677db8290b84" Description="Description for TransactionKernelDSL.Framework.Language.TransactionLayerHasTransactionHandlers" Name="TransactionLayerHasTransactionHandlers" DisplayName="Transaction Layer Has Transaction Handlers" Namespace="TransactionKernelDSL.Framework.Language" IsEmbedding="true">
      <Source>
        <DomainRole Id="31a27721-b491-4274-aa82-da4ededde1ea" Description="Description for TransactionKernelDSL.Framework.Language.TransactionLayerHasTransactionHandlers.TransactionLayer" Name="TransactionLayer" DisplayName="Source TransactionLayerHasTransactionHandlers" PropertyName="TransactionHandlers" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Transaction Handlers">
          <RolePlayer>
            <DomainClassMoniker Name="TransactionLayer" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="64188ad5-0c64-4e21-bc39-250e458dd105" Description="Description for TransactionKernelDSL.Framework.Language.TransactionLayerHasTransactionHandlers.TransactionHandler" Name="TransactionHandler" DisplayName="Target TransactionLayerHasTransactionHandlers" PropertyName="TransactionLayer" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Transaction Layer">
          <RolePlayer>
            <DomainClassMoniker Name="TransactionHandler" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="0f73908b-1a45-48ec-a84f-c6c0b59c1f64" Description="Description for TransactionKernelDSL.Framework.Language.TransactionHandlerReferencesMaintenanceTransactionHandler" Name="TransactionHandlerReferencesMaintenanceTransactionHandler" DisplayName="Transaction Handler References Maintenance Transaction Handler" Namespace="TransactionKernelDSL.Framework.Language">
      <Properties>
        <DomainProperty Id="97abbacd-248c-4b6f-851c-e17280c804c3" Description="Description for TransactionKernelDSL.Framework.Language.TransactionHandlerReferencesMaintenanceTransactionHandler.Has Custom Code" Name="HasCustomCode" DisplayName="Has Custom Code" DefaultValue="false">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
      <Source>
        <DomainRole Id="3e10d7df-069d-402b-bb99-8f9cb482be5d" Description="Description for TransactionKernelDSL.Framework.Language.TransactionHandlerReferencesMaintenanceTransactionHandler.SourceMaintenanceTransactionHandler" Name="SourceMaintenanceTransactionHandler" DisplayName="Source Maintenance Transaction Handler" PropertyName="MaintenanceTransactionHandler" Multiplicity="ZeroOne" Category="Links" PropertyDisplayName="Maintenance Transaction Handler">
          <RolePlayer>
            <DomainClassMoniker Name="TransactionHandler" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="9544f998-2983-4cb0-870b-83664e115aa3" Description="Description for TransactionKernelDSL.Framework.Language.TransactionHandlerReferencesMaintenanceTransactionHandler.TargetMaintenanceTransactionHandler" Name="TargetMaintenanceTransactionHandler" DisplayName="Target Maintenance Transaction Handler" PropertyName="CallerTransactionHandlers" PropertyDisplayName="Caller Transaction Handlers">
          <RolePlayer>
            <DomainClassMoniker Name="TransactionHandler" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="cccaedc8-5942-46f0-895b-eed583dcd8ac" Description="Description for TransactionKernelDSL.Framework.Language.InputTransactionEngineReferencesTransactionHandlers" Name="InputTransactionEngineReferencesTransactionHandlers" DisplayName="Input Transaction Engine References Transaction Handlers" Namespace="TransactionKernelDSL.Framework.Language">
      <Properties>
        <DomainProperty Id="162b9a19-9103-4582-bf74-4cf29c0a8b06" Description="Description for TransactionKernelDSL.Framework.Language.InputTransactionEngineReferencesTransactionHandlers.Has Custom Code" Name="HasCustomCode" DisplayName="Has Custom Code" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
      <Source>
        <DomainRole Id="0b15726d-41ce-4124-ae0c-9f9a4e6128fb" Description="Description for TransactionKernelDSL.Framework.Language.InputTransactionEngineReferencesTransactionHandlers.InputTransactionEngine" Name="InputTransactionEngine" DisplayName="Source Input Engine Transaction Handler" PropertyName="TransactionHandlers" PropertyDisplayName="Transaction Handlers">
          <RolePlayer>
            <DomainClassMoniker Name="InputTransactionEngine" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="015ed648-8b9c-4ed0-ad86-722e8052afcf" Description="Description for TransactionKernelDSL.Framework.Language.InputTransactionEngineReferencesTransactionHandlers.TransactionHandler" Name="TransactionHandler" DisplayName="Target Input Engine Transaction Handler" PropertyName="InputTransactionEngine" Multiplicity="ZeroOne" PropertyDisplayName="Input Transaction Engine">
          <RolePlayer>
            <DomainClassMoniker Name="TransactionHandler" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="e084a22f-54fa-46e2-bfe1-0771ffb5ecc1" Description="Description for TransactionKernelDSL.Framework.Language.TransactionHandlerReferencesOutputTransactionEngine" Name="TransactionHandlerReferencesOutputTransactionEngine" DisplayName="Transaction Handler References Output Transaction Engine" Namespace="TransactionKernelDSL.Framework.Language">
      <Properties>
        <DomainProperty Id="5d5e6595-0eee-4b1d-955d-fdab511c377f" Description="Description for TransactionKernelDSL.Framework.Language.TransactionHandlerReferencesOutputTransactionEngine.Has Custom Code" Name="HasCustomCode" DisplayName="Has Custom Code" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
      <Source>
        <DomainRole Id="7eb79f19-34dd-4e64-91da-c75f3dcfab2b" Description="Description for TransactionKernelDSL.Framework.Language.TransactionHandlerReferencesOutputTransactionEngine.SourceTransactionHandlerOutputEngine" Name="SourceTransactionHandlerOutputEngine" DisplayName="Source Transaction Handler Output Engine" PropertyName="OutputTransactionEngine" Multiplicity="ZeroOne" Category="Links" PropertyDisplayName="Output Transaction Engine">
          <RolePlayer>
            <DomainClassMoniker Name="TransactionHandler" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="ad3abf00-963c-40ae-ae83-a6b16c6588ac" Description="Description for TransactionKernelDSL.Framework.Language.TransactionHandlerReferencesOutputTransactionEngine.OutputTransactionEngine" Name="OutputTransactionEngine" DisplayName="Target Transaction Handler Output Engine" PropertyName="SourceTransactionHandlerOutputEngines" PropertyDisplayName="Source Transaction Handler Output Engines">
          <RolePlayer>
            <DomainClassMoniker Name="OutputTransactionEngine" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="12f6c104-81bf-4114-a454-8b3835ff524c" Description="Description for TransactionKernelDSL.Framework.Language.TransactionLayerHasInputTransactionEngines" Name="TransactionLayerHasInputTransactionEngines" DisplayName="Transaction Layer Has Input Transaction Engines" Namespace="TransactionKernelDSL.Framework.Language" IsEmbedding="true">
      <Source>
        <DomainRole Id="03b75e3d-9092-4baa-88bc-80de61d4e831" Description="Description for TransactionKernelDSL.Framework.Language.TransactionLayerHasInputTransactionEngines.TransactionLayer" Name="TransactionLayer" DisplayName="Transaction Layer" PropertyName="InputTransactionEngines" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Input Engines">
          <RolePlayer>
            <DomainClassMoniker Name="TransactionLayer" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="db3737e8-7f67-4dc7-87be-05472b474e1d" Description="Description for TransactionKernelDSL.Framework.Language.TransactionLayerHasInputTransactionEngines.InputTransactionEngine" Name="InputTransactionEngine" DisplayName="Input Transaction Engine" PropertyName="TransactionLayer" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Transaction Layer">
          <RolePlayer>
            <DomainClassMoniker Name="InputTransactionEngine" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="023c4cae-9fbb-4d06-8671-e0171677125e" Description="Description for TransactionKernelDSL.Framework.Language.TransactionLayerHasOutputTransactionEngines" Name="TransactionLayerHasOutputTransactionEngines" DisplayName="Transaction Layer Has Output Transaction Engines" Namespace="TransactionKernelDSL.Framework.Language" IsEmbedding="true">
      <Source>
        <DomainRole Id="cc940ffa-9e43-4506-85a0-8785703c71c6" Description="Description for TransactionKernelDSL.Framework.Language.TransactionLayerHasOutputTransactionEngines.TransactionLayer" Name="TransactionLayer" DisplayName="Transaction Layer" PropertyName="OutputTransactionEngines" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Output Engines">
          <RolePlayer>
            <DomainClassMoniker Name="TransactionLayer" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="bad54e51-b71b-4756-84ae-aa7637ee3491" Description="Description for TransactionKernelDSL.Framework.Language.TransactionLayerHasOutputTransactionEngines.OutputTransactionEngine" Name="OutputTransactionEngine" DisplayName="Output Transaction Engine" PropertyName="TransactionLayer" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Transaction Layer">
          <RolePlayer>
            <DomainClassMoniker Name="OutputTransactionEngine" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="110b305f-f948-4bfc-a003-6f5f2e02dbad" Description="Description for TransactionKernelDSL.Framework.Language.TransactionHandlerReferencesTransactionSQLServerDataSources" Name="TransactionHandlerReferencesTransactionSQLServerDataSources" DisplayName="Transaction Handler References Transaction SQLServer Data Sources" Namespace="TransactionKernelDSL.Framework.Language" AllowsDuplicates="true">
      <Properties>
        <DomainProperty Id="fdb98bcc-0bfd-481d-be66-626e934ddeda" Description="Description for TransactionKernelDSL.Framework.Language.TransactionHandlerReferencesTransactionSQLServerDataSources.Has Custom Code" Name="HasCustomCode" DisplayName="Has Custom Code" DefaultValue="true">
          <Type>
            <ExternalTypeMoniker Name="/System/Boolean" />
          </Type>
        </DomainProperty>
      </Properties>
      <Source>
        <DomainRole Id="151e78c2-c756-4158-8006-e071fc678cc5" Description="Description for TransactionKernelDSL.Framework.Language.TransactionHandlerReferencesTransactionSQLServerDataSources.TransactionHandler" Name="TransactionHandler" DisplayName="Source SQLServer Data Source Transaction Handler" PropertyName="TransactionSQLServerDataSources" Category="Links" PropertyDisplayName="Transaction SQLServer Data Sources">
          <RolePlayer>
            <DomainClassMoniker Name="TransactionHandler" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="a688c010-4246-47ac-b423-3f7e94a2213e" Description="Description for TransactionKernelDSL.Framework.Language.TransactionHandlerReferencesTransactionSQLServerDataSources.TransactionSQLServerDataSource" Name="TransactionSQLServerDataSource" DisplayName="Target SQLServer Data Source Transaction Handler " PropertyName="TransactionHandlers" PropertyDisplayName="Transaction Handlers">
          <RolePlayer>
            <DomainClassMoniker Name="TransactionSQLServerDataSource" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="8aa7ecc6-b997-4cd7-9ae1-ca07ad816586" Description="Description for TransactionKernelDSL.Framework.Language.TransactionLayerHasTransactionSQLServerDataSources" Name="TransactionLayerHasTransactionSQLServerDataSources" DisplayName="Transaction Layer Has Transaction SQLServer Data Sources" Namespace="TransactionKernelDSL.Framework.Language" IsEmbedding="true">
      <Source>
        <DomainRole Id="5008847b-86ef-43de-84e7-fd1fef41256d" Description="Description for TransactionKernelDSL.Framework.Language.TransactionLayerHasTransactionSQLServerDataSources.TransactionLayer" Name="TransactionLayer" DisplayName="Transaction Layer" PropertyName="TransactionSQLServerDataSources" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="SQLServer Data Sources">
          <RolePlayer>
            <DomainClassMoniker Name="TransactionLayer" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="32ee472c-f988-4580-afe9-c90e575e23ca" Description="Description for TransactionKernelDSL.Framework.Language.TransactionLayerHasTransactionSQLServerDataSources.TransactionSQLServerDataSource" Name="TransactionSQLServerDataSource" DisplayName="Transaction SQLServer Data Source" PropertyName="TransactionLayer" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Transaction Layer">
          <RolePlayer>
            <DomainClassMoniker Name="TransactionSQLServerDataSource" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="ed9fffc4-4b08-4381-8f64-fcecb297f97e" Description="Description for TransactionKernelDSL.Framework.Language.TransactionLayerHasOutputTransactionWebServices" Name="TransactionLayerHasOutputTransactionWebServices" DisplayName="Transaction Layer Has Output Transaction Web Services" Namespace="TransactionKernelDSL.Framework.Language" IsEmbedding="true">
      <Source>
        <DomainRole Id="af96257e-2001-4903-87dd-18d0f783f4f2" Description="Description for TransactionKernelDSL.Framework.Language.TransactionLayerHasOutputTransactionWebServices.TransactionLayer" Name="TransactionLayer" DisplayName="Transaction Layer" PropertyName="OutputTransactionWebServices" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Output Web Services">
          <RolePlayer>
            <DomainClassMoniker Name="TransactionLayer" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="88c3806d-6c7f-4463-acb6-40cb7a78721f" Description="Description for TransactionKernelDSL.Framework.Language.TransactionLayerHasOutputTransactionWebServices.OutputTransactionWebService" Name="OutputTransactionWebService" DisplayName="Output Transaction Web Service" PropertyName="TransactionLayer" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Transaction Layer">
          <RolePlayer>
            <DomainClassMoniker Name="OutputTransactionWebService" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="1394d0c1-06dc-4e67-a9fa-01e7f1b18516" Description="Description for TransactionKernelDSL.Framework.Language.TransactionHandlerReferencesOutputTransactionWebService" Name="TransactionHandlerReferencesOutputTransactionWebService" DisplayName="Transaction Handler References Output Transaction Web Service" Namespace="TransactionKernelDSL.Framework.Language">
      <Source>
        <DomainRole Id="ae0f8b82-d56a-4e5d-9531-6a057bf07c6e" Description="Description for TransactionKernelDSL.Framework.Language.TransactionHandlerReferencesOutputTransactionWebService.TransactionHandler" Name="TransactionHandler" DisplayName="Source Output WebService Transaction Handler" PropertyName="OutputTransactionWebService" Multiplicity="ZeroOne" Category="Links" PropertyDisplayName="Output Transaction Web Service">
          <RolePlayer>
            <DomainClassMoniker Name="TransactionHandler" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="a1a9e913-4b26-4686-8a25-12b165dc8ad7" Description="Description for TransactionKernelDSL.Framework.Language.TransactionHandlerReferencesOutputTransactionWebService.OutputTransactionWebService" Name="OutputTransactionWebService" DisplayName="Target Output WebService Transaction Output Web Service" PropertyName="TransactionHandlers" PropertyDisplayName="Transaction Handlers">
          <RolePlayer>
            <DomainClassMoniker Name="OutputTransactionWebService" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="abe4a254-3688-442d-ba87-ee0eb3449bbc" Description="Description for TransactionKernelDSL.Framework.Language.TransactionLayerHasTimeTriggers" Name="TransactionLayerHasTimeTriggers" DisplayName="Transaction Layer Has Time Triggers" Namespace="TransactionKernelDSL.Framework.Language" IsEmbedding="true">
      <Source>
        <DomainRole Id="0dcb324c-5588-41c4-92c2-2cc399a73117" Description="Description for TransactionKernelDSL.Framework.Language.TransactionLayerHasTimeTriggers.TransactionLayer" Name="TransactionLayer" DisplayName="Transaction Layer" PropertyName="TimeTriggers" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Time Triggers">
          <RolePlayer>
            <DomainClassMoniker Name="TransactionLayer" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="5f8ecdd1-80df-45c5-9f0e-1b9e759327c5" Description="Description for TransactionKernelDSL.Framework.Language.TransactionLayerHasTimeTriggers.TimeTrigger" Name="TimeTrigger" DisplayName="Time Trigger" PropertyName="TransactionLayer" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Transaction Layer">
          <RolePlayer>
            <DomainClassMoniker Name="TimeTrigger" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="795c1576-3149-448c-b732-c23da59085f3" Description="Description for TransactionKernelDSL.Framework.Language.InputTransactionEngineReferencesTimeTriggers" Name="InputTransactionEngineReferencesTimeTriggers" DisplayName="Input Transaction Engine References Time Triggers" Namespace="TransactionKernelDSL.Framework.Language">
      <Source>
        <DomainRole Id="f1408161-b7ed-4f0f-a5f0-d0aee56c9669" Description="Description for TransactionKernelDSL.Framework.Language.InputTransactionEngineReferencesTimeTriggers.InputTransactionEngine" Name="InputTransactionEngine" DisplayName="Input Transaction Engine" PropertyName="TimeTriggers" PropertyDisplayName="Time Triggers">
          <RolePlayer>
            <DomainClassMoniker Name="InputTransactionEngine" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="dea9cd39-64d8-4e03-9841-77216bdd3b40" Description="Description for TransactionKernelDSL.Framework.Language.InputTransactionEngineReferencesTimeTriggers.TimeTrigger" Name="TimeTrigger" DisplayName="Time Trigger" PropertyName="InputTransactionEngine" Multiplicity="One" PropertyDisplayName="Input Transaction Engine">
          <RolePlayer>
            <DomainClassMoniker Name="TimeTrigger" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="b3a2fd9f-378b-4392-a38e-897114ac8418" Description="Description for TransactionKernelDSL.Framework.Language.OutputTransactionEngineReferencesTransactionDataSourceSupport" Name="OutputTransactionEngineReferencesTransactionDataSourceSupport" DisplayName="Output Transaction Engine References Transaction Data Source Support" Namespace="TransactionKernelDSL.Framework.Language">
      <Source>
        <DomainRole Id="63efd2f7-de8b-4af7-8309-e4124e7c3ed4" Description="Description for TransactionKernelDSL.Framework.Language.OutputTransactionEngineReferencesTransactionDataSourceSupport.OutputTransactionEngine" Name="OutputTransactionEngine" DisplayName="Output Transaction Engine" PropertyName="TransactionDataSourceSupport" Multiplicity="ZeroOne" PropertyDisplayName="Transaction Data Source Support">
          <RolePlayer>
            <DomainClassMoniker Name="OutputTransactionEngine" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="534151b2-8a4b-41a4-a2d6-907534313446" Description="Description for TransactionKernelDSL.Framework.Language.OutputTransactionEngineReferencesTransactionDataSourceSupport.TransactionDataSourceSupport" Name="TransactionDataSourceSupport" DisplayName="Transaction Data Source Support" PropertyName="OutputTransactionEngines" PropertyDisplayName="Output Transaction Engines">
          <RolePlayer>
            <DomainClassMoniker Name="TransactionDataSourceSupport" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="f520fd62-a80f-4068-b724-32295ca35954" Description="Description for TransactionKernelDSL.Framework.Language.TransactionLayerHasTransactionDataSourceSupports" Name="TransactionLayerHasTransactionDataSourceSupports" DisplayName="Transaction Layer Has Transaction Data Source Supports" Namespace="TransactionKernelDSL.Framework.Language" IsEmbedding="true">
      <Source>
        <DomainRole Id="8a764b2c-8e3b-4bdf-a02e-9d88b4e58a04" Description="Description for TransactionKernelDSL.Framework.Language.TransactionLayerHasTransactionDataSourceSupports.TransactionLayer" Name="TransactionLayer" DisplayName="Transaction Layer" PropertyName="TransactionDataSourceSupports" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Transaction Data Source Supports">
          <RolePlayer>
            <DomainClassMoniker Name="TransactionLayer" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="14ae6c59-8809-45d6-a72c-f2d765623aff" Description="Description for TransactionKernelDSL.Framework.Language.TransactionLayerHasTransactionDataSourceSupports.TransactionDataSourceSupport" Name="TransactionDataSourceSupport" DisplayName="Transaction Data Source Support" PropertyName="TransactionLayer" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Transaction Layer">
          <RolePlayer>
            <DomainClassMoniker Name="TransactionDataSourceSupport" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="b978f96a-2d52-4ebb-8878-b9e3c458db78" Description="Description for TransactionKernelDSL.Framework.Language.TransactionModelHasTransactionEnvironmentLayer" Name="TransactionModelHasTransactionEnvironmentLayer" DisplayName="Transaction Model Has Transaction Environment Layer" Namespace="TransactionKernelDSL.Framework.Language" IsEmbedding="true">
      <Source>
        <DomainRole Id="5fa6734a-0e0f-4114-a1ca-8a213ea179e0" Description="Description for TransactionKernelDSL.Framework.Language.TransactionModelHasTransactionEnvironmentLayer.TransactionModel" Name="TransactionModel" DisplayName="Transaction Model" PropertyName="TransactionEnvironmentLayer" Multiplicity="ZeroOne" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Transaction Environment Layer">
          <RolePlayer>
            <DomainClassMoniker Name="TransactionModel" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="3585639e-15a0-4ba1-9b8b-11e38d13dfe5" Description="Description for TransactionKernelDSL.Framework.Language.TransactionModelHasTransactionEnvironmentLayer.TransactionEnvironmentLayer" Name="TransactionEnvironmentLayer" DisplayName="Transaction Environment Layer" PropertyName="TransactionModel" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Transaction Model">
          <RolePlayer>
            <DomainClassMoniker Name="TransactionEnvironmentLayer" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
    <DomainRelationship Id="a838994a-0199-40c5-a309-12a3465abb14" Description="Description for TransactionKernelDSL.Framework.Language.TransactionEnvironmentLayerHasTransactionEnvironmentSQLServerVariables" Name="TransactionEnvironmentLayerHasTransactionEnvironmentSQLServerVariables" DisplayName="Transaction Environment Layer Has Transaction Environment SQLServer Variables" Namespace="TransactionKernelDSL.Framework.Language" IsEmbedding="true">
      <Source>
        <DomainRole Id="134be3b8-f7d3-4230-966a-ccd0f9767ec0" Description="Description for TransactionKernelDSL.Framework.Language.TransactionEnvironmentLayerHasTransactionEnvironmentSQLServerVariables.TransactionEnvironmentLayer" Name="TransactionEnvironmentLayer" DisplayName="Transaction Environment Layer" PropertyName="TransactionEnvironmentSQLServerVariables" PropagatesCopy="PropagatesCopyToLinkAndOppositeRolePlayer" PropertyDisplayName="Transaction Environment SQLServer Variables">
          <RolePlayer>
            <DomainClassMoniker Name="TransactionEnvironmentLayer" />
          </RolePlayer>
        </DomainRole>
      </Source>
      <Target>
        <DomainRole Id="6ab5bc1d-fff9-4444-9df8-54d3ef5dcc3d" Description="Description for TransactionKernelDSL.Framework.Language.TransactionEnvironmentLayerHasTransactionEnvironmentSQLServerVariables.TransactionEnvironmentSQLServerVariable" Name="TransactionEnvironmentSQLServerVariable" DisplayName="Transaction Environment SQLServer Variable" PropertyName="TransactionEnvironmentLayer" Multiplicity="One" PropagatesDelete="true" PropertyDisplayName="Transaction Environment Layer">
          <RolePlayer>
            <DomainClassMoniker Name="TransactionEnvironmentSQLServerVariable" />
          </RolePlayer>
        </DomainRole>
      </Target>
    </DomainRelationship>
  </Relationships>
  <Types>
    <ExternalType Name="DateTime" Namespace="System" />
    <ExternalType Name="String" Namespace="System" />
    <ExternalType Name="Int16" Namespace="System" />
    <ExternalType Name="Int32" Namespace="System" />
    <ExternalType Name="Int64" Namespace="System" />
    <ExternalType Name="UInt16" Namespace="System" />
    <ExternalType Name="UInt32" Namespace="System" />
    <ExternalType Name="UInt64" Namespace="System" />
    <ExternalType Name="SByte" Namespace="System" />
    <ExternalType Name="Byte" Namespace="System" />
    <ExternalType Name="Double" Namespace="System" />
    <ExternalType Name="Single" Namespace="System" />
    <ExternalType Name="Guid" Namespace="System" />
    <ExternalType Name="Boolean" Namespace="System" />
    <ExternalType Name="Char" Namespace="System" />
    <DomainEnumeration Name="InputEngineType" Namespace="TransactionKernelDSL.Framework.Language" Description="Description for TransactionKernelDSL.Framework.Language.InputEngineType">
      <Literals>
        <EnumerationLiteral Description="Description for TransactionKernelDSL.Framework.Language.InputEngineType.InputEngine" Name="InputEngine" Value="0" />
        <EnumerationLiteral Description="Description for TransactionKernelDSL.Framework.Language.InputEngineType.ThreadedInputEngine" Name="ThreadedInputEngine" Value="2" />
        <EnumerationLiteral Description="Description for TransactionKernelDSL.Framework.Language.InputEngineType.TimeTriggeredInputEngine" Name="TimeTriggeredInputEngine" Value="3" />
        <EnumerationLiteral Description="Description for TransactionKernelDSL.Framework.Language.InputEngineType.TcpTriggeredMultiThreadedInputEngine" Name="TcpTriggeredMultiThreadedInputEngine" Value="1" />
        <EnumerationLiteral Description="Description for TransactionKernelDSL.Framework.Language.InputEngineType.TcpTriggeredSocketReusableMultiThreadedInputEngine" Name="TcpTriggeredSocketReusableMultiThreadedInputEngine" Value="4" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="PersistableSourceType" Namespace="TransactionKernelDSL.Framework.Language" Description="Description for TransactionKernelDSL.Framework.Language.PersistableSourceType">
      <Literals>
        <EnumerationLiteral Description="Description for TransactionKernelDSL.Framework.Language.PersistableSourceType.SQLServerDatabase" Name="SQLServerDatabase" Value="1" />
        <EnumerationLiteral Description="Description for TransactionKernelDSL.Framework.Language.PersistableSourceType.NotTyped" Name="NotTyped" Value="0" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="OutputEngineType" Namespace="TransactionKernelDSL.Framework.Language" Description="Description for TransactionKernelDSL.Framework.Language.OutputEngineType">
      <Literals>
        <EnumerationLiteral Description="Description for TransactionKernelDSL.Framework.Language.OutputEngineType.OutputEngine" Name="OutputEngine" Value="0" />
        <EnumerationLiteral Description="Description for TransactionKernelDSL.Framework.Language.OutputEngineType.FunneledOutputEngine" Name="FunneledOutputEngine" Value="1" />
        <EnumerationLiteral Description="Description for TransactionKernelDSL.Framework.Language.OutputEngineType.StraightOutputEngine" Name="StraightOutputEngine" Value="2" />
        <EnumerationLiteral Description="Description for TransactionKernelDSL.Framework.Language.OutputEngineType.TcpFunneledOutputEngine" Name="TcpFunneledOutputEngine" Value="3" />
        <EnumerationLiteral Description="Description for TransactionKernelDSL.Framework.Language.OutputEngineType.TcpStraightOutputEngine" Name="TcpStraightOutputEngine" Value="4" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="PersistableStage" Namespace="TransactionKernelDSL.Framework.Language" Description="Description for TransactionKernelDSL.Framework.Language.PersistableStage">
      <Literals>
        <EnumerationLiteral Description="It defines that the SP will be executed on PreProcessTransaction Method, after receiving a new requirement from the client" Name="PreProcessStage" Value="0" />
        <EnumerationLiteral Description="It defines that the SP will be executed on PostProcessTransaction Method, after reply is sent" Name="PostProcessStage" Value="1" />
        <EnumerationLiteral Description="It defines that the SP will be executed on FinalProcessTransaction Method, after the reply is sent, and eventual maintenance is finished" Name="FinalProcessStage" Value="2" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="ParserType" Namespace="TransactionKernelDSL.Framework.Language" Description="Description for TransactionKernelDSL.Framework.Language.ParserType">
      <Literals>
        <EnumerationLiteral Description="Description for TransactionKernelDSL.Framework.Language.ParserType.NotTyped" Name="NotTyped" Value="0" />
        <EnumerationLiteral Description="Description for TransactionKernelDSL.Framework.Language.ParserType.Iso8583B24" Name="Iso8583B24" Value="1" />
        <EnumerationLiteral Description="Description for TransactionKernelDSL.Framework.Language.ParserType.Iso8583" Name="Iso8583" Value="2" />
        <EnumerationLiteral Description="Description for TransactionKernelDSL.Framework.Language.ParserType.Meflur" Name="Meflur" Value="3" />
        <EnumerationLiteral Description="Meflur parser with 2-byte long length prefixing both incoming and outcoming data" Name="PrefixedMeflur" Value="4" />
        <EnumerationLiteral Description="Description for TransactionKernelDSL.Framework.Language.ParserType.PropietaryParser" Name="PropietaryParser" Value="-1" />
        <EnumerationLiteral Description="Parser of Type Beltran" Name="Beltran" Value="5" />
        <EnumerationLiteral Description="Description for TransactionKernelDSL.Framework.Language.ParserType.BPosBrowser" Name="BPosBrowser" Value="6" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="DataSourceSupportType" Namespace="TransactionKernelDSL.Framework.Language" Description="Description for TransactionKernelDSL.Framework.Language.DataSourceSupportType">
      <Literals>
        <EnumerationLiteral Description="Description for TransactionKernelDSL.Framework.Language.DataSourceSupportType.NotTyped" Name="NotTyped" Value="0" />
        <EnumerationLiteral Description="Description for TransactionKernelDSL.Framework.Language.DataSourceSupportType.SQLServerSupport" Name="SQLServerSupport" Value="1" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="WebServiceArchitecturalPattern" Namespace="TransactionKernelDSL.Framework.Language" Description="The architectural pattern used for implementing the WS's proxies">
      <Literals>
        <EnumerationLiteral Description="There will be just one instance of this output WS." Name="Singleton" Value="0" />
        <EnumerationLiteral Description="There will as many instances as threads using this output WS" Name="MultipleInstances" Value="1" />
      </Literals>
    </DomainEnumeration>
    <DomainEnumeration Name="WebServiceTechnology" Namespace="TransactionKernelDSL.Framework.Language" Description="Technology used by the remote WS implementation.">
      <Literals>
        <EnumerationLiteral Description="Technology SoapHttpClientProtocol" Name="SoapHttpClientProtocol" Value="0" />
        <EnumerationLiteral Description="Technology WCF" Name="WCF" Value="1" />
      </Literals>
    </DomainEnumeration>
  </Types>
  <Shapes>
    <GeometryShape Id="d41c7019-0b9a-4471-b88a-507fcb50b4ef" Description="Shape used to represent ExampleElements on a Diagram." Name="TransactionHandlerShape" DisplayName="Transaction Handler Shape" Namespace="TransactionKernelDSL.Framework.Language" FixedTooltipText="Transaction Handler Shape" FillColor="242, 239, 229" OutlineColor="113, 111, 110" InitialWidth="2" InitialHeight="0.75" OutlineThickness="0.01" Geometry="Rectangle">
      <Notes>The shape has a text decorator used to display the Name property of the mapped ExampleElement.</Notes>
      <ShapeHasDecorators Position="InnerBottomCenter" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="TransactionIdDecorator" DisplayName="Transaction Id Decorator" DefaultText="TransactionIdDecorator" FontStyle="Italic" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="Center" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="NameDecorator" DisplayName="Name Decorator" DefaultText="NameDecorator" FontSize="10" />
      </ShapeHasDecorators>
    </GeometryShape>
    <CompartmentShape Id="fdd62b26-04d7-4190-96e5-9ab9c2cf6176" Description="Description for TransactionKernelDSL.Framework.Language.InputTransactionEngineShape" Name="InputTransactionEngineShape" DisplayName="Input Transaction Engine Shape" Namespace="TransactionKernelDSL.Framework.Language" FixedTooltipText="Input Transaction Engine Shape" FillColor="PaleGreen" OutlineColor="SeaGreen" InitialHeight="1" Geometry="Rectangle">
      <ShapeHasDecorators Position="InnerTopCenter" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="NameDecorator" DisplayName="Name Decorator" DefaultText="NameDecorator" FontStyle="Bold" FontSize="12" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopCenter" HorizontalOffset="0" VerticalOffset="0.3">
        <TextDecorator Name="TypeDecorator" DisplayName="Type Decorator" DefaultText="TypeDecorator" FontStyle="Italic" />
      </ShapeHasDecorators>
      <Compartment Name="TransactionHandler" />
    </CompartmentShape>
    <GeometryShape Id="53b59dd6-5b35-44eb-a1f0-845036b92641" Description="Description for TransactionKernelDSL.Framework.Language.TransactionDataSourceShape" Name="TransactionDataSourceShape" DisplayName="Transaction Data Source Shape" Namespace="TransactionKernelDSL.Framework.Language" FixedTooltipText="Transaction Data Source Shape" TextColor="Indigo" FillColor="Thistle" OutlineColor="Indigo" InitialHeight="1" Geometry="Circle">
      <ShapeHasDecorators Position="OuterTopCenter" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="NameDecorator" DisplayName="Name Decorator" DefaultText="NameDecorator" FontStyle="Italic" FontSize="10" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="OuterBottomCenter" HorizontalOffset="0" VerticalOffset="0" isMoveable="true">
        <TextDecorator Name="StepOrderDecorator" DisplayName="Step Order Decorator" DefaultText="StepOrderDecorator" FontStyle="Bold" />
      </ShapeHasDecorators>
    </GeometryShape>
    <SwimLane Id="2ae941c0-6852-4957-95a7-ceab3d386583" Description="Description for TransactionKernelDSL.Framework.Language.TransactionLayerSwimLane" Name="TransactionLayerSwimLane" DisplayName="Transaction Layer Swim Lane" Namespace="TransactionKernelDSL.Framework.Language" FixedTooltipText="Transaction Layer Swim Lane" HeaderFillColor="LightSalmon" SeparatorLineStyle="Solid" SeparatorColor="OrangeRed" BodyFillColor="PeachPuff" InitialWidth="0" InitialHeight="0">
      <Decorators>
        <SwimLaneHasDecorators Position="InnerTopLeft" HorizontalOffset="0" VerticalOffset="0">
          <TextDecorator Name="NameDecorator" DisplayName="Name Decorator" DefaultText="NameDecorator" FontSize="12" />
        </SwimLaneHasDecorators>
        <SwimLaneHasDecorators Position="InnerTopRight" HorizontalOffset="0" VerticalOffset="0">
          <TextDecorator Name="LevelDecorator" DisplayName="Level Decorator" DefaultText="LevelDecorator" FontSize="12" />
        </SwimLaneHasDecorators>
      </Decorators>
    </SwimLane>
    <CompartmentShape Id="472e0b4e-3ec1-4cce-8392-c2a12b514359" Description="Description for TransactionKernelDSL.Framework.Language.OutputTransactionEngineShape" Name="OutputTransactionEngineShape" DisplayName="Output Transaction Engine Shape" Namespace="TransactionKernelDSL.Framework.Language" FixedTooltipText="Output Transaction Engine Shape" FillColor="PaleTurquoise" OutlineColor="Teal" InitialHeight="1" Geometry="Rectangle">
      <ShapeHasDecorators Position="InnerTopCenter" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="NameDecorator" DisplayName="Name Decorator" DefaultText="NameDecorator" FontStyle="Bold" FontSize="12" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopCenter" HorizontalOffset="0" VerticalOffset="0.3">
        <TextDecorator Name="TypeDecorator" DisplayName="Type Decorator" DefaultText="TypeDecorator" FontStyle="Italic" />
      </ShapeHasDecorators>
      <Compartment Name="TransactionHandler" />
    </CompartmentShape>
    <GeometryShape Id="6440eec3-989e-4cfd-9f84-0e2e1ac01e86" Description="Description for TransactionKernelDSL.Framework.Language.OutputTransactionWebServiceShape" Name="OutputTransactionWebServiceShape" DisplayName="Output Transaction Web Service Shape" Namespace="TransactionKernelDSL.Framework.Language" FixedTooltipText="Output Transaction Web Service Shape" FillColor="DarkTurquoise" OutlineColor="DarkSlateGray" InitialHeight="1" Geometry="Rectangle">
      <ShapeHasDecorators Position="InnerTopCenter" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="NameDecorator" DisplayName="Name Decorator" DefaultText="NameDecorator" FontStyle="Bold" FontSize="12" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopCenter" HorizontalOffset="0" VerticalOffset="0.3">
        <TextDecorator Name="WebServiceClassNameDecorator" DisplayName="Web Service Class Name Decorator" DefaultText="WebServiceClassNameDecorator" FontStyle="Italic" FontSize="10" />
      </ShapeHasDecorators>
    </GeometryShape>
    <GeometryShape Id="c9c134f4-e6ce-4da5-9f89-f126c00daa9b" Description="Description for TransactionKernelDSL.Framework.Language.TimeTriggerShape" Name="TimeTriggerShape" DisplayName="Time Trigger Shape" Namespace="TransactionKernelDSL.Framework.Language" FixedTooltipText="Time Trigger Shape" TextColor="DarkGreen" FillColor="SpringGreen" InitialHeight="1" Geometry="Circle">
      <ShapeHasDecorators Position="Center" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="NameDecorator" DisplayName="Name Decorator" DefaultText="NameDecorator" FontSize="10" />
      </ShapeHasDecorators>
    </GeometryShape>
    <ImageShape Id="35631102-cac9-4634-ac85-514144c471ed" Description="Description for TransactionKernelDSL.Framework.Language.TimeTriggerImageShape" Name="TimeTriggerImageShape" DisplayName="Time Trigger Image Shape" Namespace="TransactionKernelDSL.Framework.Language" FixedTooltipText="Time Trigger Image Shape" TextColor="DarkGreen" FillColor="SpringGreen" InitialHeight="1" Image="Resources\TimeTriggerShape.bmp">
      <ShapeHasDecorators Position="OuterTopCenter" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="NameDecorator" DisplayName="Name Decorator" DefaultText="NameDecorator" FontSize="10" />
      </ShapeHasDecorators>
    </ImageShape>
    <GeometryShape Id="649f27b5-fc84-40ec-990d-bb34f815bf0d" Description="Description for TransactionKernelDSL.Framework.Language.TransactionDataSourceSupportShape" Name="TransactionDataSourceSupportShape" DisplayName="Transaction Data Source Support Shape" Namespace="TransactionKernelDSL.Framework.Language" FixedTooltipText="Transaction Data Source Support Shape" TextColor="Indigo" FillColor="Thistle" OutlineColor="Indigo" InitialHeight="1" Geometry="RoundedRectangle">
      <ShapeHasDecorators Position="OuterTopCenter" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="NameDecorator" DisplayName="Name Decorator" DefaultText="NameDecorator" />
      </ShapeHasDecorators>
    </GeometryShape>
    <SwimLane Id="9f1e2894-05e1-46dd-904c-fe28d521b372" Description="Description for TransactionKernelDSL.Framework.Language.TransactionEnvironmentLayerSwimLane" Name="TransactionEnvironmentLayerSwimLane" DisplayName="Transaction Environment Layer Swim Lane" Namespace="TransactionKernelDSL.Framework.Language" FixedTooltipText="Transaction Environment Layer Swim Lane" HeaderFillColor="LimeGreen" SeparatorLineStyle="Solid" SeparatorColor="ForestGreen" BodyFillColor="GreenYellow" InitialWidth="0" InitialHeight="0">
      <Decorators>
        <SwimLaneHasDecorators Position="InnerTopLeft" HorizontalOffset="0" VerticalOffset="0">
          <TextDecorator Name="NameDecorator" DisplayName="Name Decorator" DefaultText="NameDecorator" FontSize="12" />
        </SwimLaneHasDecorators>
      </Decorators>
    </SwimLane>
    <GeometryShape Id="be15d439-00cc-42f0-980d-bde3d2e2fed7" Description="Description for TransactionKernelDSL.Framework.Language.TransactionEnvironmentSQLServerVariableShape" Name="TransactionEnvironmentSQLServerVariableShape" DisplayName="Transaction Environment SQLServer Variable Shape" Namespace="TransactionKernelDSL.Framework.Language" FixedTooltipText="Transaction Environment SQLServer Variable Shape" FillColor="Gainsboro" InitialHeight="1" Geometry="Rectangle">
      <ShapeHasDecorators Position="InnerTopCenter" HorizontalOffset="0" VerticalOffset="0">
        <TextDecorator Name="NameDecorator" DisplayName="Name Decorator" DefaultText="NameDecorator" FontSize="12" />
      </ShapeHasDecorators>
      <ShapeHasDecorators Position="InnerTopLeft" HorizontalOffset="0" VerticalOffset="0">
        <ExpandCollapseDecorator Name="ExpandCollapseDecorator" DisplayName="Expand Collapse Decorator" />
      </ShapeHasDecorators>
    </GeometryShape>
  </Shapes>
  <Connectors>
    <Connector Id="532dda07-3c9a-459b-93b9-4c1efb2be94e" Description="Connector between the ExampleShapes. Represents ExampleRelationships on the Diagram." Name="TransactionHandlerForwardingLinkConnector" DisplayName="Transaction Handler Forwarding Link Connector" Namespace="TransactionKernelDSL.Framework.Language" FixedTooltipText="Transaction Handler Forwarding Link Connector" Color="128, 64, 64" SourceEndStyle="EmptyDiamond" TargetEndStyle="EmptyArrow" Thickness="0.01" />
    <Connector Id="6df2396f-0ff6-4047-8d99-c7fa66f452e5" Description="Description for TransactionKernelDSL.Framework.Language.OutputTransactionEngineReferencesTransactionHandlersLinkConnector" Name="OutputTransactionEngineReferencesTransactionHandlersLinkConnector" DisplayName="Output Transaction Engine References Transaction Handlers Link Connector" Namespace="TransactionKernelDSL.Framework.Language" FixedTooltipText="Output Transaction Engine References Transaction Handlers Link Connector" Color="Turquoise" DashStyle="Dash" SourceEndStyle="FilledDiamond" TargetEndStyle="FilledArrow" sourceEndWidth="0.3" sourceEndHeight="0.3" />
    <Connector Id="02e3e8e4-d2ec-4194-8f93-a0345f3706aa" Description="Description for TransactionKernelDSL.Framework.Language.TransactionHandlerMaintenanceLinkConnector" Name="TransactionHandlerMaintenanceLinkConnector" DisplayName="Transaction Handler Maintenance Link Connector" Namespace="TransactionKernelDSL.Framework.Language" FixedTooltipText="Transaction Handler Maintenance Link Connector" Color="Crimson" SourceEndStyle="EmptyDiamond" TargetEndStyle="EmptyArrow" Thickness="0.01" />
    <Connector Id="e8feb6ce-4788-461c-8ed5-100b26e0275e" Description="Description for TransactionKernelDSL.Framework.Language.TransactionSQLServerDataSourceLinkConnector" Name="TransactionSQLServerDataSourceLinkConnector" DisplayName="Transaction SQLServer Data Source Link Connector" Namespace="TransactionKernelDSL.Framework.Language" FixedTooltipText="Transaction SQLServer Data Source Link Connector" Color="Purple" DashStyle="Dot" TargetEndStyle="HollowArrow" Thickness="0.005" />
    <Connector Id="a86a2a9a-f3c0-4215-9a84-6f7b460bfe6f" Description="Description for TransactionKernelDSL.Framework.Language.InputTransactionEngineReferencesTransactionHandlersLinkConnector" Name="InputTransactionEngineReferencesTransactionHandlersLinkConnector" DisplayName="Input Transaction Engine References Transaction Handlers Link Connector" Namespace="TransactionKernelDSL.Framework.Language" FixedTooltipText="Input Transaction Engine References Transaction Handlers Link Connector" Color="LimeGreen" DashStyle="Dash" SourceEndStyle="FilledDiamond" TargetEndStyle="FilledArrow" sourceEndWidth="0.3" sourceEndHeight="0.3" />
    <Connector Id="cbf17bca-3cdd-4263-93cf-c46cc11cf97d" Description="Description for TransactionKernelDSL.Framework.Language.OutputTransactionWebServiceLinkConnector" Name="OutputTransactionWebServiceLinkConnector" DisplayName="Output Transaction Web Service Link Connector" Namespace="TransactionKernelDSL.Framework.Language" FixedTooltipText="Output Transaction Web Service Link Connector" Color="Teal" DashStyle="Dash" SourceEndStyle="FilledDiamond" TargetEndStyle="FilledArrow" sourceEndWidth="0.3" sourceEndHeight="0.3" />
    <Connector Id="40e76ee8-7e93-407f-a3b5-cac612b0f312" Description="Description for TransactionKernelDSL.Framework.Language.TimeTriggerLinkConnector" Name="TimeTriggerLinkConnector" DisplayName="Time Trigger Link Connector" Namespace="TransactionKernelDSL.Framework.Language" FixedTooltipText="Time Trigger Link Connector" TextColor="Green" Color="Green" DashStyle="Dot" TargetEndStyle="EmptyDiamond" RoutingStyle="Straight" />
    <Connector Id="000d1bfa-c0d3-4600-b3d4-4bcf2848e2c2" Description="Description for TransactionKernelDSL.Framework.Language.TransactionDataSourceSupportForOutputEngineLink" Name="TransactionDataSourceSupportForOutputEngineLink" DisplayName="Transaction Data Source Support For Output Engine Link" Namespace="TransactionKernelDSL.Framework.Language" FixedTooltipText="Transaction Data Source Support For Output Engine Link" Color="Purple" DashStyle="Dot" TargetEndStyle="HollowArrow" Thickness="0.005" RoutingStyle="Straight" />
  </Connectors>
  <XmlSerializationBehavior Name="TrnxDslSerializationBehavior" Namespace="TransactionKernelDSL.Framework.Language">
    <ClassData>
      <XmlClassData TypeName="TransactionModel" MonikerAttributeName="" SerializeId="true" MonikerElementName="transactionModelMoniker" ElementName="transactionModel" MonikerTypeName="TransactionModelMoniker">
        <DomainClassMoniker Name="TransactionModel" />
        <ElementData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="transactionLayers">
            <DomainRelationshipMoniker Name="TransactionModelHasTransactionLayers" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="name">
            <DomainPropertyMoniker Name="TransactionModel/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="instanceId">
            <DomainPropertyMoniker Name="TransactionModel/InstanceId" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="namespace">
            <DomainPropertyMoniker Name="TransactionModel/Namespace" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="getValueImplementationType">
            <DomainPropertyMoniker Name="TransactionModel/GetValueImplementationType" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="sequenceFactoryImplementationType">
            <DomainPropertyMoniker Name="TransactionModel/SequenceFactoryImplementationType" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="databaseServerInstance">
            <DomainPropertyMoniker Name="TransactionModel/DatabaseServerInstance" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="databaseInitialCatalog">
            <DomainPropertyMoniker Name="TransactionModel/DatabaseInitialCatalog" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="databaseUser">
            <DomainPropertyMoniker Name="TransactionModel/DatabaseUser" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="databasePassword">
            <DomainPropertyMoniker Name="TransactionModel/DatabasePassword" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="transactionEnvironmentLayer">
            <DomainRelationshipMoniker Name="TransactionModelHasTransactionEnvironmentLayer" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="satelliteInstances">
            <DomainPropertyMoniker Name="TransactionModel/SatelliteInstances" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="scriptVersion">
            <DomainPropertyMoniker Name="TransactionModel/ScriptVersion" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="TransactionHandler" MonikerAttributeName="" SerializeId="true" MonikerElementName="transactionHandlerMoniker" ElementName="transactionHandler" MonikerTypeName="TransactionHandlerMoniker">
        <DomainClassMoniker Name="TransactionHandler" />
        <ElementData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="forwarderTransactionHandlers">
            <DomainRelationshipMoniker Name="TransactionHandlerReferencesForwarderTransactionHandlers" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="name">
            <DomainPropertyMoniker Name="TransactionHandler/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="transactionId">
            <DomainPropertyMoniker Name="TransactionHandler/TransactionId" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="maintenanceTransactionHandler">
            <DomainRelationshipMoniker Name="TransactionHandlerReferencesMaintenanceTransactionHandler" />
          </XmlRelationshipData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="outputTransactionEngine">
            <DomainRelationshipMoniker Name="TransactionHandlerReferencesOutputTransactionEngine" />
          </XmlRelationshipData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="transactionSQLServerDataSources">
            <DomainRelationshipMoniker Name="TransactionHandlerReferencesTransactionSQLServerDataSources" />
          </XmlRelationshipData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="outputTransactionWebService">
            <DomainRelationshipMoniker Name="TransactionHandlerReferencesOutputTransactionWebService" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="loggerName">
            <DomainPropertyMoniker Name="TransactionHandler/LoggerName" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="TransactionHandlerReferencesForwarderTransactionHandlers" MonikerAttributeName="" SerializeId="true" MonikerElementName="transactionHandlerReferencesForwarderTransactionHandlersMoniker" ElementName="transactionHandlerReferencesForwarderTransactionHandlers" MonikerTypeName="TransactionHandlerReferencesForwarderTransactionHandlersMoniker">
        <DomainRelationshipMoniker Name="TransactionHandlerReferencesForwarderTransactionHandlers" />
        <ElementData>
          <XmlPropertyData XmlName="isRoutingLink">
            <DomainPropertyMoniker Name="TransactionHandlerReferencesForwarderTransactionHandlers/IsRoutingLink" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="sequenceOrder">
            <DomainPropertyMoniker Name="TransactionHandlerReferencesForwarderTransactionHandlers/SequenceOrder" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="TransactionHandlerShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="transactionHandlerShapeMoniker" ElementName="transactionHandlerShape" MonikerTypeName="TransactionHandlerShapeMoniker">
        <GeometryShapeMoniker Name="TransactionHandlerShape" />
      </XmlClassData>
      <XmlClassData TypeName="TransactionHandlerForwardingLinkConnector" MonikerAttributeName="" SerializeId="true" MonikerElementName="transactionHandlerForwardingLinkConnectorMoniker" ElementName="transactionHandlerForwardingLinkConnector" MonikerTypeName="TransactionHandlerForwardingLinkConnectorMoniker">
        <ConnectorMoniker Name="TransactionHandlerForwardingLinkConnector" />
      </XmlClassData>
      <XmlClassData TypeName="TrnxDslDiagram" MonikerAttributeName="" SerializeId="true" MonikerElementName="trnxDslDiagramMoniker" ElementName="trnxDslDiagram" MonikerTypeName="trnxDslDiagramMoniker">
        <DiagramMoniker Name="TrnxDslDiagram" />
      </XmlClassData>
      <XmlClassData TypeName="InputTransactionEngineShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="inputTransactionEngineShapeMoniker" ElementName="inputTransactionEngineShape" MonikerTypeName="InputTransactionEngineShapeMoniker">
        <CompartmentShapeMoniker Name="InputTransactionEngineShape" />
      </XmlClassData>
      <XmlClassData TypeName="OutputTransactionEngineReferencesTransactionHandlersLinkConnector" MonikerAttributeName="" SerializeId="true" MonikerElementName="outputTransactionEngineReferencesTransactionHandlersLinkConnectorMoniker" ElementName="outputTransactionEngineReferencesTransactionHandlersLinkConnector" MonikerTypeName="OutputTransactionEngineReferencesTransactionHandlersLinkConnectorMoniker">
        <ConnectorMoniker Name="OutputTransactionEngineReferencesTransactionHandlersLinkConnector" />
      </XmlClassData>
      <XmlClassData TypeName="TransactionLayer" MonikerAttributeName="name" SerializeId="true" MonikerElementName="transactionLayerMoniker" ElementName="transactionLayer" MonikerTypeName="TransactionLayerMoniker">
        <DomainClassMoniker Name="TransactionLayer" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="TransactionLayer/Name" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="transactionHandlers">
            <DomainRelationshipMoniker Name="TransactionLayerHasTransactionHandlers" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="level">
            <DomainPropertyMoniker Name="TransactionLayer/Level" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="inputTransactionEngines">
            <DomainRelationshipMoniker Name="TransactionLayerHasInputTransactionEngines" />
          </XmlRelationshipData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="outputTransactionEngines">
            <DomainRelationshipMoniker Name="TransactionLayerHasOutputTransactionEngines" />
          </XmlRelationshipData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="transactionSQLServerDataSources">
            <DomainRelationshipMoniker Name="TransactionLayerHasTransactionSQLServerDataSources" />
          </XmlRelationshipData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="outputTransactionWebServices">
            <DomainRelationshipMoniker Name="TransactionLayerHasOutputTransactionWebServices" />
          </XmlRelationshipData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="timeTriggers">
            <DomainRelationshipMoniker Name="TransactionLayerHasTimeTriggers" />
          </XmlRelationshipData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="transactionDataSourceSupports">
            <DomainRelationshipMoniker Name="TransactionLayerHasTransactionDataSourceSupports" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="TransactionModelHasTransactionLayers" MonikerAttributeName="" SerializeId="true" MonikerElementName="transactionModelHasTransactionLayersMoniker" ElementName="transactionModelHasTransactionLayers" MonikerTypeName="TransactionModelHasTransactionLayersMoniker">
        <DomainRelationshipMoniker Name="TransactionModelHasTransactionLayers" />
      </XmlClassData>
      <XmlClassData TypeName="TransactionLayerHasTransactionHandlers" MonikerAttributeName="" SerializeId="true" MonikerElementName="transactionLayerHasTransactionHandlersMoniker" ElementName="transactionLayerHasTransactionHandlers" MonikerTypeName="TransactionLayerHasTransactionHandlersMoniker">
        <DomainRelationshipMoniker Name="TransactionLayerHasTransactionHandlers" />
      </XmlClassData>
      <XmlClassData TypeName="TransactionHandlerReferencesMaintenanceTransactionHandler" MonikerAttributeName="" SerializeId="true" MonikerElementName="transactionHandlerReferencesMaintenanceTransactionHandlerMoniker" ElementName="transactionHandlerReferencesMaintenanceTransactionHandler" MonikerTypeName="TransactionHandlerReferencesMaintenanceTransactionHandlerMoniker">
        <DomainRelationshipMoniker Name="TransactionHandlerReferencesMaintenanceTransactionHandler" />
        <ElementData>
          <XmlPropertyData XmlName="hasCustomCode">
            <DomainPropertyMoniker Name="TransactionHandlerReferencesMaintenanceTransactionHandler/HasCustomCode" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="TransactionHandlerMaintenanceLinkConnector" MonikerAttributeName="" SerializeId="true" MonikerElementName="transactionHandlerMaintenanceLinkConnectorMoniker" ElementName="transactionHandlerMaintenanceLinkConnector" MonikerTypeName="TransactionHandlerMaintenanceLinkConnectorMoniker">
        <ConnectorMoniker Name="TransactionHandlerMaintenanceLinkConnector" />
      </XmlClassData>
      <XmlClassData TypeName="TransactionDataSourceShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="transactionDataSourceShapeMoniker" ElementName="transactionDataSourceShape" MonikerTypeName="TransactionDataSourceShapeMoniker">
        <GeometryShapeMoniker Name="TransactionDataSourceShape" />
      </XmlClassData>
      <XmlClassData TypeName="TransactionSQLServerDataSourceLinkConnector" MonikerAttributeName="" SerializeId="true" MonikerElementName="transactionSQLServerDataSourceLinkConnectorMoniker" ElementName="transactionSQLServerDataSourceLinkConnector" MonikerTypeName="TransactionSQLServerDataSourceLinkConnectorMoniker">
        <ConnectorMoniker Name="TransactionSQLServerDataSourceLinkConnector" />
      </XmlClassData>
      <XmlClassData TypeName="TransactionLayerSwimLane" MonikerAttributeName="" SerializeId="true" MonikerElementName="transactionLayerSwimLaneMoniker" ElementName="transactionLayerSwimLane" MonikerTypeName="TransactionLayerSwimLaneMoniker">
        <SwimLaneMoniker Name="TransactionLayerSwimLane" />
      </XmlClassData>
      <XmlClassData TypeName="InputTransactionEngine" MonikerAttributeName="" SerializeId="true" MonikerElementName="inputTransactionEngineMoniker" ElementName="inputTransactionEngine" MonikerTypeName="InputTransactionEngineMoniker">
        <DomainClassMoniker Name="InputTransactionEngine" />
        <ElementData>
          <XmlPropertyData XmlName="hasUnitTests">
            <DomainPropertyMoniker Name="InputTransactionEngine/HasUnitTests" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="type">
            <DomainPropertyMoniker Name="InputTransactionEngine/Type" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="transactionHandlers">
            <DomainRelationshipMoniker Name="InputTransactionEngineReferencesTransactionHandlers" />
          </XmlRelationshipData>
          <XmlPropertyData XmlName="generatesSequenceNumber">
            <DomainPropertyMoniker Name="InputTransactionEngine/GeneratesSequenceNumber" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="timeTriggers">
            <DomainRelationshipMoniker Name="InputTransactionEngineReferencesTimeTriggers" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="OutputTransactionEngine" MonikerAttributeName="" SerializeId="true" MonikerElementName="outputTransactionEngineMoniker" ElementName="outputTransactionEngine" MonikerTypeName="OutputTransactionEngineMoniker">
        <DomainClassMoniker Name="OutputTransactionEngine" />
        <ElementData>
          <XmlPropertyData XmlName="type">
            <DomainPropertyMoniker Name="OutputTransactionEngine/Type" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="engineLoggerName">
            <DomainPropertyMoniker Name="OutputTransactionEngine/EngineLoggerName" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="transactionDataSourceSupport">
            <DomainRelationshipMoniker Name="OutputTransactionEngineReferencesTransactionDataSourceSupport" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="OutputTransactionEngineShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="outputTransactionEngineShapeMoniker" ElementName="outputTransactionEngineShape" MonikerTypeName="OutputTransactionEngineShapeMoniker">
        <CompartmentShapeMoniker Name="OutputTransactionEngineShape" />
      </XmlClassData>
      <XmlClassData TypeName="InputTransactionEngineReferencesTransactionHandlers" MonikerAttributeName="" SerializeId="true" MonikerElementName="inputTransactionEngineReferencesTransactionHandlersMoniker" ElementName="inputTransactionEngineReferencesTransactionHandlers" MonikerTypeName="InputTransactionEngineReferencesTransactionHandlersMoniker">
        <DomainRelationshipMoniker Name="InputTransactionEngineReferencesTransactionHandlers" />
        <ElementData>
          <XmlPropertyData XmlName="hasCustomCode">
            <DomainPropertyMoniker Name="InputTransactionEngineReferencesTransactionHandlers/HasCustomCode" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="TransactionHandlerReferencesOutputTransactionEngine" MonikerAttributeName="" SerializeId="true" MonikerElementName="transactionHandlerReferencesOutputTransactionEngineMoniker" ElementName="transactionHandlerReferencesOutputTransactionEngine" MonikerTypeName="TransactionHandlerReferencesOutputTransactionEngineMoniker">
        <DomainRelationshipMoniker Name="TransactionHandlerReferencesOutputTransactionEngine" />
        <ElementData>
          <XmlPropertyData XmlName="hasCustomCode">
            <DomainPropertyMoniker Name="TransactionHandlerReferencesOutputTransactionEngine/HasCustomCode" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="InputTransactionEngineReferencesTransactionHandlersLinkConnector" MonikerAttributeName="" SerializeId="true" MonikerElementName="inputTransactionEngineReferencesTransactionHandlersLinkConnectorMoniker" ElementName="inputTransactionEngineReferencesTransactionHandlersLinkConnector" MonikerTypeName="InputTransactionEngineReferencesTransactionHandlersLinkConnectorMoniker">
        <ConnectorMoniker Name="InputTransactionEngineReferencesTransactionHandlersLinkConnector" />
      </XmlClassData>
      <XmlClassData TypeName="TransactionLayerHasInputTransactionEngines" MonikerAttributeName="" SerializeId="true" MonikerElementName="transactionLayerHasInputTransactionEnginesMoniker" ElementName="transactionLayerHasInputTransactionEngines" MonikerTypeName="TransactionLayerHasInputTransactionEnginesMoniker">
        <DomainRelationshipMoniker Name="TransactionLayerHasInputTransactionEngines" />
      </XmlClassData>
      <XmlClassData TypeName="TransactionLayerHasOutputTransactionEngines" MonikerAttributeName="" SerializeId="true" MonikerElementName="transactionLayerHasOutputTransactionEnginesMoniker" ElementName="transactionLayerHasOutputTransactionEngines" MonikerTypeName="TransactionLayerHasOutputTransactionEnginesMoniker">
        <DomainRelationshipMoniker Name="TransactionLayerHasOutputTransactionEngines" />
      </XmlClassData>
      <XmlClassData TypeName="TransactionEngine" MonikerAttributeName="name" SerializeId="true" MonikerElementName="transactionEngineMoniker" ElementName="transactionEngine" MonikerTypeName="TransactionEngineMoniker">
        <DomainClassMoniker Name="TransactionEngine" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="TransactionEngine/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="autoStarts">
            <DomainPropertyMoniker Name="TransactionEngine/AutoStarts" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="parser">
            <DomainPropertyMoniker Name="TransactionEngine/Parser" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="parserSectionName">
            <DomainPropertyMoniker Name="TransactionEngine/ParserSectionName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="loggerName">
            <DomainPropertyMoniker Name="TransactionEngine/LoggerName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="parserLoggerName">
            <DomainPropertyMoniker Name="TransactionEngine/ParserLoggerName" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="TransactionSQLServerDataSource" MonikerAttributeName="" SerializeId="true" MonikerElementName="transactionSQLServerDataSourceMoniker" ElementName="transactionSQLServerDataSource" MonikerTypeName="TransactionSQLServerDataSourceMoniker">
        <DomainClassMoniker Name="TransactionSQLServerDataSource" />
        <ElementData>
          <XmlPropertyData XmlName="instanceName">
            <DomainPropertyMoniker Name="TransactionSQLServerDataSource/InstanceName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="databaseName">
            <DomainPropertyMoniker Name="TransactionSQLServerDataSource/DatabaseName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="connectionString">
            <DomainPropertyMoniker Name="TransactionSQLServerDataSource/ConnectionString" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="storedProcedure">
            <DomainPropertyMoniker Name="TransactionSQLServerDataSource/StoredProcedure" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="executedOnStage">
            <DomainPropertyMoniker Name="TransactionSQLServerDataSource/ExecutedOnStage" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="TransactionHandlerReferencesTransactionSQLServerDataSources" MonikerAttributeName="" SerializeId="true" MonikerElementName="transactionHandlerReferencesTransactionSQLServerDataSourcesMoniker" ElementName="transactionHandlerReferencesTransactionSQLServerDataSources" MonikerTypeName="TransactionHandlerReferencesTransactionSQLServerDataSourcesMoniker">
        <DomainRelationshipMoniker Name="TransactionHandlerReferencesTransactionSQLServerDataSources" />
        <ElementData>
          <XmlPropertyData XmlName="hasCustomCode">
            <DomainPropertyMoniker Name="TransactionHandlerReferencesTransactionSQLServerDataSources/HasCustomCode" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="TransactionLayerHasTransactionSQLServerDataSources" MonikerAttributeName="" SerializeId="true" MonikerElementName="transactionLayerHasTransactionSQLServerDataSourcesMoniker" ElementName="transactionLayerHasTransactionSQLServerDataSources" MonikerTypeName="TransactionLayerHasTransactionSQLServerDataSourcesMoniker">
        <DomainRelationshipMoniker Name="TransactionLayerHasTransactionSQLServerDataSources" />
      </XmlClassData>
      <XmlClassData TypeName="TransactionDataSource" MonikerAttributeName="name" SerializeId="true" MonikerElementName="transactionDataSourceMoniker" ElementName="transactionDataSource" MonikerTypeName="TransactionDataSourceMoniker">
        <DomainClassMoniker Name="TransactionDataSource" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="TransactionDataSource/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="stepOrder">
            <DomainPropertyMoniker Name="TransactionDataSource/StepOrder" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="TransactionWebService" MonikerAttributeName="name" SerializeId="true" MonikerElementName="transactionWebServiceMoniker" ElementName="transactionWebService" MonikerTypeName="TransactionWebServiceMoniker">
        <DomainClassMoniker Name="TransactionWebService" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="TransactionWebService/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="webServiceClassName">
            <DomainPropertyMoniker Name="TransactionWebService/WebServiceClassName" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="url">
            <DomainPropertyMoniker Name="TransactionWebService/Url" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="architecturalPattern">
            <DomainPropertyMoniker Name="TransactionWebService/ArchitecturalPattern" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="technology">
            <DomainPropertyMoniker Name="TransactionWebService/Technology" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="OutputTransactionWebService" MonikerAttributeName="" SerializeId="true" MonikerElementName="outputTransactionWebServiceMoniker" ElementName="outputTransactionWebService" MonikerTypeName="OutputTransactionWebServiceMoniker">
        <DomainClassMoniker Name="OutputTransactionWebService" />
      </XmlClassData>
      <XmlClassData TypeName="TransactionLayerHasOutputTransactionWebServices" MonikerAttributeName="" SerializeId="true" MonikerElementName="transactionLayerHasOutputTransactionWebServicesMoniker" ElementName="transactionLayerHasOutputTransactionWebServices" MonikerTypeName="TransactionLayerHasOutputTransactionWebServicesMoniker">
        <DomainRelationshipMoniker Name="TransactionLayerHasOutputTransactionWebServices" />
      </XmlClassData>
      <XmlClassData TypeName="TransactionHandlerReferencesOutputTransactionWebService" MonikerAttributeName="" SerializeId="true" MonikerElementName="transactionHandlerReferencesOutputTransactionWebServiceMoniker" ElementName="transactionHandlerReferencesOutputTransactionWebService" MonikerTypeName="TransactionHandlerReferencesOutputTransactionWebServiceMoniker">
        <DomainRelationshipMoniker Name="TransactionHandlerReferencesOutputTransactionWebService" />
      </XmlClassData>
      <XmlClassData TypeName="OutputTransactionWebServiceShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="outputTransactionWebServiceShapeMoniker" ElementName="outputTransactionWebServiceShape" MonikerTypeName="OutputTransactionWebServiceShapeMoniker">
        <GeometryShapeMoniker Name="OutputTransactionWebServiceShape" />
      </XmlClassData>
      <XmlClassData TypeName="OutputTransactionWebServiceLinkConnector" MonikerAttributeName="" SerializeId="true" MonikerElementName="outputTransactionWebServiceLinkConnectorMoniker" ElementName="outputTransactionWebServiceLinkConnector" MonikerTypeName="OutputTransactionWebServiceLinkConnectorMoniker">
        <ConnectorMoniker Name="OutputTransactionWebServiceLinkConnector" />
      </XmlClassData>
      <XmlClassData TypeName="TimeTrigger" MonikerAttributeName="name" SerializeId="true" MonikerElementName="timeTriggerMoniker" ElementName="timeTrigger" MonikerTypeName="TimeTriggerMoniker">
        <DomainClassMoniker Name="TimeTrigger" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="TimeTrigger/Name" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="TransactionLayerHasTimeTriggers" MonikerAttributeName="" SerializeId="true" MonikerElementName="transactionLayerHasTimeTriggersMoniker" ElementName="transactionLayerHasTimeTriggers" MonikerTypeName="TransactionLayerHasTimeTriggersMoniker">
        <DomainRelationshipMoniker Name="TransactionLayerHasTimeTriggers" />
      </XmlClassData>
      <XmlClassData TypeName="InputTransactionEngineReferencesTimeTriggers" MonikerAttributeName="" SerializeId="true" MonikerElementName="inputTransactionEngineReferencesTimeTriggersMoniker" ElementName="inputTransactionEngineReferencesTimeTriggers" MonikerTypeName="InputTransactionEngineReferencesTimeTriggersMoniker">
        <DomainRelationshipMoniker Name="InputTransactionEngineReferencesTimeTriggers" />
      </XmlClassData>
      <XmlClassData TypeName="TimeTriggerShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="timeTriggerShapeMoniker" ElementName="timeTriggerShape" MonikerTypeName="TimeTriggerShapeMoniker">
        <GeometryShapeMoniker Name="TimeTriggerShape" />
      </XmlClassData>
      <XmlClassData TypeName="TimeTriggerLinkConnector" MonikerAttributeName="" SerializeId="true" MonikerElementName="timeTriggerLinkConnectorMoniker" ElementName="timeTriggerLinkConnector" MonikerTypeName="TimeTriggerLinkConnectorMoniker">
        <ConnectorMoniker Name="TimeTriggerLinkConnector" />
      </XmlClassData>
      <XmlClassData TypeName="TimeTriggerImageShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="timeTriggerImageShapeMoniker" ElementName="timeTriggerImageShape" MonikerTypeName="TimeTriggerImageShapeMoniker">
        <ImageShapeMoniker Name="TimeTriggerImageShape" />
      </XmlClassData>
      <XmlClassData TypeName="TransactionDataSourceSupportForOutputEngineLink" MonikerAttributeName="" SerializeId="true" MonikerElementName="transactionDataSourceSupportForOutputEngineLinkMoniker" ElementName="transactionDataSourceSupportForOutputEngineLink" MonikerTypeName="TransactionDataSourceSupportForOutputEngineLinkMoniker">
        <ConnectorMoniker Name="TransactionDataSourceSupportForOutputEngineLink" />
      </XmlClassData>
      <XmlClassData TypeName="TransactionDataSourceSupport" MonikerAttributeName="name" SerializeId="true" MonikerElementName="transactionDataSourceSupportMoniker" ElementName="transactionDataSourceSupport" MonikerTypeName="TransactionDataSourceSupportMoniker">
        <DomainClassMoniker Name="TransactionDataSourceSupport" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="TransactionDataSourceSupport/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="supportType">
            <DomainPropertyMoniker Name="TransactionDataSourceSupport/SupportType" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="OutputTransactionEngineReferencesTransactionDataSourceSupport" MonikerAttributeName="" SerializeId="true" MonikerElementName="outputTransactionEngineReferencesTransactionDataSourceSupportMoniker" ElementName="outputTransactionEngineReferencesTransactionDataSourceSupport" MonikerTypeName="OutputTransactionEngineReferencesTransactionDataSourceSupportMoniker">
        <DomainRelationshipMoniker Name="OutputTransactionEngineReferencesTransactionDataSourceSupport" />
      </XmlClassData>
      <XmlClassData TypeName="TransactionDataSourceSupportShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="transactionDataSourceSupportShapeMoniker" ElementName="transactionDataSourceSupportShape" MonikerTypeName="TransactionDataSourceSupportShapeMoniker">
        <GeometryShapeMoniker Name="TransactionDataSourceSupportShape" />
      </XmlClassData>
      <XmlClassData TypeName="TransactionLayerHasTransactionDataSourceSupports" MonikerAttributeName="" SerializeId="true" MonikerElementName="transactionLayerHasTransactionDataSourceSupportsMoniker" ElementName="transactionLayerHasTransactionDataSourceSupports" MonikerTypeName="TransactionLayerHasTransactionDataSourceSupportsMoniker">
        <DomainRelationshipMoniker Name="TransactionLayerHasTransactionDataSourceSupports" />
      </XmlClassData>
      <XmlClassData TypeName="TransactionEnvironmentLayer" MonikerAttributeName="name" SerializeId="true" MonikerElementName="transactionEnvironmentLayerMoniker" ElementName="transactionEnvironmentLayer" MonikerTypeName="TransactionEnvironmentLayerMoniker">
        <DomainClassMoniker Name="TransactionEnvironmentLayer" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="TransactionEnvironmentLayer/Name" />
          </XmlPropertyData>
          <XmlRelationshipData UseFullForm="true" RoleElementName="transactionEnvironmentSQLServerVariables">
            <DomainRelationshipMoniker Name="TransactionEnvironmentLayerHasTransactionEnvironmentSQLServerVariables" />
          </XmlRelationshipData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="TransactionModelHasTransactionEnvironmentLayer" MonikerAttributeName="" SerializeId="true" MonikerElementName="transactionModelHasTransactionEnvironmentLayerMoniker" ElementName="transactionModelHasTransactionEnvironmentLayer" MonikerTypeName="TransactionModelHasTransactionEnvironmentLayerMoniker">
        <DomainRelationshipMoniker Name="TransactionModelHasTransactionEnvironmentLayer" />
      </XmlClassData>
      <XmlClassData TypeName="TransactionEnvironmentLayerSwimLane" MonikerAttributeName="" SerializeId="true" MonikerElementName="transactionEnvironmentLayerSwimLaneMoniker" ElementName="transactionEnvironmentLayerSwimLane" MonikerTypeName="TransactionEnvironmentLayerSwimLaneMoniker">
        <SwimLaneMoniker Name="TransactionEnvironmentLayerSwimLane" />
      </XmlClassData>
      <XmlClassData TypeName="TransactionEnvironmentSQLServerVariable" MonikerAttributeName="name" SerializeId="true" MonikerElementName="transactionEnvironmentSQLServerVariableMoniker" ElementName="transactionEnvironmentSQLServerVariable" MonikerTypeName="TransactionEnvironmentSQLServerVariableMoniker">
        <DomainClassMoniker Name="TransactionEnvironmentSQLServerVariable" />
        <ElementData>
          <XmlPropertyData XmlName="name" IsMonikerKey="true">
            <DomainPropertyMoniker Name="TransactionEnvironmentSQLServerVariable/Name" />
          </XmlPropertyData>
          <XmlPropertyData XmlName="defaultValue">
            <DomainPropertyMoniker Name="TransactionEnvironmentSQLServerVariable/DefaultValue" />
          </XmlPropertyData>
        </ElementData>
      </XmlClassData>
      <XmlClassData TypeName="TransactionEnvironmentSQLServerVariableShape" MonikerAttributeName="" SerializeId="true" MonikerElementName="transactionEnvironmentSQLServerVariableShapeMoniker" ElementName="transactionEnvironmentSQLServerVariableShape" MonikerTypeName="TransactionEnvironmentSQLServerVariableShapeMoniker">
        <GeometryShapeMoniker Name="TransactionEnvironmentSQLServerVariableShape" />
      </XmlClassData>
      <XmlClassData TypeName="TransactionEnvironmentLayerHasTransactionEnvironmentSQLServerVariables" MonikerAttributeName="" SerializeId="true" MonikerElementName="transactionEnvironmentLayerHasTransactionEnvironmentSQLServerVariablesMoniker" ElementName="transactionEnvironmentLayerHasTransactionEnvironmentSQLServerVariables" MonikerTypeName="TransactionEnvironmentLayerHasTransactionEnvironmentSQLServerVariablesMoniker">
        <DomainRelationshipMoniker Name="TransactionEnvironmentLayerHasTransactionEnvironmentSQLServerVariables" />
      </XmlClassData>
    </ClassData>
  </XmlSerializationBehavior>
  <ExplorerBehavior Name="TrnxDslExplorer">
    <CustomNodeSettings>
      <ExplorerNodeSettings IconToDisplay="Resources\InputTransactionEngineIcon.bmp">
        <Class>
          <DomainClassMoniker Name="InputTransactionEngine" />
        </Class>
        <PropertyDisplayed>
          <PropertyPath>
            <DomainPropertyMoniker Name="TransactionEngine/Name" />
            <DomainPath />
          </PropertyPath>
        </PropertyDisplayed>
      </ExplorerNodeSettings>
      <ExplorerNodeSettings IconToDisplay="Resources\OutputTransactionEngineIcon.bmp">
        <Class>
          <DomainClassMoniker Name="OutputTransactionEngine" />
        </Class>
        <PropertyDisplayed>
          <PropertyPath>
            <DomainPropertyMoniker Name="TransactionEngine/Name" />
            <DomainPath />
          </PropertyPath>
        </PropertyDisplayed>
      </ExplorerNodeSettings>
      <ExplorerNodeSettings IconToDisplay="Resources\OutputWebServiceIcon.bmp">
        <Class>
          <DomainClassMoniker Name="OutputTransactionWebService" />
        </Class>
        <PropertyDisplayed>
          <PropertyPath>
            <DomainPropertyMoniker Name="TransactionWebService/Name" />
            <DomainPath />
          </PropertyPath>
        </PropertyDisplayed>
      </ExplorerNodeSettings>
      <ExplorerNodeSettings IconToDisplay="Resources\TransactionHandlerIcon.bmp">
        <Class>
          <DomainClassMoniker Name="TransactionHandler" />
        </Class>
        <PropertyDisplayed>
          <PropertyPath>
            <DomainPropertyMoniker Name="TransactionHandler/Name" />
            <DomainPath />
          </PropertyPath>
        </PropertyDisplayed>
      </ExplorerNodeSettings>
    </CustomNodeSettings>
  </ExplorerBehavior>
  <ConnectionBuilders>
    <ConnectionBuilder Name="TransactionHandlerReferencesForwarderTransactionHandlersBuilder">
      <Notes>Provides for the creation of an ExampleRelationship by pointing at two ExampleElements.</Notes>
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="TransactionHandlerReferencesForwarderTransactionHandlers" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="TransactionHandler" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="TransactionHandler" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="TransactionHandlerReferencesMaintenanceTransactionHandlerBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="TransactionHandlerReferencesMaintenanceTransactionHandler" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="TransactionHandler" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="TransactionHandler" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="InputTransactionEngineReferencesTransactionHandlersBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="InputTransactionEngineReferencesTransactionHandlers" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="InputTransactionEngine" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="TransactionHandler" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="TransactionHandlerReferencesOutputTransactionEngineBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="TransactionHandlerReferencesOutputTransactionEngine" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="TransactionHandler" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="OutputTransactionEngine" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="TransactionHandlerReferencesTransactionSQLServerDataSourcesBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="TransactionHandlerReferencesTransactionSQLServerDataSources" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="TransactionHandler" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="TransactionSQLServerDataSource" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="TransactionHandlerReferencesOutputTransactionWebServiceBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="TransactionHandlerReferencesOutputTransactionWebService" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="TransactionHandler" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="OutputTransactionWebService" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="InputTransactionEngineReferencesTimeTriggersBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="InputTransactionEngineReferencesTimeTriggers" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="InputTransactionEngine" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="TimeTrigger" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
    <ConnectionBuilder Name="OutputTransactionEngineReferencesTransactionDataSourceSupportBuilder">
      <LinkConnectDirective>
        <DomainRelationshipMoniker Name="OutputTransactionEngineReferencesTransactionDataSourceSupport" />
        <SourceDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="OutputTransactionEngine" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </SourceDirectives>
        <TargetDirectives>
          <RolePlayerConnectDirective>
            <AcceptingClass>
              <DomainClassMoniker Name="TransactionDataSourceSupport" />
            </AcceptingClass>
          </RolePlayerConnectDirective>
        </TargetDirectives>
      </LinkConnectDirective>
    </ConnectionBuilder>
  </ConnectionBuilders>
  <Diagram Id="77f68ba6-65e6-488c-a7bb-228f13951eb1" Description="Description for TransactionKernelDSL.Framework.Language.TrnxDslDiagram" Name="TrnxDslDiagram" DisplayName="Minimal Language Diagram" Namespace="TransactionKernelDSL.Framework.Language">
    <Class>
      <DomainClassMoniker Name="TransactionModel" />
    </Class>
    <ShapeMaps>
      <ShapeMap>
        <DomainClassMoniker Name="TransactionHandler" />
        <ParentElementPath>
          <DomainPath>TransactionLayerHasTransactionHandlers.TransactionLayer/!TransactionLayer/TransactionModelHasTransactionLayers.TransactionModel/!TransactionModel</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="TransactionHandlerShape/NameDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="TransactionHandler/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <TextDecoratorMoniker Name="TransactionHandlerShape/TransactionIdDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="TransactionHandler/TransactionId" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <GeometryShapeMoniker Name="TransactionHandlerShape" />
      </ShapeMap>
      <SwimLaneMap>
        <DomainClassMoniker Name="TransactionLayer" />
        <ParentElementPath>
          <DomainPath>TransactionModelHasTransactionLayers.TransactionModel/!TransactionModel</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="TransactionLayerSwimLane/LevelDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="TransactionLayer/Level" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <TextDecoratorMoniker Name="TransactionLayerSwimLane/NameDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="TransactionLayer/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <SwimLane>
          <SwimLaneMoniker Name="TransactionLayerSwimLane" />
        </SwimLane>
      </SwimLaneMap>
      <CompartmentShapeMap>
        <DomainClassMoniker Name="InputTransactionEngine" />
        <ParentElementPath>
          <DomainPath>TransactionLayerHasInputTransactionEngines.TransactionLayer/!TransactionLayer/TransactionModelHasTransactionLayers.TransactionModel/!TransactionModel</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="InputTransactionEngineShape/NameDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="TransactionEngine/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <TextDecoratorMoniker Name="InputTransactionEngineShape/TypeDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="InputTransactionEngine/Type" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <CompartmentShapeMoniker Name="InputTransactionEngineShape" />
        <CompartmentMap>
          <CompartmentMoniker Name="InputTransactionEngineShape/TransactionHandler" />
          <ElementsDisplayed>
            <DomainPath>InputTransactionEngineReferencesTransactionHandlers.TransactionHandlers/!TransactionHandler</DomainPath>
          </ElementsDisplayed>
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="TransactionHandler/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </CompartmentMap>
      </CompartmentShapeMap>
      <CompartmentShapeMap>
        <DomainClassMoniker Name="OutputTransactionEngine" />
        <ParentElementPath>
          <DomainPath>TransactionLayerHasOutputTransactionEngines.TransactionLayer/!TransactionLayer/TransactionModelHasTransactionLayers.TransactionModel/!TransactionModel</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="OutputTransactionEngineShape/NameDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="TransactionEngine/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <TextDecoratorMoniker Name="OutputTransactionEngineShape/TypeDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="OutputTransactionEngine/Type" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <CompartmentShapeMoniker Name="OutputTransactionEngineShape" />
        <CompartmentMap>
          <CompartmentMoniker Name="OutputTransactionEngineShape/TransactionHandler" />
          <ElementsDisplayed>
            <DomainPath>TransactionHandlerReferencesOutputTransactionEngine.SourceTransactionHandlerOutputEngines/!SourceTransactionHandlerOutputEngine</DomainPath>
          </ElementsDisplayed>
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="TransactionHandler/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </CompartmentMap>
      </CompartmentShapeMap>
      <ShapeMap>
        <DomainClassMoniker Name="TransactionSQLServerDataSource" />
        <ParentElementPath>
          <DomainPath>TransactionLayerHasTransactionSQLServerDataSources.TransactionLayer/!TransactionLayer</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="TransactionDataSourceShape/NameDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="TransactionDataSource/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <TextDecoratorMoniker Name="TransactionDataSourceShape/StepOrderDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="TransactionDataSource/StepOrder" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <GeometryShapeMoniker Name="TransactionDataSourceShape" />
      </ShapeMap>
      <ShapeMap>
        <DomainClassMoniker Name="OutputTransactionWebService" />
        <ParentElementPath>
          <DomainPath>TransactionLayerHasOutputTransactionWebServices.TransactionLayer/!TransactionLayer</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="OutputTransactionWebServiceShape/NameDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="TransactionWebService/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <DecoratorMap>
          <TextDecoratorMoniker Name="OutputTransactionWebServiceShape/WebServiceClassNameDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="TransactionWebService/WebServiceClassName" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <GeometryShapeMoniker Name="OutputTransactionWebServiceShape" />
      </ShapeMap>
      <ShapeMap>
        <DomainClassMoniker Name="TimeTrigger" />
        <ParentElementPath>
          <DomainPath>TransactionLayerHasTimeTriggers.TransactionLayer/!TransactionLayer</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="TimeTriggerImageShape/NameDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="TimeTrigger/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <ImageShapeMoniker Name="TimeTriggerImageShape" />
      </ShapeMap>
      <ShapeMap>
        <DomainClassMoniker Name="TransactionDataSourceSupport" />
        <ParentElementPath>
          <DomainPath>TransactionLayerHasTransactionDataSourceSupports.TransactionLayer/!TransactionLayer/TransactionModelHasTransactionLayers.TransactionModel/!TransactionModel</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="TransactionDataSourceSupportShape/NameDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="TransactionDataSourceSupport/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <GeometryShapeMoniker Name="TransactionDataSourceSupportShape" />
      </ShapeMap>
      <SwimLaneMap>
        <DomainClassMoniker Name="TransactionEnvironmentLayer" />
        <ParentElementPath>
          <DomainPath>TransactionModelHasTransactionEnvironmentLayer.TransactionModel/!TransactionModel</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="TransactionEnvironmentLayerSwimLane/NameDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="TransactionEnvironmentLayer/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <SwimLane>
          <SwimLaneMoniker Name="TransactionEnvironmentLayerSwimLane" />
        </SwimLane>
      </SwimLaneMap>
      <ShapeMap>
        <DomainClassMoniker Name="TransactionEnvironmentSQLServerVariable" />
        <ParentElementPath>
          <DomainPath>TransactionEnvironmentLayerHasTransactionEnvironmentSQLServerVariables.TransactionEnvironmentLayer/!TransactionEnvironmentLayer/TransactionModelHasTransactionEnvironmentLayer.TransactionModel/!TransactionModel</DomainPath>
        </ParentElementPath>
        <DecoratorMap>
          <TextDecoratorMoniker Name="TransactionEnvironmentSQLServerVariableShape/NameDecorator" />
          <PropertyDisplayed>
            <PropertyPath>
              <DomainPropertyMoniker Name="TransactionEnvironmentSQLServerVariable/Name" />
            </PropertyPath>
          </PropertyDisplayed>
        </DecoratorMap>
        <GeometryShapeMoniker Name="TransactionEnvironmentSQLServerVariableShape" />
      </ShapeMap>
    </ShapeMaps>
    <ConnectorMaps>
      <ConnectorMap>
        <ConnectorMoniker Name="TransactionHandlerForwardingLinkConnector" />
        <DomainRelationshipMoniker Name="TransactionHandlerReferencesForwarderTransactionHandlers" />
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="TransactionHandlerMaintenanceLinkConnector" />
        <DomainRelationshipMoniker Name="TransactionHandlerReferencesMaintenanceTransactionHandler" />
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="OutputTransactionEngineReferencesTransactionHandlersLinkConnector" />
        <DomainRelationshipMoniker Name="TransactionHandlerReferencesOutputTransactionEngine" />
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="InputTransactionEngineReferencesTransactionHandlersLinkConnector" />
        <DomainRelationshipMoniker Name="InputTransactionEngineReferencesTransactionHandlers" />
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="TransactionSQLServerDataSourceLinkConnector" />
        <DomainRelationshipMoniker Name="TransactionHandlerReferencesTransactionSQLServerDataSources" />
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="OutputTransactionWebServiceLinkConnector" />
        <DomainRelationshipMoniker Name="TransactionHandlerReferencesOutputTransactionWebService" />
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="TimeTriggerLinkConnector" />
        <DomainRelationshipMoniker Name="InputTransactionEngineReferencesTimeTriggers" />
      </ConnectorMap>
      <ConnectorMap>
        <ConnectorMoniker Name="TransactionDataSourceSupportForOutputEngineLink" />
        <DomainRelationshipMoniker Name="OutputTransactionEngineReferencesTransactionDataSourceSupport" />
      </ConnectorMap>
    </ConnectorMaps>
  </Diagram>
  <Designer CopyPasteGeneration="CopyPasteOnly" FileExtension="TrnxDsl" EditorGuid="6d402703-5918-4a5a-be3f-79d73d44ab6e">
    <RootClass>
      <DomainClassMoniker Name="TransactionModel" />
    </RootClass>
    <XmlSerializationDefinition CustomPostLoad="false">
      <XmlSerializationBehaviorMoniker Name="TrnxDslSerializationBehavior" />
    </XmlSerializationDefinition>
    <ToolboxTab TabText="Transaction Model Elements">
      <ConnectionTool Name="OutputWebServiceLinkTool" ToolboxIcon="Resources\OutputWebServiceLinkIcon.bmp" Caption="Output Web Service Link Tool" Tooltip="Output Web Service Link Tool" HelpKeyword="OutputWebServiceLinkTool" SourceCursorIcon="Resources\ConnectorSourceSearch.cur" TargetCursorIcon="Resources\ConnectorTargetSearch.cur">
        <ConnectionBuilderMoniker Name="TrnxDsl/TransactionHandlerReferencesOutputTransactionWebServiceBuilder" />
      </ConnectionTool>
      <ElementTool Name="OutputWebServiceTool" ToolboxIcon="Resources\OutputWebServiceIcon.bmp" Caption="Output Web Service Tool" Tooltip="Output Web Service Tool" HelpKeyword="OutputWebServiceTool">
        <DomainClassMoniker Name="OutputTransactionWebService" />
      </ElementTool>
      <ElementTool Name="TimeTriggerTool" ToolboxIcon="Resources\TimeTriggerIcon.bmp" Caption="Time Trigger Tool" Tooltip="Time Trigger Tool" HelpKeyword="TimeTriggerTool">
        <DomainClassMoniker Name="TimeTrigger" />
      </ElementTool>
      <ConnectionTool Name="TimeTriggerLinkTool" ToolboxIcon="Resources\TimeTriggerLinkIcon.bmp" Caption="Time Trigger Link Tool" Tooltip="Time Trigger Link Tool" HelpKeyword="TimeTriggerLinkTool" SourceCursorIcon="Resources\ConnectorSourceSearch.cur" TargetCursorIcon="Resources\ConnectorTargetSearch.cur">
        <ConnectionBuilderMoniker Name="TrnxDsl/InputTransactionEngineReferencesTimeTriggersBuilder" />
      </ConnectionTool>
    </ToolboxTab>
    <ToolboxTab TabText="Engines">
      <ElementTool Name="InputTransactionEngineTool" ToolboxIcon="Resources\InputTransactionEngineIcon.bmp" Caption="Input Transaction Engine Tool" Tooltip="Input Transaction Engine Tool" HelpKeyword="InputTransactionEngineTool">
        <DomainClassMoniker Name="InputTransactionEngine" />
      </ElementTool>
      <ConnectionTool Name="InputLinkTool" ToolboxIcon="Resources\InputLinkIcon.bmp" Caption="Input Link Tool" Tooltip="Input Link Tool" HelpKeyword="InputLinkTool" SourceCursorIcon="Resources\ConnectorSourceSearch.cur" TargetCursorIcon="Resources\ConnectorTargetSearch.cur">
        <ConnectionBuilderMoniker Name="TrnxDsl/InputTransactionEngineReferencesTransactionHandlersBuilder" />
      </ConnectionTool>
      <ElementTool Name="OutputTransactionEngineTool" ToolboxIcon="Resources\OutputTransactionEngineIcon.bmp" Caption="Output Transaction Engine Tool" Tooltip="Output Transaction Engine Tool" HelpKeyword="OutputTransactionEngineTool">
        <DomainClassMoniker Name="OutputTransactionEngine" />
      </ElementTool>
      <ConnectionTool Name="OutputLinkTool" ToolboxIcon="Resources\OutputLinkIcon.bmp" Caption="Output Link Tool" Tooltip="Output Link Tool" HelpKeyword="OutputLinkTool" SourceCursorIcon="Resources\ConnectorSourceSearch.cur" TargetCursorIcon="Resources\ConnectorTargetSearch.cur">
        <ConnectionBuilderMoniker Name="TrnxDsl/TransactionHandlerReferencesOutputTransactionEngineBuilder" />
      </ConnectionTool>
      <ConnectionTool Name="TransactionDataSourceSupportForOutputEngineLinkTool" ToolboxIcon="Resources\DataSourceSupportLinkIcon.bmp" Caption="Transaction Data Source Support For Output Engine Link " Tooltip="Transaction Data Source Support For Output Engine Link Tool" HelpKeyword="TransactionDataSourceSupportForOutputEngineLinkTool" SourceCursorIcon="Resources\ConnectorSourceSearch.cur" TargetCursorIcon="Resources\ConnectorTargetSearch.cur">
        <ConnectionBuilderMoniker Name="TrnxDsl/OutputTransactionEngineReferencesTransactionDataSourceSupportBuilder" />
      </ConnectionTool>
    </ToolboxTab>
    <ToolboxTab TabText="Handlers">
      <ElementTool Name="TransactionHandlerTool" ToolboxIcon="Resources\TransactionHandlerIcon.bmp" Caption="Transaction Handler Tool" Tooltip="Create a new Transaction Handler" HelpKeyword="CreateExampleClassF1Keyword">
        <DomainClassMoniker Name="TransactionHandler" />
      </ElementTool>
      <ConnectionTool Name="TransactionHandlerForwardingLinkTool" ToolboxIcon="Resources\ForwardingLinkIcon.bmp" Caption="Forwarding Link Tool" Tooltip="Drag between Transaction Handlers to create an Transaction Handler Forwarding Link" HelpKeyword="ConnectExampleRelationF1Keyword" SourceCursorIcon="Resources\ConnectorSourceSearch.cur" TargetCursorIcon="Resources\ConnectorTargetSearch.cur">
        <ConnectionBuilderMoniker Name="TrnxDsl/TransactionHandlerReferencesForwarderTransactionHandlersBuilder" />
      </ConnectionTool>
      <ConnectionTool Name="TransactionHandlerMaintenanceLinkTool" ToolboxIcon="Resources\MaintenanceLinkIcon.bmp" Caption="Maintenance Link Tool" Tooltip="Transaction Handler Maintenance Link Tool" HelpKeyword="TransactionHandlerMaintenanceLinkTool" SourceCursorIcon="Resources\ConnectorSourceSearch.cur" TargetCursorIcon="Resources\ConnectorTargetSearch.cur">
        <ConnectionBuilderMoniker Name="TrnxDsl/TransactionHandlerReferencesMaintenanceTransactionHandlerBuilder" />
      </ConnectionTool>
      <ConnectionTool Name="TransactionSQLServerDataSourceLinkConnectionTool" ToolboxIcon="Resources\PersistingLinkIcon.bmp" Caption="Transaction SQL Server Data Source Link Tool" Tooltip="Transaction SQLServer Data Source Link Connection Tool" HelpKeyword="TransactionSQLServerDataSourceLinkConnectionTool" SourceCursorIcon="Resources\ConnectorSourceSearch.cur" TargetCursorIcon="Resources\ConnectorTargetSearch.cur">
        <ConnectionBuilderMoniker Name="TrnxDsl/TransactionHandlerReferencesTransactionSQLServerDataSourcesBuilder" />
      </ConnectionTool>
    </ToolboxTab>
    <ToolboxTab TabText="Layers">
      <ElementTool Name="TransactionLayerTool" ToolboxIcon="Resources\TransactionLayerIcon.bmp" Caption="Transaction Layer Tool" Tooltip="Transaction Layer Tool" HelpKeyword="TransactionLayerTool">
        <DomainClassMoniker Name="TransactionLayer" />
      </ElementTool>
      <ElementTool Name="TransactionEnvironmentLayerTool" ToolboxIcon="Resources\TransactionEnvironmentLayerIcon.bmp" Caption="Transaction Environment Layer Tool" Tooltip="Transaction Environment Layer Tool" HelpKeyword="TransactionEnvironmentLayerTool">
        <DomainClassMoniker Name="TransactionEnvironmentLayer" />
      </ElementTool>
    </ToolboxTab>
    <ToolboxTab TabText="Data Sources">
      <ElementTool Name="TransactionSQLServerDataSourceTool" ToolboxIcon="Resources\TransactionDataSourceIcon.bmp" Caption="Transaction SQL Server Data Source Tool" Tooltip="Transaction SQLServer Data Source Tool" HelpKeyword="TransactionSQLServerDataSourceTool">
        <DomainClassMoniker Name="TransactionSQLServerDataSource" />
      </ElementTool>
      <ElementTool Name="TransactionDataSourceSupportTool" ToolboxIcon="Resources\TransactionDataSourceSupportIcon.bmp" Caption="Transaction Data Source Support Tool" Tooltip="Transaction Data Source Support Tool" HelpKeyword="TransactionDataSourceSupportTool">
        <DomainClassMoniker Name="TransactionDataSourceSupport" />
      </ElementTool>
    </ToolboxTab>
    <ToolboxTab TabText="Environment">
      <ElementTool Name="TransactionEnvironmentSQLServerVariableTool" ToolboxIcon="Resources\TransactionEnvironmentSQLServerVariableIcon.bmp" Caption="Transaction Environment SQLServer Variable Tool" Tooltip="Transaction Environment SQLServer Variable Tool" HelpKeyword="TransactionEnvironmentSQLServerVariableTool">
        <DomainClassMoniker Name="TransactionEnvironmentSQLServerVariable" />
      </ElementTool>
    </ToolboxTab>
    <Validation UsesMenu="true" UsesOpen="true" UsesSave="true" UsesCustom="true" UsesLoad="true" />
    <DiagramMoniker Name="TrnxDslDiagram" />
  </Designer>
  <Explorer ExplorerGuid="16ee8c34-b894-4709-a0d1-ff0b5bfaa14d" Title="Transaction Model Explorer">
    <ExplorerBehaviorMoniker Name="TrnxDsl/TrnxDslExplorer" />
  </Explorer>
</Dsl>