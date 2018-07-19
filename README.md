# EMU (openEQUELLA Metadata Utility)
EMU is a tool for inspecting and bulk modifying metadata in openEQUELLA. It is a .NET application written in C# and packaged as a Windows MSI. 

## Dependencies
EMU requires .NET framework 3.5 or higher.
To make modifications to and test EMU Visual C# 2008 or higher and WiX Toolset for packaging (wixtoolset.org) is required on the workstation. 

## Packaging
Build/rebuild the solution to generate a new version of emu.exe and the associated DLLs. Then run \Package\package.bat to create emu.msi.
package.bat instructs WiX Toolset to produce emu.msi based on the settings in emu.wxs which predominantly creates the MSI from emu.exe. emu.wxs also calls Splatter2.bmp, emubig.bmp, emubig.ico and License.rtf. These files are only utilized by the installation wizard, not the EMU application itself.



