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
	exec { msbuild $SolutionFile "/p:MvcBuildViews=False;OutDir=$OutputDir;Configuration=Release" "/t:Rebuild" }
}

task Deploy -depends Init,Clean,Build {
	ri "$OutputWebDir\Web.*.config"
	ri "$OutputWebDir\packages.config"
	#App_Data is only used for SQL CE locally.
	ri "$OutputWebDir\App_Data" -Recurse
	
	#Drop in the production hibernage.cfg.xml file.
	ri "$OutputWebDir\hibernate.cfg.xml"
	cp "$BaseDir\Deploy\Config\hibernate.cfg.Production.xml" "$OutputWebDir\bin\hibernate.cfg.xml"
	
	$WebConfig = [xml](get-content "$WebConfigFile")
	#TODO: Someday, track environment and version info. 
	#($WebConfig.configuration.appSettings.add | ? {$_.key -eq "Environment"}).value = "Production"	
	#($WebConfig.configuration.appSettings.add | ? {$_.key -eq "Version"}).value = "1.0.0.$Version"	
	$WebConfig.configuration."system.web".compilation.debug="$Debug"
	$WebConfig.save($WebConfigFile)

	exec { msdeploy "-verb:sync" "-source:contentPath=$OutputWebDir" "-dest:contentPath=failtracker,computerName=tcfaccelerator.cloudapp.net,getCredentials=FailTrackerAzure" }
}