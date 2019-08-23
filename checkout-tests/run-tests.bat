::from https://gunnarpeipman.com/aspnet/aspnet-core-code-coverage/

::Generate coverage report using coverlet (exclude assemblies starting with xunit)
dotnet test --no-build --no-restore --logger:trx --results-directory:.\BuildReports\UnitTests ^
/p:collectCoverage=true /p:CoverletOutput=BuildReports\Coverage\ /p:CoverletOutputFormat=cobertura /p:Exclude=[xunit.*]*

::Convert the cobertura report into html
dotnet reportgenerator "-reports:BuildReports\Coverage\coverage.cobertura.xml" "-targetdir:BuildReports\Coverage" "-reporttypes:HTML;HTMLSummary"

::Open html in default browser
start BuildReports\Coverage\index.htm