xcopy bin\Chimera.Authentication.* ..\..\Xyperico.Website\Xyperico.Website.Host\bin\Areas /I /Y /D
xcopy bin\da\Chimera.Authentication.* ..\..\Xyperico.Website\Xyperico.Website.Host\bin\Areas\da /I /Y /D

mkdir ..\..\Xyperico.Website\Xyperico.Website.Host\Areas\UserAccounts\Views
xcopy Areas\UserAccounts\Views\*.* ..\..\Xyperico.Website\Xyperico.Website.Host\Areas\UserAccounts\Views\ /I /Y /S /D

mkdir ..\..\Xyperico.Website\Xyperico.Website.Host\Areas\UserAccounts\Styles
xcopy Areas\UserAccounts\Styles\*.* ..\..\Xyperico.Website\Xyperico.Website.Host\Areas\UserAccounts\Styles\ /I /Y /S /D

mkdir ..\..\Xyperico.Website\Xyperico.Website.Host\Areas\UserAccounts\Scripts
xcopy Areas\UserAccounts\Scripts\*.* ..\..\Xyperico.Website\Xyperico.Website.Host\Areas\UserAccounts\Scripts\ /I /Y /S /D

