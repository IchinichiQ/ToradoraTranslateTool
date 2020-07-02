@echo off
for %%A in (*.txt) do (
..\..\Data\DatWorker\Workspace\gzip.exe -nfk9 "%%~A"
)
pause