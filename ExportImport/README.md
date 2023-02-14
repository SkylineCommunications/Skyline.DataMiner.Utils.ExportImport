# Skyline.DataMiner.Utils.ExportImport

## About

### About Skyline.DataMiner.Utils.ExportImport Packages

Skyline.DataMiner.Utils.ExportImport Packages are NuGets available in the public [nuget store](https://www.nuget.org/) that contain assemblies that enhance development of DataMiner protocols or Automation scripts.

They allow to easily export and import data in a CSV, JSON or XML format.

> [!IMPORTANT]
> Prior to DataMiner 10.1.11 (RN 30755), when a .NET Standard 2.0 NuGet is used in a QAction or EXE, you need to manually add a reference to .NET Standard.
>
> ```xml
> <ItemGroup>
>   <Reference Include="netstandard" />
> </ItemGroup>
> ```
>
> When .NET Framework 4.6.2 is targeted, you will get a warning icon, but this can be ignored.

The following packages are available:

- Skyline.DataMiner.Utils.ExportImport

### About DataMiner

DataMiner is a transformational platform that provides vendor-independent control and monitoring of devices and services. Out of the box and by design, it addresses key challenges such as security, complexity, multi-cloud, and much more. It has a pronounced open architecture and powerful capabilities enabling users to evolve easily and continuously.

The foundation of DataMiner is its powerful and versatile data acquisition and control layer. With DataMiner, there are no restrictions to what data users can access. Data sources may reside on premises, in the cloud, or in a hybrid setup.

A unique catalog of 7000+ connectors already exist. In addition, you can leverage DataMiner Development Packages to build you own connectors (also known as "protocols" or "drivers").

> [!TIP]
> See also: [About DataMiner](https://aka.dataminer.services/about-dataminer)

### About Skyline Communications

At Skyline Communications, we deal in world-class solutions that are deployed by leading companies around the globe. Check out [our proven track record](https://aka.dataminer.services/about-skyline) and see how we make our customers' lives easier by empowering them to take their operations to the next level.

## Requirements

The "DataMiner Integration Studio" Visual Studio extension is required for development of connectors and Automation scripts using NuGets.

See [Installing DataMiner Integration Studio](https://aka.dataminer.services/DisInstallation)

> [!IMPORTANT]
> NuGets are mandatory to be installed with PackageReferences. DIS was redesigned to work with PackageReferences and be future-proof. 
>
> For more information on how to migrate from packages.config to PackageReferences, see [docs.microsoft.com](https://docs.microsoft.com/en-us/nuget/consume-packages/migrate-packages-config-to-package-reference).

## Getting started

For reading use your entry point should be:

```cs
Reader<DataRow> reader = ReaderFactory.GetReader<DataRow>(filePath);
List<DataRow> rows = reader.Read();
```

For writing use your entry point should be:

```cs
Writer<DataRow> writer = WriterFactory.GetWriter<DataRow>(filePath);
writer.Write(rows);
```

Based on the file extension (.csv, .json or .xml) it will return the specific reader/writer.
```
