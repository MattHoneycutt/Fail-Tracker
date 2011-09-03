param (
	$Properties = @{}
)
import-module .\psake.psm1
$psake.use_exit_on_error = $true
invoke-psake -framework 4.0 -taskList Deploy -properties $Properties
remove-module psake