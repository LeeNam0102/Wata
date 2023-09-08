delete from dbo.tbl_abcs;

SET IDENTITY_INSERT [dbo].[tbl_abcs] ON
insert into dbo.tbl_abcs
(
	[AbcID]
    ,[Name]
    ,[CreateDate]
    ,[UpdateDate]
    ,[CreateBy]
    ,[UpdateBy]
)
values
(1, 'IT-UPDATE', '2023-06-07 10:33:23.407', '2023-06-07 10:33:23.407', NULL, NULL),
(2, 'IT-UPDATE', '2023-06-07 10:33:23.407', '2023-06-07 10:33:23.407', NULL, NULL)
SET IDENTITY_INSERT [dbo].[tbl_abcs] OFF