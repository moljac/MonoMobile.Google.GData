MonoMobile.Google.GData
=======================

Google.GData port for mobile platforms
http://holisticware.net

https://groups.google.com/forum/?fromgroups#!forum/gdata-dotnet-client-library

TODOs:
	2012-07-22
		Samples
		WP
	
Procedure
	
	0. google gdata api original is for desktop versions
	
	1. added mobile
		add project "least common denominator"/"smallest subset"
		WP (SIlverlight) took too much time to fix so it is temporarily dropped from schedule
	   
		started with MonoMobile (MA and MT)
		
		with Project Linker added links
		
	2. rename google solution structure (directories and projects)
		projects w/o extensions are desktop projects (original) with some modifications.
		Modifications are perfromed with partial classes and adding classes that would silence
		the compiler (System.Diagnostics), or moving some partial implementations to desktop
		projects (Registry stuff).
		
		core\											CoreClient\
		core\Core Client.csproj							CoreClient\CoreClient.csproj
		 
		extensions\										CommonDataExtensions\
		extensions\Common Data Extensions.csproj		CommonDataExtensions\CommonDataExtensions.csproj

		gacl\											AccessControl\
		gacl\Access Control.csproj						gacl\AccessControl.csproj
		 
		analytics\										Analytics\
		blogger\										Blogger
		contentsforshopping\							ContentsForShopping\
		documents3\										Documents3\
		gapps\											Applications\
		gcalendar\										Calendar\
		gcontacts\										Contacts\
		gphotos\										Picasa\
		gspreadsheet\									Spreadsheets\
		

	3. all desktop projects target .net 3.5
	   MT target 4.0
	   do not copy MT project under windows and include in solution it has to be 
	   added through MD on mac

	4. on 
		Windows and MacOSX 
		Core.Client.MT 
			compiles fine (Trace Problem) w/o referencing our System.Diagnostics.MT project
			to make solution rebuild remove System.Diagnostics.MT ref

		MacOSX
		Core.Client.MT 
			does not compile w/o referencing our System.Diagnostics.MT project
			so add reference on MacOSX or ignore: Thet type T exists in both ...


projects to solve first
	Common Data Extensions
	Core Client

	for MM (MA+MT)
		  Registry
		  Trace
	 see Mike Bluestein's sample for YouTube

	 more
		http://mikebluestein.wordpress.com/2009/09/27/using-monotouch-with-the-net-library-for-the-google-data-api/

projects to solve first
	Common Data Extensions
	Core Client

