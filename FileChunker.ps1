# This is a script that takes a file and spits it into chunks to help DuckAI read large files.
# $ creates a variable | Read-Host gets user input
$source = Read-Host "Path to file"
#           This strips the file extension
$baseName = [System.IO.Path]::GetFileNameWithoutExtension($source)
#        Pretty self explanatory
$lines = Get-Content -LiteralPath $source
$chunkSize = 250
$outputFolder = "Context"
New-Item -ItemType Directory -Force -Path $outputFolder | Out-Null
for ($start = 0; $start -lt $lines.Count; $start += $chunkSize) {
	$end = [Math]::Min($start + $chunkSize, $lines.Count)
	$partNumber = [int]($start / $chunkSize) + 1
	$fileName = "{0}-{1:D3}.md" -f $baseName, $partNumber
	$header = "<!-- {0} lines $($start + 1)-$end of $($lines.Count) -->" -f $baseName
	$code = $lines[$start..($end - 1)] -join "`n"
	$content = "$header`n" + '```csharp' + "`n$code`n" + '```'

	Set-Content -LiteralPath (Join-Path $outputFolder $fileName) `
	-Value $content -Encoding utf8
	if (Test-Path -LiteralPath (Join-Path $outputFolder $fileName)){
		Write-Output "$fileName Created Successfully"
	}
	else {
	    Write-Output "File creation failed"
	}
}
Write-Output "Finished"
sleep 5
clear