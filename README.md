# ChannelAdvisor.
Everytime the application or service runs, it downloads the current catalog from Linnworks first (our ecommerce provider) 
and compares the incoming vendor data against that so only existing skus get updated, reformat the downloaded file and upload to ftp.
Used .NET 4.6, C#, Sql server 2008
