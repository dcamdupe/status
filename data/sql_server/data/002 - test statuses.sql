INSERT INTO [status].[dbo].[status]
           ([message]
           ,[user_id]
           ,[date_added])
SELECT
           'test message 1'
           ,user_id
           ,CURRENT_TIMESTAMP
 FROM [user]


