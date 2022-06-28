@echo off
echo Copying newly built plugin
copy bin\Debug\netstandard2.1\Impostor.Plugins.RootPostor.dll ..\..\..\Impostor-Server_1.7.0_win-x64\plugins\Impostor.Plugins.RootPostor.dll
copy bin\Debug\netstandard2.1\Impostor.Plugins.RootPostor.dll ..\Impostor.Server\bin\Debug\net6.0\plugins
pause
