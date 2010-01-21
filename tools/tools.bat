msbuild ..\..\tools\WsdlTools.csproj
copy ..\..\tools\bin\Debug\wsdltools.exe .
wsdltools.exe --action=ProcessWsdl --file=%1
wsdl.exe /par:wsdl_config_new.xml /out:%2
wsdltools.exe --action=ProcessCode --file=%2
del *.wsdl /Q
del *.exe /Q
del wsdl_config_new.xml /Q

