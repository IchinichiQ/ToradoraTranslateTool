@echo off
for %%A in (*.png) do (
Gim\GimConv.exe "%%~A" -o "%%~nA.gim"
..\..\Data\DatWorker\Workspace\gzip.exe -nf9 "%%~nA.gim"
)
pause