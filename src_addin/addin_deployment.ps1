$msdeploy = "C:\Program Files\IIS\Microsoft Web Deploy V3\msdeploy.exe"

$msdeployArguments = '-verb:sync',
		'-source:contentPath="C:\Data\Sources\HeliosOutlookAddid\src_addin"',
		'-dest:contentPath="dev-helios-addin",wmsvc=dev-helios-addin.scm.azurewebsites.net:443/msdeploy.axd?site=dev-helios-addin,userName=$dev-helios-addin,password=Y2zbwlPNZQE4qwhPcJzlKG845w2QbyrPc31XQoK4Mi757mRzdXx1trCScayH',
		'-skip:Directory="node_modules"',
		'-AllowUntrusted'

Set-Location C:\Data\Sources\HeliosOutlookAddid\src_addin
npm install
gulp build_debug

& $msdeploy $msdeployArguments