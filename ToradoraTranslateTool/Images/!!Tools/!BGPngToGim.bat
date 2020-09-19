@echo off
for %%A in (*.png) do (
Pngquant\pngquant.exe "%%~A" -f --ext .png
Gim\GimConv.exe "%%~A" --format_style psp --format_endian little --image_format index8 --pixel_order faster
..\..\Data\DatWorker\Workspace\gzip.exe -nf9 "%%~nA.gim"
ren "%%~nA.gim.gz" "%%~nA.gim"
)
pause