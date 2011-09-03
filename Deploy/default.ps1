properties {
	$BaseDir = Resolve-Path "..\"
	$SolutionFile = "$BaseDir\FailTracker.sln"
	$OutputDir = "$BaseDir\Deploy\Package\"
	$OutputWebDir = "$OutputDir" + "_PublishedWebsites\FailTracker.Web"
	$WebConfigFile = "$OutputWebDir\Web.config"
	$Version = "dev"
	$Debug="false"
}

task default -depends Deploy

task Init {
	cls
}

task Clean {
	if (Test-Path $OutputDir) {
		ri $OutputDir -Recurse
	}
}

task Build {
	exec { msbuild $SolutionFile "/p:MvcBuildViews=False;OutDir=$OutputDir" }
}

task Deploy -depends Init,Clean,Build {
	ri "$OutputWebDir\Web.*.config"
	ri "$OutputWebDir\packages.config"
	#TODO: Remove the App_Data folder? 
	
	#TODO: Localize the hibernage.cfg.xml file.
	
	$WebConfig = [xml](get-content "$WebConfigFile")
	#($WebConfig.configuration.appSettings.add | ? {$_.key -eq "Environment"}).value = "Production"	
	#($WebConfig.configuration.appSettings.add | ? {$_.key -eq "Version"}).value = "1.0.0.$Version"	
	$WebConfig.configuration."system.web".compilation.debug="$Debug"
	$WebConfig.save($WebConfigFile)

	exec { msdeploy -whatif "-verb:sync" "-source:contentPath=$OutputWebDir" "-dest:contentPath=failtracker,computerName=tcfaccelerator.cloudapp.net,getCredentials=FailTrackerAzure" }
}