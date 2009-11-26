msbuild ..\..\tools\WsdlTools.csproj
copy ..\..\tools\bin\Debug\wsdltools.exe .
wsdltools.exe --action=ProcessWsdl --file=wsdl_config.xml
wsdl.exe /par:wsdl_config_new.xml /out:AdWordsApi.cs
wsdltools.exe --action=ProcessCode --file=AdWordsApi.cs
del *.wsdl /Q
del *.exe /Q
del wsdl_config_new.xml /Q

