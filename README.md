Status
======

This is a home project to explore a number of different technologies.

See (this series of blog posts)[http://www.uberconcept.com/search/label/Internal%20Project] for more details

Getting started
======

What you need before you start:
* SQL Server 2008 or later installed
* SQL Server needs be able to be connected via (local). The site and build scripts connect to the database using this alias. Normally this means setting SQL Server needs to be listening on the default port, 1433 (See this for how to set this up)[http://msdn.microsoft.com/en-us/library/ms177440.aspx].
* powershell snapins for SQL server need to be installed. These are generally installed with SQL Server, but (this might help)[http://vsteamsystemcentral.com/cs21/blogs/timbenninghoff/archive/2008/12/21/Installing-SQL-Server-2008-PowerShell-snap_2D00_ins-for-SQL-Server-2005.aspx] if you have problems
* powershell needs to be able to run unsigned scripts. This means setting the execution policy (to unrestricted)[http://technet.microsoft.com/en-us/library/ee176961.aspx]

With that all done:
* clone the repo (see)[https://help.github.com/articles/duplicating-a-repository]
* open a powershell prompt and run the script (path relative to root directory of the repo): \data\sql_server\build\build_db.ps1
* open (relative path again) \site\Status.sln in visual studio 2013 (express or professional), hit F5 to run
* test login and password are
	* login: valid@test.com
	* password: validPassword