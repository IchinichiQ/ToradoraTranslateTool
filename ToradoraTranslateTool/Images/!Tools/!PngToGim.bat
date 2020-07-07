@echo off
for %%A in (*.png) do (
Gim\GimConv.exe "%%~A" --format_style psp --format_endian little --image_format rgba5650 --pixel_order faster
..\..\Data\DatWorker\Workspace\gzip.exe -nf9 "%%~nA.gim"
ren "%%~nA.gim.gz" "%%~nA.gim"
)
pause