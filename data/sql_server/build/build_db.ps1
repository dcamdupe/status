if ( (Get-PSSnapin -Name SqlServerCmdletSnapin100 -ErrorAction SilentlyContinue) -eq $null )
{
    Add-PsSnapin SqlServerCmdletSnapin100
}

$PSScriptRoot = Split-Path -Parent -Path $MyInvocation.MyCommand.Definition
set-location $PSScriptRoot

function create_db([string] $name)
{
Invoke-Sqlcmd -Query "IF (EXISTS (SELECT name FROM sysdatabases WHERE name = '$name'))
	BEGIN
		ALTER DATABASE [$name] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
		DROP DATABASE [$name]
	END
	CREATE DATABASE [$name]
	ALTER DATABASE [$name] SET MULTI_USER;" -ServerInstance "(local)"
}

function run_scripts_dir([string] $path)
{
	get-childitem "$path\\*.sql" | foreach {
		write-host "executing $_"
		Invoke-Sqlcmd -Database "status" -InputFile $_ -ServerInstance "(local)" }
}

create_db('status')

# first query seems to be failing, later ones are fine. This hack seems to stop the later scripts from failing.
Invoke-Sqlcmd -Query "SELECT * from information_schema.tables" -ServerInstance "(local)" -Database "status"

run_scripts_dir(resolve-path "..\\tables\\");
run_scripts_dir(resolve-path "..\\data\\");